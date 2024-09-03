using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Vector3 horizontalVelocity;
    public Vector3 verticalVelocity;
    public Transform cameraTransform;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        var moveDir = InputManager.GetInstance().GetPlayerMovement();
        moveDir = cameraTransform.forward * moveDir.z + cameraTransform.right * moveDir.x;

        PlayerSettings playerSettings = GameManager.GetInstance().playerSettings;
        horizontalVelocity = moveDir * playerSettings.speed * Time.deltaTime;
        verticalVelocity += Vector3.up * playerSettings.gravitySpeed * Time.deltaTime;
        characterController.Move(horizontalVelocity + verticalVelocity);
    }
}
