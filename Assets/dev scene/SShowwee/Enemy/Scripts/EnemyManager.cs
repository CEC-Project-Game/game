using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    EnemyLocomotionManger enemyLocomotionManger;
    EnemyAnimatorManager enemyAnimatorManager;

    public Vector3 startPos;
    public Quaternion startRotation;

    public State currentState;
    public FieldOfView fieldOfView;
    public NavMeshAgent agent;
    public Transform target;

    public bool isPreformingAction;

    public float stoppingDistance = 1f;
    public float smooothRotationTime = 3f;

    private void Awake()
    {
        enemyLocomotionManger = GetComponent<EnemyLocomotionManger>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        agent = GetComponentInChildren<NavMeshAgent>();
        agent.enabled = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetDirection(transform.forward);
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyAnimatorManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }


}
