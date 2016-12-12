using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour {

  public List<GameObject> escapees;

  void Start() {

  }

  void Update() {

  }

  public GameObject getEscapee() {
    int l = escapees.Count;
    if(l > 0)
      return escapees[Random.Range(0, l)];

    return null;
  }

  public void addEscapee(GameObject o) {
    escapees.Add(o);
  }

}
