using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieComponent))]
public class ZombieHealthComponent : HealthComponent
{
    private ZombieStateMachine ZombieStateMachine;

    // Start is called before the first frame update
    void Awake()
    {
        ZombieStateMachine = GetComponent<ZombieStateMachine>();
    }

    public override void Destroy()
    {
        ZombieStateMachine.ChangeState(ZombieStateType.Dead);
    }
}
