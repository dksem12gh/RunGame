using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour {

    private CharacterController controller;

    Rigidbody rigid;

    public GameObject Rope_rot;
    public Image stemina_bar;
    public Image Heart;
    public float HP;
    public float speed;
    public float DivingGage;                //수영 잠수 게이지용 (숨참기 게이지)

    private Vector3 moveVector = Vector3.zero;
    private GameObject Rope;

    private float verticalVelocity;
    public float gravity = 14.0f;       //물 스크립트 위해 프라이빗에서 퍼블릭 변경 - 18.05.27
    private float jumpForce = 7.0f;
    private float normalspeed = 5.0f;
    private float stemina = 10.0f;
    private float Ropesize;
    private float[] stack = new float[3];
    private float hangt;
    private float theta;

    private bool canrun = true;
    private bool grab = false;
    private bool lefthang = false, righthang = false;
    private bool stemina_bool = false, Heart_bool = false;
    private bool balloon = false;   //풍선 버섯 불값
    
    private int haning = 0;

    bool Diving = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.mass = 3.0f;
        controller = GetComponent<CharacterController>();
        for(int i = 0; i < 3; i++)
        {
            stack[i] = (float)0.2 * (i + 1);
        }
    }
    private void CheckMap()
    {
        Vector3 maplb = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maprt = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 1));

        float x = Mathf.Clamp(transform.position.x, maplb.x, maprt.x);
        float y = Mathf.Clamp(transform.position.y, maplb.y, maprt.y);

        transform.Translate(new Vector2(x, y));
    }
    private void UIalpha()
    {
        Color c = stemina_bar.color;
        if (Input.GetKey(KeyCode.LeftAlt))  //달리기
        {
            stemina_bool = true;    //스태미너창 활성화
        }
        else if (!Input.GetKey(KeyCode.LeftAlt) && stemina >= 10)
        {
            stemina_bool = false;  //스태미너창 비활성화
        }

        if (stemina_bool == true)
        {               //스태미너 창 비투명화
            c.r = 0;
            c.g = 1;
            c.b = 0;
            c.a = 1;
            stemina_bar.color = c;
        }else
        {               //스태미너 창 투명화
            c.r = 0;
            c.g = 1;
            c.b = 0;
            c.a = 0;
            stemina_bar.color = c;
        }
        if (7.0f < stemina) //스태미너 바 색 변화
        {
            c.r = 0;
            c.g = 1;
            c.b = 0;
            stemina_bar.color = c;
        }else if (3.0f < stemina)
        {
            c.r = 1;
            c.g = 1;
            c.b = 0;
            stemina_bar.color = c;
        }
        else
        {
            c.r = 1;
            c.g = 0;
            c.b = 0;
            stemina_bar.color = c;
        }
    }
    private void Update()
    {
        //        CheckMap(); //플레이어 위치확인 및 맵밖으로 못나가게 제한

        if (!Diving)
        {
            UIalpha();
            if (grab == false)  //잡기가 false일때
            {
                NormalPlay(speed);  //일반이동
                Rope_rot.transform.Rotate(0, 0, -Rope_rot.transform.rotation.z * Time.deltaTime * 200);//로프에 물리엔진
                hangt += Time.deltaTime;    //잡고있는 시간
                if (haning > 0) { haning -= Mathf.RoundToInt(hangt); }  //시간에 따라 높이가 떨어짐
                if (hangt >= 1) hangt = 0;
            }
            else if (grab == true)  //잡기가 활성화
            {
                RopePlay(); //로프이동 실행
            }
        }
        else
        {
            Swimming();
        }

        stemina_bar.fillAmount = stemina / 10.0f;   //스태미너 표시
    }
    void RopePlay()
    {
        Rope_rot.transform.Rotate(0, 0, -Rope_rot.transform.rotation.z * Time.deltaTime * 200);
        if (Input.GetKey(KeyCode.LeftControl))  //로프잡기
        {
            grab = true;
            gravity = 0.0f; //중력해제
            transform.Translate(Rope.transform.position.x - transform.position.x, 0, 0);    //플레이어 로프에 고정시키기
            moveVector.y = Input.GetAxis("Vertical") * 5.0f;    //로프잡으면 Vertical축 활성화
            controller.Move(moveVector*Time.deltaTime); //이동
            moveVector.y = Mathf.Clamp(transform.position.y, Rope.transform.position.y - Ropesize / 2, Rope.transform.position.y + Ropesize / 2);   //로프 길이로 플레이어 이동범위 제한
            transform.position = new Vector3(Rope.transform.position.x,moveVector.y,Rope.transform.position.z);
            if (Input.GetKey(KeyCode.LeftArrow) && !lefthang)   //로프잡고 왼쪽화살표
            {
                theta = Rope_rot.transform.rotation.z - 20 * Time.deltaTime * 3 * (haning + 1);
                if (Rope_rot.transform.rotation.z >= -stack[haning])
                {
                    Rope_rot.transform.Rotate(0, 0, theta);
                    if (Rope_rot.transform.rotation.z <= -stack[haning])
                    {
                        lefthang = true;
                        Debug.Log("lefthang : " + lefthang);
                        righthang = false;
                    }
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !righthang)    //로프잡고 오른쪽화살표
            {
                theta = Rope_rot.transform.rotation.z + 20 * Time.deltaTime * 3 * (haning + 1);
                if (Rope_rot.transform.rotation.z <= stack[haning])
                {
                    Rope_rot.transform.Rotate(0, 0, theta);
                    if (Rope_rot.transform.rotation.z >= stack[haning])
                    {
                        righthang = true;
                        Debug.Log("righthang : " + righthang);
                        lefthang = false;
                    }
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) == false &&
            Input.GetKey(KeyCode.RightArrow) == false)  //흔들기 각도 감소
            {
                if (haning > 0) { haning -= Mathf.RoundToInt(hangt); }
                if (hangt >= 1) hangt = 0;
            }

            if (lefthang)   //로프잡고 왼쪽화살표 눌러서 최고점
            {
                //Rope_rot.transform.Rotate(0, 0, -Rope_rot.transform.rotation.z * Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    haning++;
                    if (haning >= 2) haning = 2;
                }
                if (Rope_rot.transform.rotation.z >= 0)
                {
                    lefthang = false;
                }
            }
            else if (righthang) //로프잡고 오른쪽화살표 눌러서 최고점
            {
                //Rope_rot.transform.Rotate(0, 0, -Rope_rot.transform.rotation.z * Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    haning++;
                    if (haning >= 2) haning = 2;
                }
                if (Rope_rot.transform.rotation.z <= 0)
                {
                    righthang = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                grab = false;
                gravity = 14.0f;
            }
        }
    }

    void Swimming() //수영 스크립트
    {
        if (!Diving) return;

        verticalVelocity -= 0.2f;//중력적용

        if (stemina >= 3) canrun = true;//스태미너가 3이상일때 달리기가능

        if (Input.GetKey(KeyCode.LeftShift) && canrun == true)//좌측 알트키 누르고 달리기 가능일때
        {
            speed = 1.5f;//1.5배 속도로
            stemina -= Time.deltaTime;//스태미너 깎기
            if (stemina <= 0)//스태미너 0이하이면
            {
                Debug.Log("stemina 0");
                canrun = false;//달리기 불가
            }
        }
        else if (stemina <= 10)//스태미너 10이하일때
        {
            Color c = stemina_bar.color;
            c.r -= 0.1f;
            stemina += Time.deltaTime;//스태미너 회복
            speed = 0.3f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))//스페이스바 누르면
        {
            balloon = true;
            normalspeed = 7.5f;
            verticalVelocity = jumpForce;//중력무시후 점프
            grab = false;//잡기 false
            gravity = 20.0f;//중력다시 적용
        }
        else if (Input.GetKeyDown(KeyCode.Space))//스페이스바 누르면
        {
            balloon = true;
            verticalVelocity = jumpForce;//중력무시후 점프
            grab = false;//잡기 false
            gravity = 80.0f;//중력다시 적용
        }
        else
        {
            gravity = 1.0f;
            balloon = true;
            verticalVelocity -= gravity * Time.smoothDeltaTime;
        }

        moveVector.x = Input.GetAxis("Horizontal") * normalspeed * speed;//가로축 입력
        moveVector.y = verticalVelocity * speed;//중력 혹은 점프

        controller.Move(moveVector * Time.smoothDeltaTime);//움직이기

    }

    void NormalPlay(float speed)
    {
        if (controller.isGrounded)//땅밟고 있을때
        {
            balloon = false;    //땅 밟고있을때 풍선버섯 false

            verticalVelocity = 0.0f;//중력적용
            if (stemina >= 3) canrun = true;//스태미너가 3이상일때 달리기가능
            if (Input.GetKey(KeyCode.LeftShift) && canrun == true)//좌측 알트키 누르고 달리기 가능일때
            {
                speed = 1.5f;//1.5배 속도로
                stemina -= Time.deltaTime;//스태미너 깎기
                if(stemina <= 0)//스태미너 0이하이면
                {
                    Debug.Log("stemina 0");
                    canrun = false;//달리기 불가
                }
            }
            else if (stemina <= 10)//스태미너 10이하일때
                {
                    Color c = stemina_bar.color;
                    c.r -= 0.1f;
                    stemina += Time.deltaTime;//스태미너 회복
                    speed = 1;
                }
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))//스페이스바 누르면
            {
                balloon = true;
                normalspeed = 7.5f;
                verticalVelocity = jumpForce;//중력무시후 점프
                grab = false;//잡기 false
                gravity = 14.0f;//중력다시 적용
            }
            else if (Input.GetKeyDown(KeyCode.Space))//스페이스바 누르면
            {
                balloon = true;
                verticalVelocity = jumpForce;//중력무시후 점프
                grab = false;//잡기 false
                gravity = 14.0f;//중력다시 적용
            }
        }
        
        else
        {
            balloon = true;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        moveVector.x = Input.GetAxis("Horizontal") * normalspeed * speed;//가로축 입력
        moveVector.y = verticalVelocity * speed;//중력 혹은 점프

        controller.Move(moveVector * Time.smoothDeltaTime);//움직이기
    }

    void BalloonJump(float power,float angle)
    {
        verticalVelocity = power;//중력무시후 점프
        gravity = 14.0f;//중력다시 적용
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Rope")//로프와 충돌했을때
        {
                Rope = other.gameObject;//대상오브젝트를 Rope에 넣음
                RopePlay();//로프플레이실행
                Ropesize = Rope.GetComponent<Collider>().bounds.size.y;//로프 사이즈 받아오기
        }
        if (balloon == true)
        {
            if (other.gameObject.tag == "Ballon")
            {
                Vector3 dir = other.transform.position - transform.position;
                float Angle = Vector3.Angle(transform.forward, dir);

                BalloonJump(GameObject.Find("Balloon").GetComponent<Balloon01>().power, Angle); // 몰라 방향값 넘겨
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rope")
        {
            transform.rotation = Rope.transform.rotation;
        }

        if(other.gameObject.tag == "Water")
        {
            Diving = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rope")
        {
            transform.rotation = Quaternion.EulerRotation(0,0,0);
        }
        if (other.gameObject.tag == "Water")
        {
            Diving = false;
            gravity = 28.0f;
        }
    }
}