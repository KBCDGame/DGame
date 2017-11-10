using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayer : MonoBehaviour {

    //入力された値保持用。
    private float InputHorizontal = 0.0f;
    private float InputVertical = 0.0f;

    [SerializeField]
    //移動速度。
    public float MoveSpeed = 0.0f;
    //ジャンプ速度。
    public float JumpSpeed = 0.0f;
    //重力。
    public float Gravity = 0.0f;
    //カメラ。
    private GameObject Camera = null;
    //キャラクターコントローラー。
    private CharacterController CharaCon = null;
    //移動方向。
    private Vector3 MoveDirection = Vector3.zero;


    // Use this for initialization
    void Start()
    {
        CharaCon = GetComponent<CharacterController>();
        Camera = GameObject.Find("DemoCamera");
    }

    // Update is called once per frame
    void Update()
    {
        float y;

        y = MoveDirection.y;

        InputHorizontal = Input.GetAxis("Horizontal");
        InputVertical = Input.GetAxis("Vertical");
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * InputVertical + Camera.transform.right * InputHorizontal;

        MoveDirection.y = y;

        // 移動方向にスピードを掛ける。
        MoveDirection.x = moveForward.x * MoveSpeed;
        MoveDirection.z = moveForward.z * MoveSpeed;

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        //接地判定。
        if (CharaCon.isGrounded)
        {
            //接地中はジャンプ可。
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
        else
        {
            MoveDirection.y -= Gravity * Time.deltaTime;
        }
        //計算した移動量をキャラコンに渡す。
        CharaCon.Move(MoveDirection * Time.deltaTime);

    }

    void Jump()
    {
        MoveDirection.y =  JumpSpeed;
    }
}
