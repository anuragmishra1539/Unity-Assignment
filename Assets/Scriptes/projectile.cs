using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
   
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private float direction;
    private void Awake()
    {
        animator= GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        if (hit) return;
        float movespeed = speed*Time.deltaTime*direction;
        transform.Translate(movespeed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explode");
        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().takeDamage(1);
        else if (collision.tag == "player")
            collision.GetComponent<Health>().takeDamage(1);
    }
    public void setDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        float localscaleX= transform.localScale.x;
        if (Mathf.Sign(localscaleX) != _direction)
            localscaleX = -localscaleX;

        transform.localScale = new Vector3(localscaleX, transform.localScale.y, transform.localScale.z);

    }
    private void deavtivate()
    {
       gameObject.SetActive(false);
    }
}
