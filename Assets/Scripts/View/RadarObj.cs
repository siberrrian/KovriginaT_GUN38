using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MiniMap
{
    public class RadarObj : MonoBehaviour
    {

        [SerializeField] private Image _ico;
        //private IRadar _radar;

        //[Inject]
        //private void Inject(IRadar radar) => _radar = radar;

        private void OnValidate() => _ico = Resources.Load<Image>(path: "MiniMap/RadarObject");
        private void onDisable() => Radar.RemoveRadarObject(gameObject);
        private void OnEnable() => Radar.RegisterRadarObject(gameObject, _ico);
    }
}
