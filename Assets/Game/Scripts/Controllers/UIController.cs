using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text healthText;
    [SerializeField] Health health;

    private void Update()
    {
        healthText.text = "Health: " + health.GetHealth() + " / " + health.GetMaxHealth(); 
    }
}
