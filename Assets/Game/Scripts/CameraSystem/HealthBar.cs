using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image healthBarForeground;
    [SerializeField] Text healthText;

    [SerializeField] float decreaseSpeed = 0.5f;

    [SerializeField] float healthBarDisappearTime = 3f;

    bool isDead = false;

    private void Update()
    {
        float healthPercentage = health.GetHealthPercentage();

        if (!isDead && Mathf.Approximately(healthPercentage, 0f))
        {
            isDead = true;
            Destroy(gameObject, healthBarDisappearTime);
        }

        float xVal = healthBarForeground.transform.localScale.x - (decreaseSpeed * Time.deltaTime);
        if(!Mathf.Approximately(healthPercentage, 0f))
        {
            if (xVal <= healthPercentage) xVal = healthPercentage;
        } else
        {
            if (xVal <= 0f) xVal = 0f;
        }
        


        healthBarForeground.transform.localScale = new Vector3(xVal, 1f, 1f);
        healthText.text = (healthPercentage * 100) + "%";
    }
}
