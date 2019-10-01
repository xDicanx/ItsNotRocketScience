using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector =  new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    [Range(0,1)] [SerializeField] float movementFactor; //0 for no move, 1 for fully movement

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscilate();
    }

    private void Oscilate()
    {
        if (period <= Mathf.Epsilon) { return; }//Protects period from being zero
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offSet = movementVector * movementFactor;
        transform.position = startingPos + offSet;
    }
}
