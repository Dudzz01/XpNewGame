using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    [Header("References")]
    [SerializeField] private SpriteRenderer tankSprite;
    [SerializeField] private LayerMask layerMaskPlayer;
    
    private float posBeetweenEnemyToPlayer;
    private float posYEnemy;
    private int dirMiraEnemy;
    private int canShot;

    private RaycastHit2D hit;
    private Vector2 dirBullet;

    private void Awake()
    {
        movementSpeed = 10;
    }

    private void Update()
    {
        posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position, transform.position);

        dirBullet = target.transform.position - transform.position;
        dirBullet.y = 0;
        hit = Physics2D.Raycast(transform.position,dirBullet, 8, layerMaskPlayer);
        
        if(canShot == 0)
        {
            Shoot();  
        }
           
       SetAnimationConfig();  
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        posYEnemy = posInicial.y;

        if(posBeetweenEnemyToPlayer < 8  && hit.collider != null)
        {
            if(hit.rigidbody.name == "Player")
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, posYEnemy), new Vector2(target.transform.position.x, posYEnemy), movementSpeed * 0.01f);
            }
        }
    }

    public override void SetAnimationConfig()
    {
        if(dirBullet.x >= Vector2.right.x)
        {
            tankSprite.flipX = false;
        }
        else
        {
            tankSprite.flipX = true;
        }

        if(hit.collider == null || hit.collider != null && hit.rigidbody.name != "Player")
        {
            // Debug.Log("Pronto para atirar");
            enemyAnimController.SetInteger("CondicaoEnemyMove", 1);
        }
    }

    public override void Shoot()
    {
        if(hit.collider != null)
        {
            if(hit.rigidbody.name == "Player" && EnergyBar.isShadowed == false)
            {
                enemyAnimController.SetInteger("CondicaoEnemyMove", 2); // animacao de tiro
                GameObject bulletEm = Instantiate(bulletEnemy, transform.position, Quaternion.identity);
                bulletEm.GetComponent<EnemyBullet>().dirBullet = dirBullet.normalized;
                canShot = 1;
                
                StartCoroutine(timeReloadShoot()); 
            }
        }
    }

    IEnumerator timeReloadShoot()
    {
        yield return new WaitForSeconds(0.5f);
        enemyAnimController.SetInteger("CondicaoEnemyMove", 1); // animacao de walk
        yield return new WaitForSeconds(2f);

        if(canShot == 1)
        {
            canShot = 0;
        }

        yield return null;
    }
    
    private void OnDrawGizmos()
    {
        Vector2 dirBullet = target.transform.position - transform.position;
        dirBullet.y = 0;

        if(target != null)
            Gizmos.DrawRay(transform.position, dirBullet * 8);
    }
}