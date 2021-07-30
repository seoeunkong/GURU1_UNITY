using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public GameObject explosion;
    public float explosionRadius;
    public int bombPower;

    //�浹�ϸ� ���� ��ƼŬȿ�� ����
    //�ڽ��� ����
    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = Instantiate(explosion);
        go.transform.position = transform.position;

        //�ڽ��� �������� �����ݰ��� �˻�. �׾ȿ� ������ ã�´�.
       Collider[] enemies= Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);

        //����ź �������� ������.
        for(int i = 0; i < enemies.Length; i++)
        {
           
            Enemy_stage1 eFSM = enemies[i].transform.GetComponent<Enemy_stage1>();
            eFSM.HitEnemy(bombPower);
        }
        Debug.Log(enemies.Length);
        Destroy(gameObject);
    }

}
