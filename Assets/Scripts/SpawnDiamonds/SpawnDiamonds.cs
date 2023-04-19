using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnDiamonds : MonoBehaviour
{
   [Header("References")]
   [SerializeField] GameObject[] diamondsArray = new GameObject[10];
   [SerializeField] GameObject door;
   [SerializeField] GameObject activeDiamond {get; set;}
   [SerializeField] TextMeshProUGUI counterText;

   [Header("Properties")]
   [SerializeField] const int maxDiamondsCollect = 10;
   [SerializeField] public static int collectedDiamonds{get; set;}
   private int indexDiamondsList;
   public static bool canSpawnDiamond {get; set;}
   public static bool canPassNextLevel {get; set;}

   private void Start()
   {
      indexDiamondsList = 0;
      collectedDiamonds = 0;
      activeDiamond = GameObject.FindGameObjectWithTag("Diamond");
      canSpawnDiamond = false;
      canPassNextLevel = false;

      FillDiamondArray();
      FillDiamondText();
      FillDoorReference();
   }

   private void Update() 
   {
      if(maxDiamondsCollect <= collectedDiamonds)
      {
         // pode passar para a proxima fase
         canPassNextLevel = true;
         door.GetComponent<Animator>().SetTrigger("levelComplete");
      }

      counterText.text = collectedDiamonds.ToString() + "/" + maxDiamondsCollect.ToString();

      SpawnDiamondsScene();
      CheckNullStatus();
   }

   void CheckNullStatus()
   {
      if(door == null || counterText == null)
      {
         indexDiamondsList = 0;
         canPassNextLevel = false;
         collectedDiamonds = 0;

         FillDiamondArray();
         FillDiamondText();
         FillDoorReference();
      }
   }

   void FillDiamondArray()
   {
      GameObject diamondHolder = GameObject.Find("Scenario/Diamonds").transform.gameObject;

      for (int i = 0; i < diamondsArray.Length; i++)
      {
         diamondsArray[i] = diamondHolder.transform.GetChild(i).gameObject;
      }

      //print("FillDiamondArray()");
   }

   void FillDiamondText()
   {
      counterText = GameObject.Find("UI/DiamondCounter/DiamondAmount").transform.gameObject.GetComponent<TextMeshProUGUI>();
   }

   void FillDoorReference()
   {
      door = GameObject.Find("Scenario/Decorations/Door").transform.gameObject;
   }

   public void SpawnDiamondsScene()
   {
      if(canSpawnDiamond == true && diamondsArray.Length-1 > indexDiamondsList)
      {
         indexDiamondsList++;
         activeDiamond = diamondsArray[indexDiamondsList];
         activeDiamond.SetActive(true);
         canSpawnDiamond = false;
      }
   }
}