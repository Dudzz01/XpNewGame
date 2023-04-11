using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask rayMaskGround;

    [Header("Properties")]
    [SerializeField] float rayDistance;
    [SerializeField] public static bool isShadowed;

    [Header("Energy Bar")]
    [SerializeField] internal float remainingEnergy;
    [SerializeField] public static float maxEnergy;
    [SerializeField] Image remainingEnergyImage;

    void Start()
    {
        InitEnergy();
        maxEnergy = remainingEnergy;
    }

    void Update()
    {
        StartCoroutine(DecreaseEnergy());
        EnergyDisplay();
    }

    void FixedUpdate()
    {
        SetHeadRaycast();
    }

    void InitEnergy()
    {
        remainingEnergy = 1f;
    }

    IEnumerator DecreaseEnergy()
    {
        if(isShadowed)
        {
            remainingEnergy -= 0.2f * Time.deltaTime;
            yield return new WaitForSeconds(1f);
        }
        else
        {
            if(remainingEnergy != maxEnergy)
            {
                remainingEnergy += 0.2f * Time.deltaTime;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    void EnergyDisplay()
    {
        remainingEnergyImage.fillAmount = remainingEnergy;
        
        if(remainingEnergy > maxEnergy)
        {
            remainingEnergy = maxEnergy;
        }
        else if(remainingEnergy < 0f)
        {
            remainingEnergy = 0f;
            Debug.Log("Without energy :(");
        }
    }

    void SetHeadRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayDistance, rayMaskGround);

        if(hit.collider != null)
        {
            switch(hit.collider.name)
            {
                case "Front":
                    isShadowed = true;
                    //Debug.Log("FRONT");
                    break;
                case "WallJumpMapSupport":
                    isShadowed = true;
                    //Debug.Log("WallJumpMap");
                    break;    
                case "Translucent":
                    isShadowed = false;
                    //Debug.Log("TRANSLUCENT");
                    break;
                default:
                    return;
            }
        }
        else
        {
            isShadowed = false;
            //Debug.Log("THERE'S NO COLLIDER");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.up);
    }
}