using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 1f; //카메라가 움직이기 전 플레이어가 움직일 수 있는 x 범위
    public float yMargin = 1f; //카메라가 움직이기 전 플레이어가 움직일 수 있는 y 범위
    public float zMargin = 1f;
    public float xSmooth = 2f; //x축으로 얼마나 부드럽게 움직이는가
    public float ySmooth = 2f; //y축으로 얼마나 부드럽게 움직이는가
    public float zSmooth = 2f;
    public Vector3 maxXandYandZ; //카메라의 x와 y 최대 좌표 값
    public Vector3 minXandYandZ; //카메라의 x와 y 최소 좌표 값

    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

    bool CheckZMargin()
    {
        return Mathf.Abs(transform.position.z - player.position.z) > zMargin;
    }

    void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;
        float targetZ = transform.position.z;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
        }

        if (CheckZMargin())
        {
            targetZ = Mathf.Lerp(transform.position.z, player.position.z, zSmooth * Time.deltaTime);
        }


        targetX = Mathf.Clamp(targetX, minXandYandZ.x, maxXandYandZ.x);
        targetY = Mathf.Clamp(targetY + 0.2f, minXandYandZ.y, maxXandYandZ.y);
        targetZ = Mathf.Clamp(targetZ - 0.4f, minXandYandZ.z, maxXandYandZ.z);

        transform.position = new Vector4(targetX, targetY, targetZ, transform.position.z);
    }
}

