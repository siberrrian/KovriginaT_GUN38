using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InteractIndicator : MonoBehaviour
{
    //private ISharedData _data;

    [SerializeField]
    private Image _moveIcon;
    [SerializeField]
    private Image _attackIcon;
    [SerializeField]
    private Image _and;
    [SerializeField]
    private Image _or;

    [SerializeField, Space(15f), Range(.1f, 2f)]
    private float _disableScale = .7f;
    [SerializeField, Range(0f, 1f)]
    private float _disableAlpha = .3f;

    private void Switch(Image enable, Image disable)
    {
        enable.color = new Color(1f, 1f, 1f, 1f);
        disable.color = new Color(1f, 1f, 1f, _disableAlpha);

        enable.transform.localScale = Vector3.one;
        disable.transform.localScale = Vector3.one * _disableScale;
        /*
        (_and.enabled, _or.enabled) = _data.Destination.Settings.Mobility.MoveAndAttackInTurn
        ? (true, false)
        : (false, true);*/

    }

    private void Callback(GameEvent arg)
    {/*
        switch (arg)
        {
            case GameEvent.SwitchMode:
                gameObject.SetActive(true);
                var (enable, disable) = _data.Status is GameStatus.Move
                    ? (_moveIcon, _attackIcon)
                    : (_attackIcon, _moveIcon);
                Switch(enable, disable);
                break;
            case GameEvent.Empty:
            case GameEvent.NewTurn:
            case GameEvent.VisualizationPeriod:
                gameObject.SetActive(false);
                break;
        }*/
    }
    /*
      
    [Inject] 
    private void Construct(SignalBus signal, ISharedData data)
    {
        signal.Subscribe<GameEvent>(Callback);
        _data = data;
    }
    */
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
