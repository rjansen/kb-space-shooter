using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject bullet;
    public Transform weapon;
    public Boundary boundary = new Boundary{xMin = -6, xMax = 6, zMin = -20, zMax = 20};
    public Vector2 startWait = new Vector2(0f, 0.25f);
    public Vector2 maneuverTime = new Vector2(0.5f, 1f);
    public Vector2 maneuverWait = new Vector2(0.5f, 1f);
    public float speed = -10f;
    public float tilt = 14f;
    public float fireRate = 4f;
    public float delay = 1f;
    public float dodge = 5f;
    public float smoothing = 7.5f;

    // private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;
    private Transform player;

    void Start () {
        rb = GetComponent<Rigidbody>();
        GameObject tmpPlayer = GameObject.FindWithTag("Player");
        if (tmpPlayer != null)
            player = tmpPlayer.transform;
        rb.velocity = transform.forward * speed;
        StartCoroutine(Evade());
        InvokeRepeating("Fire", delay, fireRate);
        // currentSpeed = rb.velocity.z;
    }

    void Fire () {
        Instantiate(bullet, weapon.position, weapon.rotation);
    }

    IEnumerator Evade() {
        yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

        while (true) {
            if (player != null)
                targetManeuver = player.position.x;
            else
                targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);

            yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate () {
        float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3 (newManeuver, 0.0f, rb.velocity.z);
        rb.position = new Vector3(
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
