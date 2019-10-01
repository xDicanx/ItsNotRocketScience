using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyStationColliderTrigger : MonoBehaviour
{
    EnergyStation energyStation = null;
    // Start is called before the first frame update
    void Start()
    {
        energyStation = transform.parent.GetComponent<EnergyStation>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player is touching me");
        }
    }
}
