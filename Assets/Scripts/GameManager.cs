using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera aRCamera;
    //[SerializeField] private TextMeshProUGUI scoreText;
    //[SerializeField] private ARMeshManager arMeshManager;
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private GameObject scanButton;
    [SerializeField] private TextMeshProUGUI scanButtonText;
    [SerializeField] private GameObject spawnCarButton;
    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;
    private bool isScanning = false;
    private float currentScore = 0;

    void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        } 
        else
        {
            Instance = this;
        }
    }

    public void Scanning()
    {
        if (!isScanning)
        {
            if (!arPlaneManager.enabled)
            {
                arPlaneManager.enabled = true;

            }
            scanButtonText.text = "STOP SCAN";
            isScanning = true;
        } 
        else
        {
            if (arPlaneManager.enabled)
            {
                arPlaneManager.enabled = false;
            }
            scanButton.SetActive(false);
            spawnCarButton.SetActive(true);
        }
            
    }

    public void SpawnCar()
    {
        if (!carHUD.activeSelf)
            carHUD.SetActive(true);
        SetCarPosition();
    }

    private void SetCarPosition()
    {
        car.SetActive(false);
        //if (car != null)
        //     Destroy(car.gameObject);

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            car.SetActive(true);
            Transform t = hit.collider.gameObject.transform;
            t.position = new Vector3(
                hit.collider.gameObject.transform.position.x,
                hit.collider.gameObject.transform.position.y + 3f,
                hit.collider.gameObject.transform.position.z
            );
            car.transform.position = t.position;
            ResetCarValues();
        }
    }

    private void ResetCarValues()
    {
        PrometeoCarController prom = car.GetComponent<PrometeoCarController>();
        prom.carSpeed = 0f;
        prom.isDrifting = false;
        prom.isTractionLocked = false;
    }

    public void UpdateScore(float points)
    {
        currentScore += points;
        //scoreText.text = string.Format("Score: {0}", currentScore);
    }
}
