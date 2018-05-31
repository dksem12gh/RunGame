using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    public static PlayerCtrl instance;

    public Rigidbody rb;
    Animator ani;
    /*public Material headSkin;
    public Material bodySkin;
    Color c1 = new Color(255, 159, 159, 255);
    Color c2 = new Color(255, 250, 250, 255);
    Color c3 = new Color(255, 255, 255, 255);*/

    public int Life;
    public int lastLife;
    public int Stamina;
    public float Descrease;                                 //함정에 의한 감속 속도
    public float walkSpeed;                                 //캐릭터 걷기 속도
    public float jumpPower;                                 //캐릭터 점프 힘
    public float Invincible_Time;                           //캐릭터 무적 시간
    public bool isMoving;                                   //캐릭터 움직임 상태 bool
    public bool isGrounded;                                 //캐릭터 그라운드 체크 bool
    public bool isJumped;                                   //캐릭터 점프 상태 bool
    public bool isGrabLadder;                               //캐릭터 사다리 잡은 상태
    public bool isOnLadder;                                 //캐릭터 사다리와 겹친 상태
    public bool Invincible;                                 //캐릭터 무적 상태

    float ladderPos;

    Vector3 Movement;

    void Awake()
    {
        PlayerCtrl.instance = this;
    }

    //----------------------------------------------------------Start-------------------------------------------------------------------------------------
    void Start () {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();

        isGrounded = false;
        isJumped = false;
        Invincible = false;

        Life = 5;
        Stamina = 3;
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------

    //--------------------------------------------------------Fixed Update--------------------------------------------------------------------------------
    void FixedUpdate () {
        Move(isGrabLadder);
        Animation();
        Jump();
        LadderGrab();

        if (isOnLadder  && isGrabLadder)
        {
            isGrounded = true;
        }
	}
    //----------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        if (Life >= 5)
        {
            Life = 5;
        }

        if (Stamina >= 3)
        {
            Stamina = 3;
        }

    }

    public IEnumerator InvincibleTime()
    {
        if (Invincible)
        {
            yield return new WaitForSeconds(Invincible_Time);
            Invincible = false;
        }
    }

    //------------------------------------------------------캐릭터 (좌우)움직임----------------------------------------------------------------------------
    void Move(bool gLadder)
    {
        if (!gLadder)
        {
            float hor = Input.GetAxis("Horizontal") * (walkSpeed * Descrease) * Time.deltaTime; //LeftJoystickHorizontal

            Movement = new Vector3(hor, 0, 0);
            rb.MovePosition(transform.position + Movement);

            if (Input.GetAxis("Horizontal") > 0)    //수평축 속도가 양수일 경우
            {
                isMoving = true;                                //캐릭터는 움직이고 있음
                transform.localScale = new Vector3(1, 1, 1);    //캐릭터 우측 바라보게 하기
            }
            if (Input.GetAxis("Horizontal") < 0)    //수평축 속도가 음수일 경우
            {
                isMoving = true;                                //캐릭터는 움직이고 있음
                transform.localScale = new Vector3(1, 1, -1);   //캐릭터 좌측 바라보게 하기
            }
            if (Input.GetAxis("Horizontal") == 0)   //수평축 속도가 0일 경우
            {
                isMoving = false;                               //캐릭터는 멈춰있음
            }
        }
        else
        {
            float ver = Input.GetAxis("Vertical") * (walkSpeed * Descrease) * Time.deltaTime; //LeftJoystickVertical

            Movement = new Vector3(0, -ver, 0);
            rb.MovePosition(transform.position + Movement);
        }

        //this.transform.Translate(Vector3.forward * walkSpeed * Descrease * Time.deltaTime);     캐릭터 움직임 다른 방법
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    
    //------------------------------------------------------캐릭터 점프----------------------------------------------------------------------------
    void Jump()
    {
        if (isGrounded)                                     //캐릭터가 땅에 있을 경우
        {
            if (Input.GetKey(KeyCode.Space))     //조이패드 Y키 Joystick1Button3
            {
                isJumped = true;                            //캐릭터는 현재 점프 상태
                isGrounded = false;                         //캐릭터는 그라운드로 부터 떨어져 있음
                isGrabLadder = false;
                ani.SetBool("aniIsGrounded", false);        //애니메이션 그라운드 체크 false
                ani.SetBool("aniIsJumped", true);           //애니메이션 점프 체크 true

                Vector3 currentVelocity = rb.velocity;
                rb.velocity = new Vector3(Movement.x, jumpPower, currentVelocity.z);

                //this.rb.AddForce(transform.up * this.jumpPower);
                //this.rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);    캐릭터 점프 다른 방법
            }
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------

    //------------------------------------------------------캐릭터 애니메이션-----------------------------------------------------------------------
    void Animation()
    {
        if (isGrounded)                                     //땅에 있을 경우
        {
            ani.SetBool("aniIsGrounded", true);             //애니메이션 그라운드 체크 true
            ani.SetBool("aniIsJumped", false);              //애니메이션 점프 체크 false
        }

        if (isGrounded == false)                            //땅에서 떨어졌을 경우
        {
            ani.SetBool("aniIsGrounded", false);            //애니메이션 그라운드 체크 false
        }
                                                            //애니메이션 이동 값 체크
        ani.SetFloat("aniMoveFloat", Mathf.Abs(Input.GetAxis("Horizontal"))); 
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------

    void LadderGrab()
    {
        if (isOnLadder)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) //Joystick1Button1
            {
                isGrabLadder = !isGrabLadder;
                isGrounded = true;
                isJumped = false;
                transform.position = new Vector3(ladderPos, transform.position.y, transform.position.z);
            }
        }
    }


    //--------------------------------------------------------------충돌----------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)                     //캐릭터가 땅에 충돌했을 경우
    {
        if (other.gameObject.tag != "Ladder")
        {
            isGrounded = true;                                  //그라운드 체크 bool 값 true
            isJumped = false;                                   //점프 상태 bool 값 false
        }

        if (other.gameObject.tag == "Ladder")               //사다리 탈때 캐릭터 가운데 정렬
        {
            ladderPos = other.transform.position.x;
        }
    }

    void OnTriggerStay(Collider other)                      //캐릭터가 땅에 머무를 경우
    {

        if (other.gameObject.tag != "Ladder")
        {
            if (isJumped == false)                              //점프 상태가 아닐시
            {
                isGrounded = true;                              //땅에 계속 있으면 그라운드 체크 true
            }
        }

        if (other.gameObject.tag == "Ladder")
        {
            isOnLadder = true;
        }
    }

    void OnTriggerExit(Collider other)                      //캐릭터가 땅에서 떨어졌을 경우
    {
        isGrounded = false;                                 //그라운드 체크 bool 값 true
        if (other.gameObject.tag == "Ladder")
        {
            isGrabLadder = false;
            isOnLadder = false;
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    /*
    void CameraBlock() //플레이어가 화면(카메라) 밖으로 못나가게 하기
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환한다.
        transform.position = worldPos; //좌표를 적용한다.
    }*/
}
