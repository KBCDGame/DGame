using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGameCamera : MonoBehaviour {

    private GameObject Player = null;
    private Vector3 PlayerPos = Vector3.zero;
    private Camera DemoCamera = null;

    [SerializeField]
    //回転速度。
    public float RotateSpeed = 200.0f;
    //上に対する回転の上限。
    public float MaxRotateY = 45.0f;
    //下に対する回転の下限。
    public float MinRotateY = 0.0f;
    //プレイヤーからカメラをどれくらい離すかの距離。
    public Vector3 CameraDistance = Vector3.zero;
    //ズームの最小倍率。
    public float ZoomMin = 0.0f;
    //ズームの最大倍率。
    public float ZoomMax = 45.0f;

    // Use this for initialization
    void Start()
    {
        //プレイヤー取得。
        Player = GameObject.Find("NoboPlayer");
        PlayerPos = Player.transform.position;
        //カメラの位置をプレイヤーの位置から離した位置に設定。
        transform.position = PlayerPos + CameraDistance;

        DemoCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの移動量分、自分(カメラ)も移動する
        transform.position += Player.transform.position - PlayerPos;
        //transform.position = transform.position + CameraDistance;
        PlayerPos = Player.transform.position;

        //入力量保持用。
        float InputX = 0.0f;
        float InputY = 0.0f;

        // マウスの右クリックを押している間。
        if (Input.GetMouseButton(1))
        {
            //マウスの移動量。
            InputX = Input.GetAxis("Mouse X") * Time.deltaTime * RotateSpeed;
            InputY = Input.GetAxis("Mouse Y") * Time.deltaTime * RotateSpeed;
        }
        //マウスの右が押されていない時は右スティックの倒しを優先する。
        else
        {
            //右スティックの移動量。
            InputX = Input.GetAxis("Horizontal2") * Time.deltaTime * RotateSpeed;
            InputY = Input.GetAxis("Vertical2") * Time.deltaTime * RotateSpeed;
        }

        //マウスを使ったカメラのズーム処理。
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //現在のカメラの視野角から入力された分を引く。
        float view = DemoCamera.fieldOfView - scroll;
        //Clmpを使ってズームの最大、最小に収まるよう補正。
        DemoCamera.fieldOfView = Mathf.Clamp(view, ZoomMin, ZoomMax);

        //PlayerPosの位置のY軸を中心に、回転（公転）する。
        transform.RotateAround(PlayerPos, Vector3.up, InputX);
        //カメラの垂直移動（角度制限なし）。
        transform.RotateAround(PlayerPos, transform.right, InputY);
        //if (transform.localEulerAngles.x > MaxRotateY)
        //{
        //    transform.rotation = Quaternion.Euler(MaxRotateY - 1.0f, 0.0f, 0.0f);
        //}
    }
}
