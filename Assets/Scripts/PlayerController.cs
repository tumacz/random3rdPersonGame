using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private readonly float gravityValue = -9.81f;

    [SerializeField] private CannonBallSpawner spawner;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private GameObject gun;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterController controller;

    private Vector3 playerVelocity;
    private bool isGroundedPlayer;
    private bool canShoot = true;

    private InputAction shootAction;
    private InputAction moveAction;
    private InputAction jumpAction;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        shootAction = playerInput.actions["Shoot"];
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    private void Update()
    {
        RotationControl();
        ShootControl();
        GroundingControl();
        MovementControl();
        JumpControl();
    }

    private void RotationControl()
    {
        float hightAngle = cameraTransform.eulerAngles.x + 90f;
        float widthAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, widthAngle, 0);
        Quaternion hightRotation = Quaternion.Euler(hightAngle, widthAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, playerSettings.RotationSpeed * Time.deltaTime);
        gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, hightRotation, playerSettings.RotationSpeed * Time.deltaTime);
    }

    private void GroundingControl()
    {
        isGroundedPlayer = controller.isGrounded;
        if (isGroundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }

    private void MovementControl()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSettings.PlayerSpeed);
    }

    private void JumpControl()
    {
        if (jumpAction.triggered && isGroundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(playerSettings.JumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void ShootControl()
    {
        if (shootAction.IsPressed()&& canShoot)
        {
            StartCoroutine(ShootGun());
        }
    }

    private IEnumerator ShootGun()
    {
        canShoot = false;
        spawner.ShootGun();
        yield return new WaitForSeconds(playerSettings.ShootDelayTime);
        canShoot = true;
    }
}