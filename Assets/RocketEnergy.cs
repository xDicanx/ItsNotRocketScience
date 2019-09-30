using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketEnergy : MonoBehaviour
{
    [SerializeField] private float maxEnergy = 0.0f;
    [SerializeField] private float energyConsumePerSecond = 0.0f;

    private float actualEnergy = 0.0f;
    private bool isRocketConsumingEnergy = false;
    
    [SerializeField] Slider energySlider = null;

    // Start is called before the first frame update
    void Start()
    {
        actualEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRocketConsumingEnergy) { return; }

        ConsumeEnergyPersecond();
        UpdateEnergySlider();
        //To do
        /*
         * Reduce energy per X seconds (idle)
         * reduce energy by state (moving forwards consumes more than idle) - optional
         * Reduce energy X amount (receive damage will cause to lost fuel)- optional
         */

    }
    private void UpdateEnergySlider()
    {
        energySlider.value = actualEnergy;
    }

    private void ConsumeEnergyPersecond()
    {
        actualEnergy -= Time.deltaTime * energyConsumePerSecond;
    }

    public void StartConsumingEnergy()
    {
        isRocketConsumingEnergy = true;
    }
}
