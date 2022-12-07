using UnityEngine;
using System.Collections;
using System;

public class Camara : MonoBehaviour {

    private Transform target;
    //public GameObject deathPanel;
    public Vector3 targetPosotion;
    public static Camara estadoJuego;
    public int PuntoA;
    public int PuntoB;
    void Start() {

        GameObject tmp = GameObject.FindGameObjectWithTag("Player");
        if (tmp != null) {
            target = tmp.transform;
        }

        
        if (PlayerPrefs.GetInt("vol") == 1)
        {
            AudioListener.pause = true;
        }
        

    }

    void Awake()
    {/*
        if (estadoJuego == null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoJuego != this)
        {
            Destroy(gameObject);
        }
        */
    }

    bool adelante = true;
    void Update () {

        //cambio suave posicion a otra
        if (target)
        {
            targetPosotion = new Vector3(this.target.position.x + (this.target.position.x < 0 ? -1  : 1), this.target.position.y + 1, -10);
          //  targetPosotion.z = -10;
           // targetPosotion.y = targetPosotion.y - 0.1f;
            //seguimiento suave
            this.transform.position = Vector3.Lerp(this.transform.position, this.targetPosotion, Time.deltaTime*2);    
            //seguimiento normal



            if (target.position.x > PuntoA && target.position.x < PuntoB)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosotion, Time.deltaTime * 2);
            }
            else {
                if (targetPosotion.x<PuntoA)
                { targetPosotion.x = PuntoA + 1; }
                else { targetPosotion.x = PuntoB; }
                transform.position = Vector3.Lerp(transform.position, targetPosotion, Time.deltaTime * 2);
            }
            //transform.position = new Vector3(0,0,1);
          //  Debug.Log("Camara Sigue"+targetPosotion);
        }

    }

    
}
