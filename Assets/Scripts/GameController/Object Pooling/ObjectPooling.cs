using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Queue<GameObject> queueOfBullets = new Queue<GameObject>(); // fila de gameObjects

    [SerializeField] private int quantOfBullets;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform objectParentHiearchy; // transform do objeto pai que organiza a hierarquia na hora das instancias das bullets
    
    private static ObjectPooling _Instance;

        public static ObjectPooling Instance
        {
            get
            {
                _Instance = FindObjectOfType<ObjectPooling>();   // Singletom

                return _Instance;
            }
        }

    void Awake()
    {
        

        for(int i = 0; i< quantOfBullets; i++)
        {
            GameObject bulletP = Instantiate(bulletPrefab,bulletPrefab.transform.position,Quaternion.identity, objectParentHiearchy);
            bulletP.SetActive(false);
            queueOfBullets.Enqueue(bulletP);
        }
        
    }

    public GameObject GetBulletInPool()
    {
        GameObject bulletCurrent = queueOfBullets.Dequeue();
        queueOfBullets.Enqueue(bulletCurrent);
        bulletCurrent.SetActive(true);
        return bulletCurrent;
    }

    public void SetBulletInPool(GameObject bulletCurrent)
    {
        bulletCurrent.SetActive(false);
        
    }
}
