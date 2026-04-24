using System;
using System.Linq;
using System.Reflection;
using Zenject;

namespace Core.Installers
{
    internal sealed class BaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallFromProject();
        }

        private void InstallFromProject()
        {
            var interfaceType = typeof(IProjectInstaller);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                Type[] types;

                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types.Where(t => t != null).ToArray();
                }

                foreach (var type in types)
                {
                    if (type == null)
                        continue;

                    if (type.IsAbstract || type.IsInterface)
                        continue;

                    if (!interfaceType.IsAssignableFrom(type))
                        continue;
 
                    var instance = (IProjectInstaller)Activator.CreateInstance(type);
                    instance.Install(gameObject, Container);
                }
            }
        }
    }
}
