using Core.Machine;
using Project.Data;
using UnityEngine;

namespace Project.Machines
{
    public class LevelMachine : BaseMachine<LevelDynamicData>
    {
        public override void Initialize()
        {
            SetState<LevelMachineLobbyState>();
        }
    }
}
