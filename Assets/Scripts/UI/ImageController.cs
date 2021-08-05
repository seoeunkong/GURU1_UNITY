using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public int hasGrenades; //����ź ����
    public Text text_grenade; //����ź ���� �ؽ�Ʈ

    public GameObject grenade_img;
    public GameObject gun_img;

    public GameObject use_Grenade; //����ź ���(�����ϰ� �ִٴ� ǥ��)
    public GameObject use_Gun; //�� ���(�����ϰ� �ִٴ� ǥ��)

    bool usingGrenade;
    bool usingGun;

    GameObject gameObject;

    public bool[] hasWeapons;

   
    void Start()
    {
        gameObject = GameObject.Find("Player");
    }

   
    void Update()
    {
        Player player = gameObject.GetComponent<Player>(); //Player��ũ��Ʈ���� ��Ҹ� ���� ��.
        hasGrenades = player.hasGrenade;
        hasWeapons = player.hasWeapons;
        usingGun=player.gun; //���� ����ϰ� �ִ���
        usingGrenade = player.grenade;
    
        //�� ��뿩�ο� ���� UI
        if (hasWeapons[0])
        {
            gun_img.SetActive(true);
            use_Gun.SetActive(false);
        }
        if (usingGun == true)
        {
            use_Gun.SetActive(true);
        }

        //����ź ��뿩�ο� ���� UI
        if (hasWeapons[1])
        {
            grenade_img.SetActive(true);
            use_Grenade.SetActive(false);
        }
        if (usingGrenade == true)
        {
            use_Grenade.SetActive(true);
        }

        if (!hasWeapons[1])
        {
            grenade_img.SetActive(false);
        }

        text_grenade.text = hasGrenades+"";
    }
}
