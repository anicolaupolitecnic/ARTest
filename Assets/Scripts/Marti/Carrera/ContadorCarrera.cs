using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorCarrera : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI iniciPartit;
    [SerializeField] private int contadorMaximo = 4;

    private bool TimerOn = true;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        Timer = contadorMaximo;

    }

    // Update is called once per frame
    void Update()
    {
        if (!TimerOn)
            return;

        if (Timer >= 0)
        {
            Timer -= Time.deltaTime;
            ShowTimerLeft();

        }
        else
        {
            TimerOn = false;
            iniciPartit.enabled = false;
            //Unblock Inputs
            iniciPartit.gameObject.SetActive(false);
        }


    }

    private void ShowTimerLeft()
    {
        iniciPartit.text = Timer.ToString("F0");
    }
}
