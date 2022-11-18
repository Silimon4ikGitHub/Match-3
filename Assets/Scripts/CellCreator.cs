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
    private int[,] cells;
    private  int offsetx;
    private  int offsety;
    [SerializeField] private  int cellCounter;

    [Header ("Objects")]
    [SerializeField] private  GameObject cellPrefab;
    [SerializeField] private  GameObject[] pinPrefabs;
    [SerializeField] private  GameObject[] cellsObjects;
    [SerializeField] private  Transform parentObject; 
    
    [ContextMenu("Tools / AddCells")]
    
    public void AddCells()
    {
      
      cells = new int [rows,collums];
      cellsObjects = new GameObject [rows * collums];

        for (int i=0; i < cells.Length; i++)
        {
         Vector3 pinPosition = new Vector3(transform.position.x + offsetx, transform.position.y + offsety, transform.position.z);
         var cell = Instantiate( cellPrefab, pinPosition, transform.rotation, parentObject);
         cellsObjects[i] = parentObject.transform.GetChild(i).gameObject; // Input Pins in Array
         cellCounter ++;
         offsetx++;
        
          if (cellCounter == rows)
          {
            NextRow();
          }
          EditorUtility.SetDirty(cell);
        }
        offsetx = 0;
        offsety = 0; 
        
    }
     public  void NextRow()
        {
            offsetx = 0;
            offsety++;
            cellCounter = 0;
        }

     [ContextMenu("Tools / DeleteCells")]
     void DeleteCells()
     {
      while (transform.childCount > 0)
      {
      DestroyImmediate(transform.GetChild(0).gameObject);
      offsetx = 0;
      offsety = 0;
      cellCounter = 0;
      }
     }
}
