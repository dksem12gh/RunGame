using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronWallCtr : MonoBehaviour {

    GameObject Wall; // 철문 불러오자

    public float WallSpeed; //철문 올라가는 속도

    bool WallUp;    //장치 작동 불값
    float Wall_y;   //값 변환용
    float Wall_pt;  //값 변환용
    float Wall_yy;  //초기값 저장용
    float Wall_ptpt;//초기값 저장용

	// Use this for initialization
	void Start () {
        Wall = GameObject.Find("Cube");
        Wall_y = Wall.transform.localScale.y;
        Wall_pt = Wall.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        WallCtrl(WallUp);
	}


    void WallCtrl(bool check)
    {
        if (check)
        {
            Wall_y -= WallSpeed;
            Wall_pt += WallSpeed;
            if (Wall.transform.localScale.y > 0)
            {
                Wall.transform.localScale -= new Vector3(Wall.transform.localRotation.x, Wall_y * Time.smoothDeltaTime, Wall.transform.localRotation.z);
                Wall.transform.Translate(0,Wall_pt*Time.smoothDeltaTime,0);
            }

        }
    }


    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "ston")
        {
            WallUp = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "ston")
        {
            WallUp = false;
        }
    }
}
