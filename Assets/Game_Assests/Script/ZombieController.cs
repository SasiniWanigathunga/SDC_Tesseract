using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Vector3 FinalDestination;
    public int Health;
    public int Damage;
    public float movemnetSpeed;
    private bool isStopped;

    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
        {
            transform.Translate(new Vector3(movemnetSpeed * -1, 0, 0));
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isStopped = true;
        }
    }

    public void ReceiveDamage(int Damage)
    {
        
        if (Health - Damage <= 0)
        {
            transform.parent.GetComponent<SpawnPoint>().zombies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Health -= Damage;
        }
    }
}
