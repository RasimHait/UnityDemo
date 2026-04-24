using Core.Processors;
using Core.Services;
using Project.Data;

namespace Project.Services
{
    public class ContentService : BaseService
    {
        private readonly ResourceProcessor _resourceProcessor = new();
        public GameStaticData StaticData { get; private set; }

        protected override void OnInitialize()
        {
            StaticData = _resourceProcessor.Load<GameStaticData>("StaticData/GameStaticData");
        }
    }
}
