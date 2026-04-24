using Core.Installers;
using Project.Services;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ProjectInstaller : IProjectInstaller
    {
        public void Install(GameObject projectContext, DiContainer container)
        {
            Debug.Log("Project: Installing project bindings.");
            container.BindInterfacesAndSelfTo<EventService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<ContentService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<PoolService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<UIService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<SceneService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<ProgressService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<MachineService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<LevelService>().AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<LoopService>().AsSingle().NonLazy();
        }
    }
}
