using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public const float SENSITIVITY = 3F;
    public const float MAX_X_ROT = 30F;
    public const float EYES_HEIGHT = 0.7f;
    public const float COUNT_FREQUENCY = 10;
    public const float WALK_WAVE_MULTIPLIER = 0.0005f, RUN_WAVE_MULTIPLIER = 0.001f;

    public GameObject player;
    private Player playerController;
    private float xRotation;
    private float yRotation;

    private float runningCount;

    void Start() {
        playerController = player.GetComponent<Player>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        CameraRotation();
        WalkMovement();
    }

    void WalkMovement() {
        if(playerController.IsMoving()) {
            Quaternion qrot = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            float verticalWaveSlice = Mathf.Sin(runningCount / 2);
            float horizontalWaveSlice = Mathf.Sin(runningCount);
            runningCount += COUNT_FREQUENCY * Time.deltaTime;
            float waveMultiplier = (playerController.IsRunning() ? RUN_WAVE_MULTIPLIER : WALK_WAVE_MULTIPLIER);

            transform.position = transform.position + (qrot * new Vector3(
                (verticalWaveSlice * waveMultiplier),
                (horizontalWaveSlice * waveMultiplier), 
                0));
        } else if(transform.position != new Vector3(0f, EYES_HEIGHT, 0f)) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position + new Vector3(0f, EYES_HEIGHT, 0f),
                0.005f);

            runningCount = 0;
        }
    }

    void CameraRotation() {

        float mouseX = Input.GetAxis("Mouse X") * SENSITIVITY * 0.65f;
        float mouseY = Input.GetAxis("Mouse Y") * SENSITIVITY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 90f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0f);
        player.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
