using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private SignalBus _signal;
    private ISharedData _data;

    private GameStatus _previousStatus;

    [Inject]

    private void Construct(SignalBus signal, ISharedData data)
    {
        (_signal, _data) = (signal, data);
        _signal.Subscribe<GameEvent>(StartPlay);
    }

    private void StartPlay(GameEvent arg)
    {
        switch (arg)
        {
            case GameEvent.VisualizationPeriod:
                _previousStatus = _data.Status;
                StartCoroutine(Visualization());
                break;
            default:
                return;
        }
    }

    private IEnumerator Visualization()
    {
        yield return null;
        _data.Status = GameStatus.Lock;
        var destination = _data.Destination;
        switch (_previousStatus)
        {
            case GameStatus.ConfirmMove:
                if (destination.Visual != null)
                {
                    //destination.Visual.StartAnimation(AnimationTriggerType.Move);
                    StartCoroutine(OnMove(destination, _data.Target));
                    yield break;
                }
                destination.transform.position = _data.Target.Center;
                NewBind(_data.Target, destination);
                OnEndPlay();
                break;
            case GameStatus.ConfirmAttack:
                if (destination.Visual != null)
                {
                    //destination.Visual.StartAnimation(AnimationTriggerType.Attack);
                    StartCoroutine(WaitingAttack(destination, _data.Target.Unit));
                    yield break;
                }
               // SetDamage(destination, _data.Target.Unit);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator WaitingAttack(Unit destination, Unit target) 
    {
        target.Health -= destination.Settings.Stats.Damage;
        if (target.Health <= 0)
        {
            _data.Target.Unit = null;
            Destroy(target.gameObject);
        }
        yield return new WaitForSeconds(0.01f);
        OnEndPlay();
    }

    private IEnumerator OnMove(Unit destination, Cell cell)
    {
        var source = destination.transform;

        var start = source.position;
        var end = cell.Center;
        var time = Vector3.Distance(start, end) / destination.MoveSpeed;
        var delta = 0f;
        while (delta < time)
        {
            source.position = Vector3.Lerp(start, end, delta / time);
            delta += Time.deltaTime;
            yield return null;
        }
        NewBind(cell, destination);
        OnEndPlay();
    }

    private void NewBind(Cell cell, Unit unit)
    {
        if (cell != null) //if (!cell.IsEmpty)
        {
            Debug.LogError("Rebind busy cell", cell);
        }
        cell.Unit = unit;
        unit.Cell.Unit = null;
        unit.Cell = cell;
    }

    private void OnEndPlay()
    {
        if (_previousStatus is GameStatus.ConfirmAttack
            || !_data.CanSwitch
            || !_data.Destination.Settings.Mobility.MoveAndAttackInTurn)
        {
            _data.NextTurn();
            return;
        }

        _data.ForceAttackMode();
    }
}
