using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class EngineRPM : MonoBehaviour
{


    public const int REVLIMIT = 7000;
    public const float LIMITER = 500;
    public float horsepower = 100;
    public TMP_Text rpmText;
    public TMP_Text gearText;
    public TMP_Text torqueText;
    public WheelControl[] wheels;
    public InputActionReference moveAction;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public Rigidbody rigidBody;
    public float maxSpeed = 20f;

    float rpm = 0;
    float revs = 10;
    float torque = 0;
    int gear = 0;
    bool engineOn = true;
    Transform driveShaft;
    List<RevBand> revBands;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rpmText.text = "RPM: 0";
        gearText.text = "Gear: 0";
        driveShaft = transform.Find("DriveShaft");

        //initialize revBands
        revBands = new List<RevBand>();

        revBands.Insert(0, new RevBand(5, 1500, 0.5f));
        revBands.Insert(1, new RevBand(5, 1500, 1));
        revBands.Insert(2, new RevBand(4, 2500, 2));
        revBands.Insert(3, new RevBand(4, 3500, 6));
        revBands.Insert(4, new RevBand(4, 3750, 8));
        revBands.Insert(5, new RevBand(4, 4000, 10));
        revBands.Insert(6, new RevBand(3, 4000, 12));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && rpm < REVLIMIT)
        {
            rpm = revBands[gear].rev(rpm, REVLIMIT);
        } else if (rpm > 0)
        {
            rpm -= 2;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rpm -=10;
        }
        if(rpm < 0)
        {
            rpm = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && gear < revBands.Count - 1)
        {
            gear++;
            rpm*=0.4f;
        }
        if (Input.GetKeyDown(KeyCode.Q) && gear > 0)
        {
            gear--;
            rpm*=1.5f;
        }
    }

    void FixedUpdate()
    {
        if(rpm > REVLIMIT)
        {
            rpm -= LIMITER/revBands[gear].revPenalty; 
        }
        torque = ((horsepower * rpm) / 5252) * gear;

        rpmText.text = "RPM: " + Mathf.Round(rpm);
        gearText.text = "Gear: " + gear;
        torqueText.text = "Torque: " + Mathf.Round(torque);

        driveShaft.Rotate(Vector3.up * rpm * Time.deltaTime);

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed)); // Normalized speed factor

        float hInput = moveAction.action.ReadValue<Vector2>().x; // Steering input
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        foreach (var wheel in wheels)
        {
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (wheel.motorized)
            {
                wheel.WheelCollider.motorTorque = torque;
            }
        }
    }
}
