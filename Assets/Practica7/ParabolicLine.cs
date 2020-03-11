using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using mathf

public class ParabolicLine : MonoBehaviour
{
    //Variables for calculations
    float v_initial;
    float degree;
    float x_distance;
    float mid_x;
    float time_y;
    float time;
    float y_distance;
    float gravity;

    //Canvas
    public TMPro.TMP_InputField v_initial1;
    public TMPro.TMP_InputField degree1;
    public TMPro.TMP_Text x_distance1;
    public TMPro.TMP_Text y_distance1;

    
    //Variable to reset rocket
    Vector3 refresh_position;

    //Variables to move the rocket
    Vector3 initial_position;
    Vector3 midway_position;
    Vector3 current_position;
    Vector3 final_position;
    float t = 0.0f;
    bool takeOff = false;
    bool goingUp = false;
    bool goingDown = false;


    //Line
    LineRenderer lr;
    Vector3 cur_pos;
    Vector3 previous_pos;
    public GameObject[] myLine;



    public void ParabolicThrow()
    {
        //Resetting variables
        this.transform.position = refresh_position;
        initial_position = refresh_position;
        midway_position = refresh_position;
        final_position = refresh_position;
        goingDown = false;

        //Get variables from canvas
        v_initial = float.Parse(v_initial1.text);
        degree = float.Parse(degree1.text);


        //Change degrees to radians
        degree = Mathf.Deg2Rad * degree;

        //time at highest peak in y
        time_y = (v_initial * Mathf.Sin(degree)) / (gravity);

        //How high does it go?
        y_distance = (v_initial * Mathf.Sin(degree) * time_y) - (0.5f * gravity * time_y * time_y);

        //Time where the rocket connects to the ground and its y velocity equals 0
        time = (v_initial * Mathf.Sin(degree)) / (0.5f * gravity);

        //distance travelled
        x_distance = v_initial * Mathf.Cos(degree) * time;

        //x distance when y is at its highest peak
        mid_x = v_initial * Mathf.Cos(degree) * time_y;

        //show results in canvas
        x_distance1.text = x_distance.ToString();
        y_distance1.text = y_distance.ToString();

        takeOff = true;

        final_position = initial_position;
        final_position.x += mid_x;  // x_distance;
        final_position.y += y_distance;
        midway_position = final_position;


        previous_pos = refresh_position;
        cur_pos = refresh_position;
        int place = 0;
        for(float i = 0; i < time; i += 0.5f, place++)
        {
            float y = (v_initial * Mathf.Sin(degree) * i) - (0.5f * gravity * i * i);
            float x = v_initial * Mathf.Cos(degree) * i;
            cur_pos = new Vector3(x, y, refresh_position.z);


            //Create new line
            Debug.Log("current" + cur_pos);
            Debug.Log("prev: " + previous_pos);
            myLine[place].transform.position = previous_pos;
            lr = myLine[place].GetComponent<LineRenderer>();
            Color color = new Color(0, 0, 0, 1);
            lr.startColor = color;
            lr.endColor = color;
            lr.endWidth = 0.5f;
            lr.startWidth = 0.5f;

            lr.SetPosition(0, previous_pos);
            lr.SetPosition(1, cur_pos);

            previous_pos = cur_pos;
        }


    }

    void Start()
    {
        initial_position = this.transform.position;
        refresh_position = initial_position;
        gravity = 9.8f;
    }

    private void FixedUpdate()
    {
        //Move rocket
        if (takeOff)
        { 
            this.transform.position = Vector3.Lerp(initial_position, final_position, t);
            t += 0.4f * Time.deltaTime;

            if (t > 1.0f)
            {
                final_position = initial_position;
                final_position.x += x_distance;
                initial_position = midway_position;
                t = 0.0f;
                
                if (goingDown)
                {
                    goingDown = false;
                    takeOff = false;
                }
                goingDown = true;
            }
        }
    }
}
