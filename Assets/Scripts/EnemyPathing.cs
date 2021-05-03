using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;

    void Start()
    {
        transform.position = wayPoints[waypointIndex].transform.position;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        {
            var targetPosition = wayPoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex = Random.Range(0, wayPoints.Count);
            }
        }
    }
}
