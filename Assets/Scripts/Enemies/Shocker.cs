using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocker : Enemy
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private LayerMask layerMaskPlayer;

    [Header("Properties")]
    [SerializeField] private int canShot;
    [SerializeField] private bool canAnimWalk;

    [Header("Script Exclusive")]
    [HideInInspector] private RaycastHit2D hit;

    private void Awake()
    {
        movementSpeed = 3;
        canShot = 0;
    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, -Vector2.up, 1000, layerMaskPlayer); // raycast hit que coleta as informacoes do objeto no qual está colidindo com o raio

        Move();

        if(canShot == 0)
        {
            Shoot();
        }

        SetAnimationConfig();
    }

    void FixedUpdate()
    {
        Move();
    }

    public override void SetAnimationConfig()
    {
        if((hit.collider == null || hit.collider != null && hit.rigidbody.name != "Player"))
        {
            if(canAnimWalk == true)
            {
                enemyAnimController.SetInteger("CondicaoDroneAnim", 1);
            }
        }
    }

    public override void Move()
    {
        // Movimentacao Horizontal
        rig.velocity = new Vector2(movementSpeed, 0);
    }

    public override void Shoot()
    {
        // Verificacao de mira travada no player ao se movimentar

        if(hit.collider != null)
        {
            if(hit.rigidbody.name == "Player" && EnergyBar.isShadowed == false)
            {
               
                enemyAnimController.SetInteger("CondicaoDroneAnim", 2);
                canAnimWalk = false;
                canShot = 1;
                StartCoroutine(timeReloadShoot()); 
                return;
            }
               
            //Debug.Log("Name colision vilon: " + hit.rigidbody.name);
        }
    }

    IEnumerator timeReloadShoot()
    {
        yield return new WaitForSeconds(0.2f);

        GameObject enemyP = bulletPoolEnemy.GetComponent<ObjectPooling>().GetBulletInPool();
        enemyP.transform.position = transform.position;
        enemyP.transform.rotation = Quaternion.identity;
        enemyP.GetComponent<EnemyBullet>().dirBullet = -Vector2.up;
        canAnimWalk = true;

        yield return new WaitForSeconds(0.2f);
        
        if(canShot == 1)
        {
            canShot = 0;
        }
        yield return null;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        print(col.gameObject.tag);

        if(col.gameObject.tag == "Player")
        {
            if(EnergyBar.isShadowed == false)
            {
                //Mata o player
                PlayerController.playerIsAlive = false;
                //Debug.Log("Matou o PLAYER");
            }
        }
        else if(col.gameObject.tag == "Tilemap")
        {
            movementSpeed *= -1f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -Vector2.up);
    }
}