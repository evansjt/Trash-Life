using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;

    float maxHealth;

    bool isDead = false;

    private void Start()
    {
        maxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health = Mathf.Clamp(health -= damage, 0f, maxHealth);

        if(Mathf.Approximately(health, 0f))
        {
            GameCharacterController gameCharacter = GetComponent<GameCharacterController>();

            if(gameCharacter != null)
            {
                gameCharacter.Die();
            }

            isDead = true;

        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
    
    public float GetHealth()
    {
        return health;
    }

    public float GetHealthPercentage()
    {
        return (health / maxHealth);
    }

}
