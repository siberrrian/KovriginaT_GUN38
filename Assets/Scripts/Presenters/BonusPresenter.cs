using Cysharp.Threading.Tasks;
using Models.Interfaces;
using System;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public sealed class BonusPresenter : Zenject.IInitializable, IDisposable
    {
        private readonly IGameScoreModel _gameScoreModel;
        private readonly IBonusModel _bonusModel;
        private readonly DiContainer _container;
        private readonly GameObject _bonusPrefab;
        private readonly Sprite _starSprite;
        private readonly Sprite _hearthSprite;
        private readonly CompositeDisposable _disposables = new();

        private const float SpawnHeightY = 0.2f;

        public BonusPresenter(
            IGameScoreModel gameScoreModel,
            IBonusModel bonusModel,
            DiContainer container,
            [Inject(Id = "BonusPrefab")] GameObject bonusPrefab,
            [Inject(Id = "StarSprite")] Sprite starSprite,
            [Inject(Id = "HearthSprite")] Sprite hearthSprite)
        {
            _gameScoreModel = gameScoreModel;
            _bonusModel = bonusModel;
            _container = container;
            _bonusPrefab = bonusPrefab;
            _starSprite = starSprite;
            _hearthSprite = hearthSprite;
        }

        public void Initialize()
        {
            _bonusModel.OnBonusSpawned
                .Subscribe(data => SpawnBonus(data.type, data.spawnX))
                .AddTo(_disposables);
        }



        private void SpawnBonus(Bonuses type, float xPosition)
        {

            Vector3 position = new Vector3(xPosition, SpawnHeightY, 0f);
            GameObject bonusGo = _container.InstantiatePrefab(_bonusPrefab, position, Quaternion.identity, null);

            SpriteRenderer spriteRenderer = bonusGo.GetComponent<SpriteRenderer>();
            if (type == Bonuses.Circle)
            {
                spriteRenderer.sprite = _hearthSprite;
            }
            else
            {
                spriteRenderer.sprite = _starSprite;
            }

            bonusGo.GetOrAddComponent<BonusTrigger>().OnPlayerEntered = () =>
            {
                _bonusModel.CollectBonus(type);

                UnityEngine.Object.Destroy(bonusGo);
            };
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
