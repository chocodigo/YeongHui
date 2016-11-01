using UnityEngine;
using System;
using System.Collections;

public class GameLoading_Mgr : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadStart());

        GPGS_Mgr.GetInstance.InitializeGPGS(); // 초기화 

        Destroy(this.gameObject);
    }
}              