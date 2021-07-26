using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float JumpPower;

    public GameObject[] weapons;
    public bool[] hasWeapons;

    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;
    bool isDown1;
    bool isDown2;

    bool isJump;

    //�߷� ����
    public float gravity = -20.0f;
    Vector3 moveVec;
    //���� �ӵ� ����
    float yVelocity = 0;

    Rigidbody rigid;
    Animator anim;

    //Ʈ���ŵ� ���⸦ �����ϱ� ���� ����
    GameObject nearweapon;
    GameObject equipWeapon; //�̹� �����ִ� ����
    int equipWeaponIndex;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetInput();
        Move();
        Jump();
        //ĳ������ �����ӵ�(�߷�)�� �����Ѵ�
        yVelocity += gravity * Time.deltaTime;
        moveVec.y = yVelocity;
        w_Interation();
        Change(); //���� ��ü
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        isDown1 = Input.GetButtonDown("isDown1"); //���� 1�� ������ ��
        isDown2 = Input.GetButtonDown("isDown2"); //���� 2�� ������ ��

    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        moveVec = Camera.main.transform.TransformDirection(moveVec);

        if (wDown)
            transform.position += moveVec * speed * 0.3f * Time.deltaTime;
        else
            transform.position += moveVec * speed * Time.deltaTime;
       

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Jump()
    {
        if(jDown&&!isJump)
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            anim.SetBool("isJump",false);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void w_Interation()
    {
        if (nearweapon != null)
        {
            if (nearweapon.tag == "Weapon") //����� ������
            {
                Weapons w = nearweapon.GetComponent<Weapons>();
                int weaponIndex = w.value;
                hasWeapons[weaponIndex] = true; //��� ��ȣ�� ���⸦ ���� �ִ� ������ ����

                Destroy(nearweapon);
            }
        }
    }

    void Change()
    {
        int weaponIndex = -1;
        if (isDown1) weaponIndex = 0;
        if (isDown2) weaponIndex = 1;

        //���� �ִ� ���Ⱑ ���� ��Ȳ���� ���Ⱑ ��ü�Ǵ� ���� ����
        if (isDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;     
        if (isDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;

        if ((isDown1 || isDown2) && !isJump) //����1 �Ǵ� 2�� ������ ��쿡(�����ϴ� ��쿣 ����) ���� Ȱ��ȭ
        {
            if (equipWeapon != null) //�տ� ���Ⱑ ��ġ�� ��� ���� ����
            {
                equipWeapon.SetActive(false);
            }
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", true);
            isJump = false;
        }

    }

    void OnTriggerStay(Collider w)
    {
        // ���� �浹�� ���� �����̸�
        if (w.tag == "Weapon")
        {
            nearweapon = w.gameObject;
        }
    }

    void OnTriggerExit(Collider w)
    {
        if (w.tag == "Grenade" || w.tag == "Gun")
        {
            nearweapon = null;
        }

    }

}
