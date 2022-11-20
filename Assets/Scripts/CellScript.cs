using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    [SerializeField] private bool isFull;
    [SerializeField] private Vector3 cellPSN;
    [SerializeField] private PinsCreator pins; 
    [SerializeField] private  GameObject pinn; 
    
    void Awake()
    {
        cellPSN = transform.position;
        pinn = GameObject.Find("Pins");
        pins = pinn.GetComponent<PinsCreator>();
    }
    void Update()
    {
        if(pins.isPinsOnscene == true)
        {
        
         if(pins.pinsObjects.Length > 0)
         {
          for (int i=0; i < pins.pinsObjects.Length; i++)
          {
            if(pins.pinsObjects[i] != null)
            {
            
             if (cellPSN == pins.pinsObjects[i].transform.position)
             {
              isFull = true;
             }
             else
             {
             isFull = false;
             }
           }
          }
         }
        }
        
    }
}
