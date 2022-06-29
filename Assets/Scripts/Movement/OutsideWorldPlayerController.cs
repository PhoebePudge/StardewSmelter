using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OutsideWorldPlayerController : MonoBehaviour {
    //variables
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;
    private Animator anim;

    private static OutsideWorldPlayerController instance = null;
    private void Start() {
        //set instance to this on first time
        if (instance == null) {
            instance = this;
        }
        //destroy instance if we already have one
        if (instance != this) {
            Destroy(gameObject);
        }

        //set not to destroy on load
        GameObject.DontDestroyOnLoad(this.gameObject);

        //set variables
        controller = gameObject.AddComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }
    private void OnLevelWasLoaded(int level) { 
        if (level == 1) {
            transform.position = new Vector3(0.81f, 1.4f, 1.89f);
        }
    }
    void Update() {
        if (!ToggleConsole.displayed) {

            //sort out grounded
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            }

            //get rotation
            Quaternion rot = Camera.main.transform.rotation;
            rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);

            Vector3 inputRot = Vector3.zero;

            //get input
            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)) {
                inputRot = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            }

            //sort out animations when there is velocity
            Vector3 move = rot * inputRot;
            if (move != Vector3.zero) {
                gameObject.transform.forward = move;
                anim.SetBool("Running", true);
            } else {
                anim.SetBool("Running", false);
            }

            //sprint on shift
            if (Input.GetKey(KeyCode.LeftShift)) { 
                anim.SetInteger("Speed", 1);
                playerSpeed = 8.0f;
            } else { 
                anim.SetInteger("Speed", 0);
                playerSpeed = 4.0f;
            }

            //return before translation if we are playing the attack animation
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
                return;
            }

            //actually move
            controller.Move(move * Time.deltaTime * playerSpeed); 
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
