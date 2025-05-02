using UnityEngine;
using UnityEngine.AI;

public class EnemiesMain : MonoBehaviour
{
    public EPatrolState EPatrolState;
    public EChaseState EChaseState;
    public EAttackState EAttackState;

    public EnemiesState EnemiesState;

    public NavMeshAgent agent;


    public Rigidbody rb { get; private set; }
    public Transform player { get; private set; }
    public Vector3 position { get; private set; }
    public Vector3 velocity { get; private set; }


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        EPatrolState.Setup(this);
        EChaseState.Setup(this);
        EAttackState.Setup(this);
        EnemiesState = EPatrolState;
    }

    private void Update()
    {
        EnemiesState?.Do();
    }

    public void SwitchState(EnemiesState newState)
    {
        EnemiesState?.OnExit();
        EnemiesState = newState;
        EnemiesState?.OnEnter();
    }
}
