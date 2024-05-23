using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ZombieController : MonoBehaviour
{
    public Vector3 FinalDestination;
    public int Health;
    public int damageValue;
    public float movemnetSpeed;
    private bool isStopped;
    public float damageCoolDown;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    

    // Update is called once per frame
    
    void Update()
    {
        if (!isStopped)
        {
            transform.Translate(new Vector3(movemnetSpeed * -1, 0, 0));
        }
        
        if (transform.position.x <= FinalDestination.x)
        {
            GlobalVariable.Instance.SetIsLost(true);
            isStopped = true;
            //transform.parent.GetComponent<SpawnPoint>().zombies.Remove(this.gameObject);
            //Destroy(this.gameObject);
            
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            StartCoroutine(Attack(collision));
            isStopped = true;
        }
    }

    IEnumerator Attack(Collider2D collision)
    {
        if (collision == null)
        {
            isStopped = false;
        }
        else
        {
            try
            {
                collision.gameObject.GetComponent<PlantController>().ReceiveDamage(damageValue);
            }
            catch (NullReferenceException)
            {
                Debug.Log("No Plant to Attack");                
            }
            yield return new WaitForSeconds(damageCoolDown);
            StartCoroutine(Attack(collision));

        }
        
    }

    public void ReceiveDamage(int Damage)
    {
        
        if (Health - Damage <= 0)
        {
            transform.parent.GetComponent<SpawnPoint>().zombies.Remove(this.gameObject);
            audioManager.PlayZombieDie(audioManager.zombieDie);
            Destroy(this.gameObject);
        }
        else
        {
            Health -= Damage;
        }
    }

    

}
