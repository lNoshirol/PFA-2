using UnityEngine;
using UnityEngine.AI;

public class EnemiesMain : MonoBehaviour
{
    public EPatrolState EPatrolState;
    public EChaseState EChaseState;
    public EAttackState EAttackState;

    public EnemiesState EnemiesCurrentState;

    public NavMeshAgent agent;


    public Rigidbody rb { get; private set; }
    public Transform player { get; private set; }
    public Vector3 position { get; private set; }
    public Vector3 velocity { get; private set; }

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;


    public GameObject projectile;

    public Material mat;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        mat = gameObject.GetComponent <Renderer>().material;
    }
    private void Start()
    {
        EPatrolState.Setup(this);
        EChaseState.Setup(this);
        EAttackState.Setup(this);
        EnemiesCurrentState = EPatrolState;
    }

    private void Update()
    {
        EnemiesCurrentState?.Do();
    }

    private void FixedUpdate()
    {
        EnemiesCurrentState?.FixedDo();
    }

    public void SwitchState(EnemiesState newState)
    {
        EnemiesCurrentState?.OnExit();
        EnemiesCurrentState = newState;
        EnemiesCurrentState?.OnEnter();
    }

    public bool CheckPlayerInSightRange()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        return playerInSightRange;
    }

    public bool CheckPlayerInAttackRange()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        return playerInAttackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }



}
