using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWithCharge : MonoBehaviour
{
    [SerializeField]
    public float charge = 1;
    private Color color;

    private void Start()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        color = charge > 0 ? Color.red : Color.blue;
        GetComponent<Renderer>().material.color = color;
    }
}
