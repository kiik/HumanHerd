using UnityEngine;

public class SoundManager : MonoBehaviour {

    // Wall construction clips
    public AudioSource cameraConstructionAS;
    public AudioClip wallConstructionFinish;

    // Economy
    public AudioSource cameraEconomyAS;
    public AudioClip moneyMinus;
    public AudioClip moneyPlus;
    

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
}
