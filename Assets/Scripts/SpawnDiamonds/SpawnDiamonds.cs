using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnDiamonds : MonoBehaviour
{
   [SerializeField] private List<GameObject> diamondsList;

   [SerializeField] private GameObject door;

   public GameObject DiamondInScene {get; set;}

   [SerializeField]private int maxDiamondsCollect;

   public static int DiamondsPlayerCollect{get; set;}

   private int indexDiamondsList;

   public static bool CanSpawnDiamond {get; set;}

   public static bool canPassNextPhase {get; set;}

   [SerializeField] private Text textContador;
   
   private void Start()
   {
      indexDiamondsList = 0;
      DiamondsPlayerCollect = 0;
      DiamondInScene = GameObject.FindGameObjectWithTag("Diamond");
      CanSpawnDiamond = false;
      canPassNextPhase = false;
   }
   private void Update() 
   {
        if(maxDiamondsCollect <= DiamondsPlayerCollect)
        {
            // pode passar para a proxima fase
            canPassNextPhase = true;
            door.GetComponent<SpriteRenderer>().color = Color.green;
        }

         textContador.text = DiamondsPlayerCollect.ToString()+"/"+maxDiamondsCollect.ToString();

        SpawnDiamondsScene();
   }

   public void SpawnDiamondsScene()
   {
        
       
       if(CanSpawnDiamond == true && diamondsList.Count - 1 > indexDiamondsList)
       {
          indexDiamondsList++;
          DiamondInScene = diamondsList[indexDiamondsList];
          DiamondInScene.SetActive(true);
          CanSpawnDiamond = false;
          
       }
       
        Debug.Log(DiamondInScene);

   }
}
