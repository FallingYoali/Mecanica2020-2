using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinBall_Move : MonoBehaviour
{
    private Slider distance;
    public TMPro.TMP_Text distanceText;
    public TMPro.TMP_Text Pot_Energy;
    public Transform spring;
    public float mass;
    private float k, g, potEnergy;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.005f);
    private bool releaseString;


    void Start()
    {
        g = 9.8f;
        k = 3.0f;

        releaseString = false;
    }

    public void CalculatePotEnergy(Slider _distance)
    {
        distance = _distance;
        potEnergy = 0.5f * k * Mathf.Pow(_distance.value, 2);
        Pot_Energy.text = potEnergy.ToString();

        float scalePer = 1.0f - (_distance.value / _distance.maxValue);
        Vector3 scale = new Vector3(1, scalePer, 1);
        spring.localScale = scale;

        distanceText.text = _distance.value.ToString();
    }



    IEnumerator SpringBack(Slider _distance)
    {
        while (_distance.value > 0) {
            yield return waitForSeconds;
            Debug.Log("ello");
            _distance.value--;
            float scalePer = 1.0f - (_distance.value / _distance.maxValue);
            Vector3 scale = new Vector3(1, scalePer, 1);
            spring.localScale = scale;
        }

        _distance.value = 0;
        releaseString = false;
        Debug.Log("Done!");
        StopAllCoroutines();

    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(SpringBack(distance));
        }
        if (releaseString)
        {

        }
    }
}
