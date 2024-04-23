using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public GameObject bullet;
    public List<GameObject> zombies;
    public GameObject toAttack;
    public float attackCoolDown;
    private float attackTime;
    public int damageValue;
    public bool isAttacking;

    private void Update()
    {
        if (zombies.Count > 0)
        {
            float distance =3000;
            foreach(GameObject zombie in zombies)
            {
                float zombieDistance = Vector3.Distance(transform.position, zombie.transform.position);
                if (zombieDistance < distance)
                {
                    distance = zombieDistance;
                    toAttack = zombie;
                }
            }
        }
        else
        {
            toAttack = null;
        }
        if (toAttack != null)
        {
            if (Time.time >= attackTime )
            {
                GameObject bulletInstance = Instantiate(bullet, transform);
                bulletInstance.GetComponent<Bullet>().damageValue = damageValue;
                attackTime = Time.time + attackCoolDown;
            }    
        }
    }
}
