using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Optional")]
    public Camera mouseOrbitCamera;

    private CharacterController controller;
    private Animator anim;

    private Vector2 moveInput;
    private Vector3 velocity;

    private static readonly int VelX = Animator.StringToHash("velX");
    private static readonly int VelY = Animator.StringToHash("velY");

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && controller.isGrounded)
        {
            velocity.y = jumpForce;
            anim.SetBool("salte", true);      // ✅ activa animación de salto
            anim.SetBool("tocoSuelo", false);
        }
    }

    private void Update()
    {
        // Movimiento
        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 moveWorld;

        if (mouseOrbitCamera != null && mouseOrbitCamera.gameObject.activeInHierarchy)
        {
            Vector3 camFwd = mouseOrbitCamera.transform.forward;
            camFwd.y = 0;
            camFwd.Normalize();

            Vector3 camRight = mouseOrbitCamera.transform.right;
            camRight.y = 0;
            camRight.Normalize();

            moveWorld = camRight * input.x + camFwd * input.z;
        }
        else
        {
            moveWorld = transform.right * input.x + transform.forward * input.z;
        }

        if (moveWorld.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveWorld, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        controller.Move(moveWorld * moveSpeed * Time.deltaTime);

        // Gravedad
        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
            anim.SetBool("tocoSuelo", true);  // 
            anim.SetBool("salte", false);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Blend Tree movement
        anim.SetFloat(VelX, moveInput.x);
        anim.SetFloat(VelY, moveInput.y);

        // Si no está en el suelo y no está saltando, entonces está cayendo
        if (!controller.isGrounded && velocity.y < 0f)
        {
            anim.SetBool("tocoSuelo", false);
            anim.SetBool("salte", false);      
        }
    }
}

