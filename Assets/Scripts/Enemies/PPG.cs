using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 
public class PPG : Enemy
{
    [Header("References")]

    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private ParticleSystem particlePPG; // particula de autodestruicao
    [SerializeField] private SpriteRenderer spriteRendererPPG;
    private Color colorDamagePPG = new Color(1,0,0,0);

    [Header("Properties")]

    private bool canAutoDestroy;

    private bool canGrowthAlpha = true;

    private float posBeetweenEnemyToPlayer;

    public short DirWalk{get; set;}

    private RaycastHit2D hit;

    private float timeAlphaShaderDamage = 1.6f;
    
    private void Awake()
    {
        movementSpeed = 5;
        DirWalk = 1;
    }


    void Update()
    {
        posBeetweenEnemyToPlayer = Vector2.Distance(target.transform.position, transform.position);

        
        Move();
        
        if(canAutoDestroy)
        {
            ActiveShaderDamagePPG();
        }
        
    }

    public override void Move()
    {
        if(posBeetweenEnemyToPlayer < 6f)
        {
            rig.velocity = Vector2.Lerp(rig.velocity,new Vector2(0,0),0.006f);
            StartCoroutine(AutoDestroyEnemy());
            return;
        }

        if(canAutoDestroy == false)
        {
            rig.velocity = new Vector2(movementSpeed*DirWalk,rig.velocity.y);
        }
    }


    public void ActiveShaderDamagePPG()
    {
        
        #region loweringAlphaShader
        if(colorDamagePPG.a > 0 && canGrowthAlpha == false)
        {
            colorDamagePPG.a = Mathf.Clamp01(colorDamagePPG.a - timeAlphaShaderDamage * Time.deltaTime);
            spriteRendererPPG.material.SetColor("_Color",colorDamagePPG);

            if(colorDamagePPG.a == 0)
            {
                canGrowthAlpha = true;
            }
            
        }
        #endregion
        #region growthingAlphaShader
        else if(colorDamagePPG.a < 1 && canGrowthAlpha)
        {
            colorDamagePPG.a = Mathf.Clamp01(colorDamagePPG.a + timeAlphaShaderDamage * Time.deltaTime);
            spriteRendererPPG.material.SetColor("_Color",colorDamagePPG);

            if(colorDamagePPG.a == 1)
            {
                canGrowthAlpha = false;
            }
           
        }
        #endregion

        Debug.Log(canGrowthAlpha);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,target.transform.position);
    }

    IEnumerator AutoDestroyEnemy()
    {
        canAutoDestroy = true;
        yield return new WaitForSeconds(3f);
        particlePPG.Play();
        spriteRendererPPG.DOFade(0,0.4f);
        yield return new WaitForSeconds(particlePPG.startLifetime);
        particlePPG.Stop();
        gameObject.SetActive(false);
        // Explode o inimigo e cria uma área circular que caso o player colida, ele morre
    }
}
