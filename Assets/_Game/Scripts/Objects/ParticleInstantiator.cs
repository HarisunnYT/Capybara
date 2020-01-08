using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstantiator : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] particles;

    public void PlayParticle(int index)
    {
        GameObject particle = ObjectPooler.GetPooledObject(particles[index].gameObject);
        particle.transform.rotation = Quaternion.identity;
        particle.transform.position = transform.position;
    }
}
