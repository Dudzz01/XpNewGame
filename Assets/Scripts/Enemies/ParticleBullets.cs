using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBullets : MonoBehaviour
{
    [SerializeField]private List<ParticleSystem> particlesBullets;


    public void ShootParticleEnemy()
    {
        var particleShoot = particlesBullets[0];

        particleShoot.Play();
            
    }

    
}
