using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    [SerializeField] private bool isEmpty;
    public static bool _isEmpty;
    [SerializeField] Transform cellPSN;
    public static Transform pinPSN;


    
    void Awake()
    {
        Debug.Log("Here is Working");   
    }
    
}
