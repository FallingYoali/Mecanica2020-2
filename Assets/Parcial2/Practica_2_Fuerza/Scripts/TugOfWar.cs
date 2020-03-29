using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWar : MonoBehaviour
{
    int girl_Force;
    int boy_Force;

    public TMPro.TMP_Text girlForce;
    public TMPro.TMP_Text boyForce;

    void Start()
    {
        girl_Force = 0;
        boy_Force = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Pressed!" + transform.position.x + " vs " + Input.mousePosition.x);
            if(Input.mousePosition.x < 278)
            {
                girl_Force++;
                girlForce.text = girl_Force.ToString();
                this.transform.GetChild(0).transform.Translate(Vector3.left * 3 * Time.deltaTime);
                Debug.Log("Girl!");
            }
            else if (Input.mousePosition.x >= 278)
            {
                boy_Force++;
                boyForce.text = boy_Force.ToString();
                this.transform.GetChild(0).transform.Translate(Vector3.right * 5 * Time.deltaTime);
                Debug.Log("Boy!");
            }  
        }
    }
}
