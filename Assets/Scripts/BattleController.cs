using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using Unity.VisualScripting;

public class BattleController : MonoBehaviour
{
    private ISharedData _data;
    private Controls.MainActions _controls;

    private void OnCancel(InputAction.CallbackContext obj)
    {
        switch (_data.Status)
        {
            case GameStatus.Lock:
            case GameStatus.Select:
                return;
            case GameStatus.Move:
            case GameStatus.Attack:
            case GameStatus.ConfirmMove:
            case GameStatus.ConfirmAttack:
                if (_data.CanSwitch)
                {
                    _data.Unselect();
                } else
                {
                    _data.NextTurn();
                }
                break;
        }
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        switch (_data.Status)
        {
            case GameStatus.Lock:
            case GameStatus.Select:
                return;
            case GameStatus.Move:
                if (_data.CanSwitch)
                {
                    _data.Status = GameStatus.Attack;
                    _data.Event = GameEvent.SwitchMode;
                }
                break;
            case GameStatus.Attack:
                if (_data.CanSwitch)
                {
                    _data.Status = GameStatus.Move;
                    _data.Event = GameEvent.SwitchMode;
                }
                break;
            case GameStatus.ConfirmMove:
            case GameStatus.ConfirmAttack:
                _data.Event = GameEvent.VisualizationPeriod;
                break;
        }
    }

    [Inject]

    private void Construct(ISharedData data, Controls.MainActions controls)
    {
        (_data, _controls) = (data, controls);
        _controls.Cansel.performed += OnCancel;
        _controls.Cansel.performed += OnConfirm;
    }

    private void OnDestroy()
    {
        _controls.Cansel.performed -= OnCancel;
        _controls.Cansel.performed -= OnConfirm;
    }
}
