using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float UpdateRate = 0.1f;
    private NavMeshAgent Agent;
    private Animator Animator = null;

    private const string IsWalking = "IsWalking";
    private const string Chase = "Chase";
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Animator.SetBool(IsWalking, Agent.velocity.magnitude > 0.01f);
    }
}
