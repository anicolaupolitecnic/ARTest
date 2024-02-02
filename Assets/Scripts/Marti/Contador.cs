using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Contador : MonoBehaviour
{
    public float TimerAdventure = 0;


    [SerializeField] private GameObject Temps;
    [SerializeField] private TextMeshProUGUI ContadorTexte;
    [SerializeField] private GameObject car;
    // Start is called before the first frame update

    void Update()
    {

        if (car.activeInHierarchy)
        {
            Temps.SetActive(true);
            TimerAdventure += Time.deltaTime;

            ContadorTexte.text = "" + TimerAdventure.ToString("f1");
        }
        else
        {
            Temps.SetActive(false);
            TimerAdventure = 0;

        }




    }
}
