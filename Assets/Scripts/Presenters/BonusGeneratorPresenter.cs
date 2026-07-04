using Messages;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BonusGeneratorPresenter : Zenject.IInitializable, IDisposable
{
    private readonly CompositeDisposable _compositeDisposable = new();
    private readonly IMessageBroker _messageBroker;
    private readonly IBonusModel _bonusModel;

    // ¬—“¿¬»À» “ŒÀ‹ Œ  ŒÕ—“–” “Œ– ƒÀﬂ ZENJECT
    public BonusGeneratorPresenter(IMessageBroker messageBroker, IBonusModel bonusModel)
    {
        _messageBroker = messageBroker;
        _bonusModel = bonusModel;
    }

    public void Initialize()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        _messageBroker
            .Receive<SpawnBonusMessage>()
            .Subscribe(OnSpawnBonus)
            .AddTo(_compositeDisposable);
    }

    private void OnSpawnBonus(SpawnBonusMessage message)
    {
        _bonusModel.TrackPlatformPosition(message.PlatformXPosition);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}
