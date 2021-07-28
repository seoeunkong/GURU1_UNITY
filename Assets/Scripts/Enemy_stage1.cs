using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_stage1 : MonoBehaviour
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
    public float findDistance = 8.0f;

    // ���� ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // �̵� �ӵ�
    public float moveSpeed = 5.0f;

    // ���� ���� ����
    public float attackDistance = 2.0f;

    // ���� ���� �ð� ����
    float currentTime = 0;

    // ���� ������ �ð�
    public float attackDelayTime = 2.0f;

    // ���ݷ� ����
    public int attackPower = 10;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos;

    // �̵� ������ �Ÿ�
    public float moveDistance = 20.0f;

    // �ִ� ü�� ����
    public int maxHp = 5;

    // ���� ü�� ����
    int currentHp;

    void Start()
    {
        // �ʱ� ���´� ��� ����(Idle)
        enemyState = EnemyState.Idle;

        // �÷��̾� �˻�
        player = GameObject.Find("Player");

        // ĳ���� ��Ʈ�ѷ� �޾ƿ���
        cc = GetComponent<CharacterController>();

        // �ʱ� ��ġ �����ϱ�
        originPos = transform.position;

        // ���� ü�� ����
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
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
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;

        }
    }

    void Idle()
    {
        // ����, �÷��̾���� �Ÿ��� ���� ���� �̳����...
        if(Vector3.Distance(player.transform.position, transform.position) <= findDistance)
        {
            // ���¸� �̵� ���·� �����Ѵ�.
            enemyState = EnemyState.Move;
            print("���� ��ȯ : Idle -> Move");
        }


    }
    void Move()
    {
        // ���� �̵� �Ÿ� ���̶��...
        if(Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            // ���¸� ���� ���·� ��ȯ�Ѵ�.
            enemyState = EnemyState.Return;
            print("���� ��ȯ : Move -> Return");
        }



        // ����, ���� ���� ���̶��...
        else if(Vector3.Distance(player.transform.position, transform.position) > attackDistance)
        {
            // �̵� ������ ���Ѵ�.
            Vector3 dir = (player.transform.position - transform.position).normalized;

            // ĳ���� ��Ʈ�ѷ��� �̵� �������� �̵��Ѵ�.
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // ���� ���� �ȿ� ������...
        else
        {
            // ���¸� ���� ���·� �����Ѵ�.
            enemyState = EnemyState.Attack;
            print("���� ��ȯ : Move -> Attack");

            // ���� ��� �ð��� �̸� ����
            currentTime = attackDelayTime;
        }
    }
    void Attack()
    {
        // ����, �÷��̾ ���� ���� �̳����...
        if(Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            // ����, ���� ��� �ð��� ���� ��� �ð��� �Ѿ�ٸ�...
            if (currentTime >= attackDelayTime)
            {
                currentTime = 0;
                // �÷��̾ �����Ѵ�.
                print("����!");
                Player pm = player.GetComponent<Player>();
                pm.OnDamage(attackPower);

            }
            else
            {
                // �ð��� �����Ѵ�.
                currentTime += Time.deltaTime;
            }
        }
        else
        {
            // ���¸� �̵� ���·� ��ȯ�Ѵ�.
            enemyState = EnemyState.Move;
            print("���� ��ȯ : Attack -> Move");
        }
        
    }
    // ���� �� �ൿ �Լ�
    void Return()
    {
        // ����, ���� ��ġ�� �������� �ʾҴٸ�, �� �������� �̵��Ѵ�.
        if(Vector3.Distance(originPos, transform.position) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }

        // ���� ��ġ�� �����ϸ� ��� ���·� ��ȯ�Ѵ�.
        else
        {
            transform.position = originPos;
            enemyState = EnemyState.Idle;
            print("���� ��ȯ : Return -> Idle");

            // ü���� �ִ�ġ�� ȸ���Ѵ�.
            currentHp = maxHp;
        }
    }


    // �ǰ� �� �ൿ �Լ�
    void Damaged()
    {

    }


    // ��� �� �ൿ �Լ�
    void Die()
    {

    }




}
