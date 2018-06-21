using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Look_Teleport : MonoBehaviour
{
    private enum Hand { NONE, LEFT, RIGHT };

    [Range(0.0f, 100.0f)] public float _TeleportDistance;
    public bool _ShowDebug;
    public GameObject _TargetPrefab;

    private OVRCameraRig _CameraRig = null;
    private GameObject _TargetInstance = null;
    private Hand _InHand = Hand.NONE;

    // Use this for initialization
    void Start()
    {
        if (_TargetPrefab == null)
        {
            Debug.LogError(name + ": Missing Teleport Preview Prefab");
            enabled = false;
            return;
        }

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
        Transform centerEye = _CameraRig.centerEyeAnchor;
        Vector3 lookDir = centerEye.TransformDirection(Vector3.forward);
        Vector3 moveDir = new Vector3(lookDir.x, 0, lookDir.z);
        Vector3 moveLoc = moveDir.normalized * _TeleportDistance + transform.position;

        if (_InHand == Hand.NONE && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire3")) )
        {
            if (_TargetInstance == null)
            {
                _TargetInstance = Instantiate(_TargetPrefab, moveLoc, transform.rotation);
                if (Input.GetButtonDown("Fire1"))
                {
                    _InHand = Hand.RIGHT;
                }
                else
                {
                    _InHand = Hand.LEFT;
                }
            }
            else
            {
                return;
            }
        }

        if ((_InHand == Hand.RIGHT && Input.GetButtonUp("Fire1")) 
            || (_InHand == Hand.LEFT && Input.GetButtonUp("Fire3")))
        {
            if (_TargetInstance != null)
            {
                transform.position = _TargetInstance.transform.position;
                Destroy(_TargetInstance);
                _TargetInstance = null;
                _InHand = Hand.NONE;
            }
            else
            {
                return;
            }

            if (_ShowDebug)
            {
                Debug.Log("Teleporting Forward");
            }

        }
    }
}
