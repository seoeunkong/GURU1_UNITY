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
    public int value; //���� ��

    
    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }
}
