using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharCtroller : MonoBehaviour {
    private CharacterController controller;

    private float verticalVelocity;
    private float gravity = 14.0f;
    private float jumpForce = 7.0f;
    private bool grab = false;
    Vector3 moveVector = Vector3.zero;
    GameObject Rope;
    float Ropesize;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (grab == false)
        {
            NormalPlay();
        }
        else if (grab == true)
        {
            RopePlay();
        }
    }
    void RopePlay()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            grab = true;
            gravity = 0.0f;
            transform.Translate(Rope.transform.position.x - transform.position.x, 0, 0);
            moveVector.y = Input.GetAxis("Vertical") * 5.0f;
            controller.Move(moveVector * Time.deltaTime);
            moveVector.y = Mathf.Clamp(transform.position.y, Rope.transform.position.y - Ropesize / 2, Rope.transform.position.y + Ropesize / 2);
            transform.position = new Vector3(Rope.transform.position.x, moveVector.y, Rope.transform.position.z);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                grab = false;
                gravity = 14.0f;
            }
        }
    }
    void NormalPlay()
    {
        if (controller.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                grab = false;
                gravity = 14.0f;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        moveVector.x = Input.GetAxis("Horizontal") * 5.0f;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rope")
        {
            Rope = other.gameObject;
            RopePlay();
            Ropesize = Rope.GetComponent<Collider>().bounds.size.y;
            Debug.Log(Ropesize);
        }
    }
}
