using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform targetTransform;
    public Vector3 posOffset;
    public float heightOffset;
    public float posScale;
    public Vector3 targetPos;
    public float followSpeed;
	// Use this for initialization
	void Start () {
        posOffset = this.transform.position - targetTransform.position;
        posScale = this.transform.position.z / targetTransform.position.z;
        heightOffset = posOffset.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //targetPos = new Vector3(posScale * targetTransform.position.x,GameManager.instance.ballLayer * -10, posScale * targetTransform.position.z);
        //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, followSpeed * Time.deltaTime);
        targetPos = new Vector3(posScale * targetTransform.position.x, targetTransform.position.y + heightOffset, posScale * targetTransform.position.z);
        this.transform.position = targetPos;
        this.transform.LookAt(GameManager.instance.ballObject.transform);
        //Quaternion tmpQuater = Quaternion.identity;
        //tmpQuater.x = 60;
        //tmpQuater.y = targetTransform.rotation.y;
        //this.transform.rotation = tmpQuater;
	}
}
