using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISharedData
{
    GameStatus Status { get; set; }
    GameEvent Event { get; set; }

    Unit Destination { get; set; }
    Cell Target { get; set; }
    ICollection<Cell> Accessibles { get; }

    bool CanSwitch { get; set; }

    void Reset();
}
