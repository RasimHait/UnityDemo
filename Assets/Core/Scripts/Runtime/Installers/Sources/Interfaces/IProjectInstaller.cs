using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public interface IProjectInstaller
    {
        void Install(GameObject projectContext, DiContainer container);
    }
}
