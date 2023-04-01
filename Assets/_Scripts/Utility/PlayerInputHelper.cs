using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHelper : MonoBehaviour
{
    public static PlayerInputHelper Instance { get; private set; }

    public PlayerInputActions playerInputActions;

    public static event Action<Vector2> OnMoveChanged;
    public static event Action OnInterractPressed;

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
        playerInputActions.Enable();

        playerInputActions.Player.Move.performed += Move_performed;
        playerInputActions.Player.Interract.performed += Interract_performed;
    }

    private void Interract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInterractPressed?.Invoke();
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMoveChanged?.Invoke(obj.ReadValue<Vector2>());
    }
}
