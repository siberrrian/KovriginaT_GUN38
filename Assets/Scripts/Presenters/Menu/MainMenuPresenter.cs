using Models.Interfaces;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Presenters.Menu
{
    public sealed class MainMenuPresenter : MonoBehaviour
    {
        private const string GameSceneName = "Game";
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Button _startButton;

        [Header("Лучшие бонусы в Главном Меню")]
        [SerializeField] private TMP_Text _lastCirclesText;
        [SerializeField] private TMP_Text _lastTrianglesText;

        private IGameScoreModel _gameScoreModel;
        private IBonusModel _bonusModel;

        [Inject]
        private void Inject(IGameScoreModel gameScoreModel, IBonusModel bonusModel)
        {
            _gameScoreModel = gameScoreModel;
            _bonusModel = bonusModel;
        }
        private void Start()
        {

            _scoreText.text = _gameScoreModel.BestScore.Value.ToString();

            if (_lastCirclesText != null)
            {
                _lastCirclesText.text = _bonusModel.LastCircles.Value.ToString();
            }

            if (_lastTrianglesText != null)
            {
                _lastTrianglesText.text = _bonusModel.LastTriangles.Value.ToString();
            }

            _startButton.OnClickAsObservable().Subscribe(OnStartButtonClicked).AddTo(this);
        }

        private void OnStartButtonClicked(Unit obj) => SceneManager.LoadScene(GameSceneName);
    }
}
