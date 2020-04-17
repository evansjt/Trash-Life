using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : MonoBehaviour
{
    [SerializeField] float sphereCastRadius = 0.5f;

    Inventory inventory;

    [SerializeField] float attackDamage = 30f;

    float timeSinceLastAttack = Mathf.Infinity;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        InventoryItem item = inventory.GetItem();
        if(item != null && item.IsWeapon())
        {
            if (Input.GetMouseButtonDown(0) && timeSinceLastAttack >= item.GetRecoverTime())
            {
                Attack();
                timeSinceLastAttack = 0f;
            }
        }
    }

    private void Attack()
    {
        InventoryItem item = inventory.GetItem();
        if (!item.IsWeapon()) return;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position + (Vector3.up * 0.5f), sphereCastRadius, transform.forward, item.GetAttackRange());

        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.tag == "Player") continue;

            Health health = hit.transform.GetComponent<Health>();

            if (health == null) continue;

            health.TakeDamage(item.GetDamage());
        }

        
    }
}
