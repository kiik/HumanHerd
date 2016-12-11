using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

	void Awake()
    {
        if (!instance) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
}
