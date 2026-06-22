using Models.Interfaces;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters.Menu
{
    public sealed class BonusUIPresenter : MonoBehaviour
    {
        
        [Header("Текущие бонусы в игре")]
        [SerializeField] private TMP_Text _currentCircles;
        [SerializeField] private TMP_Text _currentTriangles;

        [Header("Рекорды в меню")]
        [SerializeField] private TMP_Text _lastCircles;
        [SerializeField] private TMP_Text _lastTriangles;

        [Inject]
        private void Inject(IBonusModel bonusModel)
        {
            bonusModel.CurrentCircles.Subscribe(CurrentCirclesUpdated).AddTo(this);
            bonusModel.CurrentTriangles.Subscribe(CurrentTrianglesUpdated).AddTo(this);

            bonusModel.LastCircles.Subscribe(LastCirclesUpdated).AddTo(this);
            _lastCircles.text = bonusModel.LastCircles.Value.ToString();

            bonusModel.LastTriangles.Subscribe(LastTrianglesUpdated).AddTo(this);
            _lastTriangles.text = bonusModel.LastTriangles.Value.ToString();
        }

        private void CurrentCirclesUpdated(int count) => _currentCircles.text = count.ToString();
        private void LastCirclesUpdated(int count) => _lastCircles.text = count.ToString();

        private void CurrentTrianglesUpdated(int count) => _currentTriangles.text = count.ToString();
        private void LastTrianglesUpdated(int count) => _lastTriangles.text = count.ToString();
    }
}