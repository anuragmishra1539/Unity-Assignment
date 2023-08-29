using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float startHealth;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject gameover;
    public float crrHealth { get; private set; }
    private Animator animator;
    private Rigidbody2D body;
    private void Awake()
    {
        crrHealth = startHealth;
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    public void takeDamage(float _damage)
    {
        crrHealth = Mathf.Clamp(crrHealth-_damage,0, startHealth);
        if (crrHealth > 0)
        {
            //hurt animation not working 
        }
        else
        {
            animator.SetTrigger("die");
            gameObject.SetActive(false);
            if (gameObject.tag=="player")

            {
                gameover.SetActive(true);
            }
            else if (gameObject.tag == "Enemy") 

            {
                victory.SetActive(true);
            }

        }
        
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //        takeDamage(1.0f);
    //}
}

