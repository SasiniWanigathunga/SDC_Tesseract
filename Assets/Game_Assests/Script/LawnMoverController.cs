using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class LawnMowerController : MonoBehaviour
{
    public GameObject bullet;
    public List<GameObject> zombies;
    public GameObject toAttack;
    public float attackCoolDown;
    private float attackTime;
    public int damageValue;
    public bool isAttacking;
    public int Health;

    public bool collided = false;

    private void Update()
    {
        if (zombies.Count > 0 && collided)
        {            //to attack last zombie in the list
            toAttack = zombies[0];            
        }
        else
        {
            toAttack = null;
        }
        if (toAttack != null)
        {
            if (Time.time - GlobalVariable.instance.elapsedTime >= attackTime )
            {
                GameObject bulletInstance = Instantiate(bullet, transform);
                bulletInstance.GetComponent<LawnMoverBullet>().damageValue = damageValue;               
                this.gameObject.GetComponent<Image>().enabled = false;
                StartCoroutine(WaitAndDestroy(bulletInstance, 2f));                
                attackTime = Time.time - GlobalVariable.instance.elapsedTime + attackCoolDown;
                                              
            }    
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
        
        
    }

    public void ReceiveDamage(int Damage)
    {
        
        if (Health - Damage <= 0)
        {            
            Destroy(this.gameObject);
        }        
    }
    private IEnumerator WaitAndDestroy(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}

