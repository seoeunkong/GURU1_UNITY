using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public GameObject explosion;
    public float explosionRadius = 5.0f;
    public int bombPower = 3;

    //충돌하면 폭발 파티클효과 형성
    //자신을 제거
    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = Instantiate(explosion);
        go.transform.position = transform.position;
      

        Destroy(gameObject);
    }

}
