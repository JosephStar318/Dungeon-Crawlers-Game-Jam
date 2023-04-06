using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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

    public static event Action OnPause;
    public static event Action OnCancelUI;
    public static event Action OnBackUI;

    public EventSystem Eventsystem { get; private set; }
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
        Eventsystem = EventSystem.current;
        playerInputActions.Enable();

        playerInputActions.Player.Move.performed += Move_performed;
        playerInputActions.Player.Interract.performed += Interract_performed;
        playerInputActions.Player.Attack.performed += Attack_performed;
        playerInputActions.Player.Drawing.performed += Drawing;
        playerInputActions.Player.Drawing.canceled += Drawing;
        playerInputActions.Player.Swap.performed += Swap_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;

        playerInputActions.UI.Cancel.performed += Cancel_performed;
        playerInputActions.UI.Back.performed += Back_performed;

        playerInput.onControlsChanged += PlayerInput_onControlsChanged;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Move.performed -= Move_performed;
        playerInputActions.Player.Interract.performed -= Interract_performed;
        playerInputActions.Player.Attack.performed -= Attack_performed;
        playerInputActions.Player.Drawing.performed -= Drawing;
        playerInputActions.Player.Drawing.canceled -= Drawing;
        playerInputActions.Player.Swap.performed -= Swap_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.UI.Cancel.performed -= Cancel_performed;
        playerInputActions.UI.Back.performed -= Back_performed;

        playerInput.onControlsChanged -= PlayerInput_onControlsChanged;
    }
    public void ChangeActionMap(string mapName)
    {
        foreach (var actionMap in playerInput.actions.actionMaps)
        {
            if (actionMap.name == mapName)
            {
                actionMap.Enable();
                playerInput.currentActionMap = actionMap;
            }
            else actionMap.Disable();
        }
    }
    public void EnableUIActions()
    {
        playerInputActions.Player.Disable();
        playerInputActions.UI.Enable();
        Eventsystem.enabled = true;
    }
    public void EnablePlayerActions()
    {
        playerInputActions.Player.Enable();
        playerInputActions.UI.Disable();
    }
    public void DisableActions()
    {
        playerInputActions.Player.Disable();
        playerInputActions.UI.Disable();
        Eventsystem.enabled = false;

    }
    public InputActionMap GetCurrentActionMap()
    {
        return playerInput.currentActionMap;
    }
    private void Back_performed(InputAction.CallbackContext obj)
    {
        OnBackUI?.Invoke();
    }
    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke();
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

    private void Cancel_performed(InputAction.CallbackContext obj)
    {
        OnCancelUI?.Invoke();
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
