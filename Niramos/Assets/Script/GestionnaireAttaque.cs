﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DamageEvent : UnityEvent<float, string, float>
{

}
public class GestionnaireAttaque : MonoBehaviour
{
    private static Dictionary<string, DamageEvent> dictionnaireEvenement = new Dictionary<string, DamageEvent>();

    public static void ajouterEvenement(string nomEvenement, UnityAction<float, string, float> action)
    {
        //Debug.Log("ajouter apeler");
        DamageEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement == null)
        {
            evenement = new DamageEvent();
            dictionnaireEvenement.Add(nomEvenement, evenement);
            //Debug.Log("ajouter");
        }

        evenement.AddListener(action);
    }

    public static void retirerEvenement(string nomEvenement, UnityAction<float, string, float> action)
    {
        DamageEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.RemoveListener(action);
        }
    }

    public static void declancherEvenement(string nomEvenement, float degat, string nomJoueur, float knockback)
    {
        DamageEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.Invoke(degat, nomJoueur, knockback);
        }
        else
        {
            Debug.LogError(nomEvenement + " existe pas");
        }
    }
}
