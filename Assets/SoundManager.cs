using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    // Temp AS
    public GameObject tempAS;

    // Wall construction clips
    public AudioSource cameraConstructionAS;
    public AudioClip wallConstructionFinish;
    public AudioClip wallLocate;
    public AudioClip wallBuildFail;

    // Wall destruction
    public AudioClip wallHit;
    public AudioClip wallDestruct;

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

    // Wall
    public void WallLocate() { cameraConstructionAS.PlayOneShot(wallLocate); }
    public void WallBuildFail() { cameraConstructionAS.PlayOneShot(wallBuildFail); }
    public void WallConstructionFinish() { cameraConstructionAS.PlayOneShot(wallConstructionFinish); }
    public void WallHit(Vector2 pos) {
        GameObject go = Instantiate(tempAS, pos, Quaternion.identity);
        AudioSource source = go.GetComponent<AudioSource>();
        source.spatialBlend = 0;
        source.PlayOneShot(wallHit);
        Destroy(go, 2f);
    }
    public void WallDestruct(Vector2 pos) {
        GameObject go =  Instantiate(tempAS, pos, Quaternion.identity);
        AudioSource source = go.GetComponent<AudioSource>();
        source.spatialBlend = 0;
        source.PlayOneShot(wallDestruct);
        Destroy(source.gameObject);
    }

    // Economy
    public void MoneyMinus() { cameraEconomyAS.PlayOneShot(moneyMinus); }
    public void MoneyPlus() { cameraEconomyAS.PlayOneShot(moneyPlus); }


    #region SoundCheck
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
    #endregion
}
