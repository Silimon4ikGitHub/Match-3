using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    [SerializeField] private bool isFull;
    [SerializeField] private int myCount;
    [SerializeField] private Vector3 cellPSN;
    [SerializeField] private Vector3 pinPSN;
    [SerializeField] private PinsCreator pinsCreatorScript;
    [SerializeField] private CellCreator cellCreatorScript;
    [SerializeField] private  GameObject pinn; 
    
    void Awake()
    {
        cellPSN = transform.position;
        pinn = GameObject.Find("Pins");
        pinsCreatorScript = pinn.GetComponent<PinsCreator>();
    }
    void Update()
    {
     SearchForSamePSN();
        
    }
    void TestCheking()
   {
    if(pinsCreatorScript.isPinsOnscene == true)
        {
        
         if(pinsCreatorScript.pinsObjects.Length > 0)
         {
          for (int i=0; i < pinsCreatorScript.pinsObjects.Length; i++)
          {
            if(pinsCreatorScript.pinsObjects[i] != null)
            {
              Debug.Log(i);
            pinPSN = pinsCreatorScript.pinsObjects[i].transform.position;
             if (cellPSN == pinPSN)
             {
              isFull = true;
             }
           }
          }
         }
        }
   }
   void SearchForSamePSN()
   {
    for (int i=0; i < pinsCreatorScript.pinsObjects.Length; i++)
          {
            if(pinsCreatorScript.pinsObjects[i] != null)
            {
              
            pinPSN = pinsCreatorScript.pinsObjects[i].transform.position;
             if (cellPSN == pinPSN)
             {
              ;
              isFull = true;
             }
           }
          }
    
   }
}
