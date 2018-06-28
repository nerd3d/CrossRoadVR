using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point_Teleport : MonoBehaviour
{
    private enum Hand { NONE, LEFT, RIGHT };

    [Range(0.0f, 100.0f)] public float _TeleportDistance;
    public bool _ShowDebug;
    public GameObject _TargetPrefab;
    public bool _ActiveAim;

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

        if (Input.GetButton("Fire1") || Input.GetButton("Fire3"))
        {
            Transform handAnchor;
            Vector3 lookDir;
            Vector3 moveDir;
            Vector3 moveLoc;

            if (_InHand == Hand.NONE && Input.GetButtonDown("Fire1"))
            {
                _InHand = Hand.RIGHT;
            }
            else if (_InHand == Hand.NONE && Input.GetButtonDown("Fire3"))
            {
                _InHand = Hand.LEFT;
            }

            if (_InHand == Hand.RIGHT)
            {
                handAnchor = _CameraRig.rightHandAnchor;
            }
            else // must be LEFT
            {
                handAnchor = _CameraRig.leftHandAnchor;
            }

            lookDir = handAnchor.TransformDirection(Vector3.forward);
            moveDir = new Vector3(lookDir.x, 0, lookDir.z);
            moveLoc = moveDir.normalized * _TeleportDistance + transform.position;

            if (_TargetInstance == null)
            {
                _TargetInstance = Instantiate(_TargetPrefab, moveLoc, transform.rotation);
            }
            else if (_ActiveAim)
            {
                _TargetInstance.transform.position = moveLoc;
                _TargetInstance.transform.rotation = transform.rotation;
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
