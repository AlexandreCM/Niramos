using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Time : MonoBehaviour
{
    [SerializeField]
    private float temps;
    [SerializeField]
    private Text timeLabel;

    [SerializeField]
    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isRunning) {
            this.temps -= Time.deltaTime;

            if (temps <= 0.0f) {
                this.temps = 0.00f;
                this.updateTimer();
                this.disableTimer();
            }

        this.updateTimer();
        }
        
    }

    void disableTimer() {
        this.enabled = false;
    }

    void updateTimer() {
        this.timeLabel.text = (Mathf.Round(temps * 100.0f) / 100.0f).ToString();
    }

    public void start() {
        this.isRunning = true;
    }

    public void stop() {
        this.isRunning = false;
    }
}
