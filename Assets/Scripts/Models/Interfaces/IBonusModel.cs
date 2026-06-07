using System;
using UniRx;


public enum Bonuses
{
    Circle,
    Triangle
}

public interface IBonusModel
{

    IReadOnlyReactiveDictionary<Bonuses, int> CurrentSessionBonuses { get; }
    IReadOnlyReactiveCollection<Bonuses> CollectedBonuses { get; }
    IObservable<(Bonuses type, float spawnX)> OnBonusSpawned { get; }
    void TrackPlatformPosition(float nextPlatformX);

    void CollectBonus(Bonuses bonus);
}
