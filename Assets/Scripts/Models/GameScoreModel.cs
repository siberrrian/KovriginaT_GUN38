using Core.SaveLoad;
using Models.Interfaces;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Models
{
    public sealed class GameScoreModel : IGameScoreModel
    {
        private const string BestScoreKey = "BestScore";
        private readonly ISaveLoadDataHandler _saveLoadDataHandler;
        private ReactiveProperty<int> _currentScore;
        private ReactiveProperty<int> _bestScore;
        private Bonuses _lastCollectedBonusType;
        public ReactiveProperty<int> CurrentScore => _currentScore;
        public ReactiveProperty<int> BestScore => _bestScore;

        public GameScoreModel(ISaveLoadDataHandler saveLoadDataHandler) => _saveLoadDataHandler = saveLoadDataHandler;

        public void Initialize()
        {
            _bestScore = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(BestScoreKey, out var bestScore) ? bestScore : 0);
            _currentScore = new ReactiveProperty<int>(0);

            _currentCircles = new ReactiveProperty<int>(0);
            _bestCircles = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(BestCirclesKey, out var bestCircles) ? bestCircles : 0);

            _currentTriangles = new ReactiveProperty<int>(0);
            _bestTriangles = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(BestTrianglesKey, out var bestTriangles) ? bestTriangles : 0);
        }

        public void IncreaseScore()
        {
            _currentScore.Value++;
            if (_currentScore.Value > _bestScore.Value)
            {
                _bestScore.Value = _currentScore.Value;
                _saveLoadDataHandler.SaveInt(BestScoreKey, _bestScore.Value);
            }
        }

        public void ResetScore()
        {
            _currentScore.Value = 0;
            _currentCircles.Value = 0;
            _currentTriangles.Value = 0;
        }


        private const string BestCirclesKey = "BestCircles";
        private const string BestTrianglesKey = "BestTriangles";


        private ReactiveProperty<int> _currentCircles;
        private ReactiveProperty<int> _bestCircles;

        private ReactiveProperty<int> _currentTriangles;
        private ReactiveProperty<int> _bestTriangles;

        public ReactiveProperty<int> CurrentCircles => _currentCircles;
        public ReactiveProperty<int> BestCircles => _bestCircles;

        public ReactiveProperty<int> CurrentTriangles => _currentTriangles;
        public ReactiveProperty<int> BestTriangles => _bestTriangles;

        public void IncreaseCircles()
        {
            _lastCollectedBonusType = Bonuses.Circle;

            _currentCircles.Value++;
            if (_currentCircles.Value > _bestCircles.Value)
            {
                _bestCircles.Value = _currentCircles.Value;
                _saveLoadDataHandler.SaveInt(BestCirclesKey, _bestCircles.Value);
            }
        }

        public void IncreaseTriangles()
        {
            _lastCollectedBonusType = Bonuses.Triangle;

            _currentTriangles.Value++;
            if (_currentTriangles.Value > _bestTriangles.Value)
            {
                _bestTriangles.Value = _currentTriangles.Value;
                _saveLoadDataHandler.SaveInt(BestTrianglesKey, _bestTriangles.Value);
            }
        }

        public void ApplyFallPenalty()
        {
            if (_lastCollectedBonusType == Bonuses.Circle)
            {
                if (_currentCircles.Value > 0)
                {
                    _currentCircles.Value--; 

                    if (_currentCircles.Value+1 >= _bestCircles.Value)
                    {
                        _bestCircles.Value = _currentCircles.Value;
                        _saveLoadDataHandler.SaveInt(BestCirclesKey, _bestCircles.Value);
                    }
                }
            }
            else// if (_lastCollectedBonusType == Bonuses.Triangle)
            {
                if (_currentTriangles.Value > 0)
                {
                    _currentTriangles.Value--;

                    if (_currentTriangles.Value+1 >= _bestTriangles.Value)
                    {
                        _bestTriangles.Value = _currentTriangles.Value;
                        _saveLoadDataHandler.SaveInt(BestTrianglesKey, _bestTriangles.Value);
                    }
                }
            }
            PlayerPrefs.Save();

        }
    }
}