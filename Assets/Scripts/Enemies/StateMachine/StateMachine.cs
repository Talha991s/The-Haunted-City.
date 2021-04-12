using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> : MonoBehaviour where T : Enum
{
    public State<T> CurrentState { get; private set; }
    protected Dictionary<T, State<T>> States;
    private bool Running;

    private void Awake()
    {
        States = new Dictionary<T, State<T>>();
    }

    public void Initialize(T startingState)
    {
        if (States.ContainsKey(startingState))
        {
            ChangeState(startingState);
        }
    }

    public void AddState(T stateName, State<T> state)
    {
        if (States.ContainsKey(stateName)) return;
        States.Add(stateName, state);
    }

    public void RemoveState(T stateName)
    {
        if (!States.ContainsKey(stateName)) return;
        States.Remove(stateName);
    }

    public void ChangeState(T nextState)
    {
        if (Running)
        {
            StopRunningState();
        }

        if (!States.ContainsKey(nextState)) return;

        CurrentState = States[nextState];
        CurrentState.Start();

        if (CurrentState.UpdateInterval > 0)
        {
            InvokeRepeating(nameof(IntervalUpdate), 0.0f, CurrentState.UpdateInterval);
        }

        Running = true;
    }

    private void StopRunningState()
    {
        Running = false;
        CurrentState.Exit();
        CancelInvoke(nameof(IntervalUpdate));
    }

    private void IntervalUpdate()
    {
        if (Running)
        {
            CurrentState.IntervalUpdate();
        }
    }

    private void Update()
    {
        if (Running)
        {
            CurrentState.Update();
        }
    }

    private void FixedUpdate()
    {
        if (Running)
        {
            CurrentState.FixedUpdate();
        }
    }
}
