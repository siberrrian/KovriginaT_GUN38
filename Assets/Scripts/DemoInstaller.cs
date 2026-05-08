using MiniMap;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Model;
using System.Presenter;
using UnityEngine;
using Zenject;
using SaveData;


namespace DefaultNamespace
{
    public sealed class DemoInstaller : MonoInstaller
    {
        [SerializeField] private Radar _radar;


        public override void InstallBindings()
        {
            BindModels();
            BindPresenters();
            BindView();
            BindServices();
        }

        private void BindModels()
        {
            Container.BindInterfacesAndSelfTo<InputModel>().AsSingle();
            Container.Bind<PlayerBall>().AsSingle();

        }

        private void BindPresenters()
        {
            //Container.BindInterfacesAndSelfTo<CameraPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavePresenter>().AsSingle();

        }

        private void BindView()
        {
            //Container.Bind<IRadar>().To<Radar>().FromInstance(_radar);
        }
        private void BindServices()
        {
            Container.Bind<IData<SavedData>>().To<BinarySerializationData<SavedData>>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<SaveDataService>().AsSingle();
        }
    }

}

