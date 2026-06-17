using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EngineRPM : MonoBehaviour
{


    public const int REVLIMIT = 7000;
    public const float LIMITER = 1000;
    public TMP_Text rpmText;
    public TMP_Text gearText;

    float rpm = 0;
    float revs = 10;
    int gear = 0;
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
        if (Input.GetKey(KeyCode.Space) && rpm < REVLIMIT)
        {
            rpm += revs;
        } else if (rpm > 0)
        {
            rpm -= 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && gear != 6)
        {
            gear += 1;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) && gear != -1)
        {
            gear -= 1;
        }
    }

    void FixedUpdate()
    {
        if(rpm > REVLIMIT)
        {
            rpm -= LIMITER; 
        }

        rpmText.text = "RPM: " + rpm;
        gearText.text = "Gear: " + gear;

        driveShaft.Rotate(Vector3.up * rpm * Time.deltaTime);
    }
}
