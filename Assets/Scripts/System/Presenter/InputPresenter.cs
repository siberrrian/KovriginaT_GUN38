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

        private readonly IVectorSet _vectorSet;
        private readonly ISaveLoadInputValues _saveLoadInputValues;

        [Inject]
        public InputPresenter(IVectorSet vectorSet, ISaveLoadInputValues saveLoadInputValues)
        {
            _vectorSet = vectorSet;
            _saveLoadInputValues = saveLoadInputValues;
        }

        public void Initialize()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => {
                    float h = Input.GetAxis("Horizontal");
                    float v = Input.GetAxis("Vertical");
                    if (h != 0 || v != 0) _vectorSet.SetVector(new Vector3(h, 0, v));
                })
                .AddTo(_inputDisposable);

            Observable.EveryUpdate()
                .Subscribe(_ => {
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        _saveLoadInputValues.SaveClicked.Value = true;
                        Debug.Log("Сохранение");
                    }
                    else
                    {
                        _saveLoadInputValues.SaveClicked.Value = false;
                    }

                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        _saveLoadInputValues.LoadClicked.Value = true;
                        Debug.Log("Загрузка");
                    }
                    else
                    {
                        _saveLoadInputValues.LoadClicked.Value = false;
                    }
                })
                .AddTo(_inputDisposable);
        }

        public void Dispose() => _inputDisposable.Dispose();
    }

}
