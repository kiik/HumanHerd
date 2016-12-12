using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    public void UpgradeWall()
    {
        if (GameManager.instance.ecoManager.GetCurrency() >= 500)
        {
            PoolManager.instance.UpgradeToStone();
            GameManager.instance.ecoManager.DecreaseCurrency(500);
            GameManager.instance.uiManager.SetMoneyText(GameManager.instance.ecoManager.GetCurrency());
        }
        else
        {
            GameManager.instance.soundManager.WallBuildFail();
        }
    }
}
