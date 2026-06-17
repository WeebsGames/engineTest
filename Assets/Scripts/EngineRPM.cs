using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EngineRPM : MonoBehaviour
{


    public const int REVLIMIT = 7000;
    public const float LIMITER = 1000;
    public int horsepower;
    public TMP_Text rpmText;
    public TMP_Text gearText;
    public TMP_Text torqueText;

    float rpm = 0;
    float revs = 10;
    float torque = 0;
    int gear = 0;
    bool engineOn = true;
    Transform driveShaft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rpmText.text = "RPM: 0";
        gearText.text = "Gear: 0";
        driveShaft = transform.Find("DriveShaft"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && rpm < REVLIMIT && engineOn)
        {
            rpm += revs;
        } else if (rpm > 0)
        {
            rpm -= 1;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !engineOn && gear == 0)
        {
            engineOn = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && gear != 6)
        {
            gear += 1;
            if(rpm < 3000)
            {
                engineOn = false;
                rpm = 0;
            } else
            {
                rpm *= 0.4f;
            }

        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) && gear != -1)
        {
            gear -= 1;
            if(rpm > 5000)
            {
                engineOn = false;
                rpm = 0;
            } else
            {
                rpm *= 1.25f;
            }
        }
    }

    void FixedUpdate()
    {
        rpm = Mathf.Round(rpm);
        if(rpm > REVLIMIT)
        {
            rpm -= LIMITER; 
        }

        rpmText.text = "RPM: " + rpm;
        gearText.text = "Gear: " + gear;
        torqueText.text = "Torque: " + torque;

        driveShaft.Rotate(Vector3.up * rpm * Time.deltaTime);
    }
}
