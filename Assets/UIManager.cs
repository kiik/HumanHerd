﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public enum MenuState { MainMenu, GameMenu, Tutorial, MenuOff };
    public MenuState menuState;

    public SoundManager soundManager;

    // Panels
    public GameObject menuPanel;
    public GameObject mainMenuPanel;
    public GameObject newGamePanel;
    public GameObject optionsPanel;
    public GameObject tutorialPanel;
    public GameObject topPanel;
    public GameObject sideBar;
    public GameObject gameOverPanel;

    // Text
    public Text moneyText;
    public Text sheepText;
    public Text populationCountText;
    public Text populationEscapedText;
    public Text mouseInfoText;

    // Toggle
    public Toggle musicToggle;

    void Start()
    {
        InitializeMenu();
    }

    void Update()
    {
        UpdateMouseText();
        KeyboardInput();
    }

    void UpdateMouseText()
    {
        mouseInfoText.transform.position = Input.mousePosition;
    }

    public void SetMouseText(string s, Color color)
    {
        mouseInfoText.text = s;
        mouseInfoText.color = color;
    }

    public void HideMouseText() { mouseInfoText.text = ""; }

    void InitializeMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            menuState = MenuState.MainMenu;
            topPanel.SetActive(false);
            sideBar.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Game(Vader)")
        {
            menuState = MenuState.MenuOff;
            topPanel.SetActive(true);
            sideBar.SetActive(true);
            mainMenuPanel.SetActive(false);
            moneyText.text = GameManager.instance.ecoManager.GetCurrency().ToString();
            sheepText.text = GameManager.instance.ecoManager.GetSheepCount().ToString();
            populationEscapedText.text = "0/3";
        }
    }

    public void SetMoneyText(int n)
    {
        moneyText.text = n.ToString();
    }

    public void ToggleMenu()
    {
        // If we are in main menu
        if (menuState == MenuState.MainMenu)
        {
            Debug.Log("In Main Menu");
            Application.Quit();
            return;
        }

        else if (menuState == MenuState.MenuOff)
        {
            menuState = MenuState.GameMenu;
            menuPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (menuState == MenuState.GameMenu)
        {
            menuState = MenuState.MenuOff;
            menuPanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else if (menuState == MenuState.Tutorial)
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                menuState = MenuState.MainMenu;
                mainMenuPanel.SetActive(true);
            }
            else if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Game(Vader)")
            {
                menuState = MenuState.GameMenu;
                menuPanel.SetActive(true);
            }
            tutorialPanel.SetActive(false);
        }
    }

    public void NewGame()
    {
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(true);
    }
    public void Continue()
    {
        ToggleMenu();
    }
    public void ToggleTutorial()
    {
        menuState = MenuState.Tutorial;
        tutorialPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        menuPanel.SetActive(false);
    }
    public void CloseTutorial()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            mainMenuPanel.SetActive(true);
            menuState = MenuState.MainMenu;
        }
        else if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Game(Vader)")
        {
            menuState = MenuState.GameMenu;
            menuPanel.SetActive(true);
        }
        tutorialPanel.SetActive(false);
    }
    public void Options()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void ToggleMusic()
    {
        if (musicToggle.isOn)
        {
            if (PlayerPrefs.HasKey("Music"))
            {
                PlayerPrefs.SetInt("Music", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Music", 1);
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("Music"))
            {
                PlayerPrefs.SetInt("Music", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Music", 0);
            }
        }
        soundManager.CheckSoundOptions();
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
	public void ExitGame()
    {
        Application.Quit();
    }
    public void NormalTheme()
    {
        SceneManager.LoadScene("Game");
    }
    public void ImperialTheme()
    {
        SceneManager.LoadScene("Game(Vader)");
    }
    public void Back()
    {
        newGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void BuySheep()
    {

    }
    public void UpgradeWall()
    {
        GameManager.instance.upgradeManager.UpgradeWall();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        menuPanel.SetActive(false);
        tutorialPanel.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void IncreaseEscapedCount()
    {
        populationEscapedText.text = GameManager.instance.ecoManager.escaped+"/3";
    }
}
