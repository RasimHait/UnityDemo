using Core.Machine;
using Project.Data;
using UnityEngine;

namespace Project.Machines
{
    public class LevelMachineLoseState : BaseState<LevelDynamicData>
    {
        protected override void Enter()
        {
            Debug.Log("Project: Level Machine entered Lose State");
        }
    }
}
