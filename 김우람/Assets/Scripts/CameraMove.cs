using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject _player1, _player2, _player3, _player4;
    GameObject _target;

    private Vector2 maplb, maprt;
    float z = 0.0f;
    
    void Checkpos()
    {
        z+=0.1f*Time.deltaTime;

        if (_player1.transform.position.x >= _player2.transform.position.x && _player1.transform.position.x >= _player3.transform.position.x && _player1.transform.position.x >= _player4.transform.position.x)
        {
            _target = _player1;
        }
        else if (_player2.transform.position.x >= _player1.transform.position.x && _player2.transform.position.x >= _player3.transform.position.x && _player2.transform.position.x >= _player4.transform.position.x)
        {
            _target = _player2;
        }
        else if (_player3.transform.position.x >= _player1.transform.position.x && _player3.transform.position.x >= _player2.transform.position.x && _player3.transform.position.x >= _player4.transform.position.x)
        {
            _target = _player3;
        }
        else _target = _player4;

        transform.Translate((_target.transform.position.x - transform.position.x) * Time.deltaTime, (_target.transform.position.y - transform.position.y + 3) * Time.deltaTime, 0);
    }
    private void Update()
    {
        Checkpos();
    }
}
