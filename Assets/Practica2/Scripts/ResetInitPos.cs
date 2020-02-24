using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInitPos : MonoBehaviour
{

    Vector3 initialPos;
    Quaternion initialRot;
    void Start()
    {
        //Save initial position
        initialPos = this.transform.position;
        initialRot = this.transform.rotation;
    }


    //Called when Update button is pressed
    public void ResetPosition()
    {
        //this.GetComponent<Rigidbody>().detectCollisions = false;
        this.GetComponent<Rigidbody>().mass = 0;

        this.transform.position = initialPos;
        this.transform.rotation = initialRot;

        //this.GetComponent<Rigidbody>().detectCollisions = true;
        //this.GetComponent<Rigidbody>().isKinematic = true;
    }
}
