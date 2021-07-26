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
    bool isFireReady;
    bool isChange;

    //중력 변수
    public float gravity = -20.0f;
    Vector3 moveVec;
    //수직 속도 변수
    float yVelocity = 0;

    Rigidbody rigid;
    Animator anim;

    //트리거된 무기를 저장하기 위한 변수
    GameObject nearweapon;
    Weapons equipWeapon; //이미 갖고있는 무기
    int equipWeaponIndex;

    float fireDelay;

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
        //캐릭터의 수직속도(중력)을 적용한다
        yVelocity += gravity * Time.deltaTime;
        moveVec.y = yVelocity;
        w_Interation();
        Change(); //무기 교체
        Attack(); //공격
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        isDown1 = Input.GetButtonDown("isDown1"); //숫자 1를 눌렀을 때
        isDown2 = Input.GetButtonDown("isDown2"); //숫자 2를 눌렀을 때

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
            if (nearweapon.tag == "Weapon") //무기와 닿으면
            {
                NotUseWeapons w = nearweapon.GetComponent<NotUseWeapons>();
                int weaponIndex = w.value;
                hasWeapons[weaponIndex] = true; //헤당 번호의 무기를 갖고 있는 것으로 설정

                Destroy(nearweapon);
            }
        }
    }

    void Change()
    {
        int weaponIndex = -1;
        if (isDown1) weaponIndex = 0;
        if (isDown2) weaponIndex = 1;

        //갖고 있는 무기가 없는 상황에서 무기가 교체되는 것을 방지
        if (isDown1 && (!hasWeapons[0] ))
        {
            Debug.Log(equipWeaponIndex);
            return;
        }
        if (isDown2 && (!hasWeapons[1] ))
            return;

        if ((isDown1 || isDown2) && !isJump) //숫자1 또는 2를 누르는 경우에(점프하는 경우엔 제외) 무기 활성화
        {
            if (equipWeapon != null) //손에 무기가 겹치게 드는 것을 방지
            {
                equipWeapon.gameObject.SetActive(false);
            }
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapons>();
            equipWeapon.gameObject.SetActive(true);

           
        }
    }

    void Attack()
    {
        if (equipWeapon == null)
            return;

        //공격가능 여부확인
        //fireDelay += Time.deltaTime;
        //isFireReady = equipWeapon.rate < fireDelay;

        if (Input.GetMouseButtonDown(0)) //왼쪽 마우스 누르면 총 발사
        {
            equipWeapon.Use();
            anim.SetTrigger("doShot");
            //fireDelay = 0;
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
        // 만약 충돌한 것이 무기이면
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
