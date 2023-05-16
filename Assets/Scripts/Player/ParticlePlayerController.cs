using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ParticlePlayerController : MonoBehaviour
{
    [Header(" List of Particle System")]

    [SerializeField] private List<ParticleSystem> particleSystemsPlayer;

    private PlayerController player;

    private EnergyBar energyBar;

    private int numJumpTimes; // so pode criar particulas quando o player pula no max 2 vezes (jump e double jump)
    private bool canPlaySlideParticle = true;

    private bool canPlayGrowthEnergyParticle = true;

    private void Start()
    {
        player = PlayerController.Instance;

        energyBar = player.gameObject.GetComponent<EnergyBar>();
    }

    private void Update()
    {
        Profiler.BeginSample("PARTICULA WALK PLAYER");
        PlayerWalkParticle();
        Profiler.EndSample();
        Profiler.BeginSample("PARTICULA SLIDE PLAYER");
        PlayerWallSlideParticle();
        Profiler.EndSample();
        Profiler.BeginSample("PARTICULA JUMP PLAYER");
        PlayerJumpParticle();
        Profiler.EndSample();
        PlayerGrowthEnergyParticle();
    }

    

    public void PlayerWalkParticle()
    {
        var particleWalk = particleSystemsPlayer[0];

        if(player.IsGround)
        {
            particleWalk.maxParticles = 200;
            numJumpTimes = 0;
            return;
        }
         
        particleWalk.maxParticles = 0; 

    }

    public void PlayerJumpParticle()
    {
        var particleJump = particleSystemsPlayer[1];

        if(Input.GetKeyDown(KeyCode.W) && numJumpTimes <2)
        {
            particleJump.Play();
            numJumpTimes++;
        }
        
    }
    
    public void PlayerWallSlideParticle()
    {   
        var particleWallSlide = particleSystemsPlayer[2];
        

         if(player.IsSliding && canPlaySlideParticle)
         {
             particleWallSlide.Play();
             canPlaySlideParticle = false;
         }
        
         if(!player.IsSliding)
         {
             particleWallSlide.Stop();
             canPlaySlideParticle = true;
         }
    }

    public void PlayerGrowthEnergyParticle()
    {
        var particleGrowthEnergy = particleSystemsPlayer[3];

        if(energyBar != null)
        {
            if(EnergyBar.isShadowed == false && canPlayGrowthEnergyParticle)
            {
                particleGrowthEnergy.Play();
                canPlayGrowthEnergyParticle = false;
            }

            if(EnergyBar.isShadowed || energyBar.remainingEnergy >= EnergyBar.maxEnergy)
            {
                particleGrowthEnergy.Stop();
                canPlayGrowthEnergyParticle = true;
            }
        }

        
    }



    
}
