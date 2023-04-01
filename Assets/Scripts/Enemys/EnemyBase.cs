using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]protected float speedMoviment;

    protected GameObject target;

    [SerializeField]protected GameObject bulletEnemy;
    [SerializeField] protected float timeShoot;


    protected void Start()
    {
         target = GameObject.FindGameObjectWithTag("Player");
    }
    
   

    public virtual void Move()
    {

    }

    public virtual void Shoot()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(EnergyBar.isShadowed == false)
            {
                //Mata o player
            }
        }
    }
}
