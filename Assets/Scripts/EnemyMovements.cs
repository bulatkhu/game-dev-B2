using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    [SerializeField] public Transform[] patrolPoints; 
    [SerializeField] public float moveSpeed;

    private int patrolDestinationIndex = 0;
    
    void Update()
    {
        Transform currentPatrolPoint = patrolPoints[patrolDestinationIndex];

        transform.position = Vector2.MoveTowards(transform.position, currentPatrolPoint.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentPatrolPoint.position) < .2f)
        {
            patrolDestinationIndex = (patrolDestinationIndex + 1) % patrolPoints.Length;
        }
    }
}
