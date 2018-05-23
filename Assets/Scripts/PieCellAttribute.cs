using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieCellAttribute : MonoBehaviour {
    public float turnTransparentSpeed;
    public float sparateForce;
    public MeshRenderer surfaceRender;
    public MeshCollider pieCellCollider;
    public Rigidbody pieCellRigidbody;

	// Use this for initialization
	void Start () {
        surfaceRender = this.GetComponentInChildren<MeshRenderer>();
        pieCellRigidbody = this.GetComponent<Rigidbody>();
        pieCellCollider = this.GetComponent<MeshCollider>();
        pieCellRigidbody.isKinematic = true;
	}
	// Update is called once per frame
	void Update () {
	}

    public void turnTransparent() {
        surfaceRender.material.color = Color.Lerp(surfaceRender.material.color, new Color(0, 0, 0, 0), 10 * Time.deltaTime);
    }

    public void sparateDown() {
        Vector3 tmpOffset = this.transform.position - GameManager.instance.ballObject.transform.position;
        tmpOffset.y = 2;
        pieCellRigidbody.isKinematic = false;
        pieCellRigidbody.AddForce(sparateForce * tmpOffset);
        //pieCellCollider.isTrigger = true;
        if (this.GetComponent<MeshCollider>().tag == "Space") {
            Destroy(this.gameObject);
        } else {
            Destroy(this.gameObject, 0.5f);
        }
        this.GetComponent<MeshCollider>().tag = "PieCell";
    }
}
