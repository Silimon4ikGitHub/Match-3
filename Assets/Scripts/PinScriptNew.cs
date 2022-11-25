using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScriptNew : MonoBehaviour
{
    [SerializeField] private float speed = 0.02f;

    [Header ("For Check Only")]
    [SerializeField] private int myPSNinArray;
    [SerializeField] private Vector3 pinPSN;
    [SerializeField] private Vector3 cellPSN;
    [SerializeField] private Vector3 targetCell;
    [SerializeField] private CellCreator cellCreatorScript;
    [SerializeField] private CellScript nextCell;
    [SerializeField] private CellScript currentCell;
    [SerializeField] private PinsCreator pinsCreatorScript;
    [SerializeField] private GameObject cellCreatorGM;
    [SerializeField] private GameObject pinsCreatorGM;



    private void Awake() 
    {
        cellCreatorGM = GameObject.Find("Cells");
        cellCreatorScript = cellCreatorGM.GetComponent<CellCreator>();

        pinsCreatorGM = GameObject.Find("Pins");
        pinsCreatorScript = pinsCreatorGM.GetComponent<PinsCreator>();

        myPSNinArray = pinsCreatorScript.countInArray - 1;
    }
    void FixedUpdate()
    {
        pinPSN = transform.position;
        cellPSN = cellCreatorScript.cellsObjects[myPSNinArray].transform.position;

        pinsCreatorScript.pinsObjects[myPSNinArray] = transform.gameObject;
        currentCell = cellCreatorScript.cellsObjects[myPSNinArray].GetComponent<CellScript>();
        
        GoToCell();

        CheckNextCell();
            
    }
    void GoToCell()
    {
        //======PIN GOIND IN DIRRECTION OF targetCell======
        speed = 0.02f;
        targetCell = cellCreatorScript.cellsObjects[myPSNinArray].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetCell, speed );
    }

    void Stop()
    {
        speed = 0;  
    }
    void CheckNextCell()
    {
     //======CHECK FOR LOCATION IN FIELD======
        if (myPSNinArray >= pinsCreatorScript.rows)
        {
            nextCell = cellCreatorScript.cellsObjects[myPSNinArray - pinsCreatorScript.rows].GetComponent<CellScript>();
        //======CHECK WHEN INSIDE OF CELL======
            if (pinPSN == cellPSN)
            {
                pinsCreatorScript.pinsObjects[myPSNinArray] = transform.gameObject;
        //======CHECK NEXT CELL IS EMPTY=======      
                if (nextCell.isFull == false)
                {
                    myPSNinArray = myPSNinArray - pinsCreatorScript.rows;
                }

            }
        }
    }
}
