using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_HUD : MonoBehaviour {

    public Text HpTxt;

    [SerializeField]
    private Player m_Player;

    private void OnEnable()
    {
        EventManager.AddListner("HealthChanged", RefreshHp);
    }

    private void OnDisable()
    {
        EventManager.RemoveListner("HealthChanged", RefreshHp);
    }

    public void RefreshHp()
    {
        HpTxt.text = m_Player.Hp.ToString();
    }
}
