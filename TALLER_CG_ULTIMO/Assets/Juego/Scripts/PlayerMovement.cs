using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f; // grados/seg

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 8f;

    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Optional")]
    public Camera mouseOrbitCamera;

    private CharacterController controller;
    private Animator anim;

    // Input System
    private Vector2 moveInput;
    private Vector3 velocity;

    // Animator (solo caminar/idle)
    private static readonly int VelX = Animator.StringToHash("velX");
    private static readonly int VelY = Animator.StringToHash("velY");

    private float velXCur, velYCur;
    [SerializeField] private float animDamp = 0.05f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        if (anim != null) anim.applyRootMotion = false;
    }

    // INPUT MOVIMIENTO
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    // INPUT SALTO (solo física, sin animación)
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (controller.isGrounded)
        {
            velocity.y = jumpForce;
        }
    }

    private void Update()
    {
        // ========== MOVIMIENTO ==========
        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 moveWorld;
        if (mouseOrbitCamera != null && mouseOrbitCamera.gameObject.activeInHierarchy)
        {
            Vector3 camFwd = mouseOrbitCamera.transform.forward;
            camFwd.y = 0f;
            camFwd.Normalize();

            Vector3 camRight = mouseOrbitCamera.transform.right;
            camRight.y = 0f;
            camRight.Normalize();

            moveWorld = camRight * input.x + camFwd * input.z;
        }
        else
        {
            moveWorld = transform.right * input.x + transform.forward * input.z;
        }

        // Rotación hacia movimiento
        Vector3 lookDir = new Vector3(moveWorld.x, 0f, moveWorld.z);
        if (lookDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        Vector3 horizontal = moveWorld * moveSpeed;
        controller.Move(horizontal * Time.deltaTime);
        // =================================

        // GRAVEDAD Y SALTO (física)
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // ANIMACIÓN (Blend Tree de caminar/idle)
        velXCur = Mathf.SmoothDamp(velXCur, moveInput.x, ref velXCur, animDamp);
        velYCur = Mathf.SmoothDamp(velYCur, moveInput.y, ref velYCur, animDamp);

        anim.SetFloat(VelX, velXCur);
        anim.SetFloat(VelY, velYCur);
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//[RequireComponent(typeof(CharacterController))]
//public class PlayerMovement : MonoBehaviour
//{
//    [Header("Movement")]
//    [SerializeField] private float moveSpeed = 5f;
//    [SerializeField] private float rotationSpeed = 720f; // grados/seg

//    [Header("Jump Settings")]
//    [SerializeField] private float jumpForce = 8f;

//    [Header("Physics")]
//    [SerializeField] private float gravity = -9.81f;

//    [Header("Optional")]
//    public Camera mouseOrbitCamera;

//    private CharacterController controller;
//    private Animator anim;

//    // Nuevo Input System
//    private Vector2 moveInput;
//    private Vector3 velocity;

//    // Animator
//    private static readonly int VelX = Animator.StringToHash("velX");
//    private static readonly int VelY = Animator.StringToHash("velY");
//    private static readonly int Salte = Animator.StringToHash("salte");
//    private static readonly int Cayendo = Animator.StringToHash("cayendo");

//    private float velXCur, velYCur;
//    [SerializeField] private float animDamp = 0.05f;

//    private void Awake()
//    {
//        controller = GetComponent<CharacterController>();
//        anim = GetComponent<Animator>();
//    }

//    // INPUT MOVIMIENTO (NO TOCADO)
//    public void OnMove(InputAction.CallbackContext ctx)
//    {
//        moveInput = ctx.ReadValue<Vector2>();
//    }


//    public void OnJump(InputAction.CallbackContext ctx)
//    {
//        if (!ctx.performed) return;

//        if (controller.isGrounded)   // SOLO SALTA SI TOCA SUELO
//        {
//            velocity.y = jumpForce;
//            anim.SetTrigger(Salte);  // animación de salto
//        }
//    }

//    private void Update()
//    {
//        // ========== MOVIMIENTO (TAL COMO LO PEDISTE, INTACTO) ==========
//        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y);

//        Vector3 moveWorld;
//        if (mouseOrbitCamera != null && mouseOrbitCamera.gameObject.activeInHierarchy)
//        {
//            Vector3 camFwd = mouseOrbitCamera.transform.forward;
//            camFwd.y = 0f;
//            camFwd.Normalize();

//            Vector3 camRight = mouseOrbitCamera.transform.right;
//            camRight.y = 0f;
//            camRight.Normalize();

//            moveWorld = camRight * input.x + camFwd * input.z;
//        }
//        else
//        {
//            moveWorld = transform.right * input.x + transform.forward * input.z;
//        }

//        Vector3 lookDir = new Vector3(moveWorld.x, 0f, moveWorld.z);
//        if (lookDir.sqrMagnitude > 0.0001f)
//        {
//            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
//            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
//        }

//        Vector3 horizontal = moveWorld * moveSpeed;
//        controller.Move(horizontal * Time.deltaTime);
//        // ================================================================

//        // GRAVEDAD + CAÍDA
//        if (controller.isGrounded && velocity.y < 0f)
//        {
//            velocity.y = -2f;
//            anim.SetBool(Cayendo, false); // dejó de caer
//        }
//        else if (!controller.isGrounded)
//        {
//            anim.SetBool(Cayendo, true); // activa animación de caer
//        }

//        velocity.y += gravity * Time.deltaTime;
//        controller.Move(velocity * Time.deltaTime);

//        // BLEND TREE (INACTO, SOLO ANIMACIÓN DE CAMINAR)
//        velXCur = Mathf.SmoothDamp(velXCur, moveInput.x, ref velXCur, animDamp);
//        velYCur = Mathf.SmoothDamp(velYCur, moveInput.y, ref velYCur, animDamp);
//        anim.SetFloat(VelX, velXCur);
//        anim.SetFloat(VelY, velYCur);
//    }
//}

