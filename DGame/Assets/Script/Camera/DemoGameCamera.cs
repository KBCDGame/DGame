using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGameCamera : MonoBehaviour {

    //プレイヤー。
    private GameObject Player = null;
    //プレイヤーの位置。
    private Vector3 PlayerPos = Vector3.zero;
    //入力値保持用。
    private float InputX, InputY, Scroll = 0.0f;
    //カメラ。
    private Camera GemaCamera = null;
    ////ズームに使う値保持用。
    //private float Scroll, View = 0.0f;

    [SerializeField]
    //回転速度。
    public float RotateSpeed = 200.0f;
    //上に対する回転の上限。
    public float MaxRotateY = 0.0f;
    //下に対する回転の下限。
    public float MinRotateY = 0.0f;
    ////ズームの最大と最小。
    //public float ZoomMax,ZoomMin = 0.0f;
    ////ズームの時のスピード。
    //public float ZoomSpeed = 1.0f;
    //


    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerPos = Player.transform.position;
        GemaCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの移動量分、自分(カメラ)も移動する
        transform.position += Player.transform.position - PlayerPos;
        PlayerPos = Player.transform.position;

        //Input関係の処理。
        InputRotate();

        //ズーム。
        //CameraZoom();

       
        //PlayerPosの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(PlayerPos, Vector3.up, InputX);

        //前後のカメラ移動。
        if (Input.GetKey(KeyCode.Joystick1Button4))
        {
            CameraMoveBeforeAndAfter();
        }
        else
        {
            //カメラの垂直移動（角度制限なし）
            transform.RotateAround(PlayerPos, transform.right, InputY);
        }
            
    }

    //Inputの回転をまとまたもの。
   private void InputRotate()
    {
        float DeltaSpeed = Time.deltaTime * RotateSpeed;
        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            //マウスの移動量
            InputX = Input.GetAxis("Mouse X") * DeltaSpeed;
            InputY = Input.GetAxis("Mouse Y") * DeltaSpeed;
        }
        //マウスの右が押されていない時は右スティックの倒しを優先する。
        else
        {
            //右スティックの移動量
            InputX = Input.GetAxis("Horizontal2") * DeltaSpeed;
            InputY = Input.GetAxis("Vertical2") * DeltaSpeed;
        }

        //ホイールの入力。
        Scroll = Input.GetAxis("Mouse ScrollWheel");
    }

    ////カメラのズーム処理。
    //private void CameraZoom()
    //{
    //    float view = GemaCamera.fieldOfView - Scroll * ZoomSpeed;
    //    //Clampを使ってマイナスや極端に大きな値にならなよう調整。
    //    GemaCamera.fieldOfView = Mathf.Clamp(value: view, min:ZoomMin, max: ZoomMax);
    //}

    //ホイールとパッドを使ったカメラの前後移動。
    private void CameraMoveBeforeAndAfter()
    {
        //カメラの移動。
        //transform.position.Set(transform.position.x, transform.position.y, transform.position.z + -InputY * Time.deltaTime * 10.0f);
        //float distance = Vector3.Distance(transform.position, PlayerPos);
        if (Mathf.Abs(Vector3.Distance((transform.forward * -InputY * Time.deltaTime * 10.0f),PlayerPos)) < 80.0f && Mathf.Abs(Vector3.Distance((transform.forward * -InputY * Time.deltaTime * 10.0f), PlayerPos)) > 3.0f)
        {
            transform.position += transform.forward * -InputY * Time.deltaTime * 10.0f;
        }
    }

}
