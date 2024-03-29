using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ModeCirManager : MonoBehaviour
{
    private GameManager gameManager;
    public Camera aRCamera;

    [SerializeField] private GameObject menuModeCircuit;
    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;
    [SerializeField] public GameObject[] cars;
    //Mode Circuit
    [SerializeField] private GameObject MenuSelectCar;
    [SerializeField] private GameObject MenuSelectCircuit;
    [SerializeField] private GameObject MenuGameCircuit;
    [SerializeField] private GameObject UIGameCircuit;
    private bool isPlacableCircuit = false;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject[] circuits;

    private GameObject selectedCircuit;
    private GameObject selectedCar;

    //[SerializeField] private ARTrackedImageManager tracking;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (gameManager.mode == gameManager.MODECIR)
        {
            if (isPlacableCircuit)
            {
                SetCircuitPosition();
            }
        }
    }

    public void EnableModeCircuit()
    {
        menuModeCircuit.SetActive(true);

        MenuSelectCar.SetActive(false);
        MenuSelectCircuit.SetActive(false);
        MenuGameCircuit.SetActive(true);
        UIGameCircuit.SetActive(true);

        planeManager.enabled = true;
        gameManager.mode = gameManager.MODECIR;
        isPlacableCircuit = true;
    }

    public void BackButton()
    {
        menuModeCircuit.SetActive(false);
        planeManager.enabled = false;
        gameManager.EnableMainMenu();

        //BOTH
        GameObject go = GameObject.Find("Trackables");
        if (go != null)
        {
            Destroy(go);
        }
        //CIRCUIT
        go = GameObject.Find("DEMO");
        if (go != null)
        {
            Destroy(go);
        }
        //ADVENT
        carHUD.SetActive(false);
        car.SetActive(false);
    }

    public void GoToCarSelection()
    {
        menuModeCircuit.SetActive(true);
        MenuSelectCar.SetActive(true);
        MenuSelectCircuit.SetActive(false);

    }

    public void GoToCircuitSelection()
    {
        MenuSelectCar.SetActive(false);
        MenuSelectCircuit.SetActive(true);
    }

    public void SelectCar1()
    {
        selectedCar = cars[0];
        //Instantiate(selectedCar);
    }

    public void SelectCar2()
    {
        selectedCar = cars[1];
        //Instantiate(selectedCar);
    }

    public void SelectCar3()
    {
        selectedCar = cars[2];
        //Instantiate(selectedCar);
    }

    private void SpawnTest()
    {
        Instantiate(selectedCircuit);
        Transform start = GameObject.Find("StartPoint").transform;
        Instantiate(selectedCar, start.position, Quaternion.identity);
    }


    public void SelectCircuit1()
    {
        selectedCircuit = circuits[0];
        selectedCar.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
        SpawnTest();
    }

    public void SelectCircuit2()
    {
        selectedCircuit = circuits[1];
        selectedCar.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        SpawnTest();
    }
    public void SelectCircuit3()
    {
        selectedCircuit = circuits[2];
        selectedCar.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        SpawnTest();
    }

    private void SetCircuitPosition()
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
                    Instantiate(selectedCircuit, hit.collider.gameObject.transform);
                    planeManager.enabled = isPlacableCircuit = false;
                    Transform start = GameObject.Find("StartPoint").transform;
                    Instantiate(selectedCar, start.position, Quaternion.identity);
                }
            }
        }
    }
}
