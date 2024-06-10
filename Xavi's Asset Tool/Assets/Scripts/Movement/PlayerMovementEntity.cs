using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementEntity : CreatureMovement
{
    [SerializeField] Camera PlayerCamera;
    [SerializeField] float MouseSensitivity;
    [SerializeField] float CameraAngleLimits;
    [SerializeField] float SprintMultiplier;
    [SerializeField] string FloorTag;
    [SerializeField] KeyCode SprintKey;
    [SerializeField] MenuManager MenuManager;

    public override void AnimationLogic()
    {
        base.AnimationLogic();
    }

    public override void MovementLogic()
    {
        base.MovementLogic();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = Vector3.zero;

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
            inputDirection = RetrieveInputDirection();

        if (inputDirection != Vector3.zero)
            EntityRb.velocity = Vector3.Lerp(EntityRb.velocity, inputDirection, Time.deltaTime * RetrieveSpeedMultiplier(ResetSpeed));
    }

    public override void RotationLogic()
    {
        base.RotationLogic();

        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        Quaternion xRotation = new Quaternion(0, mouseX, 0, 1);
        Quaternion yRotation = new Quaternion(-mouseY, 0, 0, 1);

        if (mouseX != 0 && !MenuManager.HaveOpenMenu)
            transform.rotation *= xRotation;
        if (mouseY != 0 && !MenuManager.HaveOpenMenu)
            AssignCameraRotation(yRotation);
    }

    public override void JumpLogic()
    {
        base.JumpLogic();

        float jump = Input.GetAxis("Jump");

        if (jump > 0 && CheckCollisionWithTag(FloorTag))
            EntityRb.velocity = new Vector3(EntityRb.velocity.x, jump * JumpForce, EntityRb.velocity.z);
    }

    private void AssignCameraRotation(Quaternion _rotation)
    {
        PlayerCamera.transform.localRotation *= _rotation;
        Quaternion newRotation = PlayerCamera.transform.localRotation;
        Quaternion limitedAngleRotation = new Quaternion(Mathf.Clamp(newRotation.x, -CameraAngleLimits, CameraAngleLimits), newRotation.y, newRotation.z, newRotation.w);
        PlayerCamera.transform.localRotation = limitedAngleRotation;
    }

    private Vector3 RetrieveInputDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 globalDirection = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 localDirection = transform.TransformDirection(globalDirection);

        Vector3 finalDirection = localDirection.normalized * RetrieveSpeedMultiplier(Velocity);
        finalDirection = new Vector3(finalDirection.x, EntityRb.velocity.y, finalDirection.z);

        return finalDirection;
    }

    private float RetrieveSpeedMultiplier(float _velocity)
    {
        float speed = _velocity;

        if (Input.GetKey(SprintKey))
            speed = _velocity * SprintMultiplier;

        return speed;
    }
}
