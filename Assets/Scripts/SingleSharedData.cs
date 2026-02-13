using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSharedData : ISharedData
{
    private readonly SignalBus _signal;
    private readonly HashSet<Cell> _set = new(16);

    private GameStatus _status;
    private GameEvent _event;

    public GameEvent Event
    {
        get => _event;
        set
        {
            Debug.Log(_event == value
                ? $"<b>[{nameof(GameEvent)}]</b>: <u>REPEATED</u> event: <b>{value}</b>"
                : $"<b>[{nameof(GameEvent)}]</b>: new event:{value}</b>");

            _event = value;
            _signal.Fire(value);
        }
    }

    public GameStatus Status
    {
        get => _status;
        set
        {
            Debug.Log(_status == value
                ? $"<b>[{nameof(GameEvent)}]</b>: <u>REPEATED</u> status: <b>{value}</b>"
                : $"<b>[{nameof(GameEvent)}]</b>: new status:{value}</b>");
            _status = value;
        }
    }
}
