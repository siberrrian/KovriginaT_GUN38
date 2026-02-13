using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class BattleField : IDisposable//, IInitializable
{
    [Inject]
    //private IGameplayCommand _command;
    private readonly SignalBus _signal; 
    private readonly CellPaletteSettings _palettes;
    private readonly ISharedData _data;

    private readonly Dictionary<CellNeighbour, Cell> _neighbours;
    private readonly Cell[] _cells;
    public event Action<Cell> OnCellClicked;
    public bool TryGet(Cell source, NeighbourType type, out Cell cell)
    {
        var data = new CellNeighbour(type, source);
        return _neighbours.TryGetValue(data, out cell);
    }

    private void Callback(GameEvent arg)
    {
        Material getAccessiblesMaterial()
            => _data.Status switch
            {
                GameStatus.Move or GameStatus.ConfirmMove => _palettes.MoveCell,
                GameStatus.Attack or GameStatus.ConfirmAttack => _palettes.AttackCell,
                _ => throw new ArgumentOutOfRangeException(nameof(_data.Status), _data.Status, null)
            };
        foreach (var cell in _cells)
            cell.ResetSelect();
        switch (arg)
        {
            case GameEvent.SelectDestination:
                _data.Destination.Cell.SetSelect(_palettes.SelectCell);
                break;
            case GameEvent.SwitchMode:
                var mat = getAccessiblesMaterial();
                foreach (var cell in _data.Accessibles)
                    cell.SetSelect(mat);
                goto case GameEvent.SelectDestination;
            case GameEvent.SelectTarget:
                _data.Target.SetSelect(getAccessiblesMaterial());
                goto case GameEvent.SelectDestination;
        }

        /*
        foreach (var cell in _cells)
            cell.ResetSelect();
        if (_data.Destination != null)
            _data.Destination.Cell.SetSelect(_palettes.SelectCell);*/
       // var = _data.Status switch
    }

    public BattleField(SignalBus signal, ISharedData data, CellPaletteSettings palettes, Cell[] cells)
    {
        _signal = signal;
        _data = data;
        _palettes = palettes;
    }

    public void Initialize()
    {
        _signal.Subscribe<GameEvent>(Callback);
    }

    public void Dispose()
    {
        for (int i = 0, iMax = _cells.Length; i < iMax; i++)
        {
           // _cells[i].OnClicked -= OnCellClicked;
#if UNITY_EDITOR
            _cells[i].OnClicked -= DebugOnPointerClick;
#endif

        }
    }
#if UNITY_EDITOR
    private void DebugOnPointerClick(Cell cell)
    {
        var start = cell.transform.position;
        Debug.DrawLine(start, start + Vector3.up * 5f, Color.green, 5f);/*
        foreach (var it in EnumUtility.All)
        {
            var key = new CellNeighbour(it, cell);
            if (!_neighbours.TryGetValue(key, out var neighbour))
                continue;

            start = neighbour.transform.position;
            Debug.DrawLine(start, start + Vector3.up * 5f, Color.red, 5f);
        }*/
    }
#endif

    private readonly struct CellNeighbour : IEquatable<CellNeighbour>
    {
        private readonly NeighbourType _type;
        private readonly Cell _value;

        public CellNeighbour(NeighbourType type, Cell value)
            => (_type, _value) = (type, value);

        public bool Equals(CellNeighbour other)
            => _type == other._type && Equals(_value, other._value);

        public override bool Equals(object obj)
            => obj is CellNeighbour other && Equals(other);

        public override int GetHashCode()
            => unchecked(HashCode.Combine(_type, _value) - 13);
    }
}
