using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class ShortPinsCreator : MonoBehaviour
{
    [Header ("Put Cell Detailes")]
    public  int rows;
    public  int collums;

    [Header ("Put Pin Position")]
    [SerializeField] private  int pinRow;
    [SerializeField] private  int pinCollum;

    [Header ("Public variables")]
    public int arrayIndex;
    public  bool isPinsOnscene = false;
    private int[,] pins;
    private  int offsetx;
    private  int offsety;
    private  int pinCounter;
    [SerializeField] private float pinCreationColldown;
    public float pinCreationTimer;
    [SerializeField] private float maxPinCreationTimer;


    [Header ("Objects")]
    public  GameObject[] pinsObjects;
    [SerializeField] private  GameObject[] pinPrefabs;
    [SerializeField] private  Transform parentObject;
    [SerializeField] private  CellCreator cellCreatorScript;
    [SerializeField] private  ShortPinScript[] randomPinPrefab;
    [SerializeField] private  ShortPinScript myPinScript;
    

    void FixedUpdate()
     {
       pinCreationTimer++;

       MovingPinsInArray();

       if (!cellCreatorScript.cellsObjects[cellCreatorScript.cellsObjects.Length - 1].GetComponent<ShortCellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.cellsObjects.Length - 2].GetComponent<ShortCellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.cellsObjects.Length - 3].GetComponent<ShortCellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.cellsObjects.Length - 4].GetComponent<ShortCellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.cellsObjects.Length - 5].GetComponent<ShortCellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.cellsObjects.Length - 6].GetComponent<ShortCellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.cellsObjects.Length - 7].GetComponent<ShortCellScript>().isFull &&
           !cellCreatorScript.cellsObjects[cellCreatorScript.cellsObjects.Length - 8].GetComponent<ShortCellScript>().isFull) 
       {
         if (pinCreationTimer > pinCreationColldown)
         {
          AddAllPins();
         }

       }
       RefreshPinCreationTimer();
       
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
      arrayIndex = 0;
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
         arrayIndex++;
         int random = Random.Range(0, pinPrefabs.Length);
         
         if(i > pins.Length - rows - 1)
         {
         var pin = Instantiate(randomPinPrefab[random], pinPosition, transform.rotation, parentObject);
         myPinScript = pin.GetComponent<ShortPinScript>();
         myPinScript.Init(rows, arrayIndex, transform.gameObject);
         pinsObjects[i] = pin.gameObject;
         }
         
         pinCounter ++;
         offsetx++;
         
        if (pinCounter == rows)
          {
            NextRow();
          }
          
        }
        pinCounter = 0;
        arrayIndex = 0;
        offsetx = 0;
        offsety = 0;  
    }
    

    [ContextMenu("Tools / Add All Pins")]

    void AddAllPins()
     {
       
       AddPins();
       pinCreationTimer = 0;
       
     }

    [ContextMenu("Tools / Change All Pins")]
    void ChangeAllPins()
    {
      DeletePins();
      AddAllPins();
    }
    
    private void MovingPinsInArray()
    {
      for (int i=0; i<pinsObjects.Length; i++)
       {
        
        if(pinsObjects[i] != null)
        {
          var myArrayIndex = pinsObjects[i].GetComponent<ShortPinScript>().myArrayIndex;
          pinsObjects[myArrayIndex] = pinsObjects[i];
          
        }
       
       }
    }
    private void NextRow()
        {
            offsetx = 0;
            offsety++;
            pinCounter = 0;
        }
    private void RefreshPinCreationTimer()
    {
      if (pinCreationTimer > maxPinCreationTimer)
      {
        pinCreationTimer = pinCreationColldown;
      }
    }
}
