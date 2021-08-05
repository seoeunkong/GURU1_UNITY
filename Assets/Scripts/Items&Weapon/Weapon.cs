using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public enum Type{ Gun,Grenade};
    public Type type;
    public int damage;
    public float rate;

    //�Ѿ�,ź�ǰ��� ����
    public Transform bulletPos;
    public GameObject bullet;

    public Transform targetPosition;

    public void Use()
    {
        if (type == Type.Gun) //���Ⱑ ���� ��� �ڷ�ƾ ����
        {
            StartCoroutine("Shot"); 
        }

    }

    IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        //�Ѿ˿� �ӵ� �����ϱ�
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
    }
}
