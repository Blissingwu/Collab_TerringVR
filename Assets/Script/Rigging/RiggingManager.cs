using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RiggingManager : MonoBehaviour
{
    public Transform leftHandIK;
    public Transform rightHandIK;
    public Transform headIK;

    public Transform leftHandController;
    public Transform rightHandController;
    public Transform hmd;

    public Vector3[] leftOffset;
    public Vector3[] rightOffset;
    public Vector3[] headOffset;

    public float smoothValue = 0.1f;
    public float modelHeight = 1.67f;

    void LateUpdate()
    {
        MappingHandTransform(leftHandIK, leftHandController, true);
        MappingHandTransform(rightHandIK, rightHandController, false);
        MappingBodyTransform(headIK, hmd);
        MappingHeadTransform(headIK, hmd);
    }

    void MappingHandTransform(Transform _IK, Transform _controller, bool _isLeft)
    {
        var _offset = _isLeft ? leftOffset : rightOffset;

        _IK.position = _controller.TransformPoint(_offset[0]);
        _IK.rotation = _controller.rotation * Quaternion.Euler(_offset[1]);
    }

    void MappingBodyTransform(Transform _IK, Transform _hmd)
    {
        this.transform.position = new Vector3(_hmd.position.x, _hmd.position.y - modelHeight, _hmd.position.z);
        float _yaw = _hmd.eulerAngles.y;
        var _targetRotation = new Vector3(this.transform.eulerAngles.x, _yaw, this.transform.eulerAngles.z);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(_targetRotation), smoothValue);
    }

    void MappingHeadTransform(Transform _IK, Transform _hmd)
    {
        _IK.position = _hmd.TransformPoint(headOffset[0]);
        _IK.rotation = _hmd.rotation * Quaternion.Euler(headOffset[1]);
    }
}
