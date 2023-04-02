using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDinamic : EnemyBase
{
    private float posBeetweenEnemyToPlayer;
    private float posYEnemy;
    [SerializeField] private SpriteRenderer enemyDinamicSprite;
    private int dirMiraEnemy;

    [SerializeField] private LayerMask layerMaskPlayer;
    private RaycastHit2D hit;
    private int canShot;

    private Vector2 dirBullet;
    private void Awake()
    {
        speedMoviment = 4*Time.deltaTime;
        
    }

    private void Update()
    {
        posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position,transform.position);

        dirBullet = target.transform.position - transform.position;
        dirBullet.y = 0;
        hit = Physics2D.Raycast(transform.position,dirBullet,8,layerMaskPlayer);
        
        
        if(canShot == 0)
        {
            Shoot();  
        }
           
       
       
       Move();
       
       
       
    }

    public override void Move()
    {
        posYEnemy = posInicial.y;

        if(posBeetweenEnemyToPlayer < 8 && EnergyBar.isShadowed == false && hit.collider != null)
        {
            if(hit.rigidbody.name == "Player")
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,posYEnemy),new Vector2(target.transform.position.x,posYEnemy),speedMoviment);
            }
            
            
        }
        
        if(dirBullet.x >= Vector2.right.x)
        {
            enemyDinamicSprite.flipX = false;
        }
        else
        {
             enemyDinamicSprite.flipX = true;
        }

         if(hit.collider == null || hit.collider != null && hit.rigidbody.name != "Player")
         {
            // Debug.Log("Pronto para atirar");
            enemyAnimController.SetInteger("CondicaoEnemyMove",1);
         }
         

    }

    public override void Shoot()
    {

        if(hit.collider != null)
        {
            if(hit.rigidbody.name == "Player" && EnergyBar.isShadowed == false)
            {
                enemyAnimController.SetInteger("CondicaoEnemyMove",2); // animacao de tiro
                GameObject bulletEm = Instantiate(bulletEnemy,transform.position,Quaternion.identity);
                bulletEm.GetComponent<EnemyBullet>().dirBullet = dirBullet.normalized;
                canShot = 1;
                
                StartCoroutine(timeReloadShoot()); 
            }

        }
       
    }

    IEnumerator timeReloadShoot()
    {
        yield return new WaitForSeconds(0.5f);
        enemyAnimController.SetInteger("CondicaoEnemyMove",1); // animacao de walk
        yield return new WaitForSeconds(2f);
        if(canShot == 1)
        {
            canShot = 0;
        }
        yield return null;
    }
    
    private void OnDrawGizmos()
    {
        Vector2 dirBullet = target.transform.position - transform.position;
        dirBullet.y = 0;
        //Gizmos.DrawLine(transform.position, target.transform.position);
        Gizmos.DrawRay(transform.position,dirBullet*8);
    }
}