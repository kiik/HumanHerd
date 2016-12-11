using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerKeyboardControls : MonoBehaviour {

    [SerializeField]
    Animator anim;
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
        anim.SetFloat("speedY", 1);
    }
    void Down()
    {
        rb.AddForce(-transform.up * flightForce * Time.deltaTime, ForceMode2D.Impulse);
        anim.SetFloat("speedY", -1);
    }
    void Left()
    {
        anim.SetFloat("speedX", -1);
        rb.AddForce(-transform.right * flightForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    void Right()
    {
        anim.SetFloat("speedX", 1);
        rb.AddForce(transform.right * flightForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    void Descend()
    {

    }
}
