using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float moveSpeedWithJump;
    public Vector3 movement;
    float hMove; //�¿��̵�, ������ -1
    float vMove;

    public bool jumpTrigger; //�����Լ� Ʈ����
    public bool canJump; //���� ������ ��������
    public bool isJump; //����&���� ��������
    public float jumpPower;

    private Rigidbody rigid;

    private Animator animator; //�ִϸ��̼� ��Ʈ�ѷ�

    Vector3 theScale; //�¿������
    bool isRight;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
}
    void Start()
    {
        canJump = false;
        jumpTrigger = false;
        isRight = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump) //���� ������ ���� �۵�
        {
            jumpTrigger = true; //���� Ʈ��Ŀ ����
        }

        if ((hMove == 0) && (vMove == 0)||(isJump==true)) //�ȿ����̰ų� ������ ��
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        if (hMove < 0)
            isRight = true;
        if (hMove > 0)
            isRight = false;

        if (hMove<0&& transform.localScale.x>0)
        {
            theScale = transform.localScale;
            theScale.x *= -1;

            transform.localScale = theScale;
        }
        else if (hMove > 0 && transform.localScale.x < 0)
        {
            theScale = transform.localScale;
            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    void FixedUpdate() //�����ֱ�� ȣ��
    {
        GetInput();
        Move();
        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {

        canJump = true; //��� �ݶ��̴��� ������ ���� ����
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
        movement = movement.normalized * ((canJump) ? moveSpeed : moveSpeedWithJump) * Time.deltaTime; //������ �� �ӵ� ����

        rigid.MovePosition(transform.position + movement);

        //rigid.velocity = movement; //���� �߰��� ������


    }

    public void Jump()
    {
        if (!jumpTrigger) //���� Ʈ���� ���������� �� �ְ� �ٽ� ��
            return;

        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        jumpTrigger = false;
        isJump = true;
    }
}