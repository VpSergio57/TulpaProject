using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamUno : MonoBehaviour
{
    [StringInList("drop", "spit", "shoot")]
    public string imSlamtype;
    Animator myAnimacion;
    private Renderer renderer;
    private Color color;
    private Rigidbody2D miRigidBd;
    private Vector2 dropFace;
    // Start is called before the first frame update

    private void Awake()
    {
        miRigidBd = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        color = renderer.material.color; 
    }
    void Start()
    {
        myAnimacion = this.GetComponent<Animator>();
        
        if(imSlamtype == "spit")
        {
            miRigidBd.AddRelativeForce(dropFace * 200);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      //  if(!onGround){
        if(other.gameObject.CompareTag("Ground")){
            //  Debug.LogWarning("le pego");
            myAnimacion.Play("Splash");
            convertInStaticObject();
            StartCoroutine("Difuninate");
            Destroy( this.gameObject, 3f);

        }
        else if (other.gameObject.CompareTag("Player"))
        {
            //animacion de splash en player
            Destroy(this.gameObject);
        }
          //  onGround = true;
     //   }

    }

    public void setDropFace(float position)
    {
        dropFace = new Vector2(position, 0);
    }

    private void convertInStaticObject()
    {
       Destroy(GetComponent<Rigidbody2D>());
       Destroy(GetComponent<BoxCollider2D>());
    }

    IEnumerator Difuninate(){

        for (float i=0; i < 11;i++){

            yield return new WaitForSeconds(0.3f);
            color.a -= 0.1f;
            renderer.material.color = color;

        }
    }

}
