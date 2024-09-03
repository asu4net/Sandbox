using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
public class PlayerSettings : ScriptableObject
{
    public float speed = 2.0f;
    public float gravitySpeed = -9.81f;
    public float clampAngle = 80f;
    public Vector2 lookSpeed = Vector2.one * 10f;
}