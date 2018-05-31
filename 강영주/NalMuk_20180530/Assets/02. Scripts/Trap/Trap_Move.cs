using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Move : MonoBehaviour {

    PlayerCtrl player;

    [Header("생명력 감소 수치")]
    public int Life_Decrease_Value;         //생명력 감소 수치
    [Header("수평 이동 속도")]
    public float horizontalSpeed;           //수평 이동 속도
    [Header("수직 이동 속도")]
    public float verticalSpeed;             //수직 이동 속도
    [Header("수직 진폭")]
    public float amplitude;                 //수직 진폭


    [Header("-----------------------------------------------------------")]
    public bool Trap_Activation;            //함정 발동
    public bool Life_Decrease;              //생명력 감소 활성화
    float yPos;                             //함정 Y 좌표
    Vector3 tempPosition;                   //좌표
    //int Trap_Time_Code = 2;                 //함정의 횟수 분류 코드 = 2: 지속형

    // Use this for initialization
    void Start () {

        Trap_Activation = false;
        player = PlayerCtrl.instance;

        tempPosition = transform.position;
        yPos = GameObject.Find("Moving").transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        tempPosition.x += horizontalSpeed;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude + yPos;
        transform.position = tempPosition;
	}

    void Update()
    {
        if (Trap_Activation && !player.Invincible)
        {
            TrapActivation();
        }
    }

    void TrapActivation()
    {
        if (Life_Decrease)
        {
            LifeDecrease();
            player.Invincible = true;
            StartCoroutine(player.InvincibleTime()); //InvicibleTime
        }
    }

    /*IEnumerator InvicibleTime()
    {
        if (player.Invincible)
        {
            yield return new WaitForSeconds(player.Invincible_Time);
            player.Invincible = false;
        }
    }*/

    void LifeDecrease()
    {
        player.Life -= Life_Decrease_Value;
        Debug.Log(player.Life);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !other.isTrigger)   //!other.isTrigger는 캐릭터에 있는 TriggerCollider를 무시하게 해줌. 순수 Collider만 인식
        {
            Trap_Activation = true;              //함정 발동 활성화
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Trap_Activation = false;             //함정 발동 비활성화           
        }
    }

}
