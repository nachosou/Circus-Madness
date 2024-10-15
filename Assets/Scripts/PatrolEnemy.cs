using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public float speed = 2f;           
    public float rotationSpeed = 5f;    
    public float reachDistance = 0.2f;   

    private int currentPointIndex = 0;   

    private void Update()
    {
        if (patrolPoints.Count == 0) return; 

        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        Transform targetPoint = patrolPoints[currentPointIndex]; 

        Vector3 direction = (targetPoint.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) <= reachDistance)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
        }
    }

    private void OnDrawGizmos()
    {
        if (patrolPoints == null || patrolPoints.Count == 0) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);

            if (i < patrolPoints.Count - 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position); 
            }
        }
    }
}