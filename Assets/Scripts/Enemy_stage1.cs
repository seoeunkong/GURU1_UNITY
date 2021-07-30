using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_stage1 : MonoBehaviour
{
    // 상태 별로 에너미가 상황에 맞는 행동을 하게 하고 싶다!
    // 1. 에너미의 상태
    // 2. 상태별 함수
    // 3. switch문을 통해서 상태를 체크하고, 상태별 함수를 실행한다.


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

    // 플레이어 게임 오브젝트
    GameObject player;

    // 감지 범위
    public float findDistance = 8.0f;

    // 나의 캐릭터 컨트롤러
    CharacterController cc;

    // 이동 속도
    public float moveSpeed = 5.0f;

    // 공격 가능 범위
    public float attackDistance = 2.0f;

    // 현재 누적 시간 변수
    float currentTime = 0;

    // 공격 딜레이 시간
    public float attackDelayTime = 2.0f;

    // 공격력 변수
    public int attackPower = 10;

    // 초기 위치 저장용 변수
    Vector3 originPos;

    // 이동 가능한 거리
    public float moveDistance = 20.0f;

    // 최대 체력 변수
    public int maxHp = 5;

    // 현재 체력 변수
    int currentHp;

    void Start()
    {
        // 초기 상태는 대기 상태(Idle)
        enemyState = EnemyState.Idle;

        // 플레이어 검색
        player = GameObject.Find("Player");

        // 캐릭터 컨트롤러 받아오기
        cc = GetComponent<CharacterController>();

        // 초기 위치 저장하기
        originPos = transform.position;

        // 현재 체력 설정
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
        // 만일, 플레이어와의 거리가 감지 범위 이내라면...
        if(Vector3.Distance(player.transform.position, transform.position) <= findDistance)
        {
            // 상태를 이동 상태로 변경한다.
            enemyState = EnemyState.Move;
            print("상태 전환 : Idle -> Move");
        }


    }
    void Move()
    {
        // 만일 이동 거리 밖이라면...
        if(Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            // 상태를 복귀 상태로 전환한다.
            enemyState = EnemyState.Return;
            print("상태 전환 : Move -> Return");
        }



        // 만일, 공격 범위 밖이라면...
        else if(Vector3.Distance(player.transform.position, transform.position) > attackDistance)
        {
            // 이동 방향을 구한다.
            Vector3 dir = (player.transform.position - transform.position).normalized;

            // 캐릭터 컨트롤러로 이동 방향으로 이동한다.
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // 공격 범위 안에 들어오면...
        else
        {
            // 상태를 공격 상태로 변경한다.
            enemyState = EnemyState.Attack;
            print("상태 전환 : Move -> Attack");

            // 공격 대기 시간을 미리 누적
            currentTime = attackDelayTime;
        }
    }
    void Attack()
    {
        // 만일, 플레이어가 공격 범위 이내라면...
        if(Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            // 만일, 현재 대기 시간이 공격 대기 시간을 넘어갔다면...
            if (currentTime >= attackDelayTime)
            {
                currentTime = 0;
                // 플레이어를 공격한다.
                print("공격!");
                Player pm = player.GetComponent<Player>();
                pm.OnDamage(attackPower);

            }
            else
            {
                // 시간을 누적한다.
                currentTime += Time.deltaTime;
            }
        }
        else
        {
            // 상태를 이동 상태로 전환한다.
            enemyState = EnemyState.Move;
            print("상태 전환 : Attack -> Move");
        }
        
    }
    // 복귀 시 행동 함수
    void Return()
    {
        // 만일, 원래 위치에 도달하지 않았다면, 그 방향으로 이동한다.
        if(Vector3.Distance(originPos, transform.position) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }

        // 원래 위치에 도달하면 대기 상태로 전환한다.
        else
        {
            transform.position = originPos;
            enemyState = EnemyState.Idle;
            print("상태 전환 : Return -> Idle");

            // 체력을 최대치로 회복한다.
            currentHp = maxHp;
        }
    }


    // 피격 시 행동 함수
    void Damaged()
    {

    }


    // 사망 시 행동 함수
    void Die()
    {

    }




}
