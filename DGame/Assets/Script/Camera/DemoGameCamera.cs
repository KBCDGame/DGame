using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGameCamera : MonoBehaviour {

    private GameObject Player = null;
    private Vector3 PlayerPos = Vector3.zero;

    [SerializeField]
    //回転速度。
    public float RotateSpeed = 200.0f;
    //上に対する回転の上限。
    public float MaxRotateY = 0.0f;
    //下に対する回転の下限。
    public float MinRotateY = 0.0f;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerPos = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの移動量分、自分(カメラ)も移動する
        transform.position += Player.transform.position - PlayerPos;
        PlayerPos = Player.transform.position;

        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            //マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X") * Time.deltaTime * RotateSpeed;
            float mouseInputY = Input.GetAxis("Mouse Y") * Time.deltaTime * RotateSpeed;
            //PlayerPosの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(PlayerPos, Vector3.up, mouseInputX);
            //カメラの垂直移動（角度制限なし）
            transform.RotateAround(PlayerPos, transform.right, mouseInputY );
        }
        //マウスの右が押されていない時は右スティックの倒しを優先する。
        else
        { 
            //右スティックの移動量
            float InputX = Input.GetAxis("Horizontal2") * Time.deltaTime * RotateSpeed;
            float InputY = Input.GetAxis("Vertical2") * Time.deltaTime * RotateSpeed;
            //PlayerPosの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(PlayerPos, Vector3.up, InputX);
            //カメラの垂直移動（角度制限なし）
            transform.RotateAround(PlayerPos, transform.right, InputY);
        }
    }
}
