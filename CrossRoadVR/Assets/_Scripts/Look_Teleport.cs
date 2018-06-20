using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_Teleport : MonoBehaviour
{
    [Range(0.0f, 100.0f)] public float _TeleportDistance;
    public bool _ShowDebug = false;

    private OVRCameraRig _CameraRig = null;

    // Use this for initialization
    void Start()
    {
        OVRCameraRig[] CameraRigs = gameObject.GetComponentsInChildren<OVRCameraRig>();

        if (CameraRigs.Length == 0)
            Debug.LogWarning("OVRPlayerController: No OVRCameraRig attached.");
        else if (CameraRigs.Length > 1)
            Debug.LogWarning("OVRPlayerController: More then 1 OVRCameraRig attached.");
        else
            _CameraRig = CameraRigs[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire3"))
        {
            if (_ShowDebug)
            {
                Debug.Log("Teleporting Forward");
            }

            Transform centerEye = _CameraRig.centerEyeAnchor;
            Vector3 lookDir = centerEye.TransformDirection(Vector3.forward);
            Vector3 moveDir = new Vector3(lookDir.x, 0, lookDir.z);

            transform.Translate(moveDir.normalized * _TeleportDistance);
        }
    }
}
