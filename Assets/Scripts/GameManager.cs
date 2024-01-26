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
    //[SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ARMeshManager arMeshManager;
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private GameObject scanButton;
    [SerializeField] private TextMeshProUGUI scanButtonText;
    [SerializeField] private GameObject spawnCarButton;
    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;
    private bool isScanning = true;
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
        if (Input.touchCount > 0)
        {
            Debug.Log("1");
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("2");

                // Get the position of the touch
                Vector2 touchPosition = Input.GetTouch(0).position;

                // Convert the touch position to a ray from the camera
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                // Perform the raycast
                RaycastHit hit;

                Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("A");
                    car.SetActive(true);
                    Transform t = hit.collider.gameObject.transform;
                    t.position = new Vector3(
                        hit.collider.gameObject.transform.position.x,
                        hit.collider.gameObject.transform.position.y + 0.5f,
                        hit.collider.gameObject.transform.position.z
                    );
                    car.transform.position = t.position;
                    ResetCarValues();
                }
            }
        }
    }

    public void SpawnCar()
    {
        if (!carHUD.activeSelf)
            carHUD.SetActive(true);
        //SetCarPosition();
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
                hit.collider.gameObject.transform.position.y + 0.5f,
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
