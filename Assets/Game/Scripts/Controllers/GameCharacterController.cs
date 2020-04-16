using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterController : MonoBehaviour
{
    
    public virtual void Die()
    {
        //GetComponent<Animator>().SetTrigger("Die");

        enabled = false;
    }

}
