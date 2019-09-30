using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //Script Reference variables
    RocketEnergy rocketEnergy;

    //
    [SerializeField] float launchForce = 0.0f;
    [SerializeField] float TIMER_FROM_LAUNCH_TO_IDLE = 0.0f;
    private float timeToLaunch = 0.0f;

    //Movement
    [SerializeField]float maxUpSpeed = 0.0f;
    [SerializeField] float maxDownSpeed = 0.0f;
    [SerializeField] float maxLeftSpeed = 0.0f;
    [SerializeField] float maxRightSpeed = 0.0f;


    //Rotation
    [Range(0.0f,.5f)]
    [SerializeField]float tiltAngle = 0.5f;
    
    [SerializeField] float rotationSpeed = 0.0f;
    private bool thrustsAreOn = false;
    private bool shipIsReadyToFly;
    private Rigidbody rb;

    //body references
    Transform bodyTransform;
    
    //Animator references
    Animator animator;

    //Auudio controller Reference
    AudioController audioController;

    

    //Camera reference
    [SerializeField] Camera mainCamera;
    //Statics
    public static float globalGravity = -9.81f;
    


    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("Body").GetComponent<Animator>();
        rb = transform.Find("Body").GetComponent<Rigidbody>();
        timeToLaunch = TIMER_FROM_LAUNCH_TO_IDLE;
        bodyTransform = transform.Find("Body");
        audioController = GetComponent<AudioController>();
        mainCamera = FindObjectOfType<Camera>();
        rocketEnergy = GetComponent<RocketEnergy>();
    }

    // Update is called once per frame
    void Update()
    {
        //land (raycast to the ground, if is touching a button saying to land
        
        if (!thrustsAreOn)
        {
            StartThrusts();
        }
        else
        {
            ThrustUpwards();
            ThrustDownwards();
            MoveSideWays();
        }
        
    }

    private void ThrustDownwards()
    {
        if (Input.GetKey(KeyCode.S))
        {
            //Then move down ward
            //Less energy from the thursts
            rb.AddForce(globalGravity * Vector3.up * maxDownSpeed, ForceMode.Acceleration);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Stop moving
            //Idle
            //idle energy from the thursts
            rb.velocity = Vector3.zero;
        }
    }

    private void ThrustUpwards()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //up energy consume from the thursts
            rb.AddForce(-globalGravity * Vector3.up * maxUpSpeed, ForceMode.Acceleration);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //idle energy from the thursts
            rb.velocity = Vector3.zero;
        }
    }

    private void MoveSideWays()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            //Moves to the right
            //back thursts energy consume
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
                rb.velocity = Vector3.right * maxRightSpeed;
            }
                
            
        }
       
        else if (Input.GetKey(KeyCode.A))
        {
            //Moves to the left
            //Front thursts energy consume
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
                rb.velocity = Vector3.left * maxLeftSpeed;
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
        //Must be played and keep playing the whole game and never touched
        AudioSource EngineAudio = audioController.Getaudio("EngineAudio");
        EngineAudio.Play();
        rocketEnergy.StartConsumingEnergy();
        mainCamera.GetComponent<CameraFollow>().enabled = true;
    }

}
