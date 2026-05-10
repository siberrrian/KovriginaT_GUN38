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
        [SerializeField] private Transform _playerTransform;



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
            Container.Bind<Transform>().WithId("Player").FromInstance(_playerTransform);


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
            // Заменяем Binary на Xml
            Container.Bind<IData<SavedData>>().To<XmlSerializationData<SavedData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveDataService>().AsSingle();
        }
    }

}

