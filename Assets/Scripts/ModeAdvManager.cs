using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ModeAdvManager : MonoBehaviour
{
    private float timer;
    private float playTime;
    private float targetTime = 3;
    private bool scanTimeDone = false;
    private GameManager gameManager;
    public Camera aRCamera;

    [SerializeField] private GameObject menuModeAdventure;
    //Mode Adventure
    private bool isPlacableCar = false;
    [SerializeField] private ARMeshManager arMeshManager;
    [SerializeField] private GameObject meshing;
    [SerializeField] private TextMeshProUGUI scanButtonText;
   
    [SerializeField] private GameObject respawnCarButton;
    [SerializeField] private TextMeshProUGUI respawnCarText;
    private int respawnCounter;

    [SerializeField] private GameObject carHUD;
    [SerializeField] private GameObject car;

    [SerializeField] private TextMeshProUGUI messageText;
    
    [SerializeField] private TextMeshProUGUI timerTxt;
    int minutes;
    int seconds;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        //initModeAdventure();

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= targetTime && !scanTimeDone)
        {
            scanTimeDone = true;
            if (gameManager.mode == gameManager.MODEADV)
            {
                messageText.text = "Toca al terra on vols que aparegui el cotxe!";
                isPlacableCar = true;
            }
        } 
        else
        {
            playTime = Time.time - timer;
            minutes = Mathf.FloorToInt(playTime / 60f);
            seconds = Mathf.FloorToInt(playTime % 60f);
            timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (isPlacableCar)
            {
                SetCarPosition();
            }
        }
    }

    public void initModeAdventure()
    {
        isPlacableCar = false;
        messageText.text = "Escaneja el terra!";
    }

    private void UpdateSecurityNet()
    {
        GameObject cube = GameObject.Find("Cube");

        if (cube != null)
        {
            cube.transform.position = new Vector3(car.transform.position.x, car.transform.position.y - 0.04f, car.transform.position.z);
        }
    }

    public void EnableModeAdventure()
    {
        initModeAdventure();
        menuModeAdventure.SetActive(true);
        meshing.SetActive(true);
        gameManager.mode = gameManager.MODEADV;
    }


    public void SpawnCar()
    {
        isPlacableCar = true;
        car.SetActive(false);
        //spawnCarButton.SetActive(false);
    }

    //void AddSecurityNet()
    //{
    //    GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //    cubo.transform.localScale = new Vector3(1f, 1f, 0.05f);
    //    cubo.transform.position = new Vector3(car.transform.position.x, car.transform.position.y - 0.04f, car.transform.position.z);

    //    cubo.transform.Rotate(Vector3.left, 90f);

    //    cubo.GetComponent<Renderer>().enabled = false;

    //    Rigidbody rb = cubo.AddComponent<Rigidbody>();
    //    rb.isKinematic = true;
    //    rb.useGravity = false;
    //    rb.constraints = RigidbodyConstraints.FreezeRotation;

    //    BoxCollider boxCollider = cubo.AddComponent<BoxCollider>();
    //}

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
                    t = new Vector3(t.x, t.y + 0.5f, t.z);
                    car.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    car.transform.position = t;
                    ResetCarValues();
                    isPlacableCar = false;
                    if (!carHUD.activeSelf)
                        carHUD.SetActive(true);

                    //Invoke("AddSecurityNet", 1f);
                    messageText.text = "";
                    respawnCarButton.SetActive(true);
                    respawnCarText.enabled = true;
                    timerTxt.enabled = true;
                }
            }
        }
    }

    public void RespawnCar()
    {
        isPlacableCar = true;

        Vector3 t = car.transform.position;
        t = new Vector3(t.x, t.y + 0.5f, t.z);
        car.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        car.transform.position = t;
        ResetCarValues();
        isPlacableCar = false;
        if (!carHUD.activeSelf)
            carHUD.SetActive(true);

        //Invoke("AddSecurityNet", 1f);
        messageText.text = "";
        if (!respawnCarButton.activeSelf)
            respawnCarButton.SetActive(true);

        respawnCounter++;
        respawnCarText.text = respawnCounter + " Respawns";
    }

    private void ResetCarValues()
    {
        PrometeoCarController prom = car.GetComponent<PrometeoCarController>();
        prom.carSpeed = 0f;
        prom.isDrifting = false;
        prom.isTractionLocked = false;
    }

    public void BackToMainMenu()
    {
        meshing.SetActive(false);
        gameManager.BackToMain();
    }
}
