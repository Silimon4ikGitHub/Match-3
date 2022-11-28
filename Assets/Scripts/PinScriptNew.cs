using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScriptNew : MonoBehaviour
{
    [SerializeField] const float normalSpeed = 0.02f;
    [SerializeField] private float currentSpeed;

    [Header("For Check Only")]
    [SerializeField] private int myPSNinArray;
    [SerializeField] private Vector3 pinPSN;
    [SerializeField] private Vector3 cellPSN;
    [SerializeField] private Vector3 targetCell;
    [SerializeField] private CellCreator cellCreatorScript;
    [SerializeField] private CellScript nextCell;
    [SerializeField] private CellScript currentCell;
    [SerializeField] private PinsCreator pinsCreatorScript;
    [SerializeField] private GameObject cellCreatorGO;
    [SerializeField] private GameObject pinsCreatorGO;



    private void Awake()
    {
        cellCreatorGO = GameObject.Find("Cells"); // Bad practice to use Find, add Cells throw inspector
        cellCreatorScript = cellCreatorGO.GetComponent<CellCreator>();

        pinsCreatorGO = GameObject.Find("Pins");
        pinsCreatorScript = pinsCreatorGO.GetComponent<PinsCreator>();

        myPSNinArray = pinsCreatorScript.countInArray - 1;
    }
    void FixedUpdate()
    {
        pinPSN = transform.position;
        cellPSN = cellCreatorScript.cellsObjects[myPSNinArray].transform.position;

        pinsCreatorScript.pinsObjects[myPSNinArray] = transform.gameObject;
        currentCell = cellCreatorScript.cellsObjects[myPSNinArray].GetComponent<CellScript>();

        MoveToNextCell();

        CheckNextCell();

    }
    void MoveToNextCell()
    {
        // Why I need this comment? Why may I just name this method "MoveToNextCell()" ? 
        // Also don't use magic numbers
        // Instead of "speed = 0.02f" right here, use constants
        // Or if you use Inspector, don't stop falling throw speed
        // use private bool _isPlay or _isStoped

        currentSpeed = normalSpeed;
        targetCell = cellCreatorScript.cellsObjects[myPSNinArray].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetCell, currentSpeed);
    }

    void Stop()
    {
        currentSpeed = 0;
    }
    void CheckNextCell()
    {
        //Don't use comments for that things
        //Clear code - code that any coder able to understand without comments
        //for i.g. myPSNinArray? Why? Position of what in array?
        // currentArrayPosition or currentArrayIndex or currentRowIndex or something like that more clear
        // Also you can do it in other method see **

        //======CHECK FOR LOCATION IN FIELD======
        if (myPSNinArray >= pinsCreatorScript.rows)
        {
            nextCell = cellCreatorScript.cellsObjects[myPSNinArray - pinsCreatorScript.rows].GetComponent<CellScript>();
            //======CHECK WHEN INSIDE OF CELL======
            if (pinPSN == cellPSN) //Don't use "PSN", use full names like "cellPosition"
            {
                pinsCreatorScript.pinsObjects[myPSNinArray] = transform.gameObject;
                //======CHECK NEXT CELL IS EMPTY=======      
                if (nextCell.isFull == false) //Don't use "== false", use "!nextCell.isFull"
                {
                    myPSNinArray = myPSNinArray - pinsCreatorScript.rows;
                }

            }
        }
    }

    //** private bool IsLocationInField(int rowIndex) => myPSNinArray >= pinsCreatorScript.rows;
    // Then you use it like that "if (IsLocationInField(currentRowIndex))"

    // Also you can simplifie your code:
    //
    //    if (myPSNinArray >= pinsCreatorScript.rows)
    //        if (pinPSN == cellPSN)
    //            if (nextCell.isFull == false)
    //            {
    //                nextCell = cellCreatorScript.cellsObjects[myPSNinArray - pinsCreatorScript.rows].GetComponent<CellScript>();
    //                pinsCreatorScript.pinsObjects[myPSNinArray] = transform.gameObject;
    //                myPSNinArray = myPSNinArray - pinsCreatorScript.rows;
    //            }
    //
    // Or:
    //
    //    if (myPSNinArray <= pinsCreatorScript.rows)
    //        return;
    //    if (pinPSN != cellPSN)
    //        return;
    //
    //    if (nextCell.isFull == false)
    //    {
    //        nextCell = cellCreatorScript.cellsObjects[myPSNinArray - pinsCreatorScript.rows].GetComponent<CellScript>();
    //        pinsCreatorScript.pinsObjects[myPSNinArray] = transform.gameObject;
    //        myPSNinArray = myPSNinArray - pinsCreatorScript.rows;
    //    }
    //
    // The best way:
    //
    //
    //    if (!IsLocationInField(currentRowIndex) || !IsInsideOfCell())
    //        return;
    //
    //    if (!nextCell.isFull)
    //    {
    //        nextCell = cellCreatorScript.cellsObjects[myPSNinArray - pinsCreatorScript.rows].GetComponent<CellScript>();
    //        pinsCreatorScript.pinsObjects[myPSNinArray] = transform.gameObject;
    //        myPSNinArray = myPSNinArray - pinsCreatorScript.rows;
    //    }
    //
    // Is it shorter and cleaner?)
}
