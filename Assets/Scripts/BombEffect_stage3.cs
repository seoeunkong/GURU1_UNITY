using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect_stage3 : MonoBehaviour
{
    public GameObject explosion;
    public float explosionRadius;
    public int bombPower;

    //public GameObject boss;



    public GameObject boss;


    //�浹�ϸ� ���� ��ƼŬȿ�� ����
    //�ڽ��� ����
    void OnCollisionEnter(Collision collision)
    {
        //= GameObject.Find("BossMonster")

        GameObject go = Instantiate(explosion);
        go.transform.position = transform.position;



        //�ڽ��� �������� �����ݰ��� �˻�. �׾ȿ� ������ ã�´�.
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);
        Collider[] enemies2 = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 9);
        Destroy(gameObject);
        //����ź �������� ������.
        //BOSS_stage3 b = boss.GetComponent<BOSS_stage3>();


        for (int i = 0; i < enemies.Length+1; i++)
        {
            if (i < enemies.Length)
            {
                Enemy_stage3 eFSM = enemies[i].transform.GetComponent<Enemy_stage3>();

                eFSM.HitEnemy(bombPower);
            }

            BOSS_stage3 eFSM2 = enemies2[i].transform.GetComponent<BOSS_stage3>();


            eFSM2.HitEnemy(bombPower);

        }


    }
}
