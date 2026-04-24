using Core.Machine;
using Project.Data;
using UnityEngine;

namespace Project.Machines
{
    public class UIMachineActiveState : BaseState<UIDynamicData>
    {
        protected override void Enter()
        {
            Debug.Log("Project: UI Machine entered Active(Game) State");
        }
    }
}
