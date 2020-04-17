using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : GameCharacterController
{
    protected Transform playerTransform;
    protected NavMeshAgent navMeshAgent;

    [SerializeField] float spinSpeed = 15f;

    [SerializeField] float stoppingThreshold = 0.1f;

    public override void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    protected void SetRandomDestinationWithinRangeFromAgent(float range)
    {
        Vector2 randomPointOnUnitCircle = (UnityEngine.Random.insideUnitCircle).normalized; // Normalized just in case
        Vector3 randomPointOnUnitSphere = new Vector3(randomPointOnUnitCircle.x, 0, randomPointOnUnitCircle.y); // makes the unit circle into a unit sphere
        Vector3 dir = randomPointOnUnitSphere * range;
        Vector3 sourcePointFromAI = dir + transform.position;

        navMeshAgent.destination = sourcePointFromAI;
    }

    public float CalculateDistanceToPlayer()
    {
        if (playerTransform == null) return Mathf.Infinity;

        return Vector3.Distance(transform.position, playerTransform.position);
    }

    protected bool HasReachedDestination()
    {
        return (navMeshAgent.remainingDistance <= (navMeshAgent.stoppingDistance + stoppingThreshold));
    }

    protected void FaceTarget()
    {
        //if (CalculateDistanceToPlayer() <= (navMeshAgent.stoppingDistance + 0.1f)) return;

        Vector3 dir = (playerTransform.position - transform.position).normalized;

        Quaternion lookAt = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, spinSpeed * Time.deltaTime);
    }

    protected void HandleAnimations()
    {
        Vector3 worldSpaceVelocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(worldSpaceVelocity);
        float forwardSpeed = localVelocity.z;
        if(animator != null)  // REMOVE IF STATEMENT LATER
        {
            animator.SetFloat("speed", forwardSpeed);
        }
        
    }

    public override void Die()
    {
        navMeshAgent.enabled = false;
        base.Die();
    }
}
