using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToHologram : MonoBehaviour {

	void Start ()
    {
        Material material = new Material(Shader.Find("Hologram"));
        material.color = Color.red;
        GetComponent<Renderer>().material = material;
    }

}
