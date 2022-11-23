using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScriptNew : MonoBehaviour
{
    [SerializeField] private float speed = 0.02f;
    [SerializeField] private float myRow;
    [SerializeField] private int myCount;
    [SerializeField] private bool isFullNextCell;
    [SerializeField] private Vector3 pinPSN;
    [SerializeField] private Vector3 cellPSN;
    [SerializeField] private Vector3 targetCell;
    [SerializeField] private CellCreator cellCreatorScript;
    [SerializeField] private CellScript nextCell;
    [SerializeField] private CellScript currentCell;
    [SerializeField] private GameObject cellCreatorGM;
    [SerializeField] private PinsCreator pinsCreatorScript;
    [SerializeField] private GameObject pinsCreatorGM;

    private void Awake() 
    {
        cellCreatorGM = GameObject.Find("Cells");
        cellCreatorScript = cellCreatorGM.GetComponent<CellCreator>();

        pinsCreatorGM = GameObject.Find("Pins");
        pinsCreatorScript = pinsCreatorGM.GetComponent<PinsCreator>();

        myCount = pinsCreatorScript.countInArray - 1;
    }
    void FixedUpdate()
    {
        pinPSN = transform.position;
        cellPSN = cellCreatorScript.cellsObjects[myCount].transform.position;

        
        
        currentCell = cellCreatorScript.cellsObjects[myCount].GetComponent<CellScript>();
        

        GoToCell();
        if (myCount >= 4)
        {
            nextCell = cellCreatorScript.cellsObjects[myCount - 4].GetComponent<CellScript>();

            if (pinPSN == cellPSN)
            {
                pinsCreatorScript.pinsObjects[myCount] = transform.gameObject;
                
                if (nextCell.isFull == false)
                {
                    myCount = myCount - 4;
                }

            }
        }
        
        
        
    }
    void GoToCell()
    {
        speed = 0.02f;
        targetCell = cellCreatorScript.cellsObjects[myCount].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetCell, speed );
    }

    void Stop()
    {
        speed = 0;
        
    }
    void NextCell()
    {
     myCount = myCount - 4;
    }
}
