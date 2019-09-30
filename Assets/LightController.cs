using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] float depth = 10f;
    Transform lightTransform;
    // Start is called before the first frame update
    void Start()
    {
        lightTransform = transform.Find("Body").Find("Light").transform;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowMousePosition();
    }

    void FollowMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        lightTransform.LookAt( targetPosition);
    }
    
}
