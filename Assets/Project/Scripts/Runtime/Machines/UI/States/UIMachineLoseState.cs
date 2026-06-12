using Core.Machine;
using Project.Data;
using UnityEngine;

namespace Project.Machines
{
    public class UIMachineLoseState : BaseState<UIDynamicData>
    {
        protected override void Enter()
        {
            Debug.Log("Project: UI Machine entered Lose State");
        }
    }
}
