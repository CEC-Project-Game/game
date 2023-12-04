using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyLocomotionManger enemyLocomotionManger;
    EnemyAnimatorManager enemyAnimatorManager;

    public State currentState;

    public bool isPreformingAction;

    private void Awake()
    {
        enemyLocomotionManger = GetComponent<EnemyLocomotionManger>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
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
