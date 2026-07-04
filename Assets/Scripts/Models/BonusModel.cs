using Core.SaveLoad;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;  

namespace Models
{
    public sealed class BonusModel : IBonusModel, IDisposable, IInitializable
    {
        private readonly ReactiveCollection<Bonuses> _collectedBonuses = new();

        private readonly ReactiveDictionary<Bonuses, int> _currentSessionBonuses = new();
        public IReadOnlyReactiveDictionary<Bonuses, int> CurrentSessionBonuses => _currentSessionBonuses;

        private readonly Dictionary<Bonuses, int> _lastSessionBonuses = new();

        private readonly Subject<(Bonuses type, float spawnX)> _onBonusSpawned = new();
        public IObservable<(Bonuses type, float spawnX)> OnBonusSpawned => _onBonusSpawned;

        private ReactiveProperty<int> _currentCircles;

        private ReactiveProperty<int> _currentTriangles;

        private Bonuses _lastType;

        public ReactiveProperty<int> CurrentCircles => _currentCircles;

        public ReactiveProperty<int> CurrentTriangles => _currentTriangles;



        private ReactiveProperty<int> _lastCircles;

        private ReactiveProperty<int> _lastTriangles;
        public ReactiveProperty<int> LastCircles => _lastCircles;

        public ReactiveProperty<int> LastTriangles => _lastTriangles;

        private readonly ISaveLoadDataHandler _saveLoadDataHandler;


        private float _previousPlatformX;
        private bool _isFirstPlatform = true;

        public BonusModel(ISaveLoadDataHandler saveLoadDataHandler)
        {
            _currentSessionBonuses[Bonuses.Circle] = 0;
            _currentSessionBonuses[Bonuses.Triangle] = 0;
            _saveLoadDataHandler = saveLoadDataHandler;
            LoadLastSessionData();        
        }

        public void Initialize()
        {

            _lastCircles = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt("LastCircles", out var lastcrc) ? lastcrc : 0);
            _currentCircles = new ReactiveProperty<int>(0);

            _lastTriangles = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt("LastTriangles", out var lasttr) ? lasttr : 0);
            _currentTriangles = new ReactiveProperty<int>(0);
        }




        public void TrackPlatformPosition(float nextPlatformX)
        {
            if (_isFirstPlatform)
            {
                _previousPlatformX = nextPlatformX;
                _isFirstPlatform = false;
                return;
            }

            float midPointX = (_previousPlatformX + nextPlatformX) / 2f;

            Bonuses randomBonus;
            if (UnityEngine.Random.value > 0.5f)
            {
                randomBonus = Bonuses.Circle;
            }
            else
            {
                randomBonus = Bonuses.Triangle;
            }

            _onBonusSpawned.OnNext((randomBonus, midPointX));


            _previousPlatformX = nextPlatformX;
        }

        public void CollectBonus(Bonuses bonus)
        {
            _currentSessionBonuses[bonus]++;
            if (bonus == Bonuses.Triangle)
            {
                _currentTriangles.Value++;
                _lastType = Bonuses.Triangle;
            } else
            {
                _currentCircles.Value++;
                _lastType = Bonuses.Circle;
            }

            _collectedBonuses.Add(bonus);
        }

        public void ClearCurrentSession()
        {
            _currentSessionBonuses[Bonuses.Circle] = 0;
            _currentSessionBonuses[Bonuses.Triangle] = 0;
            _currentTriangles.Value = 0;
            _currentCircles.Value = 0;
        }


        public void SaveCurrentSessionAsLast()
        {
            if (_lastType != null)
            {
                if (_lastType == Bonuses.Triangle)
                {
                    _currentSessionBonuses[Bonuses.Triangle]--;
                }
                else
                {
                    _currentSessionBonuses[Bonuses.Circle]--;
                }
            }

            _saveLoadDataHandler.SaveInt("LastCircles", _currentSessionBonuses[Bonuses.Circle]);
            _saveLoadDataHandler.SaveInt("LastTriangles", _currentSessionBonuses[Bonuses.Triangle]);


            _lastCircles.Value = _currentSessionBonuses[Bonuses.Circle];
            _lastTriangles.Value = _currentSessionBonuses[Bonuses.Triangle];

            LoadLastSessionData();
        }

        public int GetLastSessionCount(Bonuses bonus)
        {
            return _lastSessionBonuses.ContainsKey(bonus) ? _lastSessionBonuses[bonus] : 0;
        }

        private void LoadLastSessionData()
        {
            _lastSessionBonuses[Bonuses.Circle] = _saveLoadDataHandler.TryLoadInt("LastCircles", out var lastcrc) ? lastcrc : 0;
            _lastSessionBonuses[Bonuses.Triangle] = _saveLoadDataHandler.TryLoadInt("LastTriangles", out var lasttr) ? lasttr : 0;
        }


        public int GetLastCircles()
        {
            return _saveLoadDataHandler.TryLoadInt("LastCircles", out var lastcrc) ? lastcrc : 0;
        }

        public int GetLastTriangles()
        {
            return _saveLoadDataHandler.TryLoadInt("LastTriangles", out var lasttr) ? lasttr : 0;
        }

        public void Dispose()
        {
            _onBonusSpawned.Dispose();
        }

        

    }
}
