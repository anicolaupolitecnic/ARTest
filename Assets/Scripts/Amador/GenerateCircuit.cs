using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateCircuit : MonoBehaviour
{
    [SerializeField] private Transform plane;
    [SerializeField] private GameObject circuit1;
    [SerializeField] private GameObject circuit2;
    [SerializeField] private GameObject circuit3;
    private GameObject chosenCircuit;
    [SerializeField] private GameObject circuitsPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCircuit1()
    {
        Instantiate(circuit1, plane);
    }

    public void GenerateCircuit2()
    {
        Instantiate(circuit2, plane);
    }
    public void GenerateCircuit3()
    {
        Instantiate(circuit3, plane);
    }

    public void HideCircuitSelection()
    {
        circuitsPanel.SetActive(false);
    }

    private void ChooseCircuit1()
    {
        chosenCircuit = circuit1;
    }

    private void ChooseCircuit2()
    {
        chosenCircuit = circuit2;
    }

    private void ChooseCircuit3()
    {
        chosenCircuit = circuit3;
    }


    private void GenerateChosenCircuit(GameObject circuit, Transform location)
    {
        Instantiate(circuit, location);
    }
}
