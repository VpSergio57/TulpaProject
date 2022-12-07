using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ia_nc02 : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX ;
    public float waitingTime = 2f;
    private GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();
        StartCoroutine("PatrolTarget");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTarget()
    {
        //if first time, create target in the left
        if(_target == null){
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1);
            return; // el return de esta manera sirve para dejar de ejecutar el demas codigo del metodo.
        }

        //If we are in the left, change target to the right
        if(_target.transform.position.x == minX){
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1,1,1);
        }
        
        //If we are in the right, change target to the left
        else if(_target.transform.position.x == maxX){
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private IEnumerator PatrolTarget(){

        //Coroutine to move enemy
        while(Vector2.Distance(transform.position, _target.transform.position) > 0.05f){
            //let's move to target
            Vector2 direction = _target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * speed * Time.deltaTime);

            yield return null;
        }
        //At this piint , i have reached the target, let's set our position to the target's one 
        Debug.Log("Target reached");
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);

        //And let's wait for a moment
        Debug.Log("Waiting for "+ waitingTime + "Seconds");
        yield return new WaitForSeconds(waitingTime);

        //once waited, let's restore the patrol behaviour
        Debug.Log("Waiting enough, let's update the target and move again");
        UpdateTarget();
        StartCoroutine("PatrolTarget");

    }
}
