using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Speed = 1;
    public float Hp;
    private float m_MaxHp = 100;

	// Use this for initialization
	void Start () {
        Hp = 30;
        EventManager.TriggerEvent("HealthChanged");
    }

    // Update is called once per frame
    void Update () {
        transform.position += transform.forward * Speed * Time.deltaTime;
	}

    public void Heal(float a_Heal)
    {
        Hp += a_Heal;

        if (Hp > m_MaxHp)
        {
            Hp = m_MaxHp;
        }

        EventManager.TriggerEvent("HealthChanged");
    }

    public void TakeDamage(float a_Damage)
    {
        Hp -= a_Damage;

        if (Hp < 0)
        {
            Hp = 0;
            Die();
        }

        EventManager.TriggerEvent("HealthChanged");
    }

    private void Die()
    {
        // TODO
    }
}
