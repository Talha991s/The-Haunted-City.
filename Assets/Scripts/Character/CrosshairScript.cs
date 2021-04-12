using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairScript : MonoBehaviour
{
    public Vector2 CurrentMousePosition { get; private set; }

    public bool Inverted = false;
    public Vector2 MouseSensitivity = Vector2.zero;

    [SerializeField, Range(0.0f, 1.0f)]
    private float HorizontalPercentageContraint;

    [SerializeField, Range(0.0f, 1.0f)]
    private float VerticalPercentageContraint;

    private Vector2 CrosshairStartingPoint;

    private Vector2 CurrentLookDelta = Vector2.zero;

    private float HorizontalConstraint;
    private float VerticalConstraint;

    private float MinHorizontalContraintValue;
    private float MaxHorizontalContraintValue;

    private float MinVerticalContraintValue;
    private float MaxVerticalContraintValue;

    private GameInputActions InputActions;

    private void Awake()
    {
        InputActions = new GameInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.CursorActive)
            AppEvents.Invoke_MouseCursorEnable(false);

        CrosshairStartingPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        HorizontalConstraint = (Screen.width * HorizontalPercentageContraint) * 0.5f;
        MinHorizontalContraintValue = -(Screen.width * 0.5f) + HorizontalConstraint;
        MaxHorizontalContraintValue = (Screen.width * 0.5f) - HorizontalConstraint;

        VerticalConstraint = (Screen.height * VerticalPercentageContraint) * 0.5f;
        MinVerticalContraintValue = -(Screen.height * 0.5f) + VerticalConstraint;
        MaxVerticalContraintValue = (Screen.height * 0.5f) - VerticalConstraint;
    }

    // Update is called once per frame
    void Update()
    {
        float crosshairXPosition = CrosshairStartingPoint.x + CurrentLookDelta.x;
        float crosshairYPosition = Inverted
            ? CrosshairStartingPoint.y - CurrentLookDelta.y
            : CrosshairStartingPoint.y + CurrentLookDelta.y;

        CurrentMousePosition = new Vector2(crosshairXPosition, crosshairYPosition);

        transform.position = CurrentMousePosition;
    }

    private void OnLook(InputAction.CallbackContext delta)
    {
        Vector2 mouseDelta = delta.ReadValue<Vector2>();

        CurrentLookDelta.x += mouseDelta.x * MouseSensitivity.x;
        if (CurrentLookDelta.x >= MaxHorizontalContraintValue || CurrentLookDelta.x <= MinHorizontalContraintValue)
        {
            CurrentLookDelta.x -= mouseDelta.x * MouseSensitivity.x;
        }

        CurrentLookDelta.y += mouseDelta.y * MouseSensitivity.y;
        if (CurrentLookDelta.y >= MaxVerticalContraintValue || CurrentLookDelta.y <= MinVerticalContraintValue)
        {
            CurrentLookDelta.y -= mouseDelta.y * MouseSensitivity.y;
        }
    }

    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.ThirdPerson.Look.performed += OnLook;
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
