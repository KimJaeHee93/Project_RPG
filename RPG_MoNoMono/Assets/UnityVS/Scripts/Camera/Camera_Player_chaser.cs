using UnityEngine;
using System.Collections;

public class Camera_Player_chaser : MonoBehaviour
{
    // 플레이어를 따라오는 카메라의 속도.
    // 플레이어보다 약간 느리게 시간을 주면 약간의 딜레이로 인해 부드러워 보인다.
    public float cameraFollowSpeed = 2.5f; 

    public GameObject player;

    public float offsetX = 0f;
    public float offsetY = 25f;
    public float offsetZ = 30f;

    Vector3 cameraPosition;


    void LateUpdate ()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, cameraFollowSpeed * Time.deltaTime);
        // Lerp[위치이동관련](현재위치, 목표위치, 속도)
    }
}
