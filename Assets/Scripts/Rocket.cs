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
    [Header("Ship Movement")]
    [SerializeField] float maxAcceleration = 0.0f;
    [SerializeField] private float accelerationDrag = 0.25f;
    [SerializeField] private float desaccelerationDrag = 2f;
    private float desaccelerationTimer = 0.0f;

     //Rotation
    [Range(0.0f,.5f)]
    [SerializeField]float tiltAngle = 0.5f;
    [SerializeField] float rotationSpeed = 0.0f;
    private bool thrustsAreOn = false;
    private Rigidbody rb;

    [Header("Land")]
    [SerializeField] Vector3 landPositionOffset;
    Vector3 landPositionTarget;

    //body references
    Transform bodyTransform;
    
    //Animator references
    Animator animator;

    //Auudio controller Reference
    AudioController audioController;

    //Camera reference
    [SerializeField] Camera mainCamera;
    //Statics
    
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
        desaccelerationTimer = Mathf.Epsilon;
    }

    // Update is called once per frame
    void Update()
    {
        if (!thrustsAreOn)
            StartThrusts();
        else
            ShipMovement();
        //Test area
        Land();
    }
    private void Land()
    {
        if (Input.GetKey(KeyCode.L))
        {
            bodyTransform.position = Vector3.MoveTowards(bodyTransform.position, landPositionTarget + landPositionOffset, Time.deltaTime * 1);
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

    #region SHIP MOVEMENT
    private void ShipMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //moves upward
            //Todo when pressing W will consume more energy
            StartAccelerating();
            rb.AddForce(Vector3.up * maxAcceleration, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //move downwards
            StartAccelerating();
            rb.AddForce(Vector3.down * maxAcceleration, ForceMode.Acceleration);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            //Moves to the right
            StartAccelerating();
            rb.AddForce(Vector3.right * maxAcceleration, ForceMode.Acceleration);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            //Moves to the Left
            StartAccelerating();
            rb.AddForce(Vector3.left * maxAcceleration, ForceMode.Acceleration);
        }
        
        StopAccelerating();
    }

    private void StartAccelerating()
    {
        rb.drag = accelerationDrag;
        desaccelerationTimer = Mathf.Epsilon;
    }

    private void StopAccelerating()
    {
        if (desaccelerationTimer <= 0)
        {
            rb.drag = desaccelerationDrag; 
        }
        else
        {
            desaccelerationTimer -= Time.deltaTime;
        }
    }
    #endregion SHIP MOVEMENT
    public void LandPositionUpdate(Vector3 _landPositionTarget)
    {
        landPositionTarget = _landPositionTarget;
    }
}
