using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    void Start()
    {
        //slider = GetComponent<Slider>();
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public float GetHealth()
    {
        return slider.value;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }
}
