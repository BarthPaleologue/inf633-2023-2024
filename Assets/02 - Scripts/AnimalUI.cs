using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    // energy bar
    [SerializeField] private Image energyBar;
    private float energyLevel;
    private float targetEnergyLevel;

    // age bar
    [SerializeField] private Image ageBar;
    private float ageLevel;
    private float targetAgeLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setEnergyLevel(float targetEnergyLevel) {
        this.targetEnergyLevel = targetEnergyLevel;
    }

    public void setAgeLevel(float targetAgeLevel) {
        this.targetAgeLevel = targetAgeLevel;
    }

    // Update is called once per frame
    void Update()
    {
        energyLevel = Mathf.MoveTowards(energyLevel, targetEnergyLevel, 0.1f);
        energyBar.fillAmount = energyLevel;

        ageLevel = Mathf.MoveTowards(ageLevel, targetAgeLevel, 0.1f);
        ageBar.fillAmount = ageLevel;
    }
}
