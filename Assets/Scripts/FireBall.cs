using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//火球自身的控制器
public class FireBall : MonoBehaviour
{
    //出生后10s后销毁自己，避免浪费性能
    void Start()
    {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        //火球移动方法
        transform.position += new Vector3(-4*Time.deltaTime, 0, 0);
    }

    //碰撞到墙壁时销毁自己
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }

}
