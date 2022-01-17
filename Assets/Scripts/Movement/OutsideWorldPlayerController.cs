using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWorldPlayerController : MonoBehaviour
{
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform cameraPoint;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    private void Start() {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update() {
        if (!ToggleConsole.displayed) {


            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            } 

            Quaternion rot = cameraPoint.rotation;
            rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);

            Vector3 move = rot * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero) {
                gameObject.transform.forward = move;
            }


            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);


            if (Input.GetKey(KeyCode.LeftShift)) {
                playerSpeed = 4.0f;
            } else {
                playerSpeed = 2.0f;
            }
        }
    }
}