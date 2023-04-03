using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : EnemyBase
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private LayerMask layerMaskPlayer;

    private RaycastHit2D hit;

    private int canShot;

    private bool canAnimWalk;
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
         
             rig.velocity = new Vector2(speedMoviment,0);
         
         
         
         if((hit.collider == null || hit.collider != null && hit.rigidbody.name != "Player"))
         {
            // Debug.Log("Pronto para atirar");
            if(canAnimWalk == true)
            {
                enemyAnimController.SetInteger("CondicaoDroneAnim",1);
            }
            
         }
         

    }

    

    

    public override void Shoot()
    {
        // Verificacao de mira travada no player ao se movimentar

         if(hit.collider != null)
         {
            if(hit.rigidbody.name == "Player" && EnergyBar.isShadowed == false)
            {
                enemyAnimController.SetInteger("CondicaoDroneAnim",2);
                canAnimWalk = false;
                canShot = 1;
                StartCoroutine(timeReloadShoot()); 
                return;
            }
            Debug.Log("Name colision vilon: " + hit.rigidbody.name);
         }
         
        
        
    }

    IEnumerator timeReloadShoot()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject enemyP = Instantiate(bulletEnemy,transform.position,Quaternion.identity);
        enemyP.GetComponent<EnemyBullet>().dirBullet = -Vector2.up;
        canAnimWalk = true;
        yield return new WaitForSeconds(0.1f);
        if(canShot == 1)
        {
            canShot = 0;
        }
        yield return null;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(EnergyBar.isShadowed == false)
            {
                //Mata o player
                PlayerController.playerIsAlive = false;
                Debug.Log("Matou o PLAYER");
            }
        }

        if(col.gameObject.tag == "Tilemap")
        {
            speedMoviment*=-1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position,-Vector2.up);
    }
}
