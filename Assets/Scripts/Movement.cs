using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    Rigidbody leftrear;

    [SerializeField]
    Rigidbody rightrear;

    [SerializeField]
    Rigidbody leftfront;

    [SerializeField]
    Rigidbody rightfront;

    [SerializeField]
    Rigidbody mainbody;

    float steeringAngle;
    float steeringMagnitude;
    float dampeningForce;
    float acceleration;

    [Range(0, 50000)]
    public int steeringForce;

    [Range(0, 100000)]
    public int drivingForce;

    // Start is called before the first frame update
    void Start()
    {
        mainbody.centerOfMass = Vector3.up * 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            acceleration += 0.1f;
            leftrear.AddTorque(Vector3.right * (drivingForce * acceleration));
            rightrear.AddTorque(Vector3.right * (drivingForce * acceleration));
        }
        if (Input.GetKey(KeyCode.S))
        {
            acceleration -= 0.1f;
            leftrear.AddTorque(Vector3.right * (drivingForce * acceleration));
            rightrear.AddTorque(Vector3.right * (drivingForce * acceleration));
        }
        if (Input.GetKey(KeyCode.A))
        {
            steeringAngle += 5.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            steeringAngle -= 5.0f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            drivingForce += 200000;
        }
        if (Input.GetKey(KeyCode.E))
        {
            drivingForce -= 200000;
        }
        acceleration = Mathf.Clamp(acceleration, -1, 1);
        Debug.Log(acceleration);
        steeringAngle = Mathf.Clamp(steeringAngle, -45, 45);
        steeringMagnitude = Mathf.Sin(steeringAngle) * steeringForce;
        dampeningForce = (drivingForce - (Mathf.Cos(steeringAngle) * drivingForce));
        dampeningForce = Mathf.Clamp(dampeningForce, 0, drivingForce * 0.75f);
        leftfront.AddForce(Vector3.left * steeringMagnitude);
        rightfront.AddForce(Vector3.left * steeringMagnitude);
        leftrear.AddTorque((Vector3.forward * -dampeningForce) / 2);
        rightrear.AddTorque((Vector3.forward * -dampeningForce) / 2);

    }
}
