using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public enum Type
    {
        Grenade,
        Gun
    }

    public Type type; // ���� ����
    //public BoxCollider Area;
    public int damage;
    public float rate;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if (type == Type.Gun)
        {
            StartCoroutine("Shot");
        }
    }


    IEnumerator Shot()
    {
        //�Ѿ� �߻�
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
        //ź�� ����
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
    }
}
