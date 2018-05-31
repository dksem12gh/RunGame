using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderUnderTrigger : MonoBehaviour {

    public static LadderUnderTrigger instance;

    public BoxCollider bxCol;
    public bool isbxCol;

    void Awake()
    {
        LadderUnderTrigger.instance = this;
    }

    // Use this for initialization
    void Start()
    {
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
