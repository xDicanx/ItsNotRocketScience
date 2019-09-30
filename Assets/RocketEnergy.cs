using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketEnergy : MonoBehaviour
{
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyConsumePerSecond;

    private float actualEnergy;
    private bool isRocketConsumingEnergy = false;
    
    [SerializeField] Slider energySlider;

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
