using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpotSkill : MonoBehaviour
{
    public float skillTimer;
    public float spotSpeed; //스팟 범위 커지는 속도
    public bool spotTrigger;
    public Vector3 scale;
    ParticleSystem[] particls;

    // Start is called before the first frame update
    void Start()
    {
        scale.z = 2f;
        spotSpeed = 0.005f;
        spotTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<GetMousePosition>().isHit && spotTrigger == true)
        {
            skillTimer += Time.deltaTime;
            scale.x += skillTimer * spotSpeed;
            scale.y += skillTimer * spotSpeed;
            transform.localScale = scale;
        }
        else
        {
            skillTimer = 0;
            spotTrigger = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            particls = GetComponentsInChildren<ParticleSystem>();
            for(int i=0;i< particls.Length;i++)
            {
                var main = particls[i].main;
                main.loop = false;
            }
            //Destroy(gameObject, .1000f); 일정 시간 뒤 삭제되는 코드 필요, 현재 부모 오브젝트만 공란으로 남음
        }
        // ParticleSystemStopBehavior.StopEmitting
        //
    }
}
