using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    public IdleState idleState;

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

        if (enemyManager.agent.remainingDistance <= .1f)
        {
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.startRotation, Time.deltaTime * enemyManager.smooothRotationTime);
        }

        if (enemyManager.fieldOfView.IsTarget)
        {
            return this;
        }
        else
        {
            return idleState;
        }
    }

    private void Destination(EnemyManager enemyManager)
    {
        var destination = Vector3.zero;

        if (enemyManager.fieldOfView.IsTarget)
        {
            destination = enemyManager.target.position;
            enemyManager.agent.stoppingDistance = enemyManager.stoppingDistance;
            Debug.Log("Kuy gu hen Na");
        }
        else
        {
            destination = enemyManager.startPos;
            enemyManager.agent.stoppingDistance = 0;
        }

        enemyManager.agent.SetDestination(destination);
    }
}
