using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockButton : MonoBehaviour
{
    [SerializeField]
    Vector3 ButtonPos;

    [SerializeField]
    List<GameObject> block = new List<GameObject>();


    [SerializeField]
    GameObject nowBlock;


    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        nowBlock = block[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nowBlock = block[0];        //木材ブロック
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nowBlock = block[1];        //木材ハーフブロック
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nowBlock = block[2];        //ガラスブロック
        }

        player.GetComponent<PlayerCntl>().blockPrefab = nowBlock;
    }
}
