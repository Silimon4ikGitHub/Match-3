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
    [SerializeField] private  int offsetx;
    [SerializeField] private  int offsety;
    [SerializeField] private  int maxCount;
    [SerializeField] private  int counter;
    [SerializeField] private  bool isMoveRight = true;

    [Header ("Objects")]
    [SerializeField] private  GameObject cellPrefab;
    [SerializeField] private  Transform parentObject;
    
    
    
    [ContextMenu("Tools / AddCells")]
     void AddCells()
    {
      maxCount = rows * collums;
      
      
        for (int i=0; i < maxCount; i++)
        {
         Vector3 newPosition = new Vector3(transform.position.x + offsetx, transform.position.y + offsety, transform.position.z);
         Instantiate( cellPrefab, newPosition, transform.rotation, parentObject);
         
         counter ++;
        
          if (counter == rows)
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
          Debug.Log("Adding");
          
        }
        
        
    }
     public  void ChangeDirrectionOfCells()
        {
            isMoveRight =! isMoveRight;
            offsety++;
            counter = 0;
        }

     [ContextMenu("Tools / DeleteCells")]
     void DeleteCells()
     {
      while (transform.childCount > 0)
      {
      DestroyImmediate(transform.GetChild(0).gameObject);
      offsetx = 0;
      offsety = 0;
      Debug.Log("Deleting");
      }
      
     }

      
   
   
}
