using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementEntityFramework : Movement
{
    [SerializeField] Camera PlayerCamera;
    [SerializeField] float MouseSensitivity;
    [SerializeField] float CamerAngleLimits;
    [SerializeField] string FloorTag;

    public override void MovementStateMachine()
    {
        base.MovementStateMachine();
    }

    public override void MovementLogic()
    {
        base.MovementLogic();

        if (Input.anyKey)
        {
            Vector3 inputDirection = RetrieveInputDirection();
            EntityRb.velocity = Vector3.Lerp(EntityRb.velocity, inputDirection, ResetSpeed);
        }
    }

    public override void RotationLogic()
    {
        base.RotationLogic();

        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        Quaternion xRotation = new Quaternion(0, mouseX, 0, 1);
        Quaternion yRotation = new Quaternion(-mouseY, 0, 0, 1);

        if (mouseX != 0)
            transform.rotation *= xRotation;
        if (mouseY != 0)
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
        Quaternion limitedAngleRotation = new Quaternion(Mathf.Clamp(newRotation.x, -CamerAngleLimits, CamerAngleLimits), newRotation.y, newRotation.z, newRotation.w);
        PlayerCamera.transform.localRotation = limitedAngleRotation;
    }

    private Vector3 RetrieveInputDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 globalDirection = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 localDirection = transform.TransformDirection(globalDirection);

        Vector3 finalDirection = localDirection.normalized * Velocity;
        finalDirection = new Vector3(finalDirection.x, EntityRb.velocity.y, finalDirection.z);

        return finalDirection;
    }
}
