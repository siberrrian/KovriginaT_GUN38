using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Models
{
    public sealed class BonusModel : IBonusModel, IDisposable
    {
        public IReadOnlyReactiveCollection<Bonuses> CollectedBonuses => throw new NotImplementedException("Используйте словарь CurrentSessionBonuses для UI");

        private readonly ReactiveDictionary<Bonuses, int> _currentSessionBonuses = new();
        public IReadOnlyReactiveDictionary<Bonuses, int> CurrentSessionBonuses => _currentSessionBonuses;

        private readonly Dictionary<Bonuses, int> _lastSessionBonuses = new();

        private readonly Subject<(Bonuses type, float spawnX)> _onBonusSpawned = new();
        public IObservable<(Bonuses type, float spawnX)> OnBonusSpawned => _onBonusSpawned;

        private float _previousPlatformX;
        private bool _isFirstPlatform = true;

        public BonusModel()
        {
            _currentSessionBonuses[Bonuses.Circle] = 0;
            _currentSessionBonuses[Bonuses.Triangle] = 0;

            LoadLastSessionData();
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
        }

        public void ClearCurrentSession()
        {
            _currentSessionBonuses[Bonuses.Circle] = 0;
            _currentSessionBonuses[Bonuses.Triangle] = 0;
        }

        public void SaveCurrentSessionAsLast()
        {
            PlayerPrefs.SetInt("BestCircles", _currentSessionBonuses[Bonuses.Circle]);
            PlayerPrefs.SetInt("BestTriangles", _currentSessionBonuses[Bonuses.Triangle]);
            PlayerPrefs.Save();

            LoadLastSessionData();
        }

        public int GetLastSessionCount(Bonuses bonus)
        {
            return _lastSessionBonuses.ContainsKey(bonus) ? _lastSessionBonuses[bonus] : 0;
        }

        private void LoadLastSessionData()
        {
            _lastSessionBonuses[Bonuses.Circle] = PlayerPrefs.GetInt("BestCircles", 0);
            _lastSessionBonuses[Bonuses.Triangle] = PlayerPrefs.GetInt("BestTriangles", 0);
        }

        public void Dispose()
        {
            _onBonusSpawned.Dispose();
        }
    }
}
