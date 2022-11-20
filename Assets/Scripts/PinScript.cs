using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{
    [SerializeField] private  float speed = 1;
    [SerializeField] private  bool imIn;
    [SerializeField] private Vector3 pinPSN;
    [SerializeField] private CellCreator pins;
    [SerializeField] private  GameObject cell; 
    
    void Awake()
    {
        pinPSN = transform.position;
        cell = GameObject.Find("Cells");
        pins = cell.GetComponent<CellCreator>();
    }
    void Update()
    {
        
        for (int i=0; i < pins.cellsObjects.Length; i++)
        {
            
        if (pinPSN == pins.cellsObjects[i].transform.position)
         {
            imIn = true;
         }
        }
    }
    void FixedUpdate()
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }
}
