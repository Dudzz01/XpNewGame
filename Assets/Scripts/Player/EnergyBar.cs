using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask rayMask;

    [Header("Properties")]
    [SerializeField] float rayDistance;
    [SerializeField] bool isShadowed;

    [Header("Energy Bar")]
    [SerializeField] internal float remainingEnergy;
    [SerializeField] Image remainingEnergyImage;

    void Start()
    {
        InitEnergy();
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
            remainingEnergy += 0.2f * Time.deltaTime;
            yield return new WaitForSeconds(1f);
        }
    }

    void EnergyDisplay()
    {
        remainingEnergyImage.fillAmount = remainingEnergy;
        
        if(remainingEnergy > 1f)
        {
            remainingEnergy = 1f;
        }
        else if(remainingEnergy < 0f)
        {
            remainingEnergy = 0f;
            Debug.Log("Without energy :(");
        }
    }

    void SetHeadRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayDistance, rayMask);

        if(hit.collider != null)
        {
            isShadowed = true;
            Debug.Log("Collision Object: " + hit.rigidbody.name);
        }
        else
        {
            isShadowed = false;
            Debug.Log("THERE'S NO COLLIDER");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.up);
    }
}