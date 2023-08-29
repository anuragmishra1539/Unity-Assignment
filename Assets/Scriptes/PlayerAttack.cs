using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackcoolDown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] firebox;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float pauseTimer=Mathf.Infinity;
    private void Awake()
    {
        animator= GetComponent<Animator>();
        playerMovement= GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && pauseTimer > attackcoolDown && playerMovement.attackCondition())
        {
            Attack();  
        }
        pauseTimer += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        pauseTimer = 0;

        firebox[fireball()].transform.position = firepoint.position;
        firebox[fireball()].GetComponent<projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    private int fireball()
    {

        for(int i = 0; i < firebox.Length; i++)
        {
            if (!firebox[i].activeInHierarchy)
                return i;
        }
        return 0;

    }
}
