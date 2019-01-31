using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD : MonoBehaviour
{

    [SerializeField]
    private Text vie_1;
    [SerializeField]
    private Text vie_2;
    [SerializeField]
    private Text vie_3;
    [SerializeField]
    private Text vie_4;

    // Enabled
    void onEnable() {}

    // Disabled
    void onDisable() {}

    // Refresh HP Count
    void updateHealth(Text vie, Text t) {
        vie.text = t;
    }
}
