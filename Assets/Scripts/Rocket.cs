using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float launchForce;
    [SerializeField] float TIMER_FROM_LAUNCH_TO_IDLE;
    private float timeToLaunch;
    //Movement
    [SerializeField]float maxUpSpeed;
    float speedDv;
    //Rotation
    [Range(0.0f,.5f)]
    [SerializeField]float tiltAngle = 0.5f;
    [Range(0.0f, 1f)]
    [SerializeField] float rotationSpeed;
    private bool thrustsAreOn = false;
    private bool shipIsReadyToFly;
    private Rigidbody rb;

    //body references
    Transform bodyTransform;

    //Animator references
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("Body").GetComponent<Animator>();
        rb = transform.Find("Body").GetComponent<Rigidbody>();
        timeToLaunch = TIMER_FROM_LAUNCH_TO_IDLE;
        bodyTransform = transform.Find("Body");
    }

    // Update is called once per frame
    void Update()
    {
        //To do

        //Move spaceship upward
        //Move spaceship downward
        //move spaceship to the right/left
        //land
        
        if (!thrustsAreOn)
        {
            StartThrusts();
        }
        else
        {
            ThrustUpwards();
            MoveSideWays();
        }
    }
    private void ThrustUpwards()
    {
        //Pending
        
        
    }

    private void MoveSideWays()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            //Moves to the right


            if (bodyTransform.rotation.x < -.5f + tiltAngle)
            {
                //Moves slowly to the right
                rb.velocity = Vector3.right * .5f;
                Debug.Log("Onwards speed: "+ rb.velocity);
                //Rotates spaceship to the front
                bodyTransform.Rotate(rotationSpeed, 0, 0, Space.Self);
            }
            else
            {
                //Move at max speed
                rb.velocity = Vector3.right;
            }
                
            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //Moves to the left
            
            if (bodyTransform.rotation.x > -.5f - tiltAngle)
            {
                //Moves slowly to the left
                rb.velocity = Vector3.left * .5f;
                //Rotates spaceship to the left
                bodyTransform.Rotate(-rotationSpeed, 0, 0, Space.Self);
            }
            else
            {
                //Moves at max speed
                rb.velocity = Vector3.left;
            }
        }
        else
        {
            //starts idling again
            rb.velocity = Vector3.zero;

            if (bodyTransform.rotation.x > -0.5f)
            {
                //Rotates spaceship to the front
                bodyTransform.Rotate(-rotationSpeed, 0, 0, Space.Self);
            }
            if (bodyTransform.rotation.x < -0.5f)
            {
                bodyTransform.Rotate(rotationSpeed, 0, 0, Space.Self);
            }
            
        }
        
       
        
    }
    private void StartThrusts()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !thrustsAreOn)
        {
            StartCoroutine(WaitForIdlePosition());
        }
    }
    IEnumerator WaitForIdlePosition()
    {
        
        while (timeToLaunch > 0)
        {
            rb.velocity = Vector3.up * launchForce;
            timeToLaunch -= Time.deltaTime;
        }
        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector3.zero;
        thrustsAreOn = true;
        rb.useGravity = false;
    }   
}
