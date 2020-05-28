using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public float force;
    //private GameObject player;
    private GameObject wind;
    private bool created = false;
    private float moveDistance;
    private Vector3 startPosition;
    private bool fadein = false;
    private bool fadeout = false;
    //private Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        moveDistance = GetComponent<BoxCollider2D>().size.y + 0.5f;
        startPosition = new Vector3(transform.position.x - transform.up.x * GetComponent<BoxCollider2D>().size.y / 2, transform.position.y - transform.up.y * GetComponent<BoxCollider2D>().size.y / 2, 0);
        //endPosition = new Vector3(transform.position.x + transform.up.x * (GetComponent<BoxCollider2D>().size.y / 2 +0.5f), transform.position.y + transform.up.y * (GetComponent<BoxCollider2D>().size.y / 2 +0.5f),0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!created)
        {
            wind = Instantiate(Resources.Load("Wind") as GameObject, startPosition, transform.localRotation);
            wind.GetComponent<Wind>().force = force;
            wind.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
            fadein = true;
            created = true;
        }
        wind.transform.position += transform.up * 0.05f;
        if (fadein)
        {
            //Debug.Log("fadein"+ wind.GetComponent<SpriteRenderer>().color.a);
            wind.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, wind.GetComponent<SpriteRenderer>().color.a + 0.05f);
            if (wind.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                fadein = false;
            }
        }
        if (fadeout)
        {
            wind.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, wind.GetComponent<SpriteRenderer>().color.a - 0.05f);
            if (wind.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                fadeout = false;
            }
        }

        if (Vector2.Distance(wind.transform.position, startPosition) > moveDistance - 1)
        {
            fadeout = true;
            fadein = false;
        }

        if (Vector2.Distance(wind.transform.position, startPosition) > moveDistance)
        {
            wind.transform.position = startPosition;
            wind.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
            fadein = true;
            fadeout = false;
        }

        
    
    }

    
}
