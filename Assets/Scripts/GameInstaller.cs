using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private Controls _controls;

    [SerializeField]
    private CellManager _cellManager;

    [SerializeField]
    private SceneController _controller;

    [SerializeField, Space(15f)]
    private CellPaletteSettings _cellPaletteSettings;


    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Enable();
        Container.BindInstance(_controls).AsSingle(); //“ут и выше было _controls.Game но оно не работало

        Container.BindInstance(_cellManager).AsSingle();
        Container.BindInstance(_controller).AsSingle();

        Container.BindInstance(_cellPaletteSettings).AsSingle();

        // for test
        _cellManager.OnCellClicked += CellManagerOnOnCellClicked;
    }

    private void CellManagerOnOnCellClicked(Cell obj)
    {
        obj.SetSelect(_cellPaletteSettings.SelectCell);
    }

    private void OnDestroy()
    {
        _controls.Dispose();
    }
}