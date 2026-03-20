using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;

    public AIStateID initialState;

    [HideInInspector]
    public GameObject nearest;


    private NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private Animator _animator;
    public Animator Animator => _animator;


    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new Idle());
        stateMachine.RegisterState(new Collect());
        stateMachine.RegisterState(new Search());


        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();


    }
}
