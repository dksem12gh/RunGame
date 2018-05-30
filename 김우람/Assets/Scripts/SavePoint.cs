using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    GameObject player;

    public List<Vector3> playerPos = new List<Vector3>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = playerPos[0];
        }
    }

    void savePt(Vector3 pt)
    {
        if (playerPos.Count < 1) //일단 저장하는 좌표는 하나
        {
            playerPos.Add(pt);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            savePt(other.gameObject.transform.position);
        }
    }
}
