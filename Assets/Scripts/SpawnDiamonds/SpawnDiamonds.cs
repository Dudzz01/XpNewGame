using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnDiamonds : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private GameObject[] diamondsArray = new GameObject[10];
   [SerializeField] private GameObject door;
   [SerializeField] public GameObject DiamondInScene {get; set;}
   [SerializeField] const int maxDiamondsCollect = 10;
   [SerializeField] private TextMeshProUGUI counterText;

   public static int DiamondsPlayerCollect{get; set;}
   private int indexDiamondsList;

   public static bool CanSpawnDiamond {get; set;}
   public static bool canPassNextPhase {get; set;}

   private void Start()
   {
      indexDiamondsList = 0;
      DiamondsPlayerCollect = 0;
      DiamondInScene = GameObject.FindGameObjectWithTag("Diamond");
      CanSpawnDiamond = false;
      canPassNextPhase = false;

      FillDiamondArray();
      FillDiamondText();
      FillDoorReference();
   }

   private void Update() 
   {
      if(maxDiamondsCollect <= DiamondsPlayerCollect)
      {
         // pode passar para a proxima fase
         canPassNextPhase = true;
         door.GetComponent<SpriteRenderer>().color = Color.green;
      }

      counterText.text = DiamondsPlayerCollect.ToString() + "/" + maxDiamondsCollect.ToString();

      SpawnDiamondsScene();
      CheckNullStatus();
   }

   void CheckNullStatus()
   {
      if(door == null || counterText == null)
      {
         indexDiamondsList = 0;

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

      print("FillDiamondArray()");
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
      if(CanSpawnDiamond == true && diamondsArray.Length > indexDiamondsList)
      {
         indexDiamondsList++;
         DiamondInScene = diamondsArray[indexDiamondsList];
         DiamondInScene.SetActive(true);
         CanSpawnDiamond = false;
      }

      Debug.Log(DiamondInScene);
   }
}