using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    private MeshRenderer _focus;
    [SerializeField]
    private MeshRenderer _select;
    public event Action<Cell> OnPointerClickEvent;

    public Unit Unit { get; set; }
    private bool IsEmpty => Unit == null;

    public Vector3 Center => _select.transform.position;

    public Dictionary<NeighbourType, Cell> Neighbours { get; set; } = new Dictionary<NeighbourType, Cell>(0);

    public event Action<Cell> OnClicked;
    //public event Action<Cell> OnPointerClickEvent;
    /*
    private void Awake()
    {
        var nei = FindAnyObjectByType<Cell>();

    }*/

    public void SetSelect(Material material)
    {
        _select.enabled = true;
        _select.sharedMaterial = material;
    }

    public void ResetSelect()
    {
        _select.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        OnClicked.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _focus.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _focus.enabled = false;
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
