using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    List<ParticleWithCharge> particlesWithCharge;
    List<MovableParticle> movableParticles;
    [SerializeField]
    float cycleInterval = 0.01f;
    public float ForceMultiplier = 100;

    private void Start()
    {
        GetParticles();
        foreach(MovableParticle movingParticle in movableParticles)
        {
            StartCoroutine(Cycle(movingParticle));
        }

    }

    public IEnumerator Cycle(MovableParticle _movingParticle)
    {
        bool isFirst = true;
        while (true)
        {
            if (isFirst)
            {
                isFirst = false;
                yield return new WaitForSeconds(Random.Range(0, cycleInterval));
            }
            ApplyForce(_movingParticle);
            yield return new WaitForSeconds(cycleInterval);
        }
    }

    private void ApplyForce(MovableParticle _movingParticle)
    {
        Vector3 newForce = Vector3.zero;
        foreach(ParticleWithCharge particle in particlesWithCharge)
        {
            if (particle == _movingParticle)
                continue;
            float distance = Vector3.Distance(_movingParticle.transform.position, particle.transform.position);

            if(distance == 0)
                continue;

            float force = (ForceMultiplier * _movingParticle.charge * particle.charge) / Mathf.Pow(distance, 2);
            Vector3 direction = _movingParticle.transform.position - particle.transform.position;
            direction.Normalize();

            newForce = force * direction * cycleInterval;

            _movingParticle.rb.AddForce(newForce);
        }
    }

    private void GetParticles()
    {
        particlesWithCharge = new List<ParticleWithCharge>();
        movableParticles = new List<MovableParticle>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<ParticleWithCharge>() != null)
            {
                particlesWithCharge.Add(transform.GetChild(i).GetComponent<ParticleWithCharge>());
            }
            if (transform.GetChild(i).GetComponent<MovableParticle>() != null)
            {
                movableParticles.Add(transform.GetChild(i).GetComponent<MovableParticle>());
            }
        }
    }
}
