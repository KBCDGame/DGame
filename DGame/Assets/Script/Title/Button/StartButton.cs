using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour {
    void Start()
    {
        // Buttonクリック時、OnClickメソッドを呼び出す
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // 「GameScene」シーンに遷移する
        SceneManager.LoadScene("DGame");
    }
}
