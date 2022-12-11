using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class PinsCreator : MonoBehaviour
{
    [Header ("Put Cell Detailes")]
    public  int rows;
    public  int collums;

    [Header ("Put Pin Position")]
    [SerializeField] private  int pinRow;
    [SerializeField] private  int pinCollum;
    [SerializeField] private float pinCreationColldown;

    [Header ("Public variables")]
    public int countInArray;
    public  bool isPinsOnscene = false;
    private int[,] pins;
    private  int offsetx;
    private  int offsety;
    private  int pinCounter;
    private float pinCreationTimer;
    private float maxPinCreationTimer = 1000;


    [Header ("Objects")]
    [SerializeField] private  GameObject[] pinPrefabs;
    [SerializeField] private  Transform parentObject;
    [SerializeField] private  PinScriptNew[] randomPinPrefab;
    [SerializeField] private  CellCreator cellCreatorScript;
    public  GameObject[] pinsObjects;

    
    private void FixedUpdate() 
    {
      pinCreationTimer++;
      RefreshPinCreationTimer();

    if    (!cellCreatorScript.cellsObjects[cellCreatorScript.visibleCells].GetComponent<CellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.visibleCells - 1].GetComponent<CellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.visibleCells - 2].GetComponent<CellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.visibleCells - 3].GetComponent<CellScript>().isFull)
       {
         AddAllPins();
       }

       FindTwoSamePinHorizontal();
       FreeLeavedArrayObjects();
     
    }
     public  void NextRow()
        {
            offsetx = 0;
            offsety++;
            pinCounter = 0;
        }

     [ContextMenu("Tools / DeletePins")]
     void DeletePins()
     {
      while (transform.childCount > 0)
      {
      DestroyImmediate(transform.GetChild(0).gameObject);
      offsetx = 0;
      offsety = 0;
      pinCounter = 0;
      countInArray = 0;
      isPinsOnscene = false;
      }
     }
    
     [ContextMenu("Tools / Add Pin")]
     void AddPins()
     {
      isPinsOnscene = true;
      pins = new int [rows,collums];

        for (int i=0; i < pins.Length; i++)
        {
         Vector3 pinPosition = new Vector3(transform.position.x + offsetx, transform.position.y + offsety, transform.position.z);
         countInArray++;
         int random = Random.Range(0, pinPrefabs.Length);
         
         if(i > pins.Length - rows - 1)
         {
         var pin = Instantiate( pinPrefabs[random], pinPosition, transform.rotation, parentObject);
         pin.GetComponent<PinScriptNew>().myPrefabIndex = random;
         }
         else
         {
          pinsObjects[i] = null;
         }

         pinCounter ++;
         offsetx++;
         
        if (pinCounter == rows)
          {
            NextRow();
          }
          
        }
        pinCounter = 0;
        countInArray = 0;
        offsetx = 0;
        offsety = 0;  
    }
    

    [ContextMenu("Tools / Add All Pins")]

    void AddAllPins()
     {
      if (pinCreationTimer > pinCreationColldown)
      {   
       AddPins();
       pinCreationTimer = 0;
      }
     }
    [ContextMenu("Tools / Change All Pins")]
    void ChangeAllPins()
    {
      DeletePins();
      AddAllPins();
    }
    private void RefreshPinCreationTimer()
    {
      if (pinCreationTimer > maxPinCreationTimer)
      {
        pinCreationTimer = pinCreationColldown;
      }
    }

    void FindTwoSamePinHorizontal()
    {
      for (int i=0; i < pinsObjects.Length; i++)
       { 
        
       
        if (pinsObjects[i] != null &&
         pinsObjects[i].GetComponent<PinScriptNew>().amiInside == true &&
         pinsObjects[i].GetComponent<PinScriptNew>()._myArrayIndex >= 1 &&
         pinsObjects[i].GetComponent<PinScriptNew>()._myArrayIndex < cellCreatorScript.visibleCells &&
         pinsObjects[i-1] != null &&
         pinsObjects[i+1] != null)
        {
        GameObject currentPin = pinsObjects[i];
        PinScriptNew currentPinScript = pinsObjects[i].GetComponent<PinScriptNew>();
        GameObject leftPin = pinsObjects[i-1];
        PinScriptNew leftPinScript = pinsObjects[i-1].GetComponent<PinScriptNew>();
        GameObject rightPin = pinsObjects[i+1];
        PinScriptNew rightPinScript = pinsObjects[i+1].GetComponent<PinScriptNew>();
        Debug.Log(3%4);
         if (currentPinScript._myArrayIndex % 4 != 0 || currentPinScript._myArrayIndex % 4 != 3)
         {

          if (currentPinScript.myPrefabIndex == leftPinScript.myPrefabIndex && currentPinScript.myPrefabIndex == rightPinScript.myPrefabIndex)
          {
            Destroy(currentPin);
            Destroy(leftPin);
            Destroy(rightPin);
            pinsObjects[i] = null;
            pinsObjects[i-1] = null;
            pinsObjects[i+1] = null;
          }
         }
        }
       }
    }

    void FreeLeavedArrayObjects()
    {
      for (int i=0; i < pinsObjects.Length; i++)
      {
        if (pinsObjects[i] != null)
        {
          if (pinsObjects[i].GetComponent<PinScriptNew>().amiInside == false)
          {
            pinsObjects[i] = null;
            Debug.Log("Here is Working!");
          }
        }
      }
    }
}