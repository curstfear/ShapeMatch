using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TileSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ActionBar>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();
        Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        Container.Bind<VFXController>().FromComponentInHierarchy().AsSingle();
    }
}