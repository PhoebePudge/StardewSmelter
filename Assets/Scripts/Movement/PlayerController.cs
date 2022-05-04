using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private Animator anim;
    private void Start() {
        controller = gameObject.AddComponent<CharacterController>();
        controller.radius = .3f; 
        anim = GetComponentInChildren<Animator>();
    }

    void Update() {
        if (!ToggleConsole.displayed) {


            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero) {
                gameObject.transform.forward = move;
                anim.SetBool("Running", true);
            } else {
                anim.SetBool("Running", false);
            }

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer) {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);


            if (Input.GetKey(KeyCode.LeftShift)) {
                anim.speed = 2;
                playerSpeed = 4.0f;
            } else {
                playerSpeed = 2.0f;
                anim.speed = 1;
            }
        }
    }
}
