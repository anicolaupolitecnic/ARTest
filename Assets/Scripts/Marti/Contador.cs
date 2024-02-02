using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Contador : MonoBehaviour
{
    public float TimerAdventure = 0;
    [SerializeField] private TextMeshProUGUI ContadorTexte;
    // Start is called before the first frame update

    void Update()
    {

        TimerAdventure += Time.deltaTime;

        ContadorTexte.text = "" + TimerAdventure.ToString("f1");
    }
}
