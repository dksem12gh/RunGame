using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon01 : MonoBehaviour {

    public float angle;
    public float power;

	// Update is called once per frame
	void Update () {
        angle = transform.rotation.z;
	}
}
