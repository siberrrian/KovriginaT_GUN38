using ModestTree;
using System.Collections;
using System.Collections.Generic;
using System.Model;
using UnityEngine;
using UniRx;
using Zenject;
using SaveData;


namespace System.Presenter
{
    public class InputPresenter : IInitializable, IDisposable
    {
        private CompositeDisposable _inputDisposable = new();
        private CompositeDisposable _gameOverInputDisposable = new();

        private readonly KeyCode _savePlayer = KeyCode.C;
        private readonly KeyCode _loadPlayer = KeyCode.V;
        private IVectorSet _vectorSet;
        private ISaveLoadInputValues _saveLoadInputValues;
        private readonly ISaveDataService<PlayerBase> _saveService;
        [Inject]
        private InputPresenter(IVectorSet vectorSet, ISaveLoadInputValues saveLoadInputValues, ISaveDataService<PlayerBase> saveService)
        {
            _vectorSet = vectorSet;
            _saveLoadInputValues = _saveLoadInputValues;
            _saveService = saveService;
        }
        public void Initialize()
        {
            _inputDisposable.Add(Observable
                .EveryUpdate()
                .Where(t => Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                .Subscribe(OnNext));

            Observable.EveryUpdate().Subscribe(SaveData).AddTo(_inputDisposable);
            Observable.EveryUpdate().Subscribe(LoadData).AddTo(_inputDisposable);

        }
        private void SaveData(long _)
        {
            _saveLoadInputValues.SaveClicked.Value = Input.GetKeyDown(KeyCode.C);
            Debug.Log("Save");
        }
        private void LoadData(long _) => _saveLoadInputValues.SaveClicked.Value = Input.GetKeyDown(KeyCode.V);

        private void OnNext(long obj) => _vectorSet.SetVector(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        /*{
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                _vectorSet.SetVector(new Vector3(x: Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            }
        }*/

        public void Dispose() => _inputDisposable.Dispose();

    }
}
