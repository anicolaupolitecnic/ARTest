using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera aRCamera;
    private bool isPlacableCar;
    //[SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ARMeshManager arMeshManager;
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private GameObject scanButton;
    [SerializeField] private TextMeshProUGUI textDebug;
    [SerializeField] private TextMeshProUGUI scanButtonText;
    [SerializeField] private GameObject spawnCarButton;
    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;
    private bool isScanning = true;
    private float currentScore = 0;

    void Awake()
    {
        isPlacableCar = false;
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
        /*
        if (!isScanning)
        {
            if (!arPlaneManager.enabled)
            {
                arPlaneManager.enabled = true;

            }

            if (!arMeshManager.enabled)
            {
                arMeshManager.enabled = true;

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

            if (arMeshManager.enabled)
            {
                arMeshManager.enabled = false;
            }
            
            //scanButton.SetActive(false);
            spawnCarButton.SetActive(true);
        }
           */
    }

    void Update()
    {
        if (isPlacableCar)
        {
            SetCarPosition();
        }
        
    }

    public void SpawnCar()
    {
        isPlacableCar = true;
        car.SetActive(false);
    }

    private void SetCarPosition()
    {
        
        //if (car != null)
        //     Destroy(car.gameObject);

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Get the position of the touch
                Vector2 touchPosition = Input.GetTouch(0).position;

                // Convert the touch position to a ray from the camera
                Ray ray = aRCamera.ScreenPointToRay(touchPosition);

                // Perform the raycast
                RaycastHit hit;

                //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 100f);
                if (Physics.Raycast(ray, out hit))
                {
                    car.SetActive(true);
                    Vector3 t = hit.point;
                    t = new Vector3(t.x, t.y+0.5f, t.z);
                    car.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    car.transform.position = t;
                    ResetCarValues();
                    isPlacableCar = false;
                    if (!carHUD.activeSelf)
                        carHUD.SetActive(true);
                }
            }
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
