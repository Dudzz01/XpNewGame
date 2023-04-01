using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoo : EnemyBase
{
    
    private void Awake()
    {
        speedMoviment = 5*Time.deltaTime;
        bulletEnemy = null;
    }

    private void Update()
    {
        Move();
    }

    public override void Move()
    {

        float posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position,transform.position);

        if(posBeetweenEnemyToPlayer < 8 && EnergyBar.isShadowed == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position,target.transform.position,speedMoviment);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
}
