using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInputController), typeof(HUDInputController))]
[RequireComponent(typeof(PlayerInventoryController))]
public class PlayerController : MonoBehaviour, IInteractor
{
  public GameObject playerCamera;
  public float moveSpeed = 10.0f;
  public float sprintMultiplier = 1.75f;
  public float sensitivity = 1.0f;
  public float jumpForce = 5.0f;
  public float gravity = 20f;
  public float rechargeSpeed = 0.25f;

  private CharacterController _controller;
  private PlayerInputController _input;
  private HUDInputController _hud;
  private PlayerInventoryController _inventory;

  private float verticalSpeed = 0f;
  private float xRot = 0.0f;

  void Start()
  {
    _controller = GetComponent<CharacterController>();
    _input = GetComponent<PlayerInputController>();
    _hud = GetComponent<HUDInputController>();
    _inventory = GetComponent<PlayerInventoryController>();
  }

  void Update()
  {
    //Moving
    {
      float speed = moveSpeed;
      if (_input.GetSprintPressed())
      {
        speed *= sprintMultiplier;
      }
      Vector3 moveVec = transform.TransformDirection(_input.GetMovementValue()) * speed;

      float gravityMultiplier = verticalSpeed < 0f ? 2f : 1f;
      verticalSpeed = _controller.isGrounded ? 0f : verticalSpeed - gravity * Time.deltaTime * gravityMultiplier;

      //Jump
      if (_controller.isGrounded && _input.GetJumpPressed())
      {
        verticalSpeed = jumpForce;
      }

      moveVec.y = verticalSpeed;

      _controller.Move(moveVec * Time.deltaTime);
    }

    //Looking
    {
      Vector2 realLookValue = _input.GetLookValue() * 0.5f * 0.1f; //No se porque, eso esta en google...

      xRot -= realLookValue.y * sensitivity;
      xRot = Mathf.Clamp(xRot, -69f, 69f);

      transform.Rotate(new Vector3(0f, (realLookValue.x * sensitivity), 0f), Space.Self);
      playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
    }

    //Weapon
    {
      _inventory.ShowCurrent();
    }

    //Shoot
    {
      if (_input.GetShootPressed())
      {
        _inventory.Shoot();
      }
      if (_input.GetThrowPressedOnce())
      {
        _inventory.Throw(1);
      }
    }
  }

  public bool InteractOnce(IInteractable interactable)
  {
    _hud.ShowInteractionText(true);

    return false;
  }

  public bool InteractEnd(IInteractable interactable)
  {
    _hud.ShowInteractionText(false);
    return false;
  }

  public bool InteractContinous(IInteractable interactable)
  {
    if (!_input.GetInteracted())
    {
      return false;
    }

    if (interactable.GetType() == InteractableType.Weapon)
    {
      _inventory.ChangePrimary(interactable.GetData<Weapon>());
      return true;
    }

    if (interactable.GetType() == InteractableType.WaterSource)
    {
      _inventory.RechargePrimary();
      return false;
    }

    if (interactable.GetType() == InteractableType.Throwable)
    {
      _inventory.ChangeThrowable(interactable.GetData<Throwable>());
      return true;
    }

    return false;
  }
}
