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
    public  int visibleCells;     
    private int[,] cells;
    private  int offsetx;
    private  int offsety;
    [SerializeField] private  int cellCounter;

    [Header ("Objects")]
    [SerializeField] private  GameObject cellPrefab;
    [SerializeField] private  Transform parentObject;
    public  GameObject[] cellsObjects;
    public GameObject CellPrefab { get => cellPrefab; set => cellPrefab = value; }

    [ContextMenu("Tools / AddCells")]
    
    public void AddCells()
    {
      visibleCells = rows * collums - rows * 2 - 1;
      cells = new int [rows,collums];
      cellsObjects = new GameObject [rows * collums];

        for (int i=0; i < cells.Length; i++)
        {
         Vector3 pinPosition = new Vector3(transform.position.x + offsetx, transform.position.y + offsety, transform.position.z);
         var cell = Instantiate( CellPrefab, pinPosition, transform.rotation, parentObject);
         cellsObjects[i] = parentObject.transform.GetChild(i).gameObject;

         cellCounter ++;
         offsetx++;
        
          if (cellCounter == rows)
          {
            NextRow();
          }
          if (i > visibleCells)
          {
            cell.GetComponent<Image>().enabled = false;
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