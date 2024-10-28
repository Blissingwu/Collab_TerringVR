using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard_Cam : MonoBehaviour
{
    [LabelText("Replay Display Image")] public RawImage rawImage;
    [LabelText("Replay Camera")] public Camera captureCamera;
    [LabelText("Replay Camera Offset")] public Vector3 captureOffset;
    [LabelText("Replay Target")] public GameObject targetBall;
    [LabelText("Replay Texture")] public RenderTexture renderTexture;

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
