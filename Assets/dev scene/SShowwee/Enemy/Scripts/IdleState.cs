using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public PursueTargetState pursueTargetState;

    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        if (enemyManager.fieldOfView.IsTarget)
        {
            Debug.Log("SEe");
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }
}
