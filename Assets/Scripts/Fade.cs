using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private SpriteRenderer black;
    private bool done = false;
    //private bool fadeout = false;

    // Start is called before the first frame update
    void Start()
    {
        black = GetComponent<SpriteRenderer>();
        black.color = new Vector4(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (black.color.a>0 && !done)
        {
            black.color = new Vector4(0, 0, 0, black.color.a - 0.01f);
            if (black.color.a<=0)
            {
                done = true;
            }
        }
 
    }


}
