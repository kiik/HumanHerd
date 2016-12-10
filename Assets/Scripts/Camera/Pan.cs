using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Pan : MonoBehaviour
{

  public GameObject target;

  private bool m_panning = false;
  private Vector3 m_wpoint;

  void Start () {

  }

  void Update()
  {
    if(Input.GetMouseButtonDown(2)) {
      if(!m_panning) {
        m_wpoint = getAnchor();

        if(m_wpoint.magnitude > 0)
          m_panning = true;
      }
    } else if(Input.GetMouseButtonUp(2)) {
      if(m_panning) m_panning = false;
    }

    if(m_panning) {
      if(!target) return;

      Vector3 dv = m_wpoint - getAnchor();

      Debug.Log(m_wpoint);
      target.transform.Translate(Vector3.Lerp(dv, m_wpoint, 1.0f * Time.deltaTime));
    }
  }

  Vector3 getAnchor() {
    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
  }

}
