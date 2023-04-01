using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
     public Vector2 dirBullet{get; set;}

     private const float speedBullet = 8;

    // Update is called once per frame
    void Update()
    {
        rig.velocity = dirBullet*speedBullet;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Tilemap")
        {
            Destroy(gameObject);
        }

          if(col.gameObject.tag == "Player")
          {
              Destroy(gameObject);

              if(EnergyBar.isShadowed == false)
              {
                 
                  //mata o player
                  PlayerController.playerIsAlive = false;
               }

              if(EnergyBar.isShadowed == true)
              {
                  // nao mata o player, mas diminui a capacidade maxima de energia
                  EnergyBar.maxEnergy -= 0.1f;
              }
          }
    }
}
