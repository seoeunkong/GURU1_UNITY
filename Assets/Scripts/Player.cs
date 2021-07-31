using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float JumpPower;
    bool moving;

    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;
    bool isJump;
    bool sDown1; //���� 1�� ��������
    bool sDown2; //���� 2�� ��������
    public bool gun=false;
    public bool grenade=false;

    //�÷��̾� ���� ���� �迭
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public int hasGrenade; //����ź ����
    bool hasG;
    public GameObject grenadeObj; //����ź ������Ʈ
    public float throwPower = 30.0f; //�߻��� ��
    public Transform grenadePos; //�߻���ġ

    //��Ʈ���� ����, �ִ� ��Ʈ����
    int Stress;
    public int maxStress;

    //�����̴� 
    public Slider stressSlider;

    //�߷� ����
    public float gravity = -20.0f;
    Vector3 moveVec;
    //���� �ӵ� ����
    float yVelocity = 0;

    Rigidbody rigid;
    Animator anim;

    GameObject nearWeapon; //Ʈ���ŵ� ������� �����ϱ� ���� ����
    Weapon equipWeapon; //������ ������ ���⸦ �����ϴ� ����
    int equipWeaponIndex=-1;

    //���ؼ�
    public GameObject crossHair; //�Ϲ�
    public GameObject crossHair_Sniper; //��������
    bool crossHair_G; //�Ϲ�
    bool crossHair_S; //��������
    bool isZoom=false;

    private Camera camera;
    CharacterController cc; //ĳ���� ��Ʈ�ѷ� ����

    void Awake()
    {
       // rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        camera = Camera.main;

        cc = GetComponent<CharacterController>();

        //����� 50
        Stress = 50;
        //Stress=0;
    }

    void FixedUpdate() //ī�޶� ������ �����ϱ� ���� Update�Լ�
    {
        
        Move();
       // Jump();
       

    }

    void Update()
    {

        Swap();
        GetInput();
        Interaction_W();//���� ��ȣ�ۿ�
      
        Attack();
        Cross(); //���ؼ� ���
        Grenadelimit();
        
        //�����̴��� value�� ��Ʈ���� ������ �����Ѵ�
        stressSlider.value = (float)Stress / (float)maxStress;
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
        if (hAxis == -1 || hAxis == 1 || vAxis == -1 || vAxis == 1)
        {
            moving = true;
        }
        else
            moving = false;

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        moveVec = Camera.main.transform.TransformDirection(moveVec);

        //ĳ������ �����ӵ�(�߷�)�� �����Ѵ�
        yVelocity += gravity * Time.deltaTime;
        moveVec.y = yVelocity;

        if (wDown)
            transform.position += moveVec * speed * 0.3f * Time.deltaTime;
        else
            transform.position += moveVec * speed * Time.deltaTime;


        //transform.position += moveVec * speed * Time.deltaTime;
        cc.Move(moveVec * speed * Time.deltaTime);

       
        // moveVec == Vector3.zero
        anim.SetBool("isRun", moving);
       // anim.SetBool("isWalk", wDown);
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
            gun = true; //�� ��� Ȱ��ȭ
            grenade = false;
            weaponIndex = 0;
            crossHair_G = true;
            crossHair_S = false;
           // anim.SetBool("isShot", true); // �� ��� ��� Ȱ��ȭ

        }
        

        if (sDown2)
        {
            gun = false;
            grenade = true; //����ź ��� Ȱ��ȭ
            weaponIndex = 1;
            crossHair_G = false;
            crossHair_S = false;
           // anim.SetBool("isShot", false); //�� ��� ��� ��Ȱ��ȭ
        }

        //1,2�� ������ ���Ⱑ ���̰� ��.
        if (sDown1 || sDown2)
        {
            if (equipWeapon != null) //����� �ƴ϶�� 
            {
                equipWeapon.gameObject.SetActive(false); //���� �տ� ��� �־��� ���⸦ ��Ȱ��ȭ��
            }
            //anim.SetBool("isShot", true);
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true); // �տ� ��� �ִ� ���⸦ Ȱ��ȭ�Ͽ� ���̰���.
           
        }

        if (hasGrenade == 0) //����ź�� 0���̸� �������� ��.
        {
            hasWeapons[1] = false;
        }
        

    }


    void Grenadelimit() //����ź ���� ���� �� ����ź ����
    {
        if (hasGrenade == 0)
        {
            return;
        }


        if (Input.GetMouseButtonDown(0) && (grenade == true)) //���콺 ��Ŭ���� ���ÿ� �÷��̾����� 1�� �̻��� ����ź�� �ִ� ���
        {
            //anim.SetBool("isShot", false);

            GameObject bomb = Instantiate(grenadeObj);

            bomb.transform.position = grenadePos.position;



            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);


            hasGrenade -= 1; //����ź�� �������� ������ ����
            


        }
        

    }

    void Attack() // �� ����
    {
        if (equipWeapon == null) //���⸦ ���� �ִ� ��쿡�� ����
            return;

        if (Input.GetMouseButtonDown(0)&&(gun==true)) //���콺 ��Ŭ���� ���ÿ� �տ� ���� �ִ� ���
        {
                equipWeapon.Use();

            anim.SetTrigger("doShot");
        }

   
    }

    void Cross() //���ؼ�
    {
        if (Input.GetMouseButton(1) && (gun == true)) //���� ������ä�� ������ ���콺�� �� ������ �������� ���
        {
            crossHair_G = false;
            crossHair_S = true;
            isZoom = true;

        }
        if (Input.GetMouseButtonUp(1) && (gun == true)) //������ ���콺�� ���� �ٽ� �Ϲ� ���
        {
            crossHair_G = true;
            crossHair_S = false;
            isZoom = false;

        }

        crossHair.SetActive(crossHair_G); //���⸦ ������ä�� 1���� ���� ��쿡�� ���ؼ� Ȱ��ȭ
        crossHair_Sniper.SetActive(crossHair_S); //�������� ���ؼ� Ȱ��ȭ

        if (!isZoom)
        {
            Camera.main.fieldOfView = 60.0f; //�� ī�޶� �þ߰���
        }
        else
        {
            Camera.main.fieldOfView = 15.0f; //�Ϲݸ�� ī�޶� �þ߰���
        }


    }


    void Interaction_W() //���� ������� �迭�� ����
    {
        if (nearWeapon != null)
        {
            if (nearWeapon.tag == "Weapon"|| nearWeapon.tag == "Grenade")
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
        if (other.tag == "Weapon"|| other.tag == "Grenade")
        {   
            nearWeapon = other.gameObject;
        }

        if (other.tag == "Grenade")
        {
            hasGrenade++;
           
        }
       
    }

    void OnTriggerExit(Collider other) //���� �ݶ��̴����� ����������
    {
        if (other.tag == "Weapon" || other.tag == "Grenade")
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
