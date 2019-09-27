using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTEst : MonoBehaviour
{
    Quaternion ObjectQuaternion;
    Vector3 inEuler ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectQuaternion = transform.Find("Body").transform.rotation;
        Debug.Log(ObjectQuaternion.eulerAngles);
    }
}
