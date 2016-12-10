using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ProjectOnGround : MonoBehaviour
{
  private Quaternion savedRotation;

  void OnWillRenderObject()
  {
    savedRotation = transform.rotation;

		var eulers = transform.rotation.eulerAngles;

		transform.rotation = Quaternion.Euler(54.0f, eulers.y, eulers.z);
  }

  //called right after rendering the object
  void OnRenderObject()
  {
    transform.rotation = savedRotation;
  }
}
