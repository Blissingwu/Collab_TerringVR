using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    public bool isFallen = false;
    public float fallThresholdAngle = 45f;

    private void Update()
    {
        if (!isFallen)
        {
            float angle = Vector3.Angle(transform.up, Vector3.up);
            if (angle > fallThresholdAngle)
            {
                isFallen = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            PoolingManager.Instance.GetFromPool(PoolingTag.PinHit_B, this.transform.position, this.transform.rotation);
            if (!isFallen)
            {
                isFallen = true;
            }
        }
    }
}
