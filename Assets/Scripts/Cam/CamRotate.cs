using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    //회전 속력 변수
    public float rotSpeed = 300.0f;

    //회전 각도 제한
    public float rotLimit = 60.0f;

    //회전 누적 변수
    float mx = 0;
    float my = 0;

  

    void Update()
    {

        

        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;
        my = Mathf.Clamp(my, -rotLimit, rotLimit);

        //2. 입력받은 값을 이용해서 회전 방향을 결정한다
        //Vector3 dir = new Vector3(-mouse_Y, mouse_X, 0);
        //dir.Normalize();

        //3. 결정된 회전 방향을 물체의 회전 속성에 대입한다
        // r=r0+vt
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(-my, mx, 0);

        // 4. 회전 값 중에서 x축 값을 -90~90도 사이로 제한
        //Vector3 rot = transform.eulerAngles;
        //rot.x = Mathf.Clamp(rot.x,- 90.0f, 90.0f);
        //transform.eulerAngles = rot;
    }
}
