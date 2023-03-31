using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
     public Vector2 dirBullet{get; set;}

     private const float speedBullet = 8;

    // Update is called once per frame
    void Update()
    {
        rig.velocity = dirBullet*speedBullet;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Tilemap")
        {
            Destroy(gameObject);
        }
    }
}
