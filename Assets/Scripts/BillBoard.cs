using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
   

    
    void Update()
    {
        // ���� ī�޶��� ���� ����� ���� ���� ������ ��ġ��Ų��.
        transform.forward = Camera.main.transform.forward;
    }
}
