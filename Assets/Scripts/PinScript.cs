using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{
    [Header ("Pin Parameters")]
    [SerializeField] private  float speed = 1;
    [SerializeField] private  bool stop = false;
    
    [Header ("Only for Cheking")]
    [SerializeField] private  bool imIn;
    [SerializeField] private Vector3 pinPSN;
    [SerializeField] private CellCreator cells;
    [SerializeField] private  GameObject cell; 
    
    void Awake()
    {
        
        cell = GameObject.Find("Cells");
        cells = cell.GetComponent<CellCreator>();
    }
    void Update()
    {
        pinPSN = transform.position;
        //for (int i=0; i < pins.cellsObjects.Length; i++)
        //{
            
        //if (pinPSN == pins.cellsObjects[i].transform.position)
        // {
        //    imIn = true;
        //}
        //
        if(pinPSN.y <= cells.cellsObjects[1].transform.position.y)
        {
            StopMoution();

        }
    }
    void FixedUpdate()
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);

        if (imIn == true)
        {
            StopMoution();
        }
    }

    void StopMoution()
    {
        speed = 0;

    }
}
