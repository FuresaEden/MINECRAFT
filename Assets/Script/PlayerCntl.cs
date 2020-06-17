using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntl : MonoBehaviour
{

    Vector2 displayCenter;

    //プレイヤーの位置を格納
    [SerializeField]
    float moveX;
    [SerializeField]
    float moveZ;

    [SerializeField]
    float junp = 5.0f;

    Vector3 val;


    //プレイヤーの移動速度
    public float speed = 5.0f;
    Rigidbody rd;

    [SerializeField]
    Vector3 moveForward;

    // ブロックを設置する位置を一応リアルタイムで格納
    private Vector3 pos;

    [SerializeField]
    private GameObject blockPrefab;

    // Use this for initialization
    void Start()
    {
        
        //プレイヤーのRigidbodyを読み込む
        rd = this.GetComponent<Rigidbody>();

        // ↓ 画面中央の平面座標を取得する
        displayCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 rot = new Vector3(Camera.main.transform.rotation.x,0f, Camera.main.transform.rotation.z);
        //this.transform.rotation = Quaternion.LookRotation(rot);
        //this.transform.Rotate(new Vector3(transform.rotation.x,0,transform.rotation.z));

        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        val =Camera.main.transform.rotation * new Vector3(moveX,0,moveZ);
        val.y = 0f;
        this.transform.position += val * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rd.velocity += new Vector3(0.0f, junp, 0.0f);
            Debug.Log("押されてる");
        }

        // ↓ 「カメラからのレイ」を画面中央の平面座標から飛ばす
        Ray ray = Camera.main.ScreenPointToRay(displayCenter);
        // ↓ 当たったオブジェクト情報を格納する変数
        RaycastHit hit;

        // ↓ Physics.Raycast() でレイを飛ばす
        if (Physics.Raycast(ray, out hit))
        {
            // ↓ 生成位置の変数の値を「ブロックの向き + ブロックの位置」
            pos = hit.normal + hit.collider.transform.position;

            // ↓ 右クリック
            if (Input.GetMouseButtonDown(1))
            {
                // 生成位置の変数の座標にブロックを生成
                Instantiate(blockPrefab, pos, Quaternion.identity);
            }

            // ↓ 左クリック
            if (Input.GetMouseButtonDown(0))
            {
                // ↓ レイが当たっているオブジェクトを削除
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
