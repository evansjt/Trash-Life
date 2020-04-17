using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterController : MonoBehaviour
{

    protected Animator animator;
    
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Die()
    {
        if(animator != null) // REMOVE IF STATEMENT LATER
        {
            animator.SetTrigger("Die");
        }
        
        enabled = false;
    }

}
