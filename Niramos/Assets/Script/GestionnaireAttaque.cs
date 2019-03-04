using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DamageEvent : UnityEvent<float, string>
{

}
public class GestionnaireAttaque : MonoBehaviour
{
    private static Dictionary<string, DamageEvent> dictionnaireEvenement = new Dictionary<string, DamageEvent>();

    public static void ajouterEvenement(string nomEvenement, UnityAction<float, string> action)
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

    public static void retirerEvenement(string nomEvenement, UnityAction<float, string> action)
    {
        DamageEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.RemoveListener(action);
        }
    }

    public static void declancherEvenement(string nomEvenement, float degat, string nomJoueur)
    {
        DamageEvent evenement = null;
        dictionnaireEvenement.TryGetValue(nomEvenement, out evenement);
        if (evenement != null)
        {
            evenement.Invoke(degat, nomJoueur);
        }
        else
        {
            Debug.LogError(nomEvenement + " existe pas");
        }
    }
}
