using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : MonoBehaviour
{
    [SerializeField] float recoveryTime = 0.5f;
    [SerializeField] float swingDistance = 5f;

    [SerializeField] float attackDamage = 30f;

    float timeSinceLastAttack = Mathf.Infinity;

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && timeSinceLastAttack >= recoveryTime)
        {
            Attack();
            timeSinceLastAttack = 0f;
        }
    }

    private void Attack()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position + (Vector3.up * 0.5f), transform.forward, swingDistance);

        foreach(RaycastHit hit in hits)
        {
            Health health = hit.transform.GetComponent<Health>();

            if (health == null) continue;

            health.TakeDamage(attackDamage);
        }

        
    }
}
