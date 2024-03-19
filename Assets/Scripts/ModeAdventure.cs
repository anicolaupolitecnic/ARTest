using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ModeAdventure : MonoBehaviour
{
    private GameManager gameManager;
    public Camera aRCamera;

    [SerializeField] private GameObject menuModeAdventure;
    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;
    [SerializeField] private TextMeshProUGUI infoText;

    //Mode Adventure
    private bool isPlacableEnvironment = false;
    //[SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARTrackedImageManager arTrackerManager;
    [SerializeField] private GameObject environment;

    //private GameObject myCar;
    private GameObject myEnvironment;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        infoText.text = "Escaneja una superficie";
    }

    void Update()
    {
        if (gameManager.mode == gameManager.MODEADV)
        {
            if (isPlacableEnvironment)
            {
                //SetEnvironmentPosition();
            }
        }
    }

    public void EnableModeAdventure()
    {
        carHUD.SetActive(false);
        menuModeAdventure.SetActive(true);
        arTrackerManager.enabled = true;
        gameManager.mode = gameManager.MODEADV;
        isPlacableEnvironment = true;
    }

    public void BackButton()
    {
        carHUD.SetActive(false);
        menuModeAdventure.SetActive(false);
        arTrackerManager.enabled = false;
        gameManager.EnableMainMenu();
        
        if (car != null)
            car.SetActive(false);
        if (myEnvironment != null)
            Destroy(myEnvironment);

        //BOTH
        GameObject go = GameObject.Find("Trackables");
        if (go != null)
        {
            Destroy(go);
        }
        //ADVENT
        carHUD.SetActive(false);
        car.SetActive(false);
    }

    private void SetEnvironmentPosition()
    {
        Vector2 tPosition = new Vector2();

        if (Input.GetMouseButtonDown(0))
        {
            tPosition = (Input.mousePosition);
        }

        // Check for touches (for mobile devices)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                tPosition  =(touch.position);
            }
        }

        // Convert the touch position to a ray from the camera
        Ray ray = aRCamera.ScreenPointToRay(tPosition);

        // Perform the raycast
        RaycastHit hit;

        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 100f);
        if (Physics.Raycast(ray, out hit))
        {
            myEnvironment.SetActive(true);
            myEnvironment.transform.rotation = hit.collider.gameObject.transform.rotation;
            myEnvironment.transform.position = hit.collider.gameObject.transform.position;

            arTrackerManager.enabled = isPlacableEnvironment = false;
            //myCar =  Instantiate(car, start.position, Quaternion.identity);

            Transform start = GameObject.Find("StartPoint").transform;
            car.transform.position = start.position;
            car.SetActive(true);
                    
            infoText.text = "";
            carHUD.SetActive(true);
        }
    }
}
