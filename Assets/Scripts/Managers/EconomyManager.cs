using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour {

    int currency = 700;
    public List<GameObject> sheepList = new List<GameObject>();
    public int escaped = 0;

    public void AddCurrency(int n)
    {
        currency += n;
    }

    public void DecreaseCurrency(int n)
    {
        currency -= n;
    }

    public int GetCurrency() { return currency; }
    public int GetSheepCount() { return sheepList.Count; }

    public void Escaped()
    {

        escaped++;
        GameManager.instance.uiManager.IncreaseEscapedCount();
        if (escaped > 3)
        {
            GameManager.instance.uiManager.GameOver();
        }
    }
}
