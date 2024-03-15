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
    [SerializeField] public TextMeshProUGUI debugText;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject modeXperimentMenu;
    [SerializeField] private GameObject modeAdventureMenu;
    [SerializeField] private GameObject modeCircuitMenu;

    //STATE MACHINE
    public int MODEMAINMENU = 0;
    public int MODEXPER = 1;
    public int MODECIR = 2;
    public int MODEADV = 3;
    public int mode = 0;
    private ModeXperiment modeXperManager;

    private void Awake()
    {
        modeXperManager = GetComponent<ModeXperiment>();
    }

    public void EnableOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void DisableOptionsMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void SelectModeXperiment()
    {
        //modeXperManager.SetActive(true);
        modeXperManager.EnableModeAdventure();
        DisableMainMenu();
    }

    public void SelectModeCircuit()
    {
        modeCircuitMenu.SetActive(true);
        DisableMainMenu();
    }

    public void SelectModeAdventure()
    {
        //modeAdventureMenu.SetActive(true);
        DisableMainMenu();
        GetComponent<ModeAdventure>().EnableModeAdventure();
    }

    public void BackToMain()
    {
        modeCircuitMenu.SetActive(false);
        modeAdventureMenu.SetActive(false);
        EnableMainMenu();
    }
}