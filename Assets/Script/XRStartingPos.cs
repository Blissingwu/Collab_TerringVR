using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRStartingPos : MonoBehaviour
{
    public Transform startPoint;

    void Start()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
            transform.rotation = startPoint.rotation;
        }
    }
}
