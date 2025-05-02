using UnityEngine;

public class EPatrolState : EnemiesState
{
    public float walkPointRange;
    public Vector3 walkPoint;
    public bool walkPointSet = false;
    public override void OnEnter()
    {
        EnemiesMain.mat.color = Color.green;
        SearchWalkPoint();
    }
    public override void Do()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            SetEnemyDestination(walkPoint);
        }

        CheckDistanceDestination();

        if (EnemiesMain.CheckPlayerInSightRange())
        {
            EnemiesMain.SwitchState(EnemiesMain.EChaseState);
        }

    }

    public override void FixedDo()
    {
    }
    public override void OnExit()
    {
        walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, EnemiesMain.whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void SetEnemyDestination(Vector3 destination)
    {
        EnemiesMain.agent.SetDestination(destination);
    }

    private void CheckDistanceDestination()
    {
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }


}
