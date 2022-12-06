using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortPinScript : MonoBehaviour
{
    [SerializeField] const float normalSpeed = 0.02f;
    [SerializeField] private float currentSpeed;

    [Header("For Check Only")]
    
    [SerializeField] private int rows;
    [SerializeField] private bool IsLocationInField(int rowIndex) => myArrayIndex >= pinsCreatorScript.rows;
    [SerializeField] private Vector3 pinPosition;
    [SerializeField] private Vector3 cellPosition;
    [SerializeField] private Vector3 targetCell;
    [SerializeField] private CellCreator cellCreatorScript;
    [SerializeField] private ShortCellScript nextCell;
    [SerializeField] private PinsCreator pinsCreatorScript;
    [SerializeField] private GameObject cellCreatorGO;
    public int myArrayIndex;
    
    public void Init(int _rows,int _myArrayIndex, GameObject _pinsObjects )
    {
        myArrayIndex = _myArrayIndex - 1;
        rows = _rows;
        _pinsObjects = transform.gameObject;

    }
    private void Awake()
    {
        cellCreatorGO = GameObject.Find("Cells");
        cellCreatorScript = cellCreatorGO.GetComponent<CellCreator>();

        currentSpeed = normalSpeed;
    }
    void FixedUpdate()
    {
        pinPosition = transform.position;
        cellPosition = cellCreatorScript.cellsObjects[myArrayIndex].transform.position;

        MoveToNextCell();
        CheckNextCell();
    }
    void MoveToNextCell()
    {
        targetCell = cellPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetCell, currentSpeed);
    }

    void CheckNextCell()
    {

        if(myArrayIndex >= rows)
        {
            nextCell = cellCreatorScript.cellsObjects[myArrayIndex - rows].GetComponent<ShortCellScript>();
          if (pinPosition == cellPosition)
          { 
            if (nextCell.isFull == false)
            {
                myArrayIndex = myArrayIndex - rows;
            }
            
          }
        }
    }
    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
