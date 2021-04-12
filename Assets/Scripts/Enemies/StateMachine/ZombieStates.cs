public class ZombieStates : State<ZombieStateType>
{
    protected ZombieComponent OwnerZombie;

    public ZombieStates(ZombieComponent zombie, ZombieStateMachine stateMachine) : base(stateMachine)
    {
        OwnerZombie = zombie;
    }
}

public enum ZombieStateType
{
    Idle,
    Attack,
    Follow,
    Dead
}