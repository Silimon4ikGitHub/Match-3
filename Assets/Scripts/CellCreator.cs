using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class CellCreator : MonoBehaviour
{
    [Header ("Put Cell Detailes")]
    
    [SerializeField] private  int rows;
    [SerializeField] private  int collums;
    [Header ("Put Pin Position")]
    [SerializeField] private  int pinRow;
    [SerializeField] private  int pinCollum;
    private int[,] field ;
    private  int offsetx;
    private  int offsety;
    private  int cellCounter;
    private  int pinCounter;
    private  bool isMoveRight = true;

    [Header ("Objects")]
    [SerializeField] private  GameObject cellPrefab;
    [SerializeField] private  GameObject[] pinPrefabs;
    [SerializeField] private  Transform parentObject;
    
    
    
    [ContextMenu("Tools / AddCells")]
    
    public void AddCells()
    {
      field = new int [rows,collums];

        for (int i=0; i < field.Length; i++)
        {
         Vector3 pinPosition = new Vector3(transform.position.x + offsetx, transform.position.y + offsety, transform.position.z);
         Instantiate( cellPrefab, pinPosition, transform.rotation, parentObject);
         
         cellCounter ++;
        
          if (cellCounter == rows)
          {
            ChangeDirrectionOfCells();
          }
          else if (isMoveRight == true)
          {
            offsetx++;
          }
          else if (isMoveRight == false)
          {
            offsetx--;
          }
        } 
    }
     public  void ChangeDirrectionOfCells()
        {
            isMoveRight =! isMoveRight;
            offsety++;
            cellCounter = 0;
        }

     [ContextMenu("Tools / DeleteAll")]
     void DeleteCells()
     {
      while (transform.childCount > 0)
      {
      DestroyImmediate(transform.GetChild(0).gameObject);
      offsetx = 0;
      offsety = 0;
      isMoveRight = true;
      Debug.Log("Deleting");
      }
     }
     [ContextMenu("Tools / Add Pin")]
     void AddPins()
     {
      field = new int [rows,collums];
      
        for (int i=0; i < field.Length; i++)
        {
         Vector3 newPosition = new Vector3(transform.position.x + pinRow -1, transform.position.y + pinCollum -1, transform.position.z);

         int random = Random.Range(0, pinPrefabs.Length);

         if(i == pinRow)
         {
         Instantiate(pinPrefabs[random], newPosition, transform.rotation, parentObject);
         Debug.Log("Pin Added");
         }
         pinCounter ++;
        }
        
    }
     

      
   
   
}
