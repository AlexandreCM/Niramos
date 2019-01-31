using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class GestionnaireEvenement
{
    private static Dictionary<string, UnityEvent> dictionnaireEvenement;

    public static void ajouterEvenement(string nomEvenement, UnityAction action)
    {
        UnityEvent evenement;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement == null)
        {
            dictionnaireEvenement.Add(nomEvenement, evenement);
        }

        evenement.AddListener(action);
    }

    public static void retirerEvenement(string nomEvenement, UnityAction action)
    {
        UnityEvent evenement;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.RemoveListener(action);
        }
    }

    public static void declancherEvenement(string nomEvenement)
    {
        UnityEvent evenement;
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
