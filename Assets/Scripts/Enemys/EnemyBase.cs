using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
public abstract class EnemyBase : MonoBehaviour
{
    protected float speedMoviment;
    protected Vector2 posInicial;
    //protected GameObject target;

    [SerializeField]protected GameObject bulletEnemy;
    [SerializeField] protected float timeShoot;
    protected Animator enemyAnimController;

    protected GameObject target;


    
    protected void Start()
    {
         Profiler.BeginSample("TARGET ENEMY");
         target = PlayerController.Instance.gameObject;
         Profiler.EndSample();
         enemyAnimController = GetComponent<Animator>();
         posInicial = transform.position;
    }
    
   

    public virtual void Move()
    {

    }

    public virtual void Shoot()
    {
        
    }

    public virtual void SetAnimationConfig()
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
