using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    //ȸ�� �ӷ� ����
    public float rotSpeed = 300.0f;

    //ȸ�� ���� ����
    float mx = 0;


    void Update()
    {
        //������� ���콺 �Է��� �޾Ƽ� ��ü�� �����¿�� ȸ����Ű�� �ʹ�
        //1. ������� ���콺 �Է��� �޴´�
        float mouse_X = Input.GetAxis("Mouse X");


        //2. �Է¹��� ���� �̿��ؼ� ȸ�� ������ �����Ѵ�
        //Vector3 dir = new Vector3(0, mouse_X, 0);
        //dir.Normalize();
        mx += mouse_X * rotSpeed * Time.deltaTime;

        //3. ������ ȸ�� ������ ��ü�� ȸ�� �Ӽ��� �����Ѵ�
        // r=r0+vt
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
