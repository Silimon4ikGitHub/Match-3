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
    [SerializeField] private  GameObject[] pinPrefabs;
    [SerializeField] private  GameObject cellPrefab;

        public  GameObject[] cellsObjects;

    [SerializeField] private  Transform parentObject;

    public GameObject CellPrefab { get => cellPrefab; set => cellPrefab = value; }

    [ContextMenu("Tools / AddCells")]
    
    public void AddCells()
    {
      
      cells = new int [rows,collums];
      cellsObjects = new GameObject [rows * collums];

        for (int i=0; i < cells.Length; i++)
        {
         Vector3 pinPosition = new Vector3(transform.position.x + offsetx, transform.position.y + offsety, transform.position.z);
         var cell = Instantiate( CellPrefab, pinPosition, transform.rotation, parentObject);
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

    public override bool Equals(object obj)
    {
        return obj is CellCreator creator &&
               base.Equals(obj) &&
               EqualityComparer<GameObject>.Default.Equals(CellPrefab, creator.CellPrefab);
    }

    public override int GetHashCode()
    {
        int hashCode = 435074089;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(CellPrefab);
        return hashCode;
    }
}