using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    Vector3 direction = Vector3.zero;

    private float inputHorizontal;
    private float inputVertical;

    [SerializeField]
    //移動速度。
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 3.0f;


    private CharacterController CharaCon = null;
    // Use this for initialization
    void Start () {
      
    }

    // Update is called once per frame
    void Update()
    {
        //入力がローカルプレイヤーのみによって処理されるようにする。
        if (!isLocalPlayer)
        {
            return;
        }
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        //回転の処理。
        direction = new Vector3(moveX, 0, moveZ);
        if (direction.magnitude > 0.01f)
        {
            float step = rotateSpeed * Time.deltaTime;
            Quaternion myQ = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, myQ, step);
        }

        transform.Translate(moveX, 0.0f, moveZ, Space.World);
    }

    public override void OnStartLocalPlayer()
    {
        //1Pカラー。
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

}
