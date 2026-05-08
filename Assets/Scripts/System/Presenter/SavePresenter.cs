using SaveData;
using System.Collections;
using System.Collections.Generic;
using System.Model;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;


namespace System.Presenter
{
    public sealed class SavePresenter : Zenject.IInitializable, IDisposable
    {
        private PlayerBase _player;
        private readonly ISaveDataService<PlayerBase> _saveDataService;
        private readonly ISaveLoadInputValues _inputModel;

        private CompositeDisposable _compositeDisposaple = new();

        public SavePresenter([Inject(Id = "Player")] Transform player, ISaveDataService<PlayerBase> saveDataService, ISaveLoadInputValues input)
        {
            _player = player.GetComponent<PlayerBase>();
            _saveDataService = saveDataService;
            _inputModel = input;
        }

        public void Initialize()
        {
            _inputModel.LoadClicked.Subscribe(OnLoad).AddTo(_compositeDisposaple);
            _inputModel.SaveClicked.Subscribe(OnSave).AddTo(_compositeDisposaple);
        }

        private void OnLoad(bool clicked)
        {
            if (clicked)
            {
                _saveDataService.Load(_player);
            }
        }

        private void OnSave(bool clicked)
        {
            if (clicked)
            {
                _saveDataService.Save(_player);
            }
        }

        public void Dispose() => _compositeDisposaple.Dispose();
    }
}

