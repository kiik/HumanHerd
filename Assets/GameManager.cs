using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public UIManager uiManager;
    public EconomyManager ecoManager;

	void Awake()
    {
        if (!instance) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
}
