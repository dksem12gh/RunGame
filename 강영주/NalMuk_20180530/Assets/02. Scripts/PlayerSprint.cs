using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour {
    PlayerCtrl playerCtrl;
    Animator ani;
    float normalSpeed, sprintSpeed;

	// Use this for initialization
	void Start () {
        playerCtrl = (PlayerCtrl)GetComponent<PlayerCtrl>();
        ani = GetComponent<Animator>();
        normalSpeed = playerCtrl.walkSpeed;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        sprintSpeed = normalSpeed * 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftControl) && playerCtrl.isMoving == true) //RTrigger
        {
            playerCtrl.walkSpeed = sprintSpeed;
            ani.SetBool("aniIsSprint", true);
            if (playerCtrl.isMoving == false)
                ani.SetBool("aniIsSprint", false);
        }

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            playerCtrl.walkSpeed = normalSpeed;
            ani.SetBool("aniIsSprint", false);
        }
    }
}
