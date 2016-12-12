using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleCitizen : AIBase {

	  public float speed = 1.0f;

		GameObject targetGoal;

		bool m_pathing;

	  void Start() {
			m_pathing = true;
			targetGoal = null;

	    navigateTo(transform.position);
	  }

	  void Update() {
			runDescisionBehaviour();

			if(m_pathing) {
				runMotionBehaviour();
			}
	  }

	  protected override void onMotionUpdate(Vector3 dir) {
	    dir *= m_pathing ? speed : 0;
	    gameObject.GetComponent<Rigidbody2D>().AddForce(dir);
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
				GameObject[] objs = GameObject.FindGameObjectsWithTag("MigrationTarget");
				Debug.Log(objs);

				if(objs.Length > 0) {
					targetGoal = objs[Random.Range(0, objs.Length)];
					navigateTo(targetGoal.transform.position);
				}
			}
		}

		public void immobilize() {
			m_pathing = false;
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
		}

		public void mobilize() {
			m_pathing = true;
		}

}
