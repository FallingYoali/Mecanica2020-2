using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class BallMovement : MonoBehaviour
{
    public Transform[] ramps;
    private float distance, g, yI, yF, t, m, acc;
    private float startTime, vel;
    int step;
    float angle;


    private void Start()
    {
        step = 0;
        m = 1;
        g = -9.8f;
        yI = transform.position.y;

        StartCoroutine(ShootRay(Vector3.down));
        CalculateTime();

        startTime = Time.time;
        vel = distance / t;

        StartCoroutine(MoveDown(transform.position, new Vector3(transform.position.x, transform.position.y - distance, transform.position.z), t));
    }
    public float StartDistance()
    {
        return Vector3.Distance(transform.position, ramps[0].transform.position);
    }


    void CalculateTime()
    {
        t = Mathf.Abs(distance / g);
    }
    IEnumerator ShootRay(Vector3 dir)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position,
            transform.TransformDirection(dir),
            out hit,
            Mathf.Infinity))
        {
            Debug.Log("Ray hit!");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            distance = hit.distance - (transform.localScale.y / 2);
        }
        else
        {
            Debug.Log("No ray hit");
        }
        Debug.Log("distance" + distance);
        yield return null;
    }

    IEnumerator MoveDown(Vector3 a, Vector3 b, float t)
    {
       
        float lerpValue = 0.0f;
        while (lerpValue < t)
        {
            float distCovered = (Time.time - startTime) * vel;
            float fraccion = distCovered / (distance + 0.015f);
            b += Vector3.down * 0.015f;
            transform.position = Vector3.Lerp(a, b, fraccion);

            lerpValue += Time.deltaTime;
            yield return null;
        }
        step++;
        CalculationsManager();
        yield return null;
    }


    void CalculationsManager()
    {
        Debug.Log(step);
        switch (step)
        {
            case 1:  //1st plank 
                Debug.Log("Touch first plank");
                step++;
                CalculateDistancePlank(ramps[0]);

                break;
            case 2:  //2nd fall
                Debug.Log("Second Fall");
                StartCoroutine(ShootRay(Vector3.down));
                step++;
                CalculateFreeFall();
                break;
            case 3:  // 1st momentum strike 
                Debug.Log("Momentum Started");
                step++;
                CalculateMomentum(ramps[1]);
                break;
            case 4:  //2nd plank down
                step++;
                Debug.Log("Slide plank 2");
                CalculateSlidePlank2(ramps[1]);
                break;
            case 5:  //3rd fall
                step++;
                Debug.Log("Second fall");
                CalculateFall2(25.0f);
                break;
            case 6:  //3rd plank down
                step++;
                Debug.Log("Third Plank!");
                CalculateMovePlank3();
                break;
            case 7:  //3rd fall
                Debug.Log("Third Fall");
                StartCoroutine(ShootRay(Vector3.down));
                step++;
                CalculateFall3(40);
                break;
        }
    }
   void CalculateMovePlank3()
    {
        StartCoroutine(ShootRay(Vector3.left));
        CalculateTime();
        float angle = 40;
        float x = distance;
        float y = x * Mathf.Tan(angle);

        Vector3 a = transform.position;
        Vector3 b = transform.position;

        b.y += (y - 0.8f);
        b.x -= (x + 0.8f);
        t = 0.3f;
       

        StartCoroutine(MoveAcrossPlank(a, b, t));
    }
    void CalculateMomentum(Transform plank)
    {
        float angle = 25;// plank.eulerAngles.z;
        float v_x = vel * Mathf.Cos(angle);
        float v_y = v_x * Mathf.Tan(angle);
        t = Mathf.Abs((-v_y) / g);

        float y = (v_y * t) + (0.5f * g * Mathf.Pow(t, 2));
        float x = y / (Mathf.Tan(angle));

        /*t = 1.12f;
        x = 2.38f;
        y = 1.11f;*/
        Vector3 a = transform.position;
        Vector3 b = transform.position;

        b.y -= y;
        b.x -= x;

        

        startTime = Time.time;

        startTime = Time.time;
        StartCoroutine(MoveAcrossPlank(a, b, t));
    }
    void CalculateFall3(float _angle)
    {
        angle = _angle;
        float v_y = vel * Mathf.Sin(angle);
        

        float t1 = (-v_y + Mathf.Sqrt(Mathf.Pow(v_y, 2) - (4 * (0.5f * g) * distance))) / (2 * (0.5f * g));
        float t2 = (-v_y - Mathf.Sqrt(Mathf.Pow(v_y, 2) - (4 * (0.5f * g) * distance))) / (2 * (0.5f * g));
        if (t1 >= 0)
        {
            t = t1;
        }
        else
        {
            t = t2;
        }
        t = t2;
        distance = -distance;

        float x = 2.0f;
        float y = 3.02f;

        Vector3 a = transform.position;
        Vector3 b = transform.position;

        b.x -= x;
        b.y -= y;

        Debug.Log(a);
        Debug.Log(b);


        startTime = Time.time;
        float lerpValue = 0.0f;
        while (lerpValue < t)
        {

            float distCovered = (Time.time - startTime) * vel;
            Debug.Log(distCovered + " " + Time.time + " " + startTime);
            float fraccion = distCovered / (distance);
            Debug.Log("f: " + fraccion);
            transform.position = Vector3.Lerp(a, b, fraccion);

            lerpValue += Time.deltaTime;
           
        }
        CalculationsManager();

    }
    void CalculateFall2(float _angle)
    {
        angle = _angle;
        float v_y = vel * Mathf.Sin(angle);
        distance = -3.11467f;

        float t1 = (-v_y + Mathf.Sqrt(Mathf.Pow(v_y, 2) - (4 * (0.5f * g) * distance))) / (2 * (0.5f * g));
        float t2 = (-v_y - Mathf.Sqrt(Mathf.Pow(v_y, 2) - (4 * (0.5f * g) * distance))) / (2 * (0.5f * g));
        t1 = -0.94053f;
        t2 = 0.6758338f;
        if (t1 >= 0)
        {
            t = t1;
        }
        else
        {
            t = t2;
        }
        distance = -distance;

        float x = vel * Mathf.Cos(-angle) * t;
        float y = 3.02f;

        Vector3 a = transform.position;
        Vector3 b = transform.position;

        b.x += x;
        b.y -= y;

       

        startTime = Time.time;
        StartCoroutine(MoveFreeFall(a, b, t));

    }

    void CalculateSlidePlank2(Transform plank)
    {
        StartCoroutine(ShootRay(Vector3.right));
        CalculateTime();
        float angle = -25;
        float x = distance;
        float y = x * Mathf.Tan(angle);
        
        Vector3 a = transform.position;
        Vector3 b = transform.position;

        b.y += (y - 5.4f);
        b.x += x;

       
        StartCoroutine(MoveAcrossPlank(a, b, t));
    }
    void CalculateDistancePlank(Transform _length)
    {
        float plankLength = _length.localScale.x;  //hypotenusa
        angle = _length.eulerAngles.z;
        
        float x = Mathf.Sin(angle) * plankLength;  //height distance
        float y = Mathf.Cos(angle) * plankLength;  //horizontal distance

        distance = plankLength;
        CalculateTime();

        Debug.Log("Move across plank!");
        startTime = Time.time;

        vel = distance / t;
        

        Vector3 b = transform.position;  //destination

        b.y += y;
        b.x -= x;

        StartCoroutine(MoveAcrossPlank(transform.position, b, t));
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("STOP"))
        {
            Debug.Log("stop");
            col.gameObject.SetActive(false);
            StopAllCoroutines();
            CalculationsManager();
        }
    }

    void CalculateFreeFall() {
        Debug.Log("CalculateFreeFall");

        float v_y = vel * Mathf.Sin(angle);
        distance = -distance;

        float t1 = (-v_y + Mathf.Sqrt(Mathf.Pow(v_y, 2) - (4 * (0.5f * g) * distance))) / (2 * (0.5f * g));
        float t2 = (-v_y - Mathf.Sqrt(Mathf.Pow(v_y, 2) - (4 * (0.5f * g) * distance))) / (2 * (0.5f * g));

        if( t1 >= 0)
        {
            t = t1;
        }
        else
        {
            t = t2;
        }
        t = t2;
        distance = -distance;

        float x = vel * Mathf.Cos(-angle) * t;
        float y = distance;

        Vector3 a = transform.position;
        Vector3 b = transform.position;

        b.x += x;
        b.y -= y;

       

        startTime = Time.time;
        StartCoroutine(MoveFreeFall(a, b, t));

    }

    IEnumerator MoveFreeFall(Vector3 a, Vector3 b, float t)
    {
        float lerpValue = 0.0f;
        while (lerpValue < t)
        {

            float distCovered = (Time.time - startTime) * vel;
            float fraccion = distCovered / (distance);
            b.y += 0.001f;
            transform.position = Vector3.Lerp(a, b, fraccion);

            lerpValue += Time.deltaTime;
            yield return null;
        }
        CalculationsManager();
        yield return null;
    }
    IEnumerator MoveAcrossPlank(Vector3 a, Vector3 b, float t)
    {

        float lerpValue = 0.0f;
        while (lerpValue < t)
        {
            float distCovered = (Time.time - startTime) * vel;
            float fraccion = distCovered / (distance);
            b.x -= 0.001f;
            transform.position = Vector3.Lerp(a, b, fraccion);

            lerpValue += Time.deltaTime;
            yield return null;
        }
       
        CalculationsManager();
        yield return null;
    }

}
