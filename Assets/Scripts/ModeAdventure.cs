using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ModeCirManager : MonoBehaviour
{
    private GameManager gameManager;
    public Camera aRCamera;

    [SerializeField] private GameObject menuModeAdventure;
    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;
    //Mode Adventure
    private bool isPlacableEnvironment = false;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject environment;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.mode == gameManager.MODECIR)
        {
            if (isPlacableEnvironment)
            {
                SetEnvironmentPosition();
            }
        }
    }

    public void EnableModeAdventure()
    {
        menuModeAdventure.SetActive(true);
        planeManager.enabled = true;
        gameManager.mode = gameManager.MODEADV;
        isPlacableEnvironment = true;
    }

    public void BackButton()
    {
        menuModeAdventure.SetActive(false);
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
                    Instantiate(environment, hit.collider.gameObject.transform);
                    planeManager.enabled = isPlacableEnvironment = false;
                    Transform start = GameObject.Find("StartPoint").transform;
                    Instantiate(car, start.position, Quaternion.identity);
                }
            }
        }
    }
}
