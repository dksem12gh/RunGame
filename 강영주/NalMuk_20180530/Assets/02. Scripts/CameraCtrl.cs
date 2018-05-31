using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {
    public GameObject _player1;
    public GameObject _player2;
    public GameObject _player3;
    public GameObject _player4;
    public GameObject _maincamera;

    public float cameraXPos;


    void Start()
    {

    }
    void _positionCheck()//이스크립트 Playermove에 옮기기
    {
        if (_player1.transform.position.x > _player2.transform.position.x && _player1.transform.position.x > _player3.transform.position.x && _player1.transform.position.x > _player4.transform.position.x)
        {
            transform.Translate((_player1.transform.position.x - transform.position.x) + cameraXPos, 0, 0);
        }
        else if (_player2.transform.position.x > _player1.transform.position.x && _player2.transform.position.x > _player3.transform.position.x && _player2.transform.position.x > _player4.transform.position.x)
        {
            transform.Translate((_player2.transform.position.x - transform.position.x) + cameraXPos, 0, 0);
        }
        else if (_player3.transform.position.x > _player1.transform.position.x && _player3.transform.position.x > _player2.transform.position.x && _player3.transform.position.x > _player4.transform.position.x)
        {
            transform.Translate((_player3.transform.position.x - transform.position.x) + cameraXPos, 0, 0);
        }
        else
            transform.Translate((_player4.transform.position.x - transform.position.x) + cameraXPos, 0, 0);
    }
    void Update()
    {
        _positionCheck();
    }
}
