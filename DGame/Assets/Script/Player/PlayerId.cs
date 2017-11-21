using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerId : NetworkBehaviour {
    //SyncVar:[command]で変更後、全クライアントへ変更結果を送信。
    [SyncVar]
    private string playerUniqueIdentity;
    private GameObject Player;
    private NetworkInstanceId playerNetID;
    private Transform myTrabsform;
    //ネットワークマネージャーでPlayerのプレハブが生成されたとき実行。
    public override void OnStartLocalPlayer()
    {
        GetNetIdrntity();
        SetIdentity();
      
    }

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    void Awake()
    {
        //自分の名前を設定するときに使う
        myTrabsform = transform;
    }

    void Update()
    {
        //例外が発生したとき
        if (myTrabsform.name == "" || myTrabsform.name == "Player(Clone)")
        {
            SetIdentity();
        }
    }

    [Client]
    void GetNetIdrntity()
    {
        //NetworkIdentityのNetID取得
        playerNetID = GetComponent<NetworkIdentity>().netId;
        //名前を付けるメソッド
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    void SetIdentity()
    {
        //自分以外のPlayerオブジェクトの場合
        if (!isLocalPlayer)
        {
            //今ついている名前
            myTrabsform.name = playerUniqueIdentity;
        }
        else
        {
            //自分自身の場合、MakeUniqueidentityメソッドで名前を取得
            myTrabsform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        //player +NetID
        string uniqueName = Player + playerNetID.ToString();
        return uniqueName;
    }
    //Cmmand:SyncVar変数を変更し、変更結果をクライアントへ送る
    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerUniqueIdentity = name;
    }


}
