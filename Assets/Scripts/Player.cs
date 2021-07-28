using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float JumpPower;
    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;
    bool isJump;
    bool sDown1; //���� 1�� ��������
    bool sDown2; //���� 2�� ��������
    bool gun=false;
    bool grenade=false;

    //�÷��̾� ���� ���� �迭
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public int hasGrenade;
    public GameObject grenadeObj; //����ź ������Ʈ
    public float throwPower = 10.0f; //�߻��� ��
    public Transform grenadePos; //�߻���ġ

    public int Stress;
    public int maxStress;

    //�߷� ����
    public float gravity = -20.0f;
    Vector3 moveVec;


    //ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;
    Rigidbody rigid;
    Animator anim;

    GameObject nearWeapon; //Ʈ���ŵ� ������� �����ϱ� ���� ����
    Weapon equipWeapon; //������ ������ ���⸦ �����ϴ� ����
    int equipWeaponIndex=-1;

    //���ؼ�
    public GameObject crossHair;
    bool crossHair_b;

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
        Interaction_W();//���� ��ȣ�ۿ�
        Swap();
        Attack();


    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        sDown1 = Input.GetButtonDown("sDown1");
        sDown2 = Input.GetButtonDown("sDown2");
    }

    //�÷��̾� �����̰� �ϴ� �Լ�
    void Move()
    {

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        
        moveVec = Camera.main.transform.TransformDirection(moveVec);
        
        if (wDown)
            transform.position += moveVec * speed* 0.3f * Time.deltaTime;
        else
            transform.position += moveVec * speed * Time.deltaTime;


        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("iswalk", wDown);
       
    }
    
    //�÷��̾� �����ϰ� �ϴ� �Լ�
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
    void Swap() //���� ��ü
    {
        // ���⸦ ���� ���� �ʴ� ���¿��� ���Ⱑ Ȱ��ȭ�Ǵ� ���� ����
        if (sDown1 && (!hasWeapons[0]||equipWeaponIndex==0))
        {
            return;
        }
        if (sDown2 && (!hasWeapons[1]||equipWeaponIndex==1))
        {
            return;
        }

        int weaponIndex = -1;
        //���� 1�� ������ ��, ����2�� ������ ����ź
        if (sDown1)
        {
            gun = true;
            grenade = false;
            weaponIndex = 0;
            crossHair_b = true; 
        }
        if (sDown2)
        {
            gun = false;
            grenade = true;
            weaponIndex = 1;
            crossHair_b = false;
        }
        crossHair.SetActive(crossHair_b); //���⸦ ������ä�� 1���� ���� ��쿡�� ���ؼ� Ȱ��ȭ

        //1,2�� ������ ���Ⱑ ���̰� ��.
        if (sDown1 || sDown2)
        {
            if (equipWeapon != null) //����� �ƴ϶�� 
            {
                equipWeapon.gameObject.SetActive(false); //���� �տ� ��� �־��� ���⸦ ��Ȱ��ȭ��
            }

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true); // �տ� ��� �ִ� ���⸦ Ȱ��ȭ�Ͽ� ���̰���.
        }
    }

    void Attack() //����
    {
        if (equipWeapon == null) //���⸦ ���� �ִ� ��쿡�� ����
            return;

        if (Input.GetMouseButtonDown(0)&&(gun==true)) //���콺 ��Ŭ���� ���ÿ� �տ� ���� �ִ� ���
        {
            equipWeapon.Use();
            anim.SetTrigger("doShot");
        }
        

        if (Input.GetMouseButtonDown(0) && (grenade == true)) //���콺 ��Ŭ���� ���ÿ� �տ� ����ź�� �ִ� ���
        {
           // crossHair.SetActive(false);
            GameObject bomb = Instantiate(grenadeObj);
            bomb.transform.position = grenadePos.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

            //���콺 ��ġ�� ������
            /*
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray,out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
            }
            */
        }

       
    }

    

    void Interaction_W() //���� ������� �迭�� ����
    {
        if (nearWeapon != null)
        {
            if (nearWeapon.tag == "Weapon")
            {
                Item item = nearWeapon.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true; //�ش� ���Ⱑ �÷��̾����� ���� �ִٰ� ����

                Destroy(nearWeapon);
            }
        }
    }

    //�÷��̾ Floor�� �±׵� ��ü�� ������ �����ϴ� ����� ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", true);
            isJump = false;
        }

    }

    //�÷��̾ �������̶�� �±׵� ��ü�� ������ �Ͼ�� ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                //������ ��ũ��Ʈ���� ������ Ÿ�԰� value ���� ���� stress ��ġ�� ������
                //stress �� maxstress�� �Ѿ�� 0���� ����
                case Item.Type.Ammo:
                    Stress -= item.value;
                    if (Stress < 0)
                        Stress = 0;                   
                    break;
                case Item.Type.Heart:
                    Stress -= item.value;
                    if (Stress < 0)
                        Stress = 0;
                    break;
            }
            //�������� ������ �����
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other) //���� �ݶ��̴��� ������(=���� ������Ʈ ����)
    {
        if (other.tag == "Weapon")
        {
            nearWeapon = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) //���� �ݶ��̴����� ����������
    {
        if (other.tag == "Weapon")
        {
            nearWeapon = null;
        }
    }

    //�÷��̾ ������ �޾��� ��
    public void OnDamage(int value)
    {
        Stress += value;
        if (Stress> 0)
        {
            Stress = 100;
        }
        
    }
}
