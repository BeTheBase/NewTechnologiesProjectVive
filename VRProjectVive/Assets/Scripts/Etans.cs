using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etans : MonoBehaviour
{
    public float Speed = 5f;
    private PatrolSpots spots;
    private int NextPoint = 0;
    private void Start()
    {
        spots = GetComponent<PatrolSpots>();
        NextPoint = Random.Range(0, spots.patrolPoints.Length);
    }

    private void Update()
    {
        Vector3 nextPointPosition = Vector3.zero;

        if (NextPoint < spots.patrolPoints.Length)
        {
            //Move from point to point
            nextPointPosition = new Vector3(spots.patrolPoints[NextPoint].position.x, transform.position.y, spots.patrolPoints[NextPoint].position.z);

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(spots.patrolPoints[NextPoint].position.x, 0, spots.patrolPoints[NextPoint].position.z)) < 0.1f)
            {
                NextPoint = Random.Range(0, spots.patrolPoints.Length);
            }

            transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, Speed * Time.deltaTime);
        }
        else
        {
            NextPoint = 0;
            nextPointPosition = new Vector3(spots.patrolPoints[NextPoint].position.x, transform.position.y, spots.patrolPoints[NextPoint].position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, Speed * Time.deltaTime);
        }
    }
}
