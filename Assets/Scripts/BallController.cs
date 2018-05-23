using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float upSpeed;
    public float gravityForce;
    public float rotateSpeed;
    public int continuousLayerNum;
    public Transform pillerTransform;
    private Rigidbody ballRigidbody;

	// Use this for initialization
	void Start () {
        ballRigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ballRigidbody.AddForce(new Vector3(0, -1 * gravityForce, 0));
        float rotateDir = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rotateDir = 1;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            rotateDir = -1;
        }
        this.transform.RotateAround(pillerTransform.position, Vector3.up, rotateDir * rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Ground" && this.transform.position.y >= collision.transform.position.y) {
            ballRigidbody.velocity = new Vector3(0, upSpeed, 0);
            AudioManager.instance.playeAudioJump();
            if (continuousLayerNum >= 3) {
                GameManager.instance.AddScore(10);
                PillerController.instance.GenPlateform();
                PillerController.instance.collpasePlateform();
                PillerController.instance.descendPiller();
            }
            continuousLayerNum = 0;
        } else if (collision.collider.tag == "Danger") {
            ballRigidbody.velocity = new Vector3(0, upSpeed, 0);
            AudioManager.instance.playeAudioJump();
            if (continuousLayerNum >= 3) {
                GameManager.instance.AddScore(10);
                PillerController.instance.GenPlateform();
                PillerController.instance.collpasePlateform();
                PillerController.instance.descendPiller();
                continuousLayerNum = 0;
                return;
            }
            GameManager.instance.AddHealth(-1);
            if (GameManager.instance.healthValue <= 0) {
                GameManager.instance.EndGame();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Space" && this.transform.position.y < other.transform.position.y) {
            GameManager.instance.ballLayer += 1;
            GameManager.instance.AddScore();
            AudioManager.instance.playAudioThrough();
            PillerController.instance.GenPlateform();
            PillerController.instance.collpasePlateform();
            PillerController.instance.descendPiller();
            continuousLayerNum++;
        }
    }
}
