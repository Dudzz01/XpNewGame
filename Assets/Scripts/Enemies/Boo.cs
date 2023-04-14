using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boo : Enemy
{
    [Header("Properties")]
    private float posBeetweenEnemyToPlayer;
    
    private void Awake()
    {
        movementSpeed = 10;
    }

    private void Update()
    {
        posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position, transform.position);

        timeShoot += Time.deltaTime;

        if(timeShoot >2)
        {
            Shoot();
            timeShoot = 0;
        }
       
       SetAnimationConfig();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void SetAnimationConfig()
    {
        if(posBeetweenEnemyToPlayer < 7f )
        {
            enemyAnimController.SetInteger("CondicaoBooAnim", 1);
        }

        if(posBeetweenEnemyToPlayer >= 7f)
        {
            enemyAnimController.SetInteger("CondicaoBooAnim", 2);
        }
    }

    public override void Move()
    {
        if(posBeetweenEnemyToPlayer < 7f )
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, movementSpeed * 0.01f);
        }
    }

    public override void Shoot()
    {
        float posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position, transform.position);
        
        Vector2 dirBullet = target.transform.position - transform.position;

        if(posBeetweenEnemyToPlayer < 8 )
        {
            GameObject bulletEm = bulletPoolEnemy.GetComponent<ObjectPooling>().GetBulletInPool();
            bulletEm.transform.position = transform.position;
            bulletEm.transform.rotation = Quaternion.identity;
            bulletEm.GetComponent<EnemyBullet>().dirBullet = dirBullet.normalized;
        }

       // Debug.Log("Inimigo atirou! Distancia entre player e inimigo" + posBeetweenEnemyToPlayer);
    }
    
    private void OnDrawGizmos()
    {
        if(target != null)
            Gizmos.DrawLine(transform.position, target.transform.position);
    }
}