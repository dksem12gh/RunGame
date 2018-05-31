using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCtrl : MonoBehaviour
{
    public static LadderCtrl instance;
    public GameObject ladder;

    public BoxCollider BoxCol;
    public bool isBlocked;

    PlayerCtrl player;

    void Awake()
    {
        LadderCtrl.instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }

    void Start()
    {
        isBlocked = false;
    }

    void Update()
    {
        if (LadderUpperTrigger.instance.isbxCol && !LadderUnderTrigger.instance.isbxCol)
        {
            BoxCol.enabled = true;
            isBlocked = true;
        }

        if (isBlocked)
        {
            ladder.tag = "Untagged";
            player.isGrabLadder = false;
            player.isOnLadder = false;
        }
    }

}
