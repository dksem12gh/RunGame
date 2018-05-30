using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigid : MonoBehaviour {

    public float speed = 2.5f;
    Rigidbody rBody;
    bool isBottom;
    int Vertical;
    int Horizontal;
    bool Accel;
    bool Jump;
    bool Attack;
    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.mass = 10.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bottom") isBottom = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Bottom") isBottom = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Bottom") isBottom = false;
    }
    void FixedUpdate()
    {
        Accel = Input.GetKey(KeyCode.Space);

        Vector3 rot = transform.rotation.eulerAngles;//회전값을 받음
        rot.x = 0;//y축만 변동될수있으므로 x,z는 모두 0으로.
        rot.z = 0;
        rot.y += Horizontal; //좌우이동키가 바뀐경우에만 변동.

        if (Jump) rBody.AddForce(transform.up * 5f, ForceMode.Impulse);
    }

}
