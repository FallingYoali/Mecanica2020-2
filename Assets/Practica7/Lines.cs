using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{

    //Line
    GameObject myLine;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        //Starting Line
        myLine = new GameObject();
        myLine.transform.position = new Vector3(0,0,0);
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        Color color = new Color(0, 0, 0, 1);
        lr.startColor = color;
        lr.endColor = color;
        lr.endWidth = 0.1f;
        lr.startWidth = 0.1f;
        //lr.SetPosition(0, initial_position);
        //lr.SetPosition(1, initial_position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
