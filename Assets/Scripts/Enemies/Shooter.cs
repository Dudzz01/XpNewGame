using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    private void Update()
    {
            
            Shoot();

        transform.right = target.transform.position - transform.position;
        transform.position = posInicial;
    }

    public override void Shoot()
    {
        float posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position, transform.position);

        timeShoot += Time.deltaTime;

        if(posBeetweenEnemyToPlayer < 8 && EnergyBar.isShadowed == false && timeShoot > 1.5)
        {
            // GameObject bulletEm = Instantiate(bulletEnemy, transform.position, Quaternion.identity);
           
            GameObject bulletEm = bulletPoolEnemy.GetComponent<ObjectPooling>().GetBulletInPool();
            bulletEm.transform.position = transform.position;
            bulletEm.transform.rotation = Quaternion.identity;
            Vector2 dirBullet = target.transform.position - transform.position;
            bulletEm.GetComponent<EnemyBullet>().dirBullet = dirBullet.normalized;
            timeShoot = 0;
            
        }

           
           

       // Debug.Log("Inimigo atirou! Distancia entre player e inimigo" + posBeetweenEnemyToPlayer);
    }

    private void OnDrawGizmos()
    {
        if(target != null)
            Gizmos.DrawLine(transform.position,target.transform.position);
    }
}