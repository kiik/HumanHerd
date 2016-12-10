using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class Zoom : MonoBehaviour {

  public float speed = 10.0f, max = 3.0f, initialZoom = 2.0f;

  private float m_value, m_target;
  private bool m_done = true;

	void Start () {
    m_value = initialZoom;
    m_target = initialZoom;

    changeZoom(m_value);
	}

	void Update () {
		if (Input.mouseScrollDelta.y != 0) {
      m_target += Input.mouseScrollDelta.y < 0 ? 0.5f : Input.mouseScrollDelta.y > 0 ? -0.5f : 0.0f;
      m_target = Mathf.Clamp(m_target, 1, max);

      m_done = false;
    }

    if(!m_done) {
      if(Mathf.Abs(m_target - m_value) < 0.01) {
        m_value = m_target;
        m_done = true;
      } else m_value = Mathf.Lerp(m_value, m_target, speed * Time.deltaTime);

      changeZoom(m_value);
    }
	}

	public void changeZoom(float value) {
		gameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(value, 1, 10);
	}

}
