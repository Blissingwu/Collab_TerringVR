using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointContainer : MonoBehaviour
{
    public int point;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            other.GetComponent<BallController>().ballPoint.Add(point);
    }
}
