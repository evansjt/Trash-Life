using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Text healthText;
    

    private void Update()
    {
        float healthPercentage = health.GetHealthPercentage();

        if (Mathf.Approximately(healthPercentage, 0f)) Destroy(gameObject);

        healthBarForeground.transform.localScale = new Vector3(healthPercentage, 1f, 1f);
        healthText.text = (healthPercentage * 100) + "%";
    }
}
