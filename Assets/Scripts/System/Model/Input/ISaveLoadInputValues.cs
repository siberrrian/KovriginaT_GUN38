using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace System.Model
{
    public interface ISaveLoadInputValues
    {
        public ReactiveProperty<bool> SaveClicked { get; }
        public ReactiveProperty<bool> LoadClicked { get; }
    }


}
