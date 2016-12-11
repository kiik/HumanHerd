using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public enum MenuState { MainMenu, GameMenu, Tutorial, MenuOff };

    public MenuState menuState;
    public GameObject menuPanel;
    public GameObject tutorialPanel;

    void Awake()
    {
        InitializeMenu();
    }

    void Update()
    {
        KeyboardInput();
    }

    void InitializeMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            menuState = MenuState.MainMenu;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            menuState = MenuState.MenuOff;
        }
    }

    public void ToggleMenu()
    {
        // If we are in main menu
        if (menuState == MenuState.MainMenu)
        {
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
            menuState = MenuState.GameMenu;
            tutorialPanel.SetActive(false);
        }
    }
    public void Continue()
    {
        ToggleMenu();
    }
    public void ToggleTutorial()
    {
        menuState = MenuState.Tutorial;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
	public void ExitGame()
    {
        Application.Quit();
    }

    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
}
