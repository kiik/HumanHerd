using UnityEngine;

public class PickupController : MonoBehaviour {

	public enum State { Free, Busy };
    public State state;

    void Awake()
    {
        state = State.Free;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (state != State.Busy)
        {
            if (other.GetComponent<SimpleCitizen>() != null)
            {
                state = State.Busy;
            }
        }
    }

    public void Release()
    {
        // Release the person
    }
}
