using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotUseWeapons : MonoBehaviour
{
    public enum Type
    {
        Grenade,
        Gun
    }

    public Type type; // ���� ����
    public int value; //���� ��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }
}
