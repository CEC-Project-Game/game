using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyField : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Vector3 _destinationPoint;
    public float _destinationRadius;
    public float PlayerPosition;

    public float radius;

    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        if(agent.remainingDistance < 0.1f)
        {
            SearhWaypoint();
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return null;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;


        }
        else if (canSeePlayer)
            canSeePlayer = false;
    } 

    private void SearhWaypoint()
    {
        _destinationPoint  = Random.insideUnitSphere * _destinationRadius;
        _destinationPoint = _destinationPoint + transform.position;
        
        UnityEngine.AI.NavMeshHit hit;
        if(UnityEngine.AI.NavMesh.SamplePosition(_destinationPoint, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
                               //(จุดที่ต้องการตรวจสอบ, เก็บค่าจุดที่ได้กลับมา, รัศมีของการตรวจสอบ, พื้นที่ที่ต้องการตรวจสอบ)
        {
            _destinationPoint = hit.position;
            agent.SetDestination(_destinationPoint);
        }
        else SearhWaypoint();
    }

    private void EyeEnemy()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _destinationRadius);
    }

    public void UpdateSpeed(float x)
    {
        agent.speed += x;
    }


}
