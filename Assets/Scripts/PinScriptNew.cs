using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScriptNew : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float myRow;
    [SerializeField] private int myCount;
    [SerializeField] private Vector3 pinPSN;
    [SerializeField] private Vector3 cellPSN;
    [SerializeField] private Vector3 targetCell;
    [SerializeField] private CellCreator cellCreatorScript;
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
        Debug.Log(transform.position);
        cellPSN = cellCreatorScript.cellsObjects[myCount].transform.position;
    }
    void GoDown()
    {
        speed = 0.01f;
        targetCell = cellCreatorScript.cellsObjects[myCount].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetCell, speed );
    }
    void NextCell()
    {
        if (pinPSN == cellPSN)
        {

        }
    }
    
    

}
