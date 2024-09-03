using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    public Vector3 cameraRotation;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, 
        ref CameraState state, 
        float deltaTime)
    {
        if (vcam.Follow && Application.isPlaying)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (cameraRotation == null)
                {
                    cameraRotation = transform.localRotation.eulerAngles;
                }
                
                Vector2 look = InputManager.GetInstance().GetPlayerLook();
                PlayerSettings playerSettings = GameManager.GetInstance().playerSettings;

                cameraRotation.y += look.x * playerSettings.lookSpeed.x * Time.deltaTime;
                cameraRotation.x -= look.y * playerSettings.lookSpeed.y * Time.deltaTime;
                cameraRotation.x = Mathf.Clamp(cameraRotation.x, -playerSettings.clampAngle, playerSettings.clampAngle);
                state.RawOrientation = Quaternion.Euler(cameraRotation);
            }
        }
    }
}
