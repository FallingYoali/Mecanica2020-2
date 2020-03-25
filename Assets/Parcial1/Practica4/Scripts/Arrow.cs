using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{ 
    float mass = 3;
    float gravity = 9.8f;
    Vector3 weight = new Vector3(0, 0, 0);
    Vector3 force = new Vector3(300, 0, 0);
    Vector3 velocity = new Vector3(0,0,0);
    bool shoot;

    //F = mv
    //v = F/m

    void Start()
    {
        shoot = true;
        weight.y = mass * gravity;
    }

    void FixedUpdate()
    {
        if(shoot == true){
            velocity = force / mass - weight;
            print("Test" + velocity);
            this.transform.Translate(velocity * Time.deltaTime);
        }
    }

    public void Shoot()
    {
        print("Here!");
        shoot = true;
    }
}
