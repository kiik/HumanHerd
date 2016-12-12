using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerKeyboardControls : MonoBehaviour {

    [SerializeField]
    Animator anim;
    Rigidbody2D rb;
    float flightForce = 3;

    [SerializeField]
    GameObject shadow;
    Vector2 shadowOffsetX_leftVector = new Vector2(0.08f, -0.6f);
    Vector2 shadowOffsetX_rightVector = new Vector2(-0.08f, -0.6f);

    [SerializeField]
    Transform artTransform;
    Vector3 maxHeight = Vector3.zero;
    Vector3 minHeight = new Vector3(0,-0.37f,0);
    Vector3 maxShadowSize = new Vector3(0.8f,0.2f,0);
    Vector3 minShadowSize = new Vector3(0.6f, 0.15f, 0);
    int weakShadowAlpha = 190;
    int strongShadowAlpha = 255;
    [SerializeField]
    SpriteRenderer shadowSprite;
    Color32 weakShadow = new Color32(255,255,255,190);
    Color32 strongShadow = new Color32(255, 255, 255, 255);

    public float fraction = 0;
    float speed = .5f;

    [SerializeField]
    BoxCollider2D pickupControllerCollider;
    [SerializeField]
    PickupController pickupController;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        shadow.transform.localPosition = shadowOffsetX_rightVector;
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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (pickupController.state == PickupController.State.Busy)
            {
                pickupController.Release();
                pickupController.state = PickupController.State.Free;
            }
        }
        if (Input.GetKey(KeyCode.Space) && pickupController.state == PickupController.State.Free)
        {
            Descend();
        }
        else
        {
            Ascend();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Focus();
        }
    }

    void Up()
    {
        rb.AddForce(transform.up * flightForce * Time.deltaTime, ForceMode2D.Impulse);
        anim.SetFloat("speedY", 1);
        anim.SetFloat("speedX", 0);
    }
    void Down()
    {
        rb.AddForce(-transform.up * flightForce * Time.deltaTime, ForceMode2D.Impulse);
        anim.SetFloat("speedY", -1);
        anim.SetFloat("speedX", 0);
    }
    void Left()
    {
        anim.SetFloat("speedX", -1);
        anim.SetFloat("speedY", 0);
        rb.AddForce(-transform.right * flightForce * Time.deltaTime, ForceMode2D.Impulse);
        shadow.transform.localPosition = shadowOffsetX_leftVector;
    }
    void Right()
    {
        anim.SetFloat("speedX", 1);
        anim.SetFloat("speedY", 0);
        rb.AddForce(transform.right * flightForce * Time.deltaTime, ForceMode2D.Impulse);
        shadow.transform.localPosition = shadowOffsetX_rightVector;
    }
    void Descend()
    {
        if (fraction > 0f)
        {
            fraction -= Time.deltaTime / 0.2f;
        }
        else
        {
            pickupControllerCollider.enabled = true;
        }

        fraction = Mathf.Clamp(fraction, 0f, 1f);
        artTransform.localPosition = Vector3.Lerp(minHeight, maxHeight, fraction);
        shadow.transform.localScale = Vector3.Lerp(minShadowSize, maxShadowSize, fraction);
        shadowSprite.color = Color32.Lerp(strongShadow, weakShadow, fraction);
    }
    void Ascend()
    {
        if (fraction < 1f)
        {
            fraction += Time.deltaTime / 0.2f;
        }
        else
        {
            pickupControllerCollider.enabled = false;
        }

        fraction = Mathf.Clamp(fraction, 0f, 1f);
        artTransform.localPosition = Vector3.Lerp(minHeight, maxHeight, fraction);
        shadow.transform.localScale = Vector3.Lerp(minShadowSize, maxShadowSize, fraction);
        shadowSprite.color = Color32.Lerp(strongShadow, weakShadow, fraction);
    }

    void Focus()
    {
        Vector3 pos = transform.position;
        pos.z = -10;
        Camera.main.transform.position = pos;
    }
}
