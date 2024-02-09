using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public GameScript gameScript;

    void Start()
    {
        Vector3 gridCenter = gameScript.CalculateGridCenter();
        // Now use gridCenter to position your camera
        // For example:
        transform.position = new Vector3(gridCenter.x, gridCenter.y, transform.position.z);
        transform.LookAt(gridCenter);
    }



}

