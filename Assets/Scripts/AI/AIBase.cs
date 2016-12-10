using UnityEngine;
using System.Collections;

using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class AIBase : MonoBehaviour {

    public Vector3 targetPosition;
    public Path path;

    public float waypointStep = 0.04f;
    public float repathRate = 0.5f;

    private int  m_wp_id = 0;
    private float m_last_repath = -9999;

    private Seeker seeker;

    public void navigateTo(Vector3 t) {
      if((targetPosition == t) && (!motionTargetAchieved())) return;

      targetPosition = t;

      seeker = GetComponent<Seeker>();
      seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    public void OnPathComplete (Path p) {
        path = p;
        m_wp_id = 0;
    }

    public void runMotionBehaviour() {
      pathUpdate();
      pathTraversion();
    }

    public bool motionTargetAchieved() {
      if(Vector3.Distance(transform.position, targetPosition) < waypointStep) return true;
      return false;
    }

    public int pathTargetAchieved() {
      if (path == null) return 1;
      if (m_wp_id > path.vectorPath.Count) return 2;

      if (m_wp_id == path.vectorPath.Count) {
          m_wp_id++;
          return 3;
      }

      return -1;
    }

    protected void pathUpdate() {
      if(motionTargetAchieved()) return;

      if(Time.time - m_last_repath > repathRate && seeker.IsDone()) {
          m_last_repath = Time.time + Random.value*repathRate*0.5f;
          seeker.StartPath(transform.position, targetPosition, OnPathComplete);
      }
    }

    protected void pathTraversion() {
      if(pathTargetAchieved() > 0) return;

      Vector3 dir = (path.vectorPath[m_wp_id] - transform.position).normalized;
      onMotionUpdate(dir);

      if (Vector3.Distance(transform.position, path.vectorPath[m_wp_id]) < waypointStep) m_wp_id++;
    }

    protected virtual void onMotionUpdate(Vector3 dir) {

    }


}
