using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderUpperTrigger : MonoBehaviour {

    public static LadderUpperTrigger instance;

    public BoxCollider bxCol;
    public bool isbxCol;

    void Awake()
    {
        LadderUpperTrigger.instance = this;
    }

	// Use this for initialization
	void Start () {
        isbxCol = false;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isbxCol = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isbxCol = false;
        }
    }
}
