using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float MaxRange = 5;
    private PatrolSpots patrolPoints;
    public float Speed = 10;
    private int randomSpot;
    private NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = animator.GetComponent<PatrolSpots>();
        randomSpot = Random.Range(0, patrolPoints.patrolPoints.Length);
        agent = animator.GetComponent<NavMeshAgent>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var newPointDistance = patrolPoints.patrolPoints[randomSpot].position - animator.transform.position;
        var step = Speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(animator.transform.forward, newPointDistance, step, 0.0f);

        if (Vector3.Distance(new Vector3(animator.transform.position.x,0,animator.transform.position.z), new Vector3(patrolPoints.patrolPoints[randomSpot].position.x,0,patrolPoints.patrolPoints[randomSpot].position.z)) > 1f)
        {
            //animator.transform.position = Vector3.MoveTowards(animator.transform.position, patrol.patrolPoints[randomSpot].position, step);
            //animator.transform.rotation = Quaternion.LookRotation(newDir);
            agent.SetDestination(patrolPoints.patrolPoints[randomSpot].position);
        }
        else
        {
            randomSpot = Random.Range(0, patrolPoints.patrolPoints.Length);
        }

    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
