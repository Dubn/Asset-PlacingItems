using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour {
    private Transform mainCamera;

	void Start () {
        mainCamera = Camera.main.GetComponent<Transform>();
    }
	
	void Update () {
        transform.LookAt(mainCamera);
    }
}
