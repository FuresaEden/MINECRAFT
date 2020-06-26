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

    enum State
    {
        Normal = 0, Jump, Fly
    }

    State state = State.Normal;

    Vector3 val;


    public float sensitivity = 1f; // いわゆるマウス感度
    private float mouse_move_x;
    private float rotation_y = 0f;

    //プレイヤーの移動速度
    public float speed = 5.0f;
    Rigidbody rb;

    [SerializeField]
    Vector3 moveForward;

    // ブロックを設置する位置を一応リアルタイムで格納
    private Vector3 pos;

    [SerializeField]
    public GameObject blockPrefab;

    // Use this for initialization
    void Start()
    {

        //プレイヤーのRigidbodyを読み込む
        rb = this.GetComponent<Rigidbody>();

        // ↓ 画面中央の平面座標を取得する
        displayCenter = new Vector2(Screen.width / 2, Screen.height / 2);


        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        mouse_move_x = Input.GetAxis("Mouse X") * sensitivity;

        //回転角度を変更
        rotation_y += mouse_move_x;

        //rotationSpeed度回転
        transform.rotation = Quaternion.Euler(0, rotation_y, 0);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (state != State.Fly) state = State.Fly;
            else state = State.Normal;
        }

        float x = Vector3.forward.x * Input.GetAxis("Horizontal");
        float z = Vector3.forward.z * Input.GetAxis("Vertical");

        Debug.Log("Horizontal:" + Input.GetAxis("Horizontal"));
        Debug.Log("Vertical:" + Input.GetAxis("Vertical"));

        //Rigidbodyに力を加える
        rb.AddForce(x * 50f, 0, z * 50f, ForceMode.Acceleration);

        switch (state)
        {
            case State.Normal:
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity += new Vector3(0.0f, junp, 0.0f);
                    state = State.Jump;
                    Debug.Log("押されてる");
                }
                break;

            case State.Fly:
                if (Input.GetKey(KeyCode.Space))
                {
                    //val.y += 5f * Time.deltaTime;
                }
                break;
        }

        //this.transform.position += val;

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

    private void OnTriggerEnter(Collider other)
    {
        if (state == State.Jump)
        {
            state = State.Normal;
        }
    }
}