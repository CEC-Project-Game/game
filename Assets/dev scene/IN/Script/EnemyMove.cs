using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class EnemyMove : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Vector3 _destinationPoint;
    public Transform PlayerPosition;
    public float x;

    void Start()
    {
        agent.speed = 5;
        agent.angularSpeed = 300;
        agent.acceleration = 100;
    }
    void Update()
    {
        SearchWaypoint();    
    }

    private void SearchWaypoint()
    {
        _destinationPoint = PlayerPosition.position;
        UnityEngine.AI.NavMeshHit hit;
        
        if (UnityEngine.AI.NavMesh.SamplePosition(_destinationPoint, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
        {
            _destinationPoint = hit.position;
            agent.SetDestination(_destinationPoint);
        }
        else
        {
            if (retryAttempts < maxRetryAttempts)
            {
                retryAttempts++;
                SearchWaypoint();
            }
        }
    }

    private int retryAttempts = 0;
    private int maxRetryAttempts = 3; // Set a reasonable maximum number of retry attempts.

    
    public void UpdateSpeed(float x)
    {
        agent.speed = x;
    }


}*/