using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour {
    public const float WALK_SPEED = 4f;     // m/s
    public const float RUN_SPEED = 12;      // m/s
    public const float JUMP_FORCE = 100f;    // m/s
    public const float RB_SPEED_MULTIPLIER = 1000;
    public const float HEIGHT = 1.8f;

    private Rigidbody rb;

    private Vector3 motion;
    private float xMovementSpeed;
    private float zMovementSpeed;
    private float speedMultiplier;
    private float jumpForce = -Physics.gravity.y * 50f;

    private Inventory inventory;

    void Start() {
        rb = GetComponent<Rigidbody>();
        inventory = new Inventory(3);
    }

    void FixedUpdate() {
        GravityHandler();
        HorizontalMovementHandler();
        JumpHandler();
    }

    public Inventory GetInventory() { 
        return inventory;
    }


    void GravityHandler() {
        rb.AddForce(new Vector3(0, Physics.gravity.y * 2f, 0), ForceMode.Acceleration);
    }

    void HorizontalMovementHandler() {
        Vector3 maximumVelocity = IsRunning() ? new(RUN_SPEED, 0, RUN_SPEED) : new(WALK_SPEED, 0, WALK_SPEED);
        speedMultiplier = IsRunning() ? RUN_SPEED : WALK_SPEED;

        // Apply horizontal movement (Walk)
        Quaternion qrot = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        xMovementSpeed = Input.GetAxis("Horizontal") * WALK_SPEED; // No matter if is running or not, this speed is always the walking speed.
        zMovementSpeed = GetXMovementDirection() == MovementDirection.FORWARD ? Input.GetAxis("Vertical") * speedMultiplier : Input.GetAxis("Vertical") * WALK_SPEED; // If going backward, the speed is always the walking speed. Otherwise checks if is running or not
        
        motion = qrot * new Vector3 (xMovementSpeed, 0, zMovementSpeed);

        // Limits the velocity on X-Z
        motion = Vector3.ClampMagnitude(motion, IsRunning() ? RUN_SPEED : WALK_SPEED) + 
            new Vector3(0, rb.velocity.y, 0);

        rb.velocity = new Vector3(motion.x, rb.velocity.y, motion.z);
    }

    void JumpHandler() {
        if (IsJumping() && IsGrounded()) {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Acceleration);
        }
    }

    public bool IsJumping() { 
        return Input.GetKey(KeyCode.Space);
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, HEIGHT + 0.1f, Physics.IgnoreRaycastLayer);
    }

    public bool IsMoving() { 
        return GetXMovementDirection() != MovementDirection.NONE || GetZMovementDirection() != MovementDirection.NONE;
    }

    public bool IsRunning() {
        return Input.GetKey(KeyCode.LeftControl) && GetXMovementDirection() == MovementDirection.FORWARD;
    }

    public MovementDirection GetZMovementDirection() {
        if (Input.GetAxis("Horizontal") < 0) {
            return MovementDirection.LEFT;
        } else if (Input.GetAxis("Horizontal") > 0) {
            return MovementDirection.RIGHT;
        }

        return MovementDirection.NONE;
    }

    public MovementDirection GetXMovementDirection() {
        if (Input.GetAxis("Vertical") > 0) {
            return MovementDirection.FORWARD;
        } else if (Input.GetAxis("Vertical") < 0) {
            return MovementDirection.BACKWARD;
        }

        return MovementDirection.NONE;
    }

    public Vector3 GetMovement()
    {
        return motion;
    }

    public enum MovementDirection { LEFT, RIGHT, FORWARD, BACKWARD, NONE }
}
