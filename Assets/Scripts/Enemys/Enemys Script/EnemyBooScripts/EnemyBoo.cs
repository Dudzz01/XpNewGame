using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoo : EnemyBase
{
    
    private float posBeetweenEnemyToPlayer;
    
    private void Awake()
    {
        speedMoviment = 4*Time.deltaTime;
    }

    private void Update()
    {
        posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position,transform.position);

        timeShoot += Time.deltaTime;

        if(timeShoot >2)
        {
            Shoot();
            timeShoot = 0;
        }
       
       Move();
    }

    public override void Move()
    {
        if(posBeetweenEnemyToPlayer < 8 && EnergyBar.isShadowed == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position,target.transform.position,speedMoviment);
            enemyAnimController.SetInteger("CondicaoBooAnim",1);
        }

        if(posBeetweenEnemyToPlayer >= 8)
        {
            enemyAnimController.SetInteger("CondicaoBooAnim",2);
        }
    }

    public override void Shoot()
    {
        float posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position,transform.position);
        
        Vector2 dirBullet = target.transform.position - transform.position;

        if(posBeetweenEnemyToPlayer < 8 && EnergyBar.isShadowed == false)
        {
            GameObject bulletEm = Instantiate(bulletEnemy,transform.position,Quaternion.identity);
            bulletEm.GetComponent<EnemyBullet>().dirBullet = dirBullet.normalized;
        }

       // Debug.Log("Inimigo atirou! Distancia entre player e inimigo" + posBeetweenEnemyToPlayer);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
}
