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
    Rigidbody mainbody;

    // Start is called before the first frame update
    void Start()
    {
        mainbody.centerOfMass = Vector3.back;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            leftrear.AddTorque(Vector3.right * 25000);
            rightrear.AddTorque(Vector3.right * 25000);
        }
        if (Input.GetKey(KeyCode.S))
        {
            leftrear.AddTorque(Vector3.right * -25000);
            rightrear.AddTorque(Vector3.right * -25000);
        }
    }
}
