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

    //GENERAL
    [SerializeField] private GameObject mainMenu;    
    [SerializeField] private GameObject modeAdventure;
    [SerializeField] private GameObject modeCircuit;

    //Mode Adventure
    private bool isPlacableCar;
    [SerializeField] private ARMeshManager arMeshManager;
    [SerializeField] private GameObject meshing;
    [SerializeField] private TextMeshProUGUI scanButtonText;
    [SerializeField] private GameObject spawnCarButton;
    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;

    //Mode Circuit
    [SerializeField] private ARTrackedImageManager tracking;

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
        EnableModeCircuit();
    }

    void Update()
    {
        if (isPlacableCar)
        {
            SetCarPosition();
        }
    }

    public void EnableModeAdventure()
    {
        mainMenu.SetActive(false);
        modeCircuit.SetActive(false);
        modeAdventure.SetActive(true);
        meshing.SetActive(true);
    }

    public void EnableModeCircuit()
    {
        mainMenu.SetActive(false);
        modeAdventure.SetActive(false);
        modeCircuit.SetActive(true);
        tracking.enabled = true;
    }

    public void BackButton()
    {
        modeAdventure.SetActive(false);
        meshing.SetActive(false);
        modeCircuit.SetActive(false);
        tracking.enabled = false;
        mainMenu.SetActive(true);
    }

    public void SpawnCar()
    {
        isPlacableCar = true;
        car.SetActive(false);
    }

    private void SetCarPosition()
    {
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
}
