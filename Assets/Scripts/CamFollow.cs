using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //������ ��ġ�� ī�޶��� ��ġ�� �����ϰ� �ʹ�

    //ī�޶� �Ѿƴٴ� ��ġ ����
    public Transform followPosition;

    void Start()
    {

    }


    void Update()
    {
        //���� ��ġ�� followPosition ��ġ�� ��ġ��Ų��.
        transform.position = followPosition.position;
    }
}
