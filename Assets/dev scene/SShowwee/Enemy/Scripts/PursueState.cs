using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PursueState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        if (enemyManager.isPreformingAction)
            return this;

        float distanceFromTarget = Vector3.Distance(enemyManager.target.transform.position, transform.position);

        if (enemyManager.isPreformingAction)
        {
            enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            enemyManager.agent.enabled = false;
        }
        else
        {
            if (distanceFromTarget > .1f)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }
            else if (distanceFromTarget <= .1f)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }

        Destination(enemyManager);

        return this;
    }

    private void Destination(EnemyManager enemyManager)
    {
        var destination = Vector3.zero;

        if (enemyManager.fieldOfView.IsTarget)
        {
            destination = enemyManager.target.position;
            enemyManager.agent.stoppingDistance = enemyManager.stoppingDistance;
            Debug.Log("See law na");
        }
        else
        {
            destination = enemyManager.startPos;
            enemyManager.agent.stoppingDistance = 0;
        }

        enemyManager.agent.SetDestination(destination);

    }
    
}
