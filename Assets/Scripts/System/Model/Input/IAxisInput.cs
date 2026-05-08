using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Model
{
    public interface IAxisInput
    {
        IObservable<Vector3> IAxisInput { get; }
    }
}
