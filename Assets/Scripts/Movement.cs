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

    [Range(0, 50000)]
    public int steeringForce;

    [Range(0, 50000)]
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
            leftrear.AddTorque(Vector3.right * drivingForce);
            rightrear.AddTorque(Vector3.right * drivingForce);
        }
        if (Input.GetKey(KeyCode.S))
        {
            leftrear.AddTorque(Vector3.right * -drivingForce);
            rightrear.AddTorque(Vector3.right * -drivingForce);
        }
        if (Input.GetKey(KeyCode.A))
        {
            steeringAngle -= 3.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            steeringAngle += 3.0f;
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -45, 45);
        steeringMagnitude = Mathf.Sin(steeringAngle) * steeringForce;
        dampeningForce = (steeringForce - (Mathf.Cos(steeringAngle) * steeringForce));
        leftfront.AddForce(Vector3.right * steeringMagnitude);
        rightfront.AddForce(Vector3.right * steeringMagnitude);
        leftrear.AddForce(Vector3.forward * -dampeningForce);
        rightrear.AddForce(Vector3.forward * -dampeningForce);
    }
}
