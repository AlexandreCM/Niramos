using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
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
        vie.text = t.text;
    }
}
