using UnityEngine;

public class EPatrolState : EnemiesState
{
    public float walkPointRange;
    public Vector3 walkPoint;
    public bool walkPointSet = false;
    public LayerMask whatIsGround;
    public override void OnEnter()
    {
        SearchWalkPoint();
    }
    public override void Do()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else if (walkPointSet)
        {
            EnemiesMain.agent.SetDestination(walkPoint);
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

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
