using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image image;
    private HealthSystem healthSystem;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += ChangeHealth;
    }
    
    void Awake()
    {
        image = GetComponent<Image>();
    }

    void ChangeHealth(object sender, System.EventArgs e)
    {
        image.fillAmount = healthSystem.GetHealthPercent();
    }
}
