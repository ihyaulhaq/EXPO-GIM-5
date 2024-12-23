using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    //var nembak
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    private float timer;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //tembak
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < range)
        {
            timer += Time.deltaTime;

            if (timer > cooldown)
            {
                timer = 0;
                shoot();
            }
        }  
    }
    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

}

