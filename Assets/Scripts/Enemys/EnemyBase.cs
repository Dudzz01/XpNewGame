using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]protected float speedMoviment;
    protected Vector2 posInicial;
    //protected GameObject target;

    [SerializeField]protected GameObject bulletEnemy;
    [SerializeField] protected float timeShoot;
    protected Animator enemyAnimController;

    [SerializeField]protected GameObject target;

    protected void Start()
    {
         target = GameObject.FindGameObjectWithTag("Player");
         enemyAnimController = GetComponent<Animator>();
         posInicial = transform.position;
    }
    
   

    public virtual void Move()
    {

    }

    public virtual void Shoot()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
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

        
    }

    protected void OnTriggerEnter2D(Collider2D col) 
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
    }
    
        

        
    
}
