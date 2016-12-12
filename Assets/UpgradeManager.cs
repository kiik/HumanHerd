using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    public void UpgradeWall()
    {
        if (GameManager.instance.ecoManager.GetCurrency() >= 500)
        {
            PoolManager.instance.UpgradeToStone();
        }
        else
        {
            GameManager.instance.soundManager.WallBuildFail();
        }
    }
}
