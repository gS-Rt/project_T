using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player; //대상 오브젝트 삽입

    public float cameraSetX = 0;
    public float cameraSetY = 25;
    public float cameraSetZ = -35; //대상 오브젝트로부터 얼마나 x,y,z 좌표만큼 카메라가 떨어져 있을 건지 초기 설정

    public float cameraSpeed = 10; //카메라 움직이는 속도

    Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        cameraPosition.x = player.transform.position.x + cameraSetX;
        cameraPosition.y = player.transform.position.y + cameraSetY;
        cameraPosition.z = player.transform.position.z + cameraSetZ;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, cameraSpeed * Time.smoothDeltaTime);
    }
}
