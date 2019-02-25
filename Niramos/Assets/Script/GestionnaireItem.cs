using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemEvent : UnityEvent<int, string>
{
}
public class GestionnaireItem : MonoBehaviour
{
    private static Dictionary<string, ItemEvent> dictionnaireEvenement = new Dictionary<string, ItemEvent>();

    public static void ajouterEvenement(string nomEvenement, UnityAction<int, string> action)
    {
        //Debug.Log("ajouter apeler");
        ItemEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement == null)
        {
            evenement = new ItemEvent();
            dictionnaireEvenement.Add(nomEvenement, evenement);
            //Debug.Log("ajouter");
        }

        evenement.AddListener(action);
    }

    public static void retirerEvenement(string nomEvenement, UnityAction<int, string> action)
    {
        ItemEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.RemoveListener(action);
        }
    }

    public static void declancherEvenement(string nomEvenement, int idObjet, string nomJoueur)
    {
        ItemEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.Invoke(idObjet, nomJoueur);
        }
        else
        {
            Debug.LogError(nomEvenement + " existe pas");
        }
    }
}
