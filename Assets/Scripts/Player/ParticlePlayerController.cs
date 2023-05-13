using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayerController : MonoBehaviour
{
    [Header(" List of Particle System")]

    [SerializeField] private List<ParticleSystem> particleSystemsPlayer;

    private PlayerController player;

    private int numJumpTimes; // so pode criar particulas quando o player pula no max 2 vezes (jump e double jump)

    private void Start()
    {
        player = PlayerController.Instance;
    }

    private void Update()
    {
        PlayerWalkParticle();
        PlayerJumpParticle();
    }

    public void PlayerWalkParticle()
    {
        var particleWalk = particleSystemsPlayer[0];

        if(player.IsGround)
        {
            particleWalk.maxParticles = 200;
            //Debug.Log("Particula Walk Ativa");
            numJumpTimes = 0;
            return;
        }

            particleWalk.maxParticles = 0;
        
    }

    public void PlayerJumpParticle()
    {
        var particleJump = particleSystemsPlayer[1];

        Debug.Log("Variavel Jump: " + player.Jump);

        if(Input.GetKeyDown(KeyCode.W) && numJumpTimes <2)
        {
            particleJump.Play();
            numJumpTimes++;
        }


        
    }



    
}
