using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golf_parabolic : MonoBehaviour
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
    float force;
    float acc;
    float weight;

    //Canvas
    public TMPro.TMP_Text distance;
    public TMPro.TMP_Text acceleration;
    public TMPro.TMP_InputField degree1;
    public TMPro.TMP_Text force1;

    //Variable to reset rocket
    Vector3 refresh_position;

    //Variables to move the rocket
    Vector3 initial_position;
    Vector3 midway_position;
    Vector3 current_position;
    Vector3 final_position;
    float t = 0.0f;
    bool takeOff = false;
    bool goingDown = false;


    //Line
    LineRenderer lr;
    Vector3 cur_pos;
    Vector3 previous_pos;
    public GameObject myLine;


    public List<Vector3> pos;

    void Start()
    {
        initial_position = this.transform.position;
        refresh_position = initial_position;

        acc = 0;
        weight = 1;

        gravity = 9.8f;
        pos = new List<Vector3>();
    }


    public void ParabolicThrow()
    {
        //Resetting variables
        this.transform.position = refresh_position;
        initial_position = refresh_position;
        midway_position = refresh_position;
        final_position = refresh_position;
        goingDown = false;

        //Get variables from canvas
        
        force = float.Parse(force1.text);
        degree = float.Parse(degree1.text);

        acc = force / weight;

    
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
        distance.text = x_distance.ToString();
        //y_distance1.text = y_distance.ToString();

        takeOff = true;

        final_position = initial_position;
        final_position.x += mid_x;  // x_distance;
        final_position.y += y_distance;
        midway_position = final_position;

        lr = myLine.GetComponent<LineRenderer>();
        lr.positionCount = 20;
        lr.SetPosition(0, refresh_position);

        //Draw Curve
        t = 0;
        int place = 0;
        while(t < time && place < lr.positionCount)
        {
            float y = (v_initial * Mathf.Sin(degree) * t) - (0.5f * gravity * t * t);
            float x = v_initial * Mathf.Cos(degree) * t;
            cur_pos = new Vector3(x, y, this.transform.position.z);
            pos.Add(cur_pos);

            lr.SetPosition(place, cur_pos);
        }
    }

    private void FixedUpdate()
    {
        //Move rocket
        if (takeOff)
        {
            for(int i = 0; i < pos.Count; i++)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, pos[i], t);
            }
        }
    }
}
