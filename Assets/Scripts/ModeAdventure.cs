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
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject environment;

    private GameObject myCar;
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
                SetEnvironmentPosition();
            }
        }
    }

    public void EnableModeAdventure()
    {
        carHUD.SetActive(false);
        menuModeAdventure.SetActive(true);
        planeManager.enabled = true;
        gameManager.mode = gameManager.MODEADV;
        isPlacableEnvironment = true;
    }

    public void BackButton()
    {
        carHUD.SetActive(false);
        menuModeAdventure.SetActive(false);
        planeManager.enabled = false;
        gameManager.EnableMainMenu();
        
        if (myCar != null)
            Destroy(myCar);
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
                    myEnvironment = Instantiate(environment, hit.collider.gameObject.transform);

                    Collider collider = GetComponent<Collider>();

                    if (collider != null)
                    {
                        Vector3 size = collider.bounds.size;
                        myEnvironment.transform.position -= size * 0.5f;
                    }

                    myEnvironment.transform.rotation = Quaternion.Euler(Vector3.zero);
                    planeManager.enabled = isPlacableEnvironment = false;
                    Transform start = GameObject.Find("StartPoint").transform;
                    myCar =  Instantiate(car, start.position, Quaternion.identity);
                    myCar.SetActive(true);
                    infoText.text = "";
                    carHUD.SetActive(true);
                }
            }
        }
    }
}
