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

    public Type type; // 무기 종류
    public int value; //무기 값

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
