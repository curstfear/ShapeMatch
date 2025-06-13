using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameArea _gameArea;

    public override void InstallBindings()
    {
        PlayerBindings();
        CoreBindings();
    }

    private void PlayerBindings()
    {
        Container.Bind<ActionBar>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();
        Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        Container.Bind<RefreshButton>().FromComponentInHierarchy().AsSingle();
    }

    private void CoreBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TileSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameArea>().FromInstance(_gameArea).AsSingle();
        Container.Bind<VFXController>().FromComponentInHierarchy().AsSingle();

    }
}