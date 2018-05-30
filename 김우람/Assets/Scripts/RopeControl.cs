using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeControl : MonoBehaviour {

    float vx;
    GameObject Rope_rot;

    private void Awake()
    {
        Rope_rot = transform.Find("Rope_rot").gameObject;
    }
    void Update()
    {
            transform.Rotate(0, 0, transform.rotation.z);

    }
}
