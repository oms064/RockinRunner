﻿using UnityEngine;
using System.Collections;

public class RightArmBehavior : MonoBehaviour {
    private Animator animator;
    public string input = "w";

    void Awake() {
        animator = GetComponent<Animator>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(input))
        {
            animator.SetInteger("Status", 1);
        }
        else {
            animator.SetInteger("Status", 0);
        }
	}
}
