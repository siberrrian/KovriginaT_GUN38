using Core.SaveLoad;
using Models.Interfaces;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Models
{
    public sealed class BonusUIModel : IBonusUIModel
    {
        private const string LastCirclesKey = "LastCircles";
        private const string LastTrianglesKey = "LastTriangles";
        private readonly ISaveLoadDataHandler _saveLoadDataHandler;
        private ReactiveProperty<int> _lastCircles;
        private ReactiveProperty<int> _lastTriangles;
        public ReactiveProperty<int> LastCircles => _lastCircles;
        public ReactiveProperty<int> LastTriangles => _lastTriangles;


        private ReactiveProperty<int> _currentCircles;
        public ReactiveProperty<int> ÑurrenttCircles => _currentCircles;

        private ReactiveProperty<int> _currentTriangles;
        public ReactiveProperty<int> ÑurrentTriangles => _currentTriangles;

        public BonusUIModel(ISaveLoadDataHandler saveLoadDataHandler) => _saveLoadDataHandler = saveLoadDataHandler;

        public void Initialize()
        {
            _lastCircles = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(LastCirclesKey, out var lastc) ? lastc : 0);
            _lastTriangles = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(LastTrianglesKey, out var lastt) ? lastt : 0);
            _currentCircles = new ReactiveProperty<int>(0);
            _currentTriangles = new ReactiveProperty<int>(0);
            
        }

        public void IncreaseTriangles()
        {
        }

        public void IncreaseCircles()
        {
        }

        public void ResetBonuses()
        {
            _currentCircles.Value = 0;
            _currentTriangles.Value = 0;
        }

        
        private const string BestCirclesKey = "BestCircles";
        private const string BestTrianglesKey = "BestTriangles";


    }
}