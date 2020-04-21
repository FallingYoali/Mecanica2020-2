using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public Rigidbody rb;
    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "Bouncer")
        {
            //rb.AddForce(80, 0, 0, ForceMode.VelocityChange = 80);
            rb.AddForce(80, 0, 0, ForceMode.Impulse);
            Debug.Log("In collision!");

        }
            
    }
}
