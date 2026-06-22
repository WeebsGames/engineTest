using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EngineRPM : MonoBehaviour
{


    public const int REVLIMIT = 7000;
    public const float LIMITER = 1000;
    public TMP_Text rpmText;
    public TMP_Text gearText;
    public TMP_Text torqueText;

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

        revBands.Insert(0, new RevBand(5, 1500, 1));
        revBands.Insert(1, new RevBand(4, 2500, 2));
        revBands.Insert(2, new RevBand(4, 3500, 6));
        revBands.Insert(3, new RevBand(4, 3750, 8));
        revBands.Insert(4, new RevBand(4, 4000, 10));
        revBands.Insert(5, new RevBand(3, 4000, 12));
    }

    // Update is called once per frame
    void Update()
    {
        // print(revBands[0]);
        if (Input.GetKey(KeyCode.Space) && rpm < REVLIMIT)
        {
            rpm = revBands[gear].rev(rpm, REVLIMIT);
        } else if (rpm > 0)
        {
            rpm -= 2;
        }
        if(rpm < 0)
        {
            rpm = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && gear < revBands.Count - 1)
        {
            gear++;
            rpm*=0.4f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gear > 0)
        {
            gear--;
            rpm*=1.5f;
        }
        
    }

    void FixedUpdate()
    {
        if(rpm > REVLIMIT)
        {
            rpm -= LIMITER; 
        }

        rpmText.text = "RPM: " + Mathf.Round(rpm);
        gearText.text = "Gear: " + gear;
        torqueText.text = "Torque: " + torque;

        driveShaft.Rotate(Vector3.up * rpm * Time.deltaTime);
    }
}
