using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class golf_parabolic : MonoBehaviour
{
    //Variables for calculations
    float v_initial, degree, x_distance, mid_x, time_y, time, y_distance, gravity, force, acc_x, acc_y, weight;

    int limit;
    float connection_time;

    //Canvas
    public TMPro.TMP_Text distance;
    public TMPro.TMP_Text acceleration_x;
    public TMPro.TMP_Text acceleration_y;
    public TMPro.TMP_InputField degree1;
    public TMPro.TMP_Text force1;
    public Slider slider;
    

    //Variable to reset rocket
    Vector3 refresh_position;

    //Variables to move the rocket
    Vector3 initial_position;
    Vector3 midway_position;
    Vector3 current_position;
    Vector3 final_position;
    float t = 0.0f;
    bool takeOff = false;


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
         
        
        weight = 1;
        connection_time = 0.05f;
        

        gravity = 9.8f;
        pos = new List<Vector3>();
    }

    public void CalculateThrow()
    {
        force = slider.value;
        
        //Resetting variables
        this.transform.position = refresh_position;
        initial_position = refresh_position;
        midway_position = refresh_position;
        final_position = refresh_position;

        //Get variables from canvas
        force1.text = force.ToString();
        degree = float.Parse(degree1.text);

        //Getting the accelerations
        acc_x = 0;
        acc_y = -(gravity);

        //Getting the initial velocity from the force applied by the club
        v_initial = (force * connection_time) / weight;

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
        acceleration_x.text = acc_x.ToString();
        acceleration_y.text = acc_y.ToString();

        final_position = initial_position;
        final_position.x += mid_x;  // x_distance;
        final_position.y += y_distance;
        midway_position = final_position;
    }

    public void ParabolicThrow()
    {
        Debug.Log("Parabolic throw Called!");
        pos.Clear();
        
        //Draw Curve
        lr = myLine.GetComponent<LineRenderer>();
        lr.positionCount = 20;
        lr.SetPosition(0, this.transform.position);

        t = 0;
        limit = 0;

        for (float t = 0; t <= time; t += 0.05f)
        {
            float y = (v_initial * Mathf.Sin(degree) * t) - (0.5f * gravity * t * t);
            float x = v_initial * Mathf.Cos(degree) * t;
            cur_pos = new Vector3(x, y, this.transform.position.z);
            pos.Add(cur_pos);
            limit++;
           
        }

        lr.positionCount = limit;

        for (int place = 0; place < lr.positionCount; place++)
        {
            lr.SetPosition(place, pos[place]);
        }
        limit = 0;
        takeOff = true;
    }

    private void FixedUpdate()
    {
        //Move rocket
        if (takeOff)
        {
            StartCoroutine(ThrowSteps(time / (float)(pos.Count)));
        }
    }

    IEnumerator ThrowSteps(float _time)
    {
        yield return new WaitForSeconds(_time);
        Debug.Log("Routine called " + pos.Count);
        if(pos.Count <= 0)
        {
            Debug.Log("Cancelled");

            float y = (v_initial * Mathf.Sin(degree) * time) - (0.5f * gravity * time * time);
            float x = v_initial * Mathf.Cos(degree) * time;
            final_position = new Vector3(x, y, this.transform.position.z);

            this.transform.position = Vector3.Slerp(this.transform.position, final_position, _time);
            takeOff = false;
        }
        else
        {
            Debug.Log("else");
            this.transform.position = Vector3.Slerp(this.transform.position, pos[0], _time);
            pos.RemoveAt(0);
        }
       
    }
}
