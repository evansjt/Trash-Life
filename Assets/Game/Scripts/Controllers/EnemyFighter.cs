using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : MonoBehaviour
{
    GameObject playerObject;

    [SerializeField] float attackRange = 3f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float recoveryTime = 1f;

    float timeSinceLastAttack = Mathf.Infinity;

    EnemyAI enemyAI;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (!enemyAI.IsProvoked()) return;

        if((enemyAI.CalculateDistanceToPlayer() <= attackRange) && (timeSinceLastAttack >= recoveryTime))
        {
            playerObject.GetComponent<Health>().TakeDamage(attackDamage);
            timeSinceLastAttack = 0f;
        }
    }
}
