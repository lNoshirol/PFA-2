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
        if (walkPointSet)
        {
            Debug.Log("Je me déplace");
            EnemiesMain.agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint; 

        if(distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }

        Debug.Log("In Patrol State");
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
        Debug.Log("Je cherhce un point");
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
