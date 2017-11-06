using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    //移動速度。
    private float moveSpeed = 1.0f;
    //回転速度。
    private float rotateSpeed = 3.0f;

    //座標計算用。
    float moveX = 0.0f, moveZ = 0.0f;
    // Use this for initialization
    void Start () {
        //座標初期化。
        moveX = transform.localPosition.x;
        moveZ = transform.localPosition.z;
    }
	
	// Update is called once per frame
	void Update () {

        //入力がローカルプレイヤーのみによって処理されるようにする。
        if (!isLocalPlayer)
        {
            return;
        }
        //座標計算。
        moveX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        moveZ = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        //回転の処理。
        Vector3 direction = new Vector3(moveX, 0, moveZ);
        if (direction.magnitude > 0.01f)
        {
            float step = rotateSpeed * Time.deltaTime;
            Quaternion myQ = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, myQ, step);
        }

        //座標と回転を反映。
        transform.Translate(moveX, 0.0f, moveZ, Space.World);
    }

    public override void OnStartLocalPlayer()
    {
        //1Pカラー。
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
