using System;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    //private Dictionary<CellNeighbour, Cell> _neighbours;
    private Cell[] _cells;

    public event Action<Cell> OnCellClicked;

    private void Awake()
    {
        _cells = FindObjectsOfType<Cell>();
        //_neightbours = new Ductionary<CellNeighbour, Cell>(_cells.Lenght * 8);
        var positions = Array.ConvertAll(_cells, t => t.transform.position);
        var distance = 0f;
        for (int i = 0, iMax = _cells.Length; i < iMax;  i++)
        {
            _cells[i].OnPointerClickEvent += OnCellClicked;
#if UNITY_EDITOR
            //_cells[i].OnPointerClickEvent += DebugOnPointerClick;
#endif
            for(int j = 0, jMax = _cells.Length; j < iMax; j++)
            {
                if (i == j) continue;
                var source = positions[i];
                var destination = positions[j];
                /*
                var forward = destination.z.CompareTo(source.z);
                var right = destination.x.CompareTo(source.x);
                var  type = (forward, right) switch
                {
                    (1, 1) => NeighbourType.ForwardRight,
                    (1, 0) => NeighbourType.Forward,
                    (1, -1) => NeighbourType.ForwardLeft,
                    (0, 1) => NeighbourType.Right,
                }*/
            }

        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
