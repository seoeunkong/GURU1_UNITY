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
        //1. ������� ���콺 �Է��� �޴´�
        float mouse_X = Input.GetAxis("Mouse X");


        //2. �Է¹��� ���� �̿��ؼ� ȸ�� ������ �����Ѵ�
        mx += mouse_X * rotSpeed * Time.deltaTime;

        //3. ������ ȸ�� ������ ��ü�� ȸ�� �Ӽ��� �����Ѵ�
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
