using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiAtributes : MonoBehaviour
{
    private EmemiPatrolController enemiMovement;

    private AudioSource hurtSound;
    // Start is called before the first frame update
    private void Awake()
    {
        enemiMovement = this.GetComponent<EmemiPatrolController>();
        if(enemiMovement == null)
        {
            Debug.LogError("No haz agregado a algun enemigo el script de EmemiPatrolController");
        }
        GameObject hurtSoundGO = transform.Find("HurtEffect").gameObject;
        if (hurtSoundGO == null)
        {
            Debug.LogError("debes agregar un punto de daño en el enemigo");
        }
        if (hurtSoundGO != null){
            hurtSound = hurtSoundGO.GetComponent<AudioSource>();
        }

    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collider cuando me hacen daño
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemiHurt"))
        {
            Debug.LogError("ARWWWWWWWW");
            enemiMovement.jumpBack();
            hurtSound.Play();
        }
    }


}
