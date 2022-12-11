using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScriptNew : MonoBehaviour
{
    [SerializeField] const float normalSpeed = 0.02f;
    [SerializeField] private float currentSpeed;

    [Header("For Check Only")]
    [SerializeField] private int myArrayIndex;
    [SerializeField] private float maxImInsideTimer = 20;
    public float imInsideTimer;
    public int _myArrayIndex;
    public bool amInside;
    public int myPrefabIndex;
    [SerializeField] private bool IsLocationInField(int rowIndex) => myArrayIndex >= pinsCreatorScript.rows;
    [SerializeField] private Vector3 pinPosition;
    [SerializeField] private Vector3 cellPosition;
    [SerializeField] private Vector3 targetCell;
    [SerializeField] private CellCreator cellCreatorScript;
    [SerializeField] private CellScript nextCell;
    [SerializeField] private CellScript currentCell;
    [SerializeField] private PinsCreator pinsCreatorScript;
    [SerializeField] private GameObject cellCreatorGO;
    [SerializeField] private GameObject pinsCreatorGO;



    private void Awake()
    {
        cellCreatorGO = GameObject.Find("Cells");
        cellCreatorScript = cellCreatorGO.GetComponent<CellCreator>();

        pinsCreatorGO = GetComponentInParent<PinsCreator>().gameObject;
        pinsCreatorScript = pinsCreatorGO.GetComponent<PinsCreator>();

        myArrayIndex = pinsCreatorScript.countInArray - 1;
    }
    void FixedUpdate()
    {
        pinPosition = transform.position;
        cellPosition = cellCreatorScript.cellsObjects[myArrayIndex].transform.position;

        pinsCreatorScript.pinsObjects[myArrayIndex] = transform.gameObject;
        currentCell = cellCreatorScript.cellsObjects[myArrayIndex].GetComponent<CellScript>();
        
        _myArrayIndex = myArrayIndex;
        
        MoveToNextCell();

        CheckNextCell();

        InsideTimer();

    }
    void MoveToNextCell()
    {
        currentSpeed = normalSpeed;
        targetCell = cellCreatorScript.cellsObjects[myArrayIndex].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetCell, currentSpeed);
    }

    void CheckNextCell()
    {

        if (IsLocationInField(pinsCreatorScript.rows))
        {
            nextCell = cellCreatorScript.cellsObjects[myArrayIndex - pinsCreatorScript.rows].GetComponent<CellScript>();

            if (pinPosition == cellPosition) 
            {
                amInside = true;
                
                pinsCreatorScript.pinsObjects[myArrayIndex] = transform.gameObject;

                if (!nextCell.isFull)
                {
                    myArrayIndex = myArrayIndex - pinsCreatorScript.rows;
                }
            }
            else
            {
                amInside = false;
                
            }
        }
    }

    void InsideTimer()
    {
        if (amInside)
        {
        imInsideTimer = imInsideTimer + Time.deltaTime;
        }
        if (!amInside)
        {
        imInsideTimer = 0;
        }
        if (imInsideTimer > maxImInsideTimer)
        {
            imInsideTimer = 1;
        }

    }
    void OnMouseDown()
    {
        Destroy(gameObject);
    }

    //** private bool IsLocationInField(int rowIndex) => myArrayIndex >= pinsCreatorScript.rows;
    // Then you use it like that "if (IsLocationInField(currentRowIndex))"

    // Also you can simplifie your code:
    //
    //    if (myArrayIndex >= pinsCreatorScript.rows)
    //        if (pinPosition == cellPosition)
    //            if (!nextCell.isFull)
    //            {
    //                nextCell = cellCreatorScript.cellsObjects[myArrayIndex - pinsCreatorScript.rows].GetComponent<CellScript>();
    //                pinsCreatorScript.pinsObjects[myArrayIndex] = transform.gameObject;
    //                myArrayIndex = myArrayIndex - pinsCreatorScript.rows;
    //            }
    //
    // Or:
    //
    //    if (myArrayIndex <= pinsCreatorScript.rows)
    //        return;
    //    if (pinPosition != cellPosition)
    //        return;
    //
    //    if (!nextCell.isFull)
    //    {
    //        nextCell = cellCreatorScript.cellsObjects[myArrayIndex - pinsCreatorScript.rows].GetComponent<CellScript>();
    //        pinsCreatorScript.pinsObjects[myArrayIndex] = transform.gameObject;
    //        myArrayIndex = myArrayIndex - pinsCreatorScript.rows;
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
