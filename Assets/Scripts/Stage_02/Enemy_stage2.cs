using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_stage2 : MonoBehaviour
{
    // ���� ���� ���ʹ̰� ��Ȳ�� �´� �ൿ�� �ϰ� �ϰ� �ʹ�!
    // 1. ���ʹ��� ����
    // 2. ���º� �Լ�
    // 3. switch���� ���ؼ� ���¸� üũ�ϰ�, ���º� �Լ��� �����Ѵ�.


    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die

    }



    EnemyState enemyState;

    // �÷��̾� ���� ������Ʈ
    GameObject player;

    // ���� ����
    public float findDistance = 50.0f;

    // ���� ĳ���� ��Ʈ�ѷ�s
    CharacterController cc;

    // �̵� �ӵ�
    public float moveSpeed = 20.0f;

    // ���� ���� ����
    public float attackDistance = 20.0f;

    // ���� ���� �ð� ����
    float currentTime = 0;

    // ���� ������ �ð�
    public float attackDelayTime = 2.0f;

    // ���ݷ� ����
    public int attackPower = 10;

    // �ʱ� ��ġ�� ȸ�� ����� ����
    Vector3 originPos;
    Quaternion originRot;

    // �̵� ������ �Ÿ�
    public float moveDistance = 20.0f;

    // �ִ� ����ü�� ����
    public int maxMonster = 15;

    // ���� ����ü�� ����
    int currentMonster;

    // �����̴� ����
    public Slider StressSlider;

    // �ִϸ����� ������Ʈ ����
    Animator anim;

    // �׺���̼� �޽� ������Ʈ
    NavMeshAgent smith;

    bool gunDamage; //�� �������� �޴� ���
    //�Ѿ� ���ݷ�
    public GameObject game;
    public int BulletPower;

    public bool attack; //���ݿ���

    public GameObject dust; //���Ͱ� �׾��� �� ��Ÿ���� ���� ��ƼŬ
    public GameObject blood; //�ѿ� �¾��� �� ��Ÿ���� ��


    void Start()
    {
        // �ʱ� ���´� ��� ����(Idle)
        enemyState = EnemyState.Idle;

        // �÷��̾� �˻�
        player = GameObject.Find("Player");

        // ĳ���� ��Ʈ�ѷ� �޾ƿ���
        cc = GetComponent<CharacterController>();

        // �ʱ� ��ġ�� ȸ�� �����ϱ�
        originPos = transform.position;
        originRot = transform.rotation;


        // �ڽ� ������Ʈ�� �ִϸ����� ������Ʈ�� ��������
        anim = GetComponentInChildren<Animator>();

        // �׺�޽� ������Ʈ ������Ʈ ��������
        smith = GetComponent<NavMeshAgent>();
        smith.speed = moveSpeed;
        smith.stoppingDistance = attackDistance;

        // ���� ü�� ����
        currentMonster = maxMonster;


    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            /*
        case EnemyState.Return:
            Return();
            break;
            */
            case EnemyState.Damaged:
                //  Damaged();
                break;
            case EnemyState.Die:
                // Die();
                break;

        }
        HitEvent();
        // hp �����̴��� ���� ü�� ������ �����Ѵ�.
        StressSlider.value = (float)currentMonster / (float)maxMonster;

        Bullet b = game.GetComponent<Bullet>();
        BulletPower = b.damage;

        GunDamage();

    }

    void Idle()
    {
        // ����, �÷��̾���� �Ÿ��� ���� ���� �̳����...
        if (Vector3.Distance(player.transform.position, transform.position) <= findDistance)
        {
            // ���¸� �̵� ���·� �����Ѵ�.
            enemyState = EnemyState.Move;
            print("���� ��ȯ : Idle -> Move");
            anim.SetTrigger("IdleToMove");
        }


    }
    void Move()
    {
        /*
        // ���� �̵� �Ÿ� ���̶��...
        if(Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            // ���¸� ���� ���·� ��ȯ�Ѵ�.
            enemyState = EnemyState.Return;
            print("���� ��ȯ : Move -> Return");
        }
        */


        // ����, ���� ���� ���̶��...
        if (Vector3.Distance(player.transform.position, transform.position) > attackDistance)
        {
            // �̵� ������ ���Ѵ�.
            // Vector3 dir = (player.transform.position - transform.position).normalized;

            // ���� ���� ������ �̵� ����� ��ġ��Ų��.
            // transform.forward = dir;

            // ĳ���� ��Ʈ�ѷ��� �̵� �������� �̵��Ѵ�.
            // cc.Move(dir * moveSpeed * Time.deltaTime);


            // ����޽� ������Ʈ�� �̿��Ͽ� Ÿ�� �������� �̵��Ѵ�.
            smith.SetDestination(player.transform.position);
            smith.stoppingDistance = attackDistance;


        }
        // ���� ���� �ȿ� ������...
        else
        {
            // ���¸� ���� ���·� �����Ѵ�.
            enemyState = EnemyState.Attack;
            print("���� ��ȯ : Move -> Attack");

            anim.SetTrigger("MoveToAttackDelay");


            // ���� ��� �ð��� �̸� ����
            currentTime = attackDelayTime;

            // �̵��� ���߰�, Ÿ���� �ʱ�ȭ�Ѵ�.
            smith.isStopped = true;
            smith.ResetPath();
        }
    }
    void Attack()
    {
        // ����, �÷��̾ ���� ���� �̳����...
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            // ����, ���� ��� �ð��� ���� ��� �ð��� �Ѿ�ٸ�...
            if (currentTime >= attackDelayTime)
            {
                currentTime = 0;

                if (currentTime == 0)
                    attack = true;
                // �÷��̾ �����Ѵ�.
                print("����!");
                anim.SetTrigger("StartAttack");

            }

            else
            {
                // �ð��� �����Ѵ�.
                attack = false;
                currentTime += Time.deltaTime;
            }

        }
        else
        {

            // ���¸� �̵� ���·� ��ȯ�Ѵ�.
            enemyState = EnemyState.Move;

            print("���� ��ȯ : Attack -> Move");
            // attack = false;
            anim.SetTrigger("AttackToMove");



        }

    }

    // �÷��̾�� �������� �ִ� �Լ�
    public void HitEvent()
    {
        if (attack == true) //���ʹ̵��� ���� �ڼ��� ���� ���
        {
            Debug.Log("hit");
            Player pm = player.GetComponent<Player>();
            pm.OnDamage(attackPower);
        }
    }


    // ���� �� �ൿ �Լ�
    void Return()
    {
        // ����, ���� ��ġ�� �������� �ʾҴٸ�, �� �������� �̵��Ѵ�.
        if (Vector3.Distance(originPos, transform.position) > 0.1f)
        {
            // Vector3 dir = (originPos - transform.position).normalized;
            // transform.forward = dir;
            // cc.Move(dir * moveSpeed * Time.deltaTime);

            smith.SetDestination(originPos);
            smith.stoppingDistance = 0;
        }

        // ���� ��ġ�� �����ϸ� ��� ���·� ��ȯ�Ѵ�.
        else
        {
            smith.isStopped = true;
            smith.ResetPath();


            transform.position = originPos;
            transform.rotation = originRot;




            enemyState = EnemyState.Idle;
            print("���� ��ȯ : Return -> Idle");
            anim.SetTrigger("MoveToIdle");

            // ü���� �ִ�ġ�� ȸ���Ѵ�.
            currentMonster = maxMonster;
        }
    }


    // �ǰ� �� �ൿ �Լ�
    void Damaged()
    {
        // �ڷ�ƾ �Լ��� �����Ѵ�.
        StartCoroutine(DamageProcess());
    }
    IEnumerator DamageProcess()
    {
        // 2�ʰ� �����Ѵ�.
        yield return new WaitForSeconds(1.0f);

        // ���¸� �̵� ���·� ��ȯ�Ѵ�.
        enemyState = EnemyState.Move;
        print("���� ��ȯ : Damaged -> Move");

    }

    // ��� �� �ൿ �Լ�
    void Die()
    {
        // ������ ����� �ڷ�ƾ���� ��� ����
        StopAllCoroutines();
        // ��� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ��� ��Ȱ��ȭ�Ѵ�.
        cc.enabled = false;


        // 2�ʰ� ��ٷȴٰ� ��ü�� �����Ѵ�.
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);

    }

    // ������ ó�� �Լ�
    public void HitEnemy(int value)
    {
        // ����, ���� ���°� �ǰ�, ����, ��� ������ ������ �Լ��� �����Ѵ�.
        if (enemyState == EnemyState.Damaged || enemyState == EnemyState.Return || enemyState == EnemyState.Die)
        {
            return;
        }

        currentMonster -= value;


        // ����, ���� ���� ü���� 0���� ũ�ٸ�...
        if (currentMonster > 0)
        {
            // ���¸� �ǰ� ���·� ��ȯ�Ѵ�
            enemyState = EnemyState.Damaged;
            print("���� ��ȯ : Any state -> Damaged");
            anim.SetTrigger("Damaged");
            Damaged();

        }
        // �׷��� �ʴٸ�,
        else
        {
            GameObject go = Instantiate(dust); //���� ��ƼŬ(�ǰ�ȿ��)
            go.transform.position = transform.position;

            // ���¸� ��� ���·� ��ȯ�Ѵ�.
            enemyState = EnemyState.Die;
            print("���� ��ȯ : Any state -> Die");
            anim.SetTrigger("Die");
            Die();

            Destroy(go, 5.0f);
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon") //���� ���Ϳ� ���� ��ü�� �Ѿ��̶��
        {
            gunDamage = true;
            Debug.Log("��������" + currentMonster);

            GameObject go = Instantiate(blood); //��(�ǰ�ȿ��)
            go.transform.position = collision.transform.position;
            Destroy(go, 1.0f);
        }


    }

    void GunDamage()
    {
        if (gunDamage == true)
        {
            HitEnemy(BulletPower); //���� ü���� �Ѿ� ���ݷ¸�ŭ �پ���
            gunDamage = false;
        }
    }
}
