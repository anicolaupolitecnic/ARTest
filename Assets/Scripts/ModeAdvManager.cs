using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ModeAdvManager : MonoBehaviour
{
    private GameManager gameManager;
    public Camera aRCamera;

    [SerializeField] private GameObject menuModeAdventure;
    //Mode Adventure
    private bool isPlacableCar = false;
    [SerializeField] private ARMeshManager arMeshManager;
    [SerializeField] private GameObject meshing;
    [SerializeField] private TextMeshProUGUI scanButtonText;
    [SerializeField] private GameObject spawnCarButton;
    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        isPlacableCar = false;

    }

    void Update()
    {
        if (gameManager.mode == gameManager.MODEADV)
        {
            Debug.Log("Entra modo adv");
            if (isPlacableCar)
            {
                Debug.Log("Entra isPlacable");
                SetCarPosition();
            }
                
        }
    }

    public void EnableModeAdventure()
    {
        menuModeAdventure.SetActive(true);
        meshing.SetActive(true);
        gameManager.mode = gameManager.MODEADV;
    }


    public void SpawnCar()
    {
        isPlacableCar = true;
        car.SetActive(false);

        Invoke("AddSecurityNet", 1f);
    }

    void AddSecurityNet()
    {
        GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubo.transform.localScale = new Vector3(100f, 100f, 1f);
        cubo.transform.position = new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z - 1.785f);

        cubo.transform.Rotate(Vector3.left, 90f);

        cubo.GetComponent<Renderer>().enabled = false;

        Rigidbody rb = cubo.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        BoxCollider boxCollider = cubo.AddComponent<BoxCollider>();
    }

    private void SetCarPosition()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Entra touck 1");
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Entra touck 2");
                // Get the position of the touch
                Vector2 touchPosition = Input.GetTouch(0).position;

                // Convert the touch position to a ray from the camera
                Ray ray = aRCamera.ScreenPointToRay(touchPosition);

                // Perform the raycast
                RaycastHit hit;

                //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 100f);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Entra touck 3");
                    car.SetActive(true);
                    Vector3 t = hit.point;
                    t = new Vector3(t.x, t.y + 0.5f, t.z);
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
