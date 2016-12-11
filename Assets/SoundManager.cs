using UnityEngine;

public class SoundManager : MonoBehaviour {

    // Camera Construction clips
    public AudioClip wallConstructionFinish;
    public AudioSource cameraConstructionAS;

    public void WallConstructionFinish()
    {
        cameraConstructionAS.PlayOneShot(wallConstructionFinish);
    }
}
