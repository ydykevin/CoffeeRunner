using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    // Use this for initialization

    private Vector3 BirthNode;
    Rigidbody2D rigidbody;
	void Start () {
        BirthNode = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(rigidbody.velocity);
        if (rigidbody.velocity.y<-5)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -5);
        }
        if (transform.position.y<-10)
        {
            var x = Random.Range(-7, 7);
            transform.position = new Vector3(x,BirthNode.y,BirthNode.z);
            rigidbody.velocity = Vector2.zero;
        }
	}
}
