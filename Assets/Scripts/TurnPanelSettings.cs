using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New TurnPanelSettings", menuName ="Settings/TurnPanelSettings", order = 51)]
public class TurnPanelSettings: ScriptableObject
{
    [SerializeField]
    private TurnPreset _default;
    [SerializeField]
    private TurnPreset[] _presets;

    public TurnPreset this[Team team]
    {
        get
        {
            var index = Array.FindIndex(_presets, t=> t.Team == team);
            if (index == -1)
            {
                Debug.LogError($"Can't find {nameof(TurnPreset)} with value = {team}");
                return _default;
            }
            return _presets[index];
        }
    }
}
[Serializable]
public struct TurnPreset
{
    public Sprite Icon;
    public Team Team;
}
