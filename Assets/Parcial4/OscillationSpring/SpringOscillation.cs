using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringOscillation : MonoBehaviour
{
    public float k;
    public float m;
    public float balanceDistance = 0;
    public float A;
    public float v;
    public float T;
    public float f;
    public float dampening;

    public float acc;
    float t = 0.0f;

    public float offset;

    bool activate = false, activate2 = false;

    Vector3 finalScale;
    Vector3 originalScale;

    public Transform spring;


    public void ButtonPressed()
    {
        Debug.Log("button pressed");

        float displacement = balanceDistance - A;
        dampening = k * displacement;  //ley de hook
        acc += ((k / m) * displacement) - dampening;

        finalScale = new Vector3(spring.localScale.x, spring.localScale.y - acc + offset, spring.localScale.z);
        originalScale = new Vector3(spring.localScale.x, spring.localScale.y + offset, spring.localScale.z);

        activate = true;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            spring.localScale = Vector3.Slerp(originalScale, finalScale, t);
            t += 0.01f;
            if (t >= 1)
            {
                t = 0;
                activate = false;
                activate2 = true;
            }
        }
        

        if (activate2)
        {
            spring.localScale = Vector3.Slerp(finalScale, originalScale, t);
            t += 0.01f;
            if (t >= 1)
            {
                t = 0;
                activate = true;
                activate2 = false;
            }
        }
    }
}
