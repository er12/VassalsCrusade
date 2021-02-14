using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider cosmicSlider;


    void OnEnable()
    {
        PlayerController.CosmicUpdate += SetCosmic;
        PlayerController.HealthUpdate += SetHealth;
    }

    void OnDisable()
    {
        PlayerController.CosmicUpdate -= SetCosmic;
        PlayerController.HealthUpdate -= SetHealth;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    public void DecreaseHealth(float decrease)
    {
        healthSlider.value -= decrease;
    }

    public void IncreaseHealth(float increase)
    {
        healthSlider.value -= increase;
    }

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }


    //-----------Cosmic
    public void SetCosmic(float cosmic)
    {
        cosmicSlider.value = cosmic;
    }

    public void DecreaseCosmic(float decrease)
    {
        cosmicSlider.value -= decrease;
    }

    public void IncreaseCosmic(float increase)
    {
        cosmicSlider.value -= increase;
    }

    public void SetMaxCosmic(float cosmic)
    {
        cosmicSlider.maxValue = cosmic;
        cosmicSlider.value = cosmic;
    }
}
