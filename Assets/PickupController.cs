using UnityEngine;

public class PickupController : MonoBehaviour {

	public enum State { Free, Busy };
    public State state;
    [SerializeField]
    Transform parentTransform;

    SimpleCitizen carriage;
    [SerializeField]
    Rigidbody2D playerRb;

    void Awake()
    {
        state = State.Free;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (state != State.Busy)
        {
            SimpleCitizen ai = other.GetComponent<SimpleCitizen>();
            if (ai != null)
            {
                BoxCollider2D aiCol = ai.GetComponent<BoxCollider2D>();
                aiCol.enabled = false;
                state = State.Busy;
                Debug.Log("State is: " + state);
                ai.immobilize();
                Rigidbody2D rb = ai.GetComponent<Rigidbody2D>();
                rb.simulated = false;
                ai.transform.parent = parentTransform;
                carriage = ai;
            }
        }
    }

    public void Release()
    {
        carriage.transform.parent = null;
        BoxCollider2D aiCol = carriage.GetComponent<BoxCollider2D>();
        aiCol.enabled = true;
        Rigidbody2D rb = carriage.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.velocity = playerRb.velocity;

        carriage.mobilize();

        carriage = null;

        if (Physics2D.Raycast(parentTransform.position, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("LandingArea")))
        {
            GameManager.instance.ecoManager.AddCurrency(10);
            GameManager.instance.uiManager.SetMoneyText(GameManager.instance.ecoManager.GetCurrency());
        }
    }
}
