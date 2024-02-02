using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    //GENERAL
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject modeAdventureMenu;
    [SerializeField] private GameObject modeCircuitMenu;

    //STATE MACHINE
    public int MODEMAINMENU = 0;
    public int MODEADV = 1;
    public int MODECIR = 2;
    public int mode = 0;


    public void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void SelectModeAdventure()
    {
        modeAdventureMenu.SetActive(true);
        DisableMainMenu();
    }

    public void SelectModeCircuit()
    {
        modeCircuitMenu.SetActive(true);
        DisableMainMenu();
    }
}
