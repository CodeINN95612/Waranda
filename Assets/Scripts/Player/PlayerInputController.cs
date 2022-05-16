using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private Vector3 _movementValue = new Vector3();
    private Vector2 _lookValue = new Vector3();
    private bool _jumpPressed = false;
    private bool _sprintPressed = false;
    private bool _interacted = false;
    private bool _shoot = false;
    private bool _throw = false;

    public Vector3 GetMovementValue()
    {
        return _movementValue;
    }

    public Vector2 GetLookValue()
    {
        return _lookValue;
    }

    public bool GetJumpPressed()
    {
        return _jumpPressed;
    }

    public bool GetSprintPressed()
    {
        return _sprintPressed;
    }

    public bool GetInteracted()
    {
        return _interacted;
    }

    public bool GetShootPressed()
    {
        return _shoot;
    }

    public bool GetThrowPressedOnce()
    {
        bool wasThrewn = _throw;
        _throw = false;
        return wasThrewn;
    }

    //Movement Actions
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _movementValue = new Vector3(input.x, _movementValue.y, input.y);
    }

    void OnLook(InputValue value)
    {
        _lookValue = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        _jumpPressed = value.isPressed;
    }

    void OnSprint(InputValue value)
    {
        _sprintPressed = value.isPressed;
    }

    void OnInteract(InputValue value)
    {
        _interacted = value.isPressed;
    }

    void OnShoot(InputValue value)
    {
        _shoot = value.isPressed;
    }

    void OnThrow(InputValue value)
    {
        _throw = value.isPressed;
    }
}
