using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRotation;

    float smooothRotationTime = 3f;
    public float startWaitTime = 4f;                 //  Wait time of every action
    public float speedWalk = 4f;                     //  Walking speed, speed in the nav mesh agent
    public float speedRun = 7.5f;                      //  Running speed

    float m_WaitTime;                               //  Variable of the wait time that makes the delay
    private Animator Animator = null;

    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] float stoppingDistance = 1f;

    public Transform[] waypoints;                   //  All the waypoints where the enemy patrols
    int m_CurrentWaypointIndex;                     //  Current waypoint where the enemy is going to

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
        m_WaitTime = startWaitTime;                 //  Set the wait time variable that will change
    }

    private void Update()
    {
        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetDirection(transform.forward);

        Destination();

        if (agent.remainingDistance <= .1f)
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * smooothRotationTime);
    }

    private void Destination()
    {
        var destination = Vector3.zero;

        if (fieldOfView.IsTarget)
        {
            destination = target.position;
            agent.stoppingDistance = stoppingDistance;
            Move(speedRun);
            /*Animator.SetBool(Chase, agent.velocity.magnitude > speedWalk);*/
            
        }
        else
        {
            destination =waypoints[m_CurrentWaypointIndex].position;
            destination = startPos;
            /*agent.stoppingDistance = 0;*/
            /*agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);*/    //  Set the enemy destination to the next waypoint
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                    /*Animator.SetBool(IsWalking, agent.velocity.magnitude > 0.01f);*/
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
                
            }
        }
        agent.SetDestination(destination);
    }

    /*private void Patroling() //fix
    {
        Agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the enemy destination to the next waypoint
        if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
            if (m_WaitTime <= 0)
            {
                NextPoint();
                Move(speedWalk);
                m_WaitTime = startWaitTime;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }*/
    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        Debug.Log(m_CurrentWaypointIndex);
        Debug.Log(waypoints[m_CurrentWaypointIndex].position);
        /*agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);*/
    }
    void Stop()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }
    void Move(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }
}