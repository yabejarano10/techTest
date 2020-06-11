using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Rigidbody2D body2d;
    public float leftPushRange;
    public float rightPushRange;
    public float velocityThreshold;

    private bool rotate;

    private float angularVel;
    // Start is called before the first frame update
    void Start()
    {
        rotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            Push();
        }
    }

    public void Push()
    {
        if (transform.rotation.z > rightPushRange)
        {
            body2d.angularVelocity = velocityThreshold*-1;
            angularVel = body2d.angularVelocity;

        }
        else if (transform.rotation.z < leftPushRange)
        {
            body2d.angularVelocity = velocityThreshold;
            angularVel = body2d.angularVelocity;
        }
    }

    public float GetAngularVel()
    {
        return angularVel;
    }

    public void SetPendulum()
    {
        body2d = GetComponent<Rigidbody2D>();
        body2d.angularVelocity = velocityThreshold;
        rotate = true;
    }


}
