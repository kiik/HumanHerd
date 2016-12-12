using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public UIManager uiManager;
    public EconomyManager ecoManager;
    public SoundManager soundManager;
    public UpgradeManager upgradeManager;

	void Awake()
    {
        if (!instance) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
}
