using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    public float F1 = 0;
    public float F2 = 0;
    public float D1 = 0;
    public float D2 = 0;

    float answer;
    string type_answer;

    /*public TMPro.TMP_InputField Monita;
    public TMPro.TMP_InputField Atinom1;
    public TMPro.TMP_InputField distance;*/

    public Rigidbody Monita;
    public Rigidbody Atinom;


    void Start()
    {
        if (F1 == 0)
        {
            F1 = (F2 * D2) / D1;
            type_answer = "Monita weighs: ";
            answer = F1;
        }
        else if (F2 == 0)
        {
            F2 = (F1 * D1) / D2;
            type_answer = "Atinom weighs: ";
            answer = F2;
        }
        else if (D1 == 0)
        {
            D1 = (F2 * D2) / F1;
            type_answer = "Monita travels: ";
            answer = D1;
        }
        else if (D2 == 0)
        {
            D2 = (F1 * D1) / F2;
            type_answer = "Atinom travels: ";
            answer = D2;
        }
        else
        {
            print("Nothing to calculate.");
            return;
        }

        print(type_answer + answer);

        Monita.mass = F1;
        Atinom.mass = F2;



    }

    public void Calculate()
    {
        /*float F1 = Monita.GetComponent<Text>();
        float F2 = Atinom.GetComponent<Text>();
        float D1 = distance.GetComponent<Text>();

       if(F1 != 0 || F2 != 0 || D1 != 0)
       {

       }*/
    }

}
