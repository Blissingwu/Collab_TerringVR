using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallController : MonoBehaviour
{
    public GameBoard_Manager gameBoard;
    public float baseForceMagnitude;
    public PhysicMaterial ballMaterial;
    public Transform hitterTransform;

    private Rigidbody rb;
    private AudioSource audioSource;

    public List<int> ballPoint;
    public bool hittenBall = false;
    public bool isMoving = false;

    public float forceScalingFactor = 0.5f;
    public float stopThreshold = 0.1f;

    private XRGrabInteractable grabInteractable;
    public bool isGrabbed = false;


    public void Init()
    {
        ballPoint.Clear();
        hittenBall = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().material = ballMaterial;

        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void Update()
    {
        if (hitterTransform != null)
        {
            Vector3 direction = (transform.position - hitterTransform.position);
            direction.y = 0;
            direction = direction.normalized;
        }

        if (rb.velocity.magnitude > stopThreshold)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ball") && gameObject.CompareTag("Ball"))
        {
            AudioManager.Instance.PlaySFX(SFXTag.SFX_Hit, audioSource);
            PoolingManager.Instance.GetFromPool(PoolingTag.BallHit_B, other.contacts[0].point, other.transform.rotation);
        }

        if (other.transform.CompareTag("Hitter"))
        {
            AudioManager.Instance.PlaySFX(SFXTag.SFX_Hit, audioSource);
            hittenBall = true;

            HitterMotionTracker motionTracker = other.transform.GetComponent<HitterMotionTracker>();
            if (motionTracker != null)
            {
                ContactPoint contact = other.contacts[0];
                Vector3 contactPoint = contact.point;

                // Calculate force direction more effectively
                Vector3 forceDirection = (contactPoint - other.transform.position).normalized;
                forceDirection.y = 0.0f; // Keep horizontal

                float hitterSpeed = Mathf.Clamp(motionTracker.Velocity.magnitude, 0, 10); // Clamped speed to prevent excessive force
                float forceMagnitude = baseForceMagnitude * hitterSpeed * forceScalingFactor;

                // Make sure the ball is always shot forward by the hitter's forward direction
                Vector3 forwardForce = other.transform.forward * forceMagnitude;

                // Ensure we apply the force only in the horizontal plane
                forwardForce.y = 0.0f; // Preventing upward movement

                Debug.Log(forwardForce);

                rb.AddForce(forwardForce, ForceMode.Impulse);

                Quaternion rotation = Quaternion.LookRotation(forceDirection);
                PoolingManager.Instance.GetFromPool(PoolingTag.BallHit_R, contactPoint, rotation);
                PoolingManager.Instance.GetFromPool(PoolingTag.BallHit_Trail, this.transform.position, this.transform.rotation, this.transform);
            }
        }
    }



    // 오브젝트가 잡혔을 때 호출되는 함수
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        gameBoard.gameBoard_Cam.targetBall = this.gameObject;
    }
}