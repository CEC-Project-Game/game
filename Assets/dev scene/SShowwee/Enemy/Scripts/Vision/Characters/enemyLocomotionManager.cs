using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRotation;

    float smoothRotationTime = 3f;
    public float startWaitTime = 4f;
    public float speedWalk = 4f;
    public float speedRun = 7.5f;

    float m_WaitTime;

    [SerializeField]
    private Animator animator;

    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] float stoppingDistance = 1f;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    private const string IsWalking = "IsWalking";
    private const string Chase = "Chase";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        m_WaitTime = startWaitTime;
    }

    private void Update()
    {
        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetDirection(transform.forward);

        Destination();

        if (agent.remainingDistance <= 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * smoothRotationTime);
        }
    }

    private void Destination()
    {
        var destination = Vector3.zero;

        if (fieldOfView.IsTarget)
        {
            destination = target.position;
            agent.stoppingDistance = stoppingDistance;
            Move(speedRun);
            animator.SetBool(IsWalking, false);
            animator.SetBool(Chase, agent.velocity.magnitude > speedRun);
        }
        else
        {
            destination = waypoints[m_CurrentWaypointIndex].position;
            agent.stoppingDistance = stoppingDistance;

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                    animator.SetBool(IsWalking, false);
                    return;
                }
            }

            Move(speedWalk);
            animator.SetBool(Chase, false);
            animator.SetBool(IsWalking, agent.velocity.magnitude > 0.1f);
        }

        agent.SetDestination(destination);
    }

    private void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    private void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void Move(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }
}
