using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformController : MonoBehaviour {
    public PieCellAttribute[] pieCellList = new PieCellAttribute[12];
    public int[] typeList = new int[12];
    public GameObject spacePieCell;
    public GameObject groundPieCell;
    public GameObject dangerPieCell;
    public float spaceRatio;
    public float dangerRatio;
	// Use this for initialization
	void Start () {
        spaceRatio = (float)(10 - Mathf.Max(Mathf.Log10(GameManager.instance.plateformLayer))) / (5 + GameManager.instance.plateformLayer);
        dangerRatio = Mathf.Min((Mathf.Log10(GameManager.instance.plateformLayer)), 0.5f);
        GenPieCell();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void setSpacePieCellPos() {
        //Random Space Position
        int spaceCounter = 0;
        for(int tmp_i = 0; tmp_i < typeList.Length;++tmp_i) {
            if (Random.Range(0f, 1f) < spaceRatio) {
                typeList[tmp_i] = 1;
                ++spaceCounter;
            }
        }
        if (spaceCounter == 0) {
            int tmpN = Random.Range(0, 12);
            typeList[tmpN] = 1;
        }

    }

    void setDangerPieCellPos() {
        //Random Danger Position
        bool isDanger = Random.Range(0f, 1f) < dangerRatio;
        if (isDanger) {
            for (int tmp_i = 0; tmp_i < typeList.Length; ++tmp_i) {
                if (typeList[tmp_i]==0 && Random.Range(0f,1f) < dangerRatio) {
                    typeList[tmp_i] = 2;
                }
            }
        }
    }

    void GenPieCell() {
        setSpacePieCellPos();
        setDangerPieCellPos();
        for (int i = 0; i < 12; ++i) {
            GameObject tmpPieCell =null;
            if (typeList[i] == 0) {
                tmpPieCell = GameObject.Instantiate(groundPieCell, this.transform.position, Quaternion.Euler(90, i * 30, 0));
            } else if (typeList[i] == 1) {
                tmpPieCell = GameObject.Instantiate(spacePieCell, this.transform.position, Quaternion.Euler(90, i * 30, 0));
            } else if (typeList[i] == 2) {
                tmpPieCell = GameObject.Instantiate(dangerPieCell, this.transform.position, Quaternion.Euler(90, i * 30, 0));
            }
            tmpPieCell.transform.SetParent(this.transform);
            pieCellList[i] = tmpPieCell.GetComponent<PieCellAttribute>();
        }
    }

    public void collapse() {
        for (int i = 0;i < 12;++i) {
            pieCellList[i].sparateDown();
        }
        Destroy(this.gameObject, 0.5f);
    }
}
