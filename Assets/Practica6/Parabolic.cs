using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using mathf

public class Parabolic : MonoBehaviour
{
    public float v_initial;
    public float degree;
    float x_distance;
    float time;
    float y_distance;
    float gravity;

    void Start()
    {
        gravity = 9.8f;
        //y = v_initial + v_initial * sin(degree) * time - 1/2 * gravity * time to the power of two

        //time at highest peak in y
        degree = Mathf.Deg2Rad * degree;
        time = (v_initial * Mathf.Sin(degree)) / (gravity);
        y_distance = (v_initial * Mathf.Sin(degree) * time) - (0.5f * gravity * time * time);

        time = (v_initial * Mathf.Sin(degree)) / (0.5f * gravity);
        print(time);
        x_distance = v_initial * Mathf.Cos(degree) * time;
        print(x_distance);
        print("Final time: " + time);
        print("Y: " + y_distance);
        print("X: " + x_distance);
    }



}
