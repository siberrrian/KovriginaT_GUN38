using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;
using Unity.VisualScripting;
using TMPro;
using Palmmedia.ReportGenerator.Core;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private Transform _camera;
    private UnitStats _stats;

    [SerializeField]
    private TextMeshPro _health;
    public GameObject Visual;

    [SerializeField]
    public float MoveSpeed { get; private set; }

    [field: SerializeField, Space(15f)]
    public UnitGameSettings Settings { get; private set; }
    [field: SerializeField]
    public Team Team { get; private set; }
    [field: SerializeField]
    public UnitPower Power { get; private set; }
    public Cell Cell { get; set; }

    public event Action OnMoveEndCallback;

    public int Health
    {
        get => _stats.Health;
        set {
            _stats.Health = value;
            _health.text = value.ToString();
        }
    }

    private void Awake()
    {
        _stats = Settings.Stats;
        _health.text = Settings.Stats.Health.ToString();
        _health.text = Settings.Stats.Health.ToString();
    }

    [Inject]
    private void Construct(Camera camera)
    {
        _camera = camera.transform;
        _health.enabled = false;
        _health.text = Health.ToString();
    }

    public void ShowHealth()
    {
        _health.transform.rotation = Quaternion.LookRotation(_health.transform.position - _camera.position);
        _health.text = Health.ToString();
        _health.enabled = true;
    }
    public void HideHealth()
    {
        _health.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Cell.OnPointerClick(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowHealth();
        Cell.OnPointerClick(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideHealth();
        Cell.OnPointerClick(eventData);
        
    }

}
