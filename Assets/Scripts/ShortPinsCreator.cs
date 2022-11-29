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
    public int countInArray;
    public  bool isPinsOnscene = false;
    private int[,] pins;
    private  int offsetx;
    private  int offsety;
    private  int pinCounter;


    [Header ("Objects")]
    [SerializeField] private  GameObject[] pinPrefabs;
    [SerializeField] private  Transform parentObject;
    [SerializeField] private  ShortPinScript[] randomPinPrefab;
    [SerializeField] private  CellCreator cellCreatorScript;
    [SerializeField] private  ShortPinScript myPinScript;
    public  GameObject[] pinsObjects;

    void FixedUpdate()
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
         var pin = Instantiate(randomPinPrefab[random], pinPosition, transform.rotation, parentObject);
         myPinScript = pin.GetComponent<ShortPinScript>();
         myPinScript.Init(rows, countInArray, transform.gameObject);
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
        countInArray = 0;
        offsetx = 0;
        offsety = 0;  
    }
    

    [ContextMenu("Tools / Add All Pins")]

    void AddAllPins()
     {
      isPinsOnscene = true;
      pins = new int [rows,collums];
      pinsObjects = new GameObject [rows * collums];

        for (int i=0; i < pins.Length; i++)
        {
         Vector3 pinPosition = new Vector3(transform.position.x + offsetx, transform.position.y + offsety, transform.position.z);

         int random = Random.Range(0, pinPrefabs.Length);
         var cell = Instantiate( pinPrefabs[random], pinPosition, transform.rotation, parentObject);
         pinsObjects[i] = parentObject.transform.GetChild(i).gameObject;
         
         pinCounter ++;
         offsetx++;
         
          if (pinCounter == rows)
          {
            NextRow();
          }
          EditorUtility.SetDirty(cell);
        }
        pinCounter = 0;
        offsetx = 0;
        offsety = 0;  
    }
    [ContextMenu("Tools / Change All Pins")]
    void ChangeAllPins()
    {
      DeletePins();
      AddAllPins();
    }
}
