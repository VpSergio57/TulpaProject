using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TulpaAtributes : MonoBehaviour
{
  //  public TulpaController TulpaController;
    private Renderer renderer;
    private Color color;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        color = renderer.material.color; 
    }

    // Update is called once per framed
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHurt"))
        {
           // Debug.LogError("VALIENDO CHETOS");
           // TulpaController.jumpBack();
            StartCoroutine("GetInvulneravility");
        }
    }

    IEnumerator GetInvulneravility(){
        
        Physics2D.IgnoreLayerCollision(10,11, true); // son id de layer  AtacksLayer,Player

        for (int i=0; i < 7;i++){
            color.a = 0.1f;
            renderer.material.color = color;
            yield return new WaitForSeconds(0.15f);
            color.a = 0.8f;
            renderer.material.color = color;
            yield return new WaitForSeconds(0.15f);
        }

        
        
        color.a = 1f;
        renderer.material.color = color;
        Physics2D.IgnoreLayerCollision(10,11, false);

    }

}
