using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName ="InventoryItem/Create New Inventory Object", order = 0)]
public class InventoryItem : ScriptableObject
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] float damage = -1f;
    [SerializeField] float recoveryTime = -1f;
    [SerializeField] float attackRange = -1f;
    [SerializeField] GameObject projectileObject;
    [SerializeField] GameObject particleSystem;
    [SerializeField] bool isRightHanded = true;
    [SerializeField] bool isWeapon = true;

    [SerializeField] string itemName = "Default Name";

    [SerializeField] AnimatorOverrideController overrideController;

    public void SpawnObject(Transform rightHand, Transform leftHand, Animator animator)
    {
        Transform hand;
        hand = GetHand(rightHand, leftHand);
        if (hand == null) return;

        DestroyOldWeapon(rightHand, leftHand);

        if(objectPrefab != null)
        {
            GameObject spawnedWeapon = Instantiate(objectPrefab, hand);
            spawnedWeapon.name = "Item";
        }

        HandleAnimatorOverride(animator);
    }

    void HandleAnimatorOverride(Animator animator)
    {
        if(overrideController != null)
        {
            animator.runtimeAnimatorController = overrideController;
            return;
        }


        AnimatorOverrideController currentController = animator.runtimeAnimatorController as AnimatorOverrideController;
        if (currentController == null) return;

        animator.runtimeAnimatorController = currentController.runtimeAnimatorController;
    }

    void DestroyOldWeapon(Transform rightHand, Transform leftHand)
    {
        Transform oldWeapon;
        oldWeapon = rightHand.Find("Item");
        if(oldWeapon == null)
        {
            oldWeapon = leftHand.Find("Item");
        }
        if (oldWeapon == null) return;

        oldWeapon.name = "DESTROYING";
        Destroy(oldWeapon.gameObject);
    }

    Transform GetHand(Transform r, Transform l)
    {
        if(isRightHanded)
        {
            return r;
        }
        return l;
    }

    void FireProjectile(Transform rightHand, Transform leftHand, GameObject sender, Vector3 dir)
    {
        // Projectile Firing Code Here
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetRecoverTime()
    {
        return recoveryTime;
    }

    public bool HasProjectile()
    {
        return (projectileObject != null);
    }

    public bool IsWeapon()
    {
        return isWeapon;
    }

    public string GetItemName()
    {
        return itemName;
    }

}
