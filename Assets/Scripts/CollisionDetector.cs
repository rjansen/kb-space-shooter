using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {
    public GameObject explosion;
    public GameObject playerExplosion;
    public int score = 10;

    GameController gameController;

    void Start() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Boundary") || collider.CompareTag("Enemy")) return;
        if (explosion != null) Instantiate(explosion, transform.position, transform.rotation);
        gameController.AddScore(score);
        if (collider.tag == "Player") {
            Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation);
            gameController.GameOver();
        }
        Destroy(collider.gameObject);
        Destroy(gameObject);
    }
}
