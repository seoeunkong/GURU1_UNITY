using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    // ���ʹ�_��������1 ������Ʈ ����
    public Enemy_stage1 eStage1;

    public void OnHit()
    {
        // �÷��̾�� �������� �ִ� �Լ��� �����Ѵ�.

        eStage1.HitEvent();
    }
}
