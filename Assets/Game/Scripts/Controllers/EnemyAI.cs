﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : AIController
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float viewingDistance = 10f;
    [SerializeField] float escapeDistance = 15f;

    [SerializeField] float searchTime = 5f;
    float currentSearchTimer = 0f;

    Vector3 lastKnownPlayerLocation;

    Vector3 guardPositon;

    State state;


    enum State
    {
        Idle,
        Provoked,
        Searching
    }

    private void Start()
    {
        state = State.Idle;

        base.Start();

        guardPositon = transform.position;
    }

    private void Update()
    {
        if(state == State.Idle)
        {
            IdleBehavior();
        } else if (state == State.Provoked)
        {
            ProvokedBehavior();
        } else if (state == State.Searching)
        {
            SearchingBehavior();
        }
    }

    private void ChasePlayer()
    {
        state = State.Provoked;
        lastKnownPlayerLocation = playerTransform.position;
        navMeshAgent.destination = playerTransform.position;
        FaceTarget();
        currentSearchTimer = 0f;
    }

    private void IdleBehavior()
    {
        if(CalculateDistanceToPlayer() <= viewingDistance)
        {
            ChasePlayer();
            return;
        }
    }

    private void ProvokedBehavior()
    {
        if(CalculateDistanceToPlayer() <= escapeDistance)
        {
            ChasePlayer();
            return;
        }

        state = State.Searching;
        navMeshAgent.destination = lastKnownPlayerLocation;
    }

    private void SearchingBehavior()
    {
        if(CalculateDistanceToPlayer() <= escapeDistance)
        {
            ChasePlayer();
            return;
        }
        if (!HasReachedDestination()) return;

        currentSearchTimer += Time.deltaTime;

        if(currentSearchTimer >= searchTime)
        {
            state = State.Idle;
            navMeshAgent.destination = guardPositon;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewingDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, escapeDistance);

    }

    public bool IsProvoked()
    {
        return state == State.Provoked;
    }

    public override void Die()
    {
        GetComponent<EnemyFighter>().enabled = false;

        base.Die();
    }
}
