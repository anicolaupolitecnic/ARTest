using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LapControl : MonoBehaviour
{
    private TextMeshProUGUI voltesUI;
    private TextMeshProUGUI cronometroUI;
    private ContadorCarrera timer;
    private GameObject canvasMapa;
    private GameObject winnerUI;

    [SerializeField] private List<float> bestLap = new List<float>();

    [SerializeField] private int Laps = 2;
    [SerializeField] private float RespawnY = -10;

    private int contadorCheck;
    private int voltesCotxe;
    private float cronometroCheck;
    private bool winner = false;
    private bool carrera;

    private Vector3 transformCheckPoint;
    private GameObject CheckPoint;

    private void Start()
    {
        timer = GameObject.Find("UI").GetComponent<ContadorCarrera>();
        canvasMapa = GameObject.Find("UI");
        voltesUI = canvasMapa.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        cronometroUI = canvasMapa.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();
        winnerUI = canvasMapa.transform.GetChild(0).GetChild(2).gameObject;


        voltesUI.enabled = false;
        cronometroUI.enabled = false;
        transformCheckPoint = transform.position;
        contadorCheck = 0;
        voltesCotxe = 1;
        cronometroCheck = 0;
        voltesUI.SetText("Laps " + voltesCotxe.ToString() + " / " + Laps.ToString());
    }

    private void Update()
    {

        if (!timer.TimerOn)
        {
            voltesUI.enabled = true;
            carrera = true;
            cronometroUI.enabled = true;

            cronometroCheck += Time.deltaTime;

            cronometroUI.text = cronometroCheck.ToString("f1");
        }

        if (gameObject.transform.position.y < (RespawnY))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            transform.position = transformCheckPoint;
            transform.rotation = CheckPoint.transform.rotation;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkpoints") && winner == false)
        {
            CheckPoint = other.gameObject;
            NumCheckPoint pepito = other.gameObject.GetComponent<NumCheckPoint>();

            if (pepito.numeroCheckPoint == 1 && contadorCheck == 0)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 1");
            }
            else if (pepito.numeroCheckPoint == 2 && contadorCheck == 1)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 2");
            }
            else if (pepito.numeroCheckPoint == 3 && contadorCheck == 2)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 3");
            }
            else if (pepito.numeroCheckPoint == 4 && contadorCheck == 3)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 4");
            }
            else if (pepito.numeroCheckPoint == 5 && contadorCheck == 4)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 5");
            }
        }
        if (other.CompareTag("meta"))
        {
            if (contadorCheck == 5 && winner == false)
            {
                contadorCheck = 0;
                if (voltesCotxe < Laps)
                {
                    voltesCotxe++;
                    voltesUI.SetText("Laps " + voltesCotxe.ToString() + " / " + Laps.ToString());
                    bestLap.Add(cronometroCheck);
                    contadorCheck = 0;
                }
                else
                {
                    winnerUI.SetActive(true);
                    voltesUI.enabled = false;
                    winner = true;
                }
            }
        }
    }
}
