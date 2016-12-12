using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    // Wall construction clips
    public AudioSource cameraConstructionAS;
    public AudioClip wallConstructionFinish;
    public AudioClip wallLocate;

    // Economy
    public AudioSource cameraEconomyAS;
    public AudioClip moneyMinus;
    public AudioClip moneyPlus;

    // Music
    public AudioSource cameraMusicAS;
    
    void Awake()
    {
        CheckSoundOptions();
    }

    public void WallLocate()
    {
        cameraConstructionAS.PlayOneShot(wallLocate);
    }

    public void WallConstructionFinish()
    {
        cameraConstructionAS.PlayOneShot(wallConstructionFinish);
    }

    public void MoneyMinus()
    {
        cameraEconomyAS.PlayOneShot(moneyMinus);
    }
    public void MoneyPlus()
    {
        cameraEconomyAS.PlayOneShot(moneyPlus);
    }

    public void CheckSoundOptions()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            int choice = PlayerPrefs.GetInt("Music");
            if (choice == 1) { ActivateMusic(); }
            else if (choice == 0) { DeactivateMusic(); }
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
        }
    }

    void ActivateMusic() { cameraMusicAS.loop = true; cameraMusicAS.Play(); cameraMusicAS.volume = SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Game(Vader)" ? 0.4f : 1; }
    void DeactivateMusic() { cameraMusicAS.Stop(); cameraMusicAS.volume = 0; }
}
