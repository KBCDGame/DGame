using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGameCamera : MonoBehaviour
{

    //プレイヤー。
    private GameObject Player = null;
    //プレイヤーの位置。
    private Vector3 PlayerPos = Vector3.zero;
    //入力量保持用。
    private float InputX, InputY = 0.0f;
    //ホイールの入力量保持用。
    float Scroll = 0.0f;
    ////カメラ。
    //private Camera GemaCamera = null;
    ////ズームに使う値保持用。
    //private float Scroll, View = 0.0f;

    [SerializeField]
    //回転速度。
    public float RotateSpeed = 200.0f;
    //上に対する回転の上限。
    public float MaxRotateY = 0.0f;
    //下に対する回転の下限。
    public float MinRotateY = 0.0f;
    //プレイヤーとカメラ間の最大距離。
    public float MaxDistance = 80.0f;
    //プレイヤーとカメラ間の最小距離。
    public float MinDistance = 3.0f;
    //スタート時にプレイヤからどれだけカメラをずらすか。
    public Vector3 StartOffsetPos = Vector3.zero;
    //コントローラーを使ったカメラの前後移動のスピード。
    public float ControllerCameraMoveBeforeAndAfterSpeed = 2.0f;
    //マウスを使ったカメラの前後移動のスピード。
    public float MouseCameraMoveBeforeAndAfterSpeed = 20.0f;
    ////ズームの最大と最小。
    //public float ZoomMax,ZoomMin = 0.0f;
    ////ズームの時のスピード。
    //public float ZoomSpeed = 1.0f;


    // Use this for initialization
    void Start()
    {
        //最大と最小が反転して設定されていた場合。
        if (MinDistance > MaxDistance)
        {
            float work = MinDistance;
            MinDistance = MaxDistance;
            MaxDistance = work;
            Debug.Log("プレイヤーとカメラ間の最小距離とプレイヤーとカメラ間の最大距離が逆でした。");

        }
        //プレイヤー取得。
        Player = GameObject.FindWithTag("Player");
        ////カメラ取得。
        //GemaCamera = GetComponent<Camera>();
        //プレイヤーの位置取得。
        PlayerPos = Player.transform.position;
        //カメラの位置をプレイヤーからずらした位置に設定。
        Vector3 newPos = PlayerPos + StartOffsetPos;
        newPos.z += MinDistance;
        transform.position += newPos;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの移動量分、自分(カメラ)も移動する
        transform.position += Player.transform.position - PlayerPos;
        PlayerPos = Player.transform.position;

        //Input関係の処理。
        InputRotate();

        //PlayerPosの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(PlayerPos, Vector3.up, InputX);

        //カメラの前後移動。
        CameraMoveBeforeAndAfter();

        //カメラの垂直移動（角度制限なし）
        transform.RotateAround(PlayerPos, transform.right, InputY);
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
        var controllerNames = Input.GetJoystickNames();

        float value;
        float speed;
        //コントローラーが接続されていないならホイールの入力量。
        if (controllerNames[0] == "")
        {
            value = Scroll;
            speed = MouseCameraMoveBeforeAndAfterSpeed;
        }
        //コントローラーが接続されているなら右スティックの入力量。
        else
        {
            value = InputY;
            speed = ControllerCameraMoveBeforeAndAfterSpeed;
        }

        //どれくらい移動するか。
        Vector3 MovePos = transform.forward * -value * Time.deltaTime * speed;

        //カメラとプレイヤーの距離計算。
        float distance = (transform.position.z + MovePos.z) - PlayerPos.z;

        //スティックが前に倒されている。
        if (value > 0.0f)
        {
            //プレイヤーとカメラの距離が指定範囲内なら移動。
            if (Mathf.Abs(distance) < 80.0f)
            {
                transform.position += MovePos;
                return;
            }
        }
        //右スティックが後ろに倒されている。
        else if (value < 0.0f)
        {
            //プレイヤーとカメラの距離が指定範囲内なら移動。
            if (Mathf.Abs(distance) > 3.0f)
            {
                transform.position += MovePos;
                return;
            }
        }
    }
}