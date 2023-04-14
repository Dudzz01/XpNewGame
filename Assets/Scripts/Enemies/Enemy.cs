using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public abstract class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject bulletEnemy;
    [SerializeField] protected GameObject bulletPoolEnemy;
    [SerializeField] protected GameObject target;
    [SerializeField] protected Animator enemyAnimController;
    
    [Header("Properties")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float timeShoot;
    [Space(10)]
    [SerializeField] protected Vector2 posInicial;

    void Start()
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
                //Debug.Log("Matou o PLAYER");
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
                //Debug.Log("Matou o PLAYER");
            }
        }
    }
}