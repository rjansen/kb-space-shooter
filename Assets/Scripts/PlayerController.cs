using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
    public GameObject bullet;
    public Transform weapon;
    public Boundary boundary = new Boundary{xMin = -6, xMax = 6, zMin = -4, zMax = 8};
    public float speed = 10;
    public float tilt = 4;
    public float fireRate = 4f;

    Rigidbody rb;
    float nextFireTime;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime) {
            Fire();
        }
    }

    void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3(
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    void Fire() {
        Instantiate(bullet, weapon.position, weapon.rotation);
        nextFireTime = Time.time + 1f / fireRate;
    }
}
