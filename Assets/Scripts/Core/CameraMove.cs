using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;

    public float cameraSetX = 0;
    public float cameraSetY = 25;
    public float cameraSetZ = -35;

    public float cameraSpeed = 10;

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
