using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //������ Ÿ���̶� value�� �����Ҽ� �ְ� ��
    public enum Type { Heart, Ammo};
    public Type type;
    public int value;

    //������ ���ڸ����� ȸ���ϰ� ����� �Լ�
    //�ӵ� �� ���̽÷��� ���� �ø��ø� �Ǿ��!
    private void Update()
    {
        transform.Rotate(Vector3.up * 25 * Time.deltaTime);
    }
}