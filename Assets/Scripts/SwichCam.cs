using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwichCam : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Canvas thirdPersonAimCanvas;
    [SerializeField] private Canvas focusAimCanvas;

    private readonly int cameraPriortyAmount = 10;
    private InputAction aimAction;
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        focusAimCanvas.enabled = false;
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        virtualCamera.Priority += cameraPriortyAmount;
        focusAimCanvas.enabled = true;
        thirdPersonAimCanvas.enabled = false;
    }

    private void CancelAim()
    {
        virtualCamera.Priority -= cameraPriortyAmount;
        focusAimCanvas.enabled = false;
        thirdPersonAimCanvas.enabled = true;
    }
}    