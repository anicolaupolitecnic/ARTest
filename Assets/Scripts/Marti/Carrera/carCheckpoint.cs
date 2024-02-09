using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carCheckpoint : MonoBehaviour
{
    private Vector3 transformCheckPoint;
    private GameObject CheckPoint;
    private string Numero;

    private void Start()
    {
        transformCheckPoint = transform.position;
        
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
        CheckPoint = other.gameObject;
        transformCheckPoint = other.transform.position;
        Debug.Log(transformCheckPoint + " " + other.gameObject.GetComponent<NumCheckPoint>().NumeroChekPoint);
    }
}
