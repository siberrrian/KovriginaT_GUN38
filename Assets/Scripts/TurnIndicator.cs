using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TurnIndicator : MonoBehaviour
{
    //private ITurn _turn;

    private (Team team, Image icon, Image arrow) _left;
    private (Team team, Image icon, Image arrow) _right;

    [SerializeField]
    private Image _leftIcon;
    [SerializeField]
    private Image _leftArrow;


    [SerializeField]
    private Image _rightIcon;
    [SerializeField]
    private Image _rightArrow;

    [SerializeField, Space(15f), Range(.1f, 2f)]
    private float _disableScale = .7f;
    [SerializeField, Range(0f, 1f)]
    private float _disableAlpha = .3f;
    /*
    private void Callback(GameEvent arg)
    {
        switch (arg){
            default:
                return;
            case GameEvent.NewTurn:
                _turn.Next();
                break;
        }
        var (enable, disable) = _left.team == _turn.Current
            ? (_left, _right)
            : (_right, _left);
        enable.arrow.enabled = true;
        enable.icon.transform.localScale = Vector3.one;
        enable.icon.color = new Color(1f, 1f, 1f, 1f);

        disable.arrow.enabled = false;
        disable.icon.transform.localScale = Vector3.one * _disableScale;
        disable.icon.color = new Color(1f, 1f, 1f, _disableAlpha);

    }
    [Inject]
    private void Construct(SignalBus signal, ITurn turn, TurnPanelSettings settings)
    {
        _turn = turn;
        signal.Subscribe<GameEvent>(Callback);

        var (left, right) = (settings[Team.White], settings[Team.Black]);
        _left = (left.Team, _leftIcon, _leftArrow);
        _right = (right.Team, _rightIcon, _rightArrow);
        (_leftIcon.sprite, _rightIcon.sprite) = (left.Icon, right.Icon);
    }*/

}
