using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyStation : MonoBehaviour
{
    [SerializeField] private float energyPerSecond = 1f;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] RocketEnergy rocketEnergy = null;

    //cache
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.Find("Body").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponent<Rocket>().LandPositionUpdate(transform.position);
        }
    }
}
