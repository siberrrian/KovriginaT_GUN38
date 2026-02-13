using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Settings/UnitGameSettings",fileName ="New UnitGameSettigs", order = 1)]
public class UnitGameSettings : ScriptableObject
{
    [field: SerializeField]

    public UnitStats Stats { get; private set; }

    [field: SerializeField]
    public UnitMobility Mobility { get; private set; }

}

[Serializable]

public struct UnitStats
{
    public int Health;
    public int Damage;
    public int CriticalPersent;
    public int CriticalChance;
    public int MissChance;
}

[Serializable]

public struct UnitMobility
{
    public int CrossMove;
    public int DiagonalMove;
    public int CrossAttack;
    public int DiagonalAttack;
    public bool MoveAndAttackInTurn;
}
