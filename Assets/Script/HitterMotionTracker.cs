using UnityEngine;

public class HitterMotionTracker : MonoBehaviour
{
    private Vector3 previousPosition;
    public Vector3 Velocity { get; private set; }

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Velocity = ((transform.position - previousPosition) / Time.fixedDeltaTime) * 0.7f;
        previousPosition = transform.position;
    }
}
