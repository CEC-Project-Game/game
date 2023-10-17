using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyField : MonoBehaviour
{
    [Header("Agent")]
    public UnityEngine.AI.NavMeshAgent agent;
    public Vector3 _destinationPoint;
    public float _destinationRadius;
    public Transform PlayerPosition;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    [Header("setting")]
    public float radius;
    [Range(0, 360)]
    public float angle;

    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        if(agent.remainingDistance < 0.5f || canSeePlayer)
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
        if(!canSeePlayer)
        {
            _destinationPoint  = Random.insideUnitSphere * _destinationRadius;
            _destinationPoint = _destinationPoint + transform.position;
            Debug.Log("Cant see");
        }    

        if(canSeePlayer)
        {
            _destinationPoint = PlayerPosition.position;
            Debug.Log("Can see player");
        }
        
        UnityEngine.AI.NavMeshHit hit;
        if(UnityEngine.AI.NavMesh.SamplePosition(_destinationPoint, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
                               //(จุดที่ต้องการตรวจสอบ, เก็บค่าจุดที่ได้กลับมา, รัศมีของการตรวจสอบ, พื้นที่ที่ต้องการตรวจสอบ)
        {
            _destinationPoint = hit.position;
            agent.SetDestination(_destinationPoint);
        }
        else SearhWaypoint();
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
