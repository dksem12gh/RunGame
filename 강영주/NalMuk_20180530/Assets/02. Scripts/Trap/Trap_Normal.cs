using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Normal : MonoBehaviour {

    PlayerCtrl player;
    [Header("발동 지연시간 수치")]
    public float Trap_DelayTime_Value;      //발동 지연시간 수치
    [Header("생명력 감소 수치")]
    public int Life_Decrease_Value;         //생명력 감소 수치
    [Header("스태미너 감소 수치")]
    public int Stamina_Decrease_Value;      //스태미너 감소 수치
    [Header("이동속도 감소 수치")]
    public float Slow_Value;                //이동속도 감소 수치
    [Header("조작제한 지속시간")]
    public float Control_Limit_Duration;    //조작제한 지속시간
    [Header("발동 반복 간격 수치")]
    public float Trap_Interval_Value;       //발동 반복 간격 수치
    [Header("함정 횟수 분류")]
    [Range(1,3)]
    [Tooltip("1: 일회형, 2: 지속형, 3: 반복형")]
    public int Trap_Time_Code;              //함정의 횟수 분류 코드- 1: 1회형, 2: 지속형, 3: 반복형
    
    [Header("-----------------------------------------------------------")]
    public bool Trap_Activation;
    public bool Trap_DelayTime;             //발동 지연시간 활성화
    //public bool State_Decrease;             //능력치 감소 활성화
    public bool Life_Decrease;              //생명력 감소 활성화
    public bool Stamina_Decrease;           //스태미너 감소 활성화
    public bool Slow;                       //이동속도 감소 활성화
    public bool Control_Limit;              //조작제한 활성화
    public bool Trap_Interval;              //발동 반복 간격 활성화
    public bool Trap_Detroyable;            //발동 후 파괴 활성화

    float nextTime = 0;

    // Use this for initialization
    void Start () {
        Trap_Activation = false;
        player = PlayerCtrl.instance;

        nextTime = Time.time + Trap_Interval_Value;
    }
	
	// Update is called once per frame
	void Update () {
        TrapTimeCode();

        if (Trap_Activation)                 //함정 발동
        {
            TrapDelaytime();
        }
        else if (!Trap_Activation)
        {
            CancelInvoke();
        }
    }

    void TrapDelaytime()
    {
        if (Trap_DelayTime)                                     //발동 지연시간이 있을 경우
        {
            Invoke("TrapInterval", Trap_DelayTime_Value);
        }
        else if (!Trap_DelayTime)                               //발동 지연시간이 없을 경우
        {
            TrapInterval();
        }
    }

    void TrapInterval()
    {
        if (Trap_Interval)                                  //발동 반복 간격이 있을 경우
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + Trap_Interval_Value;
                TrapActivation();
            }
        }
        else if (!Trap_Interval)                            //발동 반복 간격이 없을 경우
        {
            TrapActivation();
            if (Trap_Time_Code == 1)
            {
                //enabled = false;
                Trap_Activation = false;
            }
            else
                //enabled = true;
                Trap_Activation = true;
        }
    }

    void TrapTimeCode()
    {
        switch (Trap_Time_Code)
        {
            case 1:
                Trap_Interval = false;
                break;
            case 2:
                Trap_Interval = false;
                break;
            case 3:
                Trap_Interval = true;
                break;
        }
    }

    void TrapActivation()
    {
        if (Life_Decrease)
        {
            LifeDecrease();
        }
        if (Stamina_Decrease)
        {
            StaminaDecrease();
        }
        if (Slow)
        {
            Slowing();
        }
        if (Control_Limit)
        {
            ControlLimit();
        }
    }

    void LifeDecrease()
    {
        player.Life -= Life_Decrease_Value;
        Debug.Log(player.Life);
    }

    void StaminaDecrease()
    {
        player.Stamina -= Stamina_Decrease_Value;
    }

    void Slowing()
    {
        player.Descrease = Slow_Value;
    }

    void ControlLimit()
    {
        player.Descrease = 0;
        Invoke("OriginalSpeed", Control_Limit_Duration);
    }

    void OriginalSpeed()
    {
        player.Descrease = 1;
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
            OriginalSpeed();
            Trap_Activation = false;             //함정 발동 비활성화            
        }
    }
}
