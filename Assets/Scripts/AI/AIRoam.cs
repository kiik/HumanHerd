using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class AIRoam : AIBase {

  public float speed = 1.0f;

  void Start() {
    navigateTo(new Vector3(3.0f, 0, 0));
  }

  void Update() {
    runMotionBehaviour();

    //controller.SimpleMove(new Vector3(0.0f, 0.1f, 0) * Time.deltaTime);
  }

  protected override void onMotionUpdate(Vector3 dir) {
    dir *= speed;
    //gameObject.GetComponent<Rigidbody2D>().AddForce(dir * speed);

    //Debug.Log(dir);
    gameObject.GetComponent<Rigidbody2D>().velocity = dir * speed;


    //Debug.Log(dir * speed);
  }

  void OnTriggerEnter2D(Collider2D o) {
    //Debug.Log((transform.position - o.gameObject.transform.position));
  }

}
