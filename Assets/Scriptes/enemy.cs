using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;
using UnityEditor;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.XR;
using Unity.Burst.CompilerServices;
using System;

public class enemy : MonoBehaviour
{
    [SerializeField] public Transform pointA;
    [SerializeField] public Transform pointB;
    [SerializeField] public Transform enemyA;
    [SerializeField] private float range;
    [SerializeField] private GameObject[] firebox;
    [SerializeField] private Transform firepoint;
    public Transform player;
    public float detectionRange = 10f;
    
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float attackcooldown;
    private float cooldown=Mathf.Infinity;
    [SerializeField] private BoxCollider2D boxCollider;

    private bool playerDetected = false;

    public float moveSpeed = 3.0f;
    private Vector3 initScale;
    private Transform currentTarget;
    private bool moveleft;
    private void Awake()
    {
        initScale = enemyA.localScale;
    }

   
    private void Update()
    {
        if (moveleft)
        {
            if (enemyA.position.x <= pointA.position.x)
            {
                Movedirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemyA.position.x >= pointB.position.x)
            {
                Movedirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
       if (PlayerInSight()&&cooldown>=attackcooldown)
        {
            Attack();
        }
        cooldown += Time.deltaTime;
    }

    private void Attack()
    {
        cooldown = 0;
        firebox[fireball()].transform.position = firepoint.position;
        firebox[fireball()].GetComponent<projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private void DirectionChange()
    {
        moveleft = !moveleft;
    }
    private void Movedirection(int _direction)
    {
        enemyA.localScale = new Vector3(Mathf.Abs(initScale.x)*_direction,initScale.y,initScale.z);
        enemyA.position = new Vector3(enemyA.position.x + Time.deltaTime * _direction * moveSpeed, enemyA.position.y, enemyA.position.z);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private int fireball()
    {

        for (int i = 0; i < firebox.Length; i++)
        {
            if (!firebox[i].activeInHierarchy)
                return i;
        }
        return 0;

    }

}


