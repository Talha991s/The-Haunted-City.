using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeathState : ZombieStates
{
    private static readonly int MovementZHash = Animator.StringToHash("MovementZ");
    private static readonly int IsDeadHash = Animator.StringToHash("IsDead");

    public ZombieDeathState(ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {

    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        OwnerZombie.ZombieNavMesh.isStopped = true;
        OwnerZombie.ZombieNavMesh.ResetPath();

        OwnerZombie.ZombieAnimator.SetFloat(MovementZHash, 0.0f);
        OwnerZombie.ZombieAnimator.SetBool(IsDeadHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        OwnerZombie.ZombieNavMesh.isStopped = false;
        OwnerZombie.ZombieAnimator.SetBool(IsDeadHash, false);
    }
}
