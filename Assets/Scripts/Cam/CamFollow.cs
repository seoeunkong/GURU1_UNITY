using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    //ī�޶� �Ѿƴٴ� ��ġ ����
    public Transform followPosition;
    void Update()
    {
        //���� ��ġ�� followPosition ��ġ�� ��ġ��Ų��.
        transform.position = followPosition.position;
    }
}
