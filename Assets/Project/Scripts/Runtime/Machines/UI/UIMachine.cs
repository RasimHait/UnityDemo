using Core.Machine;
using Project.Data;

namespace Project.Machines
{
    public class UIMachine : BaseMachine<UIDynamicData>
    {
        public override void Initialize()
        {
            SetState<UIMachineStartState>();
        }
    }
}
