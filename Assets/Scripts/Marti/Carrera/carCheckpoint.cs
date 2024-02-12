using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carCheckpoint : MonoBehaviour
{
    private int contadorCheck;
    private int voltesCotxe;

    private Vector3 transformCheckPoint;
    private GameObject CheckPoint;

    private void Start()
    {
        transformCheckPoint = transform.position;
        contadorCheck = 0;
        voltesCotxe = 0;
    }

    private void Update()
    {
        if (gameObject.transform.position.y < (-5))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            transform.position = transformCheckPoint;
            transform.rotation = CheckPoint.transform.rotation;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkpoints"))
        {
            CheckPoint = other.gameObject;
            NumCheckPoint pepito = other.gameObject.GetComponent<NumCheckPoint>();

            if (pepito.numeroCheckPoint == 1 && contadorCheck == 0)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 1");
            }
            else if (pepito.numeroCheckPoint == 2 && contadorCheck == 1)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 2");
            }
            else if (pepito.numeroCheckPoint == 3 && contadorCheck == 2)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 3");
            }
            else if (pepito.numeroCheckPoint == 4 && contadorCheck == 3)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 4");
            }
            else if (pepito.numeroCheckPoint == 5 && contadorCheck == 4)
            {
                contadorCheck++;
                transformCheckPoint = other.transform.position;
                Debug.Log("CkeckPoint 5");
                voltesCotxe++;
            }
        }
    }
}
