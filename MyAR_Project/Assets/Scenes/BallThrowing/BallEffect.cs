using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallEffect : MonoBehaviour
{
    public ParticleSystem particle;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player")) return;
        Instantiate(particle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
