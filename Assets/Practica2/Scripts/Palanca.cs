using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Palanca : MonoBehaviour
{
    //Variables
    public float F1 = 0;
    public float F2 = 0;
    public float D1 = 0;
    public float D2 = 0;

    float answer;
    string type_answer;

    public TMPro.TMP_InputField InputField_Monita;
    public TMPro.TMP_InputField InputField_Atinom;
    public TMPro.TMP_InputField InputField_Distance;

    public Rigidbody Monita;
    public Rigidbody Atinom;

    //Used to reset the stage
    Vector3 Palanca_initialPos;
    Quaternion Palanca_initRot;


    void Start()
    {
        //Save Palanca's initial position
        Palanca_initialPos = this.transform.position;
        Palanca_initRot = this.transform.rotation;

        /*
        //Depending on what value is not given, the program will calculate the missing value.
        // Formula: F1 * D1 = F2 * D2
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

        //Show answer in Console
        print(type_answer + answer);
        */
    }

    //Called when the button Update is pressed during gameplay
    public void Calculate()
    {
        //Reset Stage
        this.transform.position = Palanca_initialPos;
        this.transform.rotation = Palanca_initRot;        

        //Check to see everything has been filled out:
        if (string.IsNullOrEmpty(InputField_Monita.text) || 
            string.IsNullOrEmpty(InputField_Atinom.text) || 
            string.IsNullOrEmpty(InputField_Distance.text))
        {
            print("You need to fill out all the empty spots.");
        }
        //If everything has been filled out:
        else
        {
            //Turn the text into floats for equation
            F1 = float.Parse(InputField_Monita.text);
            F2 = float.Parse(InputField_Atinom.text);
            D1 = float.Parse(InputField_Distance.text);

            //This is the result
            float D2;

            //Check that nothing has the value of zero
            if (F1 != 0 && F2 != 0 && D1 != 0)
            {
                //Find the answer
                D2 = (F1 * D1) / F2;
                print("The distance traveled is: " + D2);

                //Change the mass of the avatars to represent their weight.
                Monita.mass = F1;
                Atinom.mass = F2;
            }
            else print("You cannot set any number to 0.");
        }
    }
}
