using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    Rigidbody leftrear;
    [SerializeField]
    Rigidbody rightrear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            leftrear.AddTorque(Vector3.right * 5000);
            rightrear.AddTorque(Vector3.right * 5000);
        }
        if (Input.GetKey(KeyCode.S))
        {
            leftrear.AddTorque(Vector3.right * -5000);
            rightrear.AddTorque(Vector3.right * -5000);
        }
    }
}
