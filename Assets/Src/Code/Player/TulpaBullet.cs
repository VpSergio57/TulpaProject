using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TulpaBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    public Vector2 direction;
    public float livigTime = 2f;
    public bool leftFace = false;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        mySpriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Destroy( this.gameObject, livigTime);
        mySpriteRenderer.flipX = leftFace;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
