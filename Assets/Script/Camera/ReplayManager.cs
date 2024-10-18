using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class ReplayManager : MonoBehaviour
{
    public static ReplayManager Instance;

    public RawImage rawImage;
    public Camera captureCamera;
    public Vector3 captureOffset;
    public GameObject targetBall;
    public RenderTexture renderTexture;

    void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        if (targetBall != null)
        {
            captureCamera.transform.position = targetBall.transform.position;
            captureCamera.targetTexture = renderTexture;
        }
    }

    private void Update()
    {
        if (targetBall != null)
        {
            captureCamera.transform.position = targetBall.transform.position + captureOffset;
        }
    }


}
