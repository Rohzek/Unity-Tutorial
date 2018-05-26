using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGUI : MonoBehaviour
{
    AmmoCount ammo;
    Text text;

    void Start()
    {
        ammo = GameObject.Find("GameMaster").GetComponent<AmmoCount>(); ;
        text = GameObject.Find("AmmoCountDisplay").GetComponent<Text>();
    }

    void LateUpdate ()
    {
        text.text = ammo.Ammo.ToString();
	}
}
