using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortPinScript : MonoBehaviour
{
    [SerializeField] const float normalSpeed = 0.02f;
    [SerializeField] private float currentSpeed;

    [Header("For Check Only")]
    [SerializeField] private int myArrayIndex;
    [SerializeField] private int rows;
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
    
    public void Init(int _rows,int _myArrayIndex, GameObject _pinsObjects )
    {
        myArrayIndex = _myArrayIndex;
        rows = _rows;
        _pinsObjects = transform.gameObject;

    }
    private void Awake()
    {
        cellCreatorGO = GameObject.Find("Cells");
        cellCreatorScript = cellCreatorGO.GetComponent<CellCreator>();

    }
    void FixedUpdate()
    {
        pinPosition = transform.position;
        cellPosition = cellCreatorScript.cellsObjects[myArrayIndex].transform.position;

        pinsCreatorScript.pinsObjects[myArrayIndex] = transform.gameObject;
        currentCell = cellCreatorScript.cellsObjects[myArrayIndex].GetComponent<CellScript>();

        MoveToNextCell();

        CheckNextCell();

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
                pinsCreatorScript.pinsObjects[myArrayIndex] = transform.gameObject;

                if (!nextCell.isFull)
                {
                    myArrayIndex = myArrayIndex - pinsCreatorScript.rows;
                }
            }
        }
    }
}
