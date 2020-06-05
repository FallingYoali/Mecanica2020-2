using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableParticle : ParticleWithCharge
{

    [SerializeField]
    private float mass = 1;

    public Rigidbody rb;

    private void Start()
    {
        UpdateColor();
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = mass;
        rb.useGravity = false;
    }

}
