using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JoinTogether : MonoBehaviour
{
    FixedJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(joint != null) Debug.Log("joint: " + joint.anchor);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Spring"))
        {
            /*Debug.Log("Catch spring!");
            // creates joint
            joint = gameObject.AddComponent<FixedJoint>();
            // sets joint position to point of contact
            joint.anchor = col.contacts[0].point;
            // conects the joint to the other object
            joint.connectedBody = col.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false;*/
        }
    }
}
