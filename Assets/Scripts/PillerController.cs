using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillerController : MonoBehaviour {
    public static PillerController instance;
    public float plateformIntervalHeight;
    public GameObject plateformPrefab;
    public GameObject plateformParent;
    public Queue<PlateformController> plateformQueue = new Queue<PlateformController>();
    private void Awake() {
        instance = this;
    }
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() { 
    }

    public void GenPlateform() {
        Vector3 tmpPos = new Vector3(0, -1 * plateformIntervalHeight * GameManager.instance.plateformLayer, 0);
        GameObject tmpPlateform = GameObject.Instantiate(plateformPrefab, tmpPos, Quaternion.identity);
        tmpPlateform.transform.SetParent(GameManager.instance.transform);
        plateformQueue.Enqueue(tmpPlateform.GetComponent<PlateformController>());
        GameManager.instance.AddPlateformLayer();
    }

    public void collpasePlateform() {
        PlateformController tmpPlate = plateformQueue.Dequeue();
        tmpPlate.collapse();
    }
    public void descendPiller() {
        Vector3 tmpPos = this.transform.position;
        tmpPos.y -= plateformIntervalHeight;
        this.transform.position = tmpPos;
    }
}
