using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class GestionnaireEvenement
{
    private static Dictionary<string, UnityEvent> dictionnaireEvenement = new Dictionary<string, UnityEvent>();

    public static void ajouterEvenement(string nomEvenement, UnityAction action)
    {
        //Debug.Log("ajouter apeler");
        UnityEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement == null)
        {
            evenement = new UnityEvent();
            dictionnaireEvenement.Add(nomEvenement, evenement);
            //Debug.Log("ajouter");
        }

        evenement.AddListener(action);
    }

    public static void retirerEvenement(string nomEvenement, UnityAction action)
    {
        UnityEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.RemoveListener(action);
        }
    }

    public static void declancherEvenement(string nomEvenement)
    {
        UnityEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.Invoke();
        }
        else
        {
            Debug.LogError(nomEvenement + " existe pas");
        }
    }
}
