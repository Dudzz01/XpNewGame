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

    [Header("Portraits")]
    [SerializeField] Image portrait;
    [SerializeField] Sprite[] portraits;

    void Start()
    {
        InitEnergy();
    }

    void Update()
    {
        StartCoroutine(DecreaseEnergy());
        EnergyDisplay();
        DisplayEnergyPortrait();
    }

    void FixedUpdate()
    {
        SetHeadRaycast();
    }

    void InitEnergy()
    {
        maxEnergy = remainingEnergy;
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
            //Debug.Log("Without energy :(");
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

    public float GetCurrentEnergy()
    {
        return remainingEnergy;
    }

    void DisplayEnergyPortrait()
    {
        if(remainingEnergyImage.fillAmount > 0.5f)
        {
            portrait.sprite = portraits[0]; //Default
        }
        else if(remainingEnergyImage.fillAmount < 0.5f && remainingEnergyImage.fillAmount > 0.1f)
        {
            portrait.sprite = portraits[1]; //Tired
        }
        else if(remainingEnergyImage.fillAmount == 0)
        {
            portrait.sprite = portraits[2]; //Dead
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.up);
    }
}