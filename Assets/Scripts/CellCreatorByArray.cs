using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCreatorByArray : MonoBehaviour
{
    [SerializeField] private float[,] field;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform parentObject;
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private int maxCount;
    [SerializeField] private int rows;
    [SerializeField] private int collums;
    

    public void  Update() 
    {
        if (Input.GetKeyDown("e"))
        {
            CreateCell();
        }
    }

    private void CreateField()
    {
        x = rows;
        y = collums;
        maxCount = rows * collums;

        field = new float [x,y];

        foreach( var item in field)
        {
            Debug.Log(field);
        }
        
    }
    private void CreateCell()
    {
        
    }
}
