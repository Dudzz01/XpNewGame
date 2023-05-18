using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] protected GameObject bulletPoolEnemy;
     public Vector2 dirBullet{get; set;}
     
     [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip[] soundClipBullet = new AudioClip[1];
     private const float speedBullet = 8;
     private int nume;

    
    void Start()
    {
        bulletPoolEnemy = ObjectPooling.Instance.gameObject;
        soundSource.clip = soundClipBullet[0];
        soundSource.volume = 0.05f;
        soundSource.Play();
    }
    void Update()
    {
        rig.velocity = dirBullet*speedBullet;
    }

    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Tilemap")
        {
            bulletPoolEnemy.GetComponent<ObjectPooling>().SetBulletInPool(gameObject);
        }

          if(col.gameObject.tag == "Player")
          {
              bulletPoolEnemy.GetComponent<ObjectPooling>().SetBulletInPool(gameObject);

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
