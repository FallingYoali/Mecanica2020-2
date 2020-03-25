using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public TMPro.TMP_InputField force;
    int distance = 12;
    float energy = 0;
    public TMPro.TMP_Text energy_answer;


    public void Calculate()
    {
        energy = float.Parse(force.text);
        energy = energy * distance;
        print("Energy: " + energy);
        energy_answer.text = energy.ToString();

        this.GetComponent<Rigidbody>().AddForce(new Vector3(float.Parse(force.text), 0f, 0f));
    }
}
