using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    Rigidbody leftRearWheel;

    [SerializeField]
    Rigidbody rightRearWheel;

    [SerializeField]
    Rigidbody leftFrontWheel;

    [SerializeField]
    Rigidbody rightFrontWheel;

    [SerializeField]
    Rigidbody mainBody;

    [SerializeField]
    GameObject leftRearCompressionObject;

    [SerializeField]
    GameObject rightRearCompressionObject;

    [SerializeField]
    GameObject leftFrontCompressionObject;

    [SerializeField]
    GameObject rightFrontCompressionObject;

    float leftRearCompression;
    float rightRearCompression;
    float leftFrontCompression;
    float rightFrontCompression;

    float steeringAngle;
    float steeringMagnitude;
    float dampeningForce;
    float acceleration;

    [Range(0, 50000)]
    public int steeringForce;

    [Range(0, 100000)]
    public int drivingForce;

    [SerializeField]
    Image leftFrontCompressionBar;

    [SerializeField]
    Image leftRearCompressionBar;

    [SerializeField]
    Image rightFrontCompressionBar;

    [SerializeField]
    Image rightRearCompressionBar;

    /*
    List<float> leftRearCompressionList;
    List<float> rightRearCompressionList;
    List<float> leftFrontCompressionList;
    List<float> rightFrontCompressionList;
    List<float> timeStampList;
    List<float> steeringAngleList;
    */

    //Start is called before the first frame update
    void Awake()
    {
        mainBody.centerOfMass = Vector3.up * 0.5f;
        using (StreamWriter fileWriter = new StreamWriter("telemetryData.csv", false))
        {
            fileWriter.WriteLine("unixTime" + "," + "leftRearCompression" + "," + "rightRearCompression" +
                "," + "leftFrontCompression" + "," + "rightFrontCompression" + "," + "steeringAngle");
        }
    }
    

    void Update()
    {
        using (StreamWriter fileWriter = new StreamWriter("telemetryData.csv", true))
        {
            fileWriter.WriteLine(System.DateTime.Now.Ticks.ToString("##################") + "," + leftRearCompression + "," + rightRearCompression +
                "," + leftFrontCompression + "," + rightFrontCompression + "," + steeringAngle);
        }
        leftFrontCompressionBar.fillAmount = leftFrontCompression;
        leftRearCompressionBar.fillAmount = leftRearCompression;
        rightFrontCompressionBar.fillAmount = rightFrontCompression;
        rightRearCompressionBar.fillAmount = rightRearCompression;
    }

    //FixedUpdate has a 50Hz refresh rate, being called 50 times a second
    void FixedUpdate()
    {
		//This bit drives
        if (Input.GetKey(KeyCode.W))
        {
            acceleration += 0.1f;
            leftRearWheel.AddTorque(Vector3.right * (drivingForce * acceleration));
            rightRearWheel.AddTorque(Vector3.right * (drivingForce * acceleration));
        }
        if (Input.GetKey(KeyCode.S))
        {
            acceleration -= 0.1f;
            leftRearWheel.AddTorque(Vector3.right * (drivingForce * acceleration));
            rightRearWheel.AddTorque(Vector3.right * (drivingForce * acceleration));
        }
		//This bit steers
        if (Input.GetKey(KeyCode.A))
        {
            steeringAngle += 5.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            steeringAngle -= 5.0f;
        }
		//This bit does complicated steering calculation maff
        acceleration = Mathf.Clamp(acceleration, -1, 1);
        steeringAngle = Mathf.Clamp(steeringAngle, -45, 45);
        CalculateSteering();
        CalculateSuspensionCompression();
        Debug.Log(Time.time);
    }
    void CalculateSuspensionCompression()
    {
        leftRearCompression = Vector3.Distance(leftRearCompressionObject.transform.position, leftRearWheel.transform.position);
        rightRearCompression = Vector3.Distance(rightRearCompressionObject.transform.position, rightRearWheel.transform.position);
        leftFrontCompression = Vector3.Distance(leftFrontCompressionObject.transform.position, leftFrontWheel.transform.position);
        rightFrontCompression = Vector3.Distance(rightFrontCompressionObject.transform.position, rightFrontWheel.transform.position);
        Debug.Log(leftRearCompression);
        Debug.Log(rightRearCompression);
        Debug.Log(leftFrontCompression);
        Debug.Log(rightFrontCompression);
    }
    void CalculateSteering()
    {
        steeringMagnitude = Mathf.Sin(steeringAngle) * steeringForce;
        dampeningForce = (drivingForce - (Mathf.Cos(steeringAngle) * drivingForce));
        dampeningForce = Mathf.Clamp(dampeningForce, 0, drivingForce * 0.75f);
        leftFrontWheel.AddForce(Vector3.left * steeringMagnitude);
        rightFrontWheel.AddForce(Vector3.left * steeringMagnitude);
        leftRearWheel.AddTorque((Vector3.forward * -dampeningForce) / 2);
        rightRearWheel.AddTorque((Vector3.forward * -dampeningForce) / 2);
    }
    void RecordTelemetryData()
    {

    }
}
