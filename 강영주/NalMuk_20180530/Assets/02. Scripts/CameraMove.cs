using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject _player1;
    public GameObject _player2;
    public GameObject _player3;
    public GameObject _player4;
    GameObject _target;

	void Checkpos()
    {
        if(_player1.transform.position.x >= _player2.transform.position.x && _player1.transform.position.x >= _player3.transform.position.x && _player1.transform.position.x >= _player4.transform.position.x)
        {
            _target = _player1;
        }
        if (_player2.transform.position.x >= _player1.transform.position.x && _player2.transform.position.x >= _player3.transform.position.x && _player2.transform.position.x >= _player4.transform.position.x)
        {
            _target = _player2;
        }
        if (_player3.transform.position.x >= _player2.transform.position.x && _player3.transform.position.x >= _player1.transform.position.x && _player3.transform.position.x >= _player4.transform.position.x)
        {
            _target = _player3;
        }
        else
        {
            _target = _player4;
        }
        transform.Translate((_target.transform.position.x - transform.position.x)+5, (_target.transform.position.y - transform.position.y), 0);
        if (_target.transform.position.y >= transform.position.y)
        {
            transform.Translate(0, (_target.transform.position.y+2 - transform.position.y),0);
        }
    }
		
	
	
	void Update () {
        Checkpos();
	}
}
