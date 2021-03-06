﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntl : MonoBehaviour
{
    public float sensitivity = 1f; // いわゆるマウス感度
    private float mouse_move_y;


    //x軸回転角度の最大値
    public float max_rotation_x = 0f;
    //現在の回転角度
    private float rotation_x = 0f;

    public GameObject gameObj;

    // Update is called once per frame
    void Update()
    {
        mouse_move_y = Input.GetAxis("Mouse Y") * sensitivity;
        
        //回転角度を変更
        rotation_x -= mouse_move_y;

        if (rotation_x >= max_rotation_x)
        {
            mouse_move_y = 0;
            //rotation_x = max_rotation_x;

        }
        if (rotation_x <= -max_rotation_x)
        {
            mouse_move_y = 0;
            //rotation_x = -max_rotation_x;
        }

        Debug.Log("rotation.x:" + transform.rotation.x);

        //rotationSpeed度回転
        transform.Rotate(new Vector3(1, 0, 0), -mouse_move_y);
    }
}
