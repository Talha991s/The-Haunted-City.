using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{
    private GameObject FollowTarget;
    private IDamageable DamageableObject;
    private float AttackRange = 2.0f;

    private static readonly int MovementZHash = Animator.StringToHash("MovementZ");
    private static readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    public ZombieAttackState(GameObject followTarget, ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        FollowTarget = followTarget;
        UpdateInterval = 2.0f;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        OwnerZombie.ZombieNavMesh.isStopped = true;
        OwnerZombie.ZombieNavMesh.ResetPath();
        OwnerZombie.ZombieAnimator.SetFloat(MovementZHash, 0.0f);
        OwnerZombie.ZombieAnimator.SetBool(IsAttackingHash, true);

        DamageableObject = FollowTarget.GetComponent<IDamageable>();
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        DamageableObject?.TakeDamage(OwnerZombie.ZombieDamage);
    }

    // Update is called once per frame
    public override void Update()
    {
        OwnerZombie.transform.LookAt(FollowTarget.transform.position, Vector3.up);

        float distanceBetween = Vector3.Distance(OwnerZombie.transform.position, FollowTarget.transform.position);
        if (distanceBetween > AttackRange)
        {
            StateMachine.ChangeState(ZombieStateType.Follow);
        }
    }

    public override void Exit()
    {
        base.Exit();
        OwnerZombie.ZombieAnimator.SetBool(IsAttackingHash, false);
    }
}
