using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EngineRPM : MonoBehaviour
{


    public const int REVLIMIT = 7000;
    public const float LIMITER = 1000;
    public TMP_Text rpmText;

    float rpm = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rpmText.text = "RPM: 0"; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) & rpm < REVLIMIT)
        {
            rpm += 10;
        } else if (rpm > 0)
        {
            rpm -= 1;
        }
    }

    void FixedUpdate()
    {
        if(rpm > REVLIMIT)
        {
            rpm -= LIMITER; 
        }

        rpmText.text = "RPM: " + rpm;
    }
}
