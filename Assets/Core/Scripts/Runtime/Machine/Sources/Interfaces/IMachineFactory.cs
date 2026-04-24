namespace Core.Machine
{
    public interface IMachineFactory
    {
        T CreateMachine<T>() where T : IBaseMachine;
    }
}
