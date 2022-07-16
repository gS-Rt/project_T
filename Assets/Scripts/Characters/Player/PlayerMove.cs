using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float moveSpeedWithJump;
    public Vector3 movement;
    float hMove;
    float vMove;

    public bool jumpTrigger; //점프함수 트리거
    public bool canJump; //점프 누르기 가능한지
    public bool isJump; //점프&공중 상태인지
    public float jumpPower;

    private Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
}
    void Start()
    {
        canJump = false;
        jumpTrigger = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump) //점프 가능할 때만 작동
        {
            jumpTrigger = true; //점프 트리커 켜짐
        }
    }

    void FixedUpdate() //일정주기로 호출
    {
        GetInput();
        Move();
        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {

        canJump = true; //어느 콜라이더라도 밟으면 점프 가능
        isJump = false;


    }

    private void OnTriggerExit(Collider other)
    {

        //if (isJump)
        canJump = false;
    }

    private void GetInput()
    {

        hMove = Input.GetAxisRaw("Horizontal");
        vMove = Input.GetAxisRaw("Vertical");

    }

    public void Move()
    {

        movement.Set(hMove, 0, vMove);
        movement = movement.normalized * ((canJump) ? moveSpeed : moveSpeedWithJump) * Time.deltaTime; //점프할 땐 속도 변경

        rigid.MovePosition(transform.position + movement);

        //rigid.velocity = movement; //벡터 추가로 움직임

    }

    public void Jump()
    {
        if (!jumpTrigger) //점프 트리거 켜져있으면 힘 주고 다시 끔
            return;

        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        jumpTrigger = false;
        isJump = true;
    }
}