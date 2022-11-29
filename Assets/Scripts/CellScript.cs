using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool isFull;
    [SerializeField] private int myCount;
    [SerializeField] private int pinsOnSceneCounter;
    [SerializeField] private Vector3 cellPosition;
    [SerializeField] private Vector3 pinPosition;
    [SerializeField] private PinsCreator pinsCreatorScript;
    [SerializeField] private  GameObject pinn; 
    
    void Awake()
    {
        cellPosition = transform.position;
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
              
            pinPosition = pinsCreatorScript.pinsObjects[i].transform.position;
            pinsOnSceneCounter++;
             if (cellPosition == pinPosition)
             {
              pinsOnSceneCounter = 0;
              isFull = true;
             }
             if (pinsOnSceneCounter >= pinsCreatorScript.pinsObjects.Length)
             {
              isFull = false;
             }
           }
          }
    
   }
}
