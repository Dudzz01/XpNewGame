using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticScript : EnemyBase
{
    private void Update()
    {
        timeShoot += Time.deltaTime;

        if(timeShoot >2)
        {
            Shoot();
            timeShoot = 0;
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

       // Debug.Log("Inimigo atirou! Distancia entre player e inimigo" + posBeetweenEnemyToPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,target.transform.position);
    }
}