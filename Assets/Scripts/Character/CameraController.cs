using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject FollowTarget;
    [SerializeField] private float RotationSpeed = 1.0f;
    [SerializeField] private float HorizontalDamping = 1.0f;

    private Transform FollowTargetTransform;

    private Vector2 PreviousMouseInput;

    // Start is called before the first frame update
    void Start()
    {
        FollowTargetTransform = FollowTarget.transform;
        PreviousMouseInput = Vector2.zero;
    }

    public void OnLook(InputValue delta)
    {
        Vector2 aimValue = delta.Get<Vector2>();

        FollowTargetTransform.rotation *=
            Quaternion.AngleAxis(
                    Mathf.Lerp(PreviousMouseInput.x, aimValue.x, 1.0f / HorizontalDamping) * RotationSpeed,
                    transform.up
                );

        transform.rotation = Quaternion.Euler(0, FollowTargetTransform.transform.rotation.eulerAngles.y, 0);

        FollowTargetTransform.localEulerAngles = Vector3.zero;

        PreviousMouseInput = aimValue;
    }
}
