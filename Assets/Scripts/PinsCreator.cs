using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class PinsCreator : MonoBehaviour
{
    [Header ("Put Cell Detailes")]
    [SerializeField] private  int rows;
    [SerializeField] private  int collums;
    [Header ("Put Pin Position")]
    [SerializeField] private  int pinRow;
    [SerializeField] private  int pinCollum;
    private int[,] pins;
    private  int offsetx;
    private  int offsety;
    private  int pinCounter;

    [Header ("Objects")]
    [SerializeField] private  GameObject[] pinPrefabs;
    [SerializeField] private  GameObject[] pinsObjects;
    [SerializeField] private  Transform parentObject;

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
      }
     }
     [ContextMenu("Tools / Add Pin")]
     void AddPins()
     {
      pins = new int [rows,collums];
      
        for (int i=0; i < pins.Length; i++)
        {
         Vector3 newPosition = new Vector3(transform.position.x + pinRow -1, transform.position.y + pinCollum -1, transform.position.z);

         int random = Random.Range(0, pinPrefabs.Length);

         if(i == pinRow)
         {
         var cell = Instantiate(pinPrefabs[random], newPosition, transform.rotation, parentObject);
         
         EditorUtility.SetDirty(cell);
         }
         pinCounter ++;
         
        }
    }
    [ContextMenu("Tools / Add All Pins")]
    void AddAllPins()
     {
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

