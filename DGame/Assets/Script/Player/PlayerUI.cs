using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
    private GameObject namePlate;
    public Text nameText;

     void Start()
    {
        namePlate = nameText.transform.parent.gameObject;
    }
    void LateUpdate()
    {
        namePlate.transform.rotation = Camera.main.transform.rotation;
    }

    void SetName(string name)
    {
        nameText.text = name;
    }
}
