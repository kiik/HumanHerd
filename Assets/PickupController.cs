using UnityEngine;

public class PickupController : MonoBehaviour {

	

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
    }
}
