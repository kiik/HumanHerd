using UnityEngine;
using System.Collections;

public class AIRoam : AIBase {

  public Bounds roamArea;

  public void roam() {
    Vector3 p = new Vector3(Random.Range(roamArea.min.x, roamArea.max.x), Random.Range(roamArea.min.y, roamArea.max.y), 0);
    navigateTo(p);
  }


}
