using System;
using UniRx;
using Zenject;
using UnityEngine;


public enum Bonuses
{
    Circle,
    Triangle
}

public interface IBonusModel : IInitializable
{

    IReadOnlyReactiveDictionary<Bonuses, int> CurrentSessionBonuses { get; }
    IReadOnlyReactiveCollection<Bonuses> CollectedBonuses { get; }
    IObservable<(Bonuses type, float spawnX)> OnBonusSpawned { get; }
    void TrackPlatformPosition(float nextPlatformX);
    
    void SaveCurrentSessionAsLast();
    void CollectBonus(Bonuses bonus);
    void ClearCurrentSession();

    ReactiveProperty<int> CurrentCircles { get; }
    ReactiveProperty<int> CurrentTriangles { get; }


    ReactiveProperty<int> LastCircles { get; }
    ReactiveProperty<int> LastTriangles { get; }

}
