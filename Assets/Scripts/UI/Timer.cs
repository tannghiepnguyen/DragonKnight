using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI TMP_TimerUI;
    private bool isContinue = false;

    public float currentTime = 0.2f;

    void Start()
    {
        isContinue = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        TMP_TimerUI.text = TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss\\.fff");
        
        
    }

    void StopTimer(){
        isContinue = false;
    }

    void StartTimer(){
        isContinue = true;
    }


}
