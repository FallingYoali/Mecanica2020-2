using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using mathf

public class Parabolic : MonoBehaviour
{
    float v_initial;
    float degree;
    float x_distance;
    float time;
    float y_distance;
    float gravity;


    public TMPro.TMP_InputField v_initial1;
    public TMPro.TMP_InputField degree1;
    public TMPro.TMP_Text x_distance1;
    public TMPro.TMP_Text time1;
    public TMPro.TMP_Text y_distance1;

    public void ParabolicThrow()
    {
        v_initial = float.Parse(v_initial1.text);
        degree = float.Parse(degree1.text);

        //Change degrees to radians
        degree = Mathf.Deg2Rad * degree;

        //time at highest peak in y
        time = (v_initial * Mathf.Sin(degree)) / (gravity);

        //How high does it go?
        y_distance = (v_initial * Mathf.Sin(degree) * time) - (0.5f * gravity * time * time);

        //Time where the rocket connects to the ground and its y velocity equals 0
        time = (v_initial * Mathf.Sin(degree)) / (0.5f * gravity);

        //distance travelled
        x_distance = v_initial * Mathf.Cos(degree) * time;


        x_distance1.text = x_distance.ToString();
        time1.text = time.ToString();
        y_distance1.text = y_distance.ToString();
}

    void Start()
    {
        gravity = 9.8f;
        //y = v_initial + v_initial * sin(degree) * time - 1/2 * gravity * time to the power of two


    }


}
