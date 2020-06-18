using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockButton : MonoBehaviour
{
    [SerializeField]
    Vector3 ButtonPos;

    [SerializeField]
    GameObject block;

    GameObject blocks;
    // Start is called before the first frame update
    void Start()
    {
        blocks = gameObject.GetComponent<PlayerCntl>().blockPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = ButtonPos;
    }

    public void OnClick()
    {
        blocks = block;
    }
}
