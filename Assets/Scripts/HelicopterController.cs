using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HelicopterController : MonoBehaviour
{
    [Header("Main Engine Parameters")]
    [SerializeField] private float maxSpeed = 50f; //Max velocity by xyz

    [Header("Torque Parameters")]
    [SerializeField] private float torqueRollForce = 2000f;
    [SerializeField] private float torquePitchForce = 3000f;
    [SerializeField] private float torqueYawForce = 100f;

    [Header("Rotors Parameters")]
    [SerializeField] private float maxRPM = 500f;
    [SerializeField] private float rotorEffectivness = 5f;
    [SerializeField] private float timeToMaxForce = 3f;
    [SerializeField] private Transform rotorTransform;

    [Header("Other")]
    [SerializeField] private Transform com; //Center of mass position


    private Rigidbody rb;
    private float roll;
    private float pitch;
    private float yaw;
    public float currentRPM { get; private set; }
    private float rotorVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com.localPosition;
    }
    private void Update()
    {
        PlayerInput();
        //Smooth Start/End rotor rotation
        
        rotorTransform.Rotate(currentRPM / 0.16667f * Time.deltaTime * Vector3.up);
    }
    private void FixedUpdate()
    {
        PhysicsMovement();
    }
    void PlayerInput()
    {
        roll = Input.GetAxisRaw("Horizontal");
        pitch = Input.GetAxisRaw("Vertical");
        yaw = Input.GetAxisRaw("Yaw");

        float targetSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            targetSpeed = maxRPM;
        else if (Input.GetKey(KeyCode.LeftControl))
            targetSpeed = 0;
        else
            targetSpeed = currentRPM;

        currentRPM = Mathf.SmoothDamp(currentRPM, targetSpeed,
            ref rotorVelocity, timeToMaxForce, maxRPM / 10f);
        //currentForce = Mathf.Clamp(currentForce, 0, engineMaxForce);
    }
    void PhysicsMovement()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed); //Max speed xyz
        rb.AddForce(transform.up * currentRPM / 10f * rotorEffectivness, ForceMode.Impulse); //UpForce || ( Shift / CTRL ) input
        rb.AddTorque(transform.forward * roll * 1000 * torqueRollForce); //Horizontal input
        rb.AddTorque(-transform.right * pitch * 1000 * torquePitchForce); //Vertical input
        rb.AddTorque(transform.up * yaw * 1000 * torqueYawForce); //Yaw input ( Q / E )
    }
}
