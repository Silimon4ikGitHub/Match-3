using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CellCreator : MonoBehaviour
{
    [Header ("Put Cell Detailes")]
    [SerializeField] private int rows;
    [SerializeField] private int collums;
    private int offsetx;
    private int offsety;
    private int maxCount;
    private int counter;
    private bool isMoveRight = false;
    [Header ("Objects")]
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform parentObject;
    
    public void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            maxCount = rows * collums;
            AddCells();
        }
    }

    public void AddCells()
    {
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
          
        }
    }
     public void ChangeDirrectionOfCells()
        {
            isMoveRight =! isMoveRight;
            offsety++;
            counter = 0;
        }
   
   
}
