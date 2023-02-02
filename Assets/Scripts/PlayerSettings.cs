using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "player settings")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float shootPower =100;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float shootDelayTime = 1f;

    public float PlayerSpeed => playerSpeed;
    public float JumpHeight => jumpHeight;
    public float RotationSpeed => rotationSpeed;
    public float ShootDelayTime => shootDelayTime;
    public float ShootPower => shootPower;
}
