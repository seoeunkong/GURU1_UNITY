using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //������,���� Ÿ���̶� value�� �����Ҽ� �ְ� ��
    public enum Type { Heart, Ammo,Grenade,Weapon};
    public Type type;
    public int value;

    //������ ���ڸ����� ȸ���ϰ� ����� �Լ�
    private void Update()
    {
        transform.Rotate(Vector3.up * 25 * Time.deltaTime);
    }
}
