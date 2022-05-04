using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWorldPlayerController : MonoBehaviour
{  
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;
    private Animator anim;
    private void Start() {
        GameObject.DontDestroyOnLoad(this.gameObject);
        controller = gameObject.AddComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update() { 
        if (!ToggleConsole.displayed) { 

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            } 

            Quaternion rot = Camera.main.transform.rotation;
            rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);

            Vector3 inputRot = Vector3.zero;

            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)) {
                inputRot = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            }

            Vector3 move = rot * inputRot; 
            controller.Move(move * Time.deltaTime * playerSpeed);
            if (move != Vector3.zero) {
                gameObject.transform.forward = move;
                anim.SetBool("Running", true);
            } else {
                anim.SetBool("Running", false);
            }
            

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);


            if (Input.GetKey(KeyCode.LeftShift)) {
                anim.speed = 2;
                playerSpeed = 4.0f;
            } else {
                anim.speed = 1;
                playerSpeed = 2.0f;
            }
        }
    }
}
