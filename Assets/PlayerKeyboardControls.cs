using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerKeyboardControls : MonoBehaviour {

    Rigidbody2D rb;
    float flightForce = 3;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        KeyboardInput();
	}

    void KeyboardInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Up();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Down();
        }
        if (Input.GetKey(KeyCode.A))
        {
            Left();
        }
        if (Input.GetKey(KeyCode.D))
        {
            Right();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Descend();
        }
    }

    void Up()
    {
        rb.AddForce(transform.up * flightForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    void Down()
    {
        rb.AddForce(-transform.up * flightForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    void Left()
    {
        rb.AddForce(-transform.right * flightForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    void Right()
    {
        rb.AddForce(transform.right * flightForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    void Descend()
    {

    }
}
