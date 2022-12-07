using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemiPatrolController : MonoBehaviour
{
    [StringInList("NC1", "NC2", "NC3")]
    public string im;
    [Range(1, 10)]
    public float SpeedOne = 1f;
    [Range(1, 10)]
    public float SpeedTwo = 1f;
    public bool UseOnlySpeedOne = false;
    public float minX;
    public float maxX;
    public float waitingTime = 1f;
    [Range(2, 10)]
    public float timeToShootAgain;
    public float timeToWaitToShooting = 0.5f;
    public GameObject slam;
    private Transform shootPoint;
    private float diferencia=0;
    private bool canImove;
    private float defaultSpeed=0;
    private float timeToShootAgainCounter = 0;
    private Rigidbody2D body;
    private GameObject _target;
    private Transform whereIm;
    private Transform playerTransform;
    private Animator _animator;
    private Vector2 movement;
    // Start is called before the first frame update

    void Awake()
    {
        defaultSpeed = SpeedOne;
        this._animator = GetComponent<Animator>();
        this.whereIm = this.gameObject.transform;
        this.movement = new Vector2();
        body = this.GetComponent<Rigidbody2D>();
        shootPoint = this.transform.Find("shootingPoint");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Start()
    {
        UpdateTarget();
        StartCoroutine("PatrolTarget");     
    }

    // Update is called once per frame
    void Update()
    {
        timeToShootAgainCounter += Time.deltaTime;
        if(timeToShootAgainCounter >timeToShootAgain){
            timeToShootAgainCounter = 0;
            enemiAtack();
        }
        //para saber si me debo mover
        diferencia = System.Math.Abs(playerTransform.position.x - this.whereIm.position.x);
        if (diferencia < 20)
        {
            canImove = true;
        }
        else
        {
            canImove = false;
        }
      //  Debug.LogError(diferencia);
    }

    public void jumpBack()
    {
        Debug.Log(transform.position.x + " <> " + transform.position.y);
        
        if(playerTransform.localScale.x > 0)
        {
           // body.AddForce(Vector2.right * 3500);
            body.MovePosition( new Vector2(this.transform.position.x +0.7f, this.transform.position.y));
        }
        else
        {
             body.MovePosition( new Vector2(this.transform.position.x -0.7f, this.transform.position.y));
            //body.AddForce(Vector2.left * 3500);
        }
        StartCoroutine(WhaitTime(0.3f));
    }

    private void UpdateTarget()
    {
        //if first time, create target in the left
        if(this._target == null){
            this._target = new GameObject("Target");
            this._target.transform.position = new Vector2(minX, transform.position.y);
           // transform.localScale = new Vector3(-1,1,1);
            return; // el return de esta manera sirve para dejar de ejecutar el demas codigo del metodo.
        }

        //If we are in the left, change target to the right
        if(this._target.transform.position.x == minX){
            this._target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1,1,1);
        }
        
        //If we are in the right, change target to the left
        else if(this._target.transform.position.x == maxX){
            this._target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private IEnumerator PatrolTarget(){

      //  Debug.LogError("HI");
        if (canImove)
        {
            if (!UseOnlySpeedOne)
            {
                if (1 == Random.Range(1, 4)) //Esto debe ser mayor a 0
                {
                    SpeedOne = SpeedTwo;
                }
                else
                {
                    SpeedOne = defaultSpeed;
                }
            }
            //Coroutine to move enemy
            while (Vector2.Distance(transform.position, this._target.transform.position) > 0.15f )
            {
                //let's move to target
                Vector2 direction = this._target.transform.position - transform.position;
                float xDirection = direction.x;
               // Debug.Log(this._target.transform.position +"<<<>>>"+transform.position);
               // Debug.Log(Vector2.Distance(transform.position, this._target.transform.position)+"<<>>"+direction.normalized.x);
                this._animator.SetBool("Walking", true);

                movement = body.velocity;
                movement.x = direction.normalized.x * SpeedOne; 
                body.velocity = movement;
               // transform.Translate(direction.normalized * SpeedOne * Time.deltaTime);

                yield return null;
            }
            //At this piint , i have reached the target, let's set our position to the target's one 
            
           // movement = body.velocity;
            movement.x = 1 * 0; 
            body.velocity = movement;


            Debug.Log("Target reached");
            transform.position = new Vector2(this._target.transform.position.x, transform.position.y);
            this._animator.SetBool("Walking", false);
            //And let's wait for a moment
         //   Debug.Log("Waiting for " + waitingTime + "Seconds");
            yield return new WaitForSeconds(waitingTime);

            //once waited, let's restore the patrol behaviour
        //    Debug.Log("Waiting enough, let's update the target and move again");

            UpdateTarget();
        }
        else
        {
            yield return null;
        }

        
        StartCoroutine("PatrolTarget");

    }

    private void enemiAtack()
    {
      //  Debug.Log("hola> " + transform.position.normalized.x);
        if (slam != null)
        {
            StartCoroutine(WhaitTime(timeToWaitToShooting));
            GameObject currentSlam =  Instantiate(slam, shootPoint.position, Quaternion.identity);
            SlamUno slamInstance = currentSlam.GetComponent<SlamUno>();
            slamInstance.setDropFace(transform.localScale.x);
            //Debug.Log("hola> "+transform.localScale.x);
        }
    }
  
    IEnumerator WhaitTime(float time)
    {
		SpeedOne = 0;
		yield return new WaitForSeconds(time);
		SpeedOne = defaultSpeed;
	}


}
