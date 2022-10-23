using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RocksController : MonoBehaviour
{
    public LogicController lc;
    public Text rocksText;
    private float rocksCnt;
    // Start is called before the first frame update
    void Start()
    {
        rocksCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rocksCnt = lc.rocks;
        rocksText.text = "ROCKS: " + rocksCnt.ToString();

    }
}