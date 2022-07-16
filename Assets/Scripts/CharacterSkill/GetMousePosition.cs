using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePosition : MonoBehaviour
{
    public GameObject itemPrifeb;
    public bool isHit = false; //유효한 벽에 스팟했는지
    private RaycastHit playerRayHit;
    public Vector3 playerRayHitPos;
    Vector3 hitPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                hitPos = hit.point;
                //hitPos.y = 0.3f;
                isHit = true;

                Ray playerRay = new Ray(transform.position, hit.point - transform.position);
                Physics.Raycast(playerRay, out playerRayHit, 100f);
                playerRayHitPos = playerRayHit.point;
                if (playerRayHitPos == hitPos)
                {
                    GameObject item = Instantiate(itemPrifeb);
                    item.transform.position = hitPos;
                    item.transform.rotation = Quaternion.LookRotation(hit.normal);
                }

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            isHit = false;
        }

    }

    void FixedUpdate()
    {

    }


    
}
