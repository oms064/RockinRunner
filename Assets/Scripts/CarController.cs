﻿using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
   
    public Manager manager;
    private Vector3 direccion, posicion;
    private Quaternion origRot;
    private Rigidbody rb;
    private float velocidad, velocidadRot;
    private float tiempo;
    private bool giro;

    // Use this for initialization
    void Awake () {
        rb = this.GetComponent<Rigidbody>();
        direccion = rb.rotation.eulerAngles;
        posicion = this.transform.position;
        origRot = rb.rotation;
        tiempo = 0.0f;
    }

    void Start() {
        giro = true;
    }

    // Update is called once per frame
    void FixedUpdate () {
        velocidad = manager.velocidad * -10.0f;
        velocidadRot = velocidad * 5.0f;

#if UNITY_EDITOR
        if (!giro || (Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") > -0.1f)) {
            giro = false;
            rb.rotation = Quaternion.Lerp(rb.rotation, origRot, ( 1.0f - manager.velocidad) * (Time.time - tiempo));
            direccion = rb.rotation.eulerAngles;
            if (rb.rotation.Equals(origRot)) {
                giro = true;
            }
        }
        else if(giro) {
            direccion.y += Input.GetAxis("Horizontal") * Time.deltaTime * velocidadRot;
            posicion.z -= Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

            direccion.y = Mathf.Clamp(direccion.y, 80.0f, 100.0f);
            posicion.z = Mathf.Clamp(posicion.z, -2.0f, 2.0f);

            //rb.MoveRotation(Quaternion.Euler(direccion * Time.deltaTime) * rb.rotation);
            rb.rotation = Quaternion.Euler(direccion);
            rb.position = posicion;
            tiempo = Time.time;
        }

#elif UNITY_ANDROID
        if (giro || (Input.acceleration.x < 0.1f && Input.acceleration.x > -0.1f)) {
            giro = false;
            rb.rotation = Quaternion.Lerp(rb.rotation, origRot, ( 1.0f - manager.velocidad) * (Time.time - tiempo));
            direccion = rb.rotation.eulerAngles;
            if (rb.rotation.Equals(origRot)) {
                giro = true;
            }
        }
        else {
            direccion.y += Input.acceleration.x * Time.deltaTime * velocidadRot;
            posicion.z -= Input.acceleration.x * Time.deltaTime * velocidad;

            direccion.y = Mathf.Clamp(direccion.y, 80.0f, 100.0f);
            posicion.z = Mathf.Clamp(posicion.z, -2.0f, 2.0f);

            //rb.MoveRotation(Quaternion.Euler(direccion * Time.deltaTime) * rb.rotation);
            rb.rotation = Quaternion.Euler(direccion);
            rb.position = posicion;
            tiempo = Time.time;
        }
#endif
    }



}
