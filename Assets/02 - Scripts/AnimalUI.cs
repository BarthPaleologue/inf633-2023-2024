using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    [SerializeField] private Image energyBar;
    private float energyLevel;
    private float targetEnergyLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setEnergyLevel(float targetEnergyLevel) {
        this.targetEnergyLevel = targetEnergyLevel;
    }

    // Update is called once per frame
    void Update()
    {
        energyLevel = Mathf.MoveTowards(energyLevel, targetEnergyLevel, 0.1f);
        energyBar.fillAmount = energyLevel;
    }
}
