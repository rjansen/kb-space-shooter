using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour {
    void OnTriggerExit(Collider collider) {
        Destroy(collider.gameObject);
    }
}
