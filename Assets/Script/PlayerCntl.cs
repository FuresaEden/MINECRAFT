using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntl : MonoBehaviour
{
    //プレイヤーの位置を格納
    [SerializeField]
    float moveX;
    [SerializeField]
    float moveZ;

    [SerializeField]
    float jump = 5.0f;

    enum State
    {
        Normal = 0, Jump, Fly
    }

    State state = State.Normal;

    Vector3 val;

    //プレイヤーの移動速度
    public float speed = 5.0f;
    Rigidbody rb;

    [SerializeField]
    Vector3 moveForward;


    private float mouse_move_x;
    private float rotation_y = 0f;

    public float sp = 3f;
    
    private float h, v;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        //プレイヤーのRigidbodyを読み込む
        rb = this.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (state != State.Fly) state = State.Fly;
            else state = State.Normal;
        }

        mouse_move_x = Input.GetAxis("Mouse X") * 1f;

        rotation_y += mouse_move_x;

        //rotationSpeed度回転
        transform.rotation = Quaternion.Euler(0, rotation_y, 0);

        float x = Vector3.forward.x * Input.GetAxis("Horizontal");
        float z = Vector3.forward.z * Input.GetAxis("Vertical");
        
        //Rigidbodyに力を加える
        //rb.AddForce(x * 50f, 0, z * 50f, ForceMode.Force);
         h = Input.GetAxis("Horizontal");
         v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            moveDirection = sp * new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.y = rb.velocity.y;
            rb.velocity = moveDirection;
        }

        switch (state)
        {
            case State.Normal:
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity += new Vector3(0.0f, jump, 0.0f);
                    state = State.Jump;
                    //rb.AddForce(0, 5f, 0, ForceMode.Impulse);
                    Debug.Log("押されてる");
                }
                break;

            case State.Fly:
                break;
        }

        //this.transform.position += val;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == State.Jump)
        {
            state = State.Normal;
        }
    }
}