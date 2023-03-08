using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 90f;
    public Rigidbody rb;
    public Animator animator;
    public GameObject model;

    private Vector2 movementInput;

    private bool isWalking;
    private bool isRunning;

    private void Start()
    {
        GameManager.Instance.PlayerControls.Gameplay.MoveForward.performed += OnMovementPerformed;
        GameManager.Instance.PlayerControls.Gameplay.MoveForward.canceled += OnMovementCanceled;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerControls.Gameplay.MoveForward.performed -= OnMovementPerformed;
        GameManager.Instance.PlayerControls.Gameplay.MoveForward.canceled -= OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        float magnitude = movementInput.magnitude;
        isWalking = magnitude <= 0.7f;
        isRunning = magnitude > 0.7f;

        if (isRunning)
            moveSpeed = 8;
        else
            moveSpeed = 5;
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
        isWalking = false;
        isRunning = false;
    }

    void Update()
    {
        // Move the character
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Rotate the character
        if (movement != Vector3.zero)
        {
            model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, Quaternion.LookRotation(movement.normalized), turnSpeed * Time.deltaTime);
        }

        // Update animator
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
    }
}
