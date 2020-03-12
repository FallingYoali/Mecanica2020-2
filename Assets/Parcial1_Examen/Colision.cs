using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    public Transform Sonic;
    public Transform Cartero;
    Vector3 SonicStartPos;
    Vector3 CarteroStartPos;

    public TMPro.TMP_InputField Sonic_vel_text;
    public TMPro.TMP_InputField Cartero_vel_text;
    public TMPro.TMP_Text time_text;
    public TMPro.TMP_Text SonicDis_text;
    public TMPro.TMP_Text CarteroDis_text;

    float SonicVel;
    float CarteroVel;
    float SonicDis;
    float CarteroDis;
    float distance;
    float time;
    float meeting_pos;
    float offset;

    bool Move = false;

    void Start()
    {
        distance = 500f;
        SonicStartPos = Sonic.position;
        CarteroStartPos = Cartero.position;

        //half the width of Sonic and the Cartero
        offset = 5f;
    }

    private void Update()
    {
        //will only start once button is activated
        if (Move)
        {
            if(Sonic.position.x <= meeting_pos - offset)
                Sonic.Translate(Vector3.right * Time.deltaTime * SonicVel);
            if(Cartero.position.x >= meeting_pos + offset)
                Cartero.Translate(Vector3.left * Time.deltaTime * CarteroVel);
        }
    }

    public void SonicAndCarteroClash()
    {
        Move = false;
        Sonic.position = SonicStartPos;
        Cartero.position = CarteroStartPos;

        //Math
        SonicVel = float.Parse(Sonic_vel_text.text);
        CarteroVel = float.Parse(Cartero_vel_text.text);

        time = distance / (SonicVel + CarteroVel);

        //v = d/t
        SonicDis = SonicVel * time;
        CarteroDis = CarteroVel * time;
        meeting_pos = SonicStartPos.x + SonicDis;

        //Show results
        time_text.text = time.ToString();
        SonicDis_text.text = SonicDis.ToString();
        CarteroDis_text.text = CarteroDis.ToString();

        
        //Start Action!
        Move = true;

    }

}
