using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleInvader : AIBase {

	  public float speed = 1.0f;

		GameObject targetGoal;

	  void Start() {
			targetGoal = null;

	    navigateTo(transform.position);
	  }

	  void Update() {
			runDescisionBehaviour();
	    runMotionBehaviour();
	  }

	  protected override void onMotionUpdate(Vector3 dir) {
	    dir *= speed;
	    gameObject.GetComponent<Rigidbody2D>().AddForce(dir * speed);
	  }

	  void OnTriggerEnter2D(Collider2D o) {

	  }

		void OnCollisionEnter2D(Collision2D c) {
			Vector3 dir = transform.position - c.transform.position;
			dir.Normalize();

			gameObject.GetComponent<Rigidbody2D>().AddForce(dir * speed * 2.0f);
		}

		void runDescisionBehaviour() {
			if(targetGoal == null) {
				GameObject[] objs = GameObject.FindGameObjectsWithTag("InvasionTarget");

				if(objs.Length > 0) {
					targetGoal = objs[Random.Range(0, objs.Length)];
					navigateTo(targetGoal.transform.position);
				}
			}
		}

}
