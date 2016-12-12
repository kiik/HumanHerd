using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleCitizen : AIRoam {

	  public float speed = 1.0f;

		GameObject targetGoal = null;
		bool m_pathing;

		public bool m_roaming;

	  void Start() {
			m_pathing = true;
			m_roaming = true;

			GameObject obj = GameObject.FindGameObjectWithTag("InvaderSpawner");
			roamArea = obj.GetComponent<Collider2D>().bounds;

			navigateTo(transform.position);
			roam();
	  }

	  void Update() {
			runDescisionBehaviour();

			if(m_pathing) {
				runMotionBehaviour();
			}
	  }

	  protected override void onMotionUpdate(Vector3 dir) {
	    dir *= speed;
	    gameObject.GetComponent<Rigidbody2D>().AddForce(dir * speed);
	  }

	  void OnTriggerEnter2D(Collider2D o) {
			if(o.gameObject.tag == "InvasionTarget") {
				onEndgoalReached();
			}
	  }

		void OnTriggerExit2D(Collider2D o) {
			if(o.gameObject.tag == "InvaderSpawner") {
				startEmigrating();
			}
		}

		void OnCollisionEnter2D(Collision2D c) {
			Vector3 dir = transform.position - c.transform.position;
			dir.Normalize();

			gameObject.GetComponent<Rigidbody2D>().AddForce(dir * speed * 2.0f);
		}

		void runDescisionBehaviour() {
			if(m_roaming) {
				if(pathTargetAchieved() > 0) {
					roam();
				}
			} else {
				if(targetGoal == null) {
					findEmigrationTarget();
				}
			}
		}

		Vector3 findEmigrationTarget() {
			if(targetGoal == null) {
				GameObject[] objs = GameObject.FindGameObjectsWithTag("InvasionTarget");

				if(objs.Length > 0) {
					targetGoal = objs[Random.Range(0, objs.Length)];

					Collider2D[] cols = targetGoal.GetComponents<Collider2D>();

					Bounds b = cols[Random.Range(0, cols.Length)].bounds;
					Vector3 p = new Vector3(Random.Range(b.min.x, b.max.x), Random.Range(b.min.y, b.max.y), 0);

					navigateTo(p);
					Debug.Log(p);
					return p;
				}
			}

			targetGoal = null;

			return new Vector3(0, 0, 0);
		}

		void startRoaming() {
			m_roaming = true;
			navigateTo(transform.position);
		}

		void startEmigrating() {
			m_roaming = false;
			findEmigrationTarget();

			GameObject.Find("Managers").GetComponent<SheepManager>().addEscapee(gameObject);
		}

		public void immobilize() {
			m_pathing = false;
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
		}

		public void mobilize() {
			m_pathing = true;
		}

		void onEndgoalReached() {
            GameManager.instance.ecoManager.Escaped();
		}

}
