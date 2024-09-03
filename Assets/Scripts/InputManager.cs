using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public static InputManager GetInstance()
    {
        return TryCreateSingleton();
    }

    public static InputManager TryCreateSingleton()
    {
        if (!instance && Application.isPlaying)
        {
            var inputManagerObject = new GameObject("Input Manager");
            var inputManager = inputManagerObject.AddComponent<InputManager>(); 

            instance = inputManager;
            DontDestroyOnLoad(instance);
        }

        return instance;
    }

    public static void TryMakeSingleton(InputManager inputManager)
    {
        if (!instance)
        {
            instance = inputManager;
            DontDestroyOnLoad(instance);
            return;
        }
        else if (instance && instance != inputManager)
        {
            Destroy(instance);
        }
    }

    public PlayerControls playerControls;

    public Vector3 GetPlayerMovement() 
    {
        Vector2 inputValue = playerControls.Default.Movement.ReadValue<Vector2>(); 
        return new Vector3(inputValue.x, 0, inputValue.y);
    }

    public Vector2 GetPlayerLook() 
    {
        return playerControls.Default.Look.ReadValue<Vector2>();
    }

    void Awake()
    {
        TryMakeSingleton(this);
        playerControls = new PlayerControls();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        playerControls?.Enable();
    }

    void OnDisable()
    {
        playerControls?.Disable();
    }
}