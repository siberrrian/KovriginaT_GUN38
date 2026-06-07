using Models.Interfaces;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters.Menu
{
    public sealed class GameScorePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentScore;
        [FormerlySerializedAs("_maxScore")][SerializeField] private TMP_Text _bestScore;

        [Header("Текущие бонусы в игре")]
        [SerializeField] private TMP_Text _currentCircles;
        [SerializeField] private TMP_Text _currentTriangles;

        [Header("Рекорды в меню")]
        [SerializeField] private TMP_Text _bestCircles;
        [SerializeField] private TMP_Text _bestTriangles;

        [Inject]
        private void Inject(IGameScoreModel model)
        {
            model.CurrentScore.Subscribe(CurrentScoreUpdated).AddTo(this);
            model.BestScore.Subscribe(BestScoreUpdated).AddTo(this);
            _bestScore.text = model.BestScore.Value.ToString();

            model.CurrentCircles.Subscribe(CurrentCirclesUpdated).AddTo(this);
            model.CurrentTriangles.Subscribe(CurrentTrianglesUpdated).AddTo(this);

            model.BestCircles.Subscribe(BestCirclesUpdated).AddTo(this);
            _bestCircles.text = model.BestCircles.Value.ToString();

            model.BestTriangles.Subscribe(BestTrianglesUpdated).AddTo(this);
            _bestTriangles.text = model.BestTriangles.Value.ToString();
        }

        private void CurrentScoreUpdated(int score) => _currentScore.text = score.ToString();

        private void BestScoreUpdated(int score) => _bestScore.text = score.ToString();

        private void CurrentCirclesUpdated(int count) => _currentCircles.text = count.ToString();
        private void BestCirclesUpdated(int count) => _bestCircles.text = count.ToString();

        private void CurrentTrianglesUpdated(int count) => _currentTriangles.text = count.ToString();
        private void BestTrianglesUpdated(int count) => _bestTriangles.text = count.ToString();
    }
}