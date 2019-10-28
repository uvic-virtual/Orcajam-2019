using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuciferProjectile : MonoBehaviour {
    private Transform cameraTran;
    private Vector3 initDir;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        cameraTran = GameObject.Find("VRCamera").transform;
        initDir = transform.position - cameraTran.position;
        Debug.DrawRay(gameObject.transform.position, initDir, Color.red);
        rb.AddForce(initDir * 600f);

    }

    // Update is called once per frame
    void Update () {
	}
}
