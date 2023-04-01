using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : EnemyBase
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private LayerMask layerMaskPlayer;

    private RaycastHit2D hit;

    private int canShot;
    private void Awake()
    {
          speedMoviment = 5;
          rig.velocity = Vector2.right*speedMoviment;
          canShot = 0;
    }
    private void Update()
    {
        hit = Physics2D.Raycast(transform.position,-Vector2.up,1000,layerMaskPlayer); // raycast hit que coleta as informacoes do objeto no qual está colidindo com o raio

        Move();

        if(canShot == 0)
        {
            Shoot();
            
        }

       
    }


    public override void Move()
    {
         // Movimentacao Horizontal
         if(transform.position.x < -4.5)
         {
             rig.velocity = new Vector2(speedMoviment,0);
         }
         if(transform.position.x > 4.5)
         {
            rig.velocity = new Vector2(-speedMoviment,0);
         }
         
         
         if(hit.collider == null)
         {
            canShot = 0;
         }
         

    }

    public override void Shoot()
    {
        // Verificacao de mira travada no player ao se movimentar

         if(hit.collider != null)
         {
            if(hit.rigidbody.name == "Player")
            {
                GameObject enemyP = Instantiate(bulletEnemy,transform.position,Quaternion.identity);
                enemyP.GetComponent<EnemyBullet>().dirBullet = -Vector2.up;
                canShot = 1;
                return;
            }
            
         }
         
            
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position,-Vector2.up);
    }
}
