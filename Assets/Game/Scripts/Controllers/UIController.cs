using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject PlayerInformationCanvas;
    [SerializeField] Health health;
    [SerializeField] Image healthBarForeground;
    [SerializeField] float healthBarDecreaseSpeed = 0.5f;

    [SerializeField] GameObject DeathCanvas;

    [SerializeField] GameObject DialogCanvas;
    [SerializeField] Text dialogText;

    bool isDead = false;

    private void Start()
    {
        ShowPlayerInformationCanvas(true);
        ShowDeathCanvas(false);
        ShowDialogCanvas(false);
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (isDead) return;

        float healthPercentage = health.GetHealthPercentage();

        float xVal = healthBarForeground.transform.localScale.x - (healthBarDecreaseSpeed * Time.deltaTime);
        if (!Mathf.Approximately(healthPercentage, 0f))
        {
            if (xVal <= healthPercentage) xVal = healthPercentage;
        }
        else
        {
            if (xVal <= 0f) xVal = 0f;
        }

        healthBarForeground.transform.localScale = new Vector3(xVal, 1f, 1f);

        if (Mathf.Approximately(xVal, 0f))
        {
            ShowDeathCanvas(true);
            ShowPlayerInformationCanvas(false);
            ShowDialogCanvas(false);

            isDead = true;
        }

    }

    private void ShowDeathCanvas(bool val)
    {
        DeathCanvas.SetActive(val);
    }

    private void ShowDialogCanvas(bool val)
    {
        DialogCanvas.SetActive(val);
    }

    private void ShowPlayerInformationCanvas(bool val)
    {
        PlayerInformationCanvas.SetActive(val);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // update later
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
