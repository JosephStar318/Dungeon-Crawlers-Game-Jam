using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHelper : MonoBehaviour
{
    public static PlayerInputHelper Instance { get; private set; }

    public PlayerInputActions playerInputActions;
    private PlayerInput playerInput;

    public static event Action<Vector2> OnMoveChanged;
    public static event Action OnInterractPressed;
    public static event Action OnAttack;
    public static event Action OnSwap;
    public static event Action<bool> OnDrawing;

    public string CurrentControlScheme { get; private set; } = "KeyboardMouse";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.Enable();

        playerInputActions.Player.Move.performed += Move_performed;
        playerInputActions.Player.Interract.performed += Interract_performed;
        playerInputActions.Player.Attack.performed += Attack_performed;
        playerInputActions.Player.Drawing.performed += Drawing;
        playerInputActions.Player.Drawing.canceled += Drawing;
        playerInputActions.Player.Swap.performed += Swap_performed;

        playerInput.onControlsChanged += PlayerInput_onControlsChanged;
    }
 
    private void OnDestroy()
    {
        playerInputActions.Player.Move.performed -= Move_performed;
        playerInputActions.Player.Interract.performed -= Interract_performed;
        playerInputActions.Player.Attack.performed -= Attack_performed;
        playerInputActions.Player.Drawing.performed += Drawing;
        playerInputActions.Player.Drawing.canceled -= Drawing;

        playerInput.onControlsChanged += PlayerInput_onControlsChanged;
    }

    private void Swap_performed(InputAction.CallbackContext obj)
    {
        OnSwap?.Invoke();
    }

    private void Drawing(InputAction.CallbackContext obj)
    {
        OnDrawing?.Invoke(obj.ReadValueAsButton());
    }

    private void PlayerInput_onControlsChanged(PlayerInput obj)
    {
        CurrentControlScheme = playerInput.currentControlScheme;
    }


    private void Attack_performed(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }

    private void Interract_performed(InputAction.CallbackContext obj)
    {
        OnInterractPressed?.Invoke();
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        OnMoveChanged?.Invoke(obj.ReadValue<Vector2>());
    }
}
