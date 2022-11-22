using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool isFull;
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
        isFull = false;
    }
    void Update()
    {
     SearchForSamePSN();
        
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
              isFull = true;
              Debug.Log("TRUE!");
             }
           }
          }
   }
}
