using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AutonomousSheep : AIRoam {

  public float speed = 1.0f;

	GameObject targetGoal = null;
	float target_upd_time;

  void Start() {
		navigateTo(transform.position);
		target_upd_time = Time.time;
  }

  void Update() {
		if(Time.time > target_upd_time) {
			if(targetGoal != null) navigateTo(targetGoal.transform.position);
			else {
				navigateTo(new Vector3(0, 0, 0));
				selectTarget();
			}

			target_upd_time = Time.time + 0.5f;
		}

		runMotionBehaviour();
  }

	void selectTarget() {
		targetGoal = GameObject.Find("Managers").GetComponent<SheepManager>().getEscapee();
	}

	protected override void onMotionUpdate(Vector3 dir) {
		dir *= speed;
		gameObject.GetComponent<Rigidbody2D>().velocity = dir * speed;
	}

}
