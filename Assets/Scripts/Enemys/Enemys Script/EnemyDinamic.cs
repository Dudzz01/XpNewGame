using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDinamic : EnemyBase
{
    private float posBeetweenEnemyToPlayer;
    
    
    private void Update()
    {
        posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position,transform.position);

        timeShoot+=Time.deltaTime;

        if(timeShoot >2)
        {
             Shoot();
             timeShoot = 0;
        }
       
       Move();
    }

    public override void Move()
    {
        

        if(posBeetweenEnemyToPlayer < 8)
        {
            transform.position = Vector2.MoveTowards(this.transform.position,target.transform.position,5*Time.deltaTime);
        }
    }

    public override void Shoot()
    {
        float posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position,transform.position);
        
        Vector2 dirBullet = target.transform.position - transform.position;

        if(posBeetweenEnemyToPlayer < 8)
        {
            GameObject bulletEm = Instantiate(bulletEnemy,transform.position,Quaternion.identity);
            bulletEm.GetComponent<EnemyBullet>().dirBullet = dirBullet.normalized;
        }

        Debug.Log("Inimigo atirou! Distancia entre player e inimigo" + posBeetweenEnemyToPlayer);
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,target.transform.position);
    }
}
