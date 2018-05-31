using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCtrl : MonoBehaviour {

    PlayerCtrl player;

    public enum GRAB {GRAB_ON, GRAB_OFF, GRAB_MOVE};
    GRAB _grab;
    public bool isOnLadder;

	// Use this for initialization
	void Start () {
;       player = GetComponent<PlayerCtrl>();
	}
	
	// Update is called once per frame
	void Update () {
        GrabLadder(player.isGrabLadder, player.rb);
	}

    void GrabLadder(bool grab, Rigidbody rb)
    {
        if (!grab)
        {
            _grab = GRAB.GRAB_OFF;
            isOnLadder = false;
            Physics.gravity = new Vector3(0, -20, 0);
            return;
        }
        else
        {
            _grab = GRAB.GRAB_MOVE;
            isOnLadder = true;

            Physics.gravity = new Vector3(0, 0, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
