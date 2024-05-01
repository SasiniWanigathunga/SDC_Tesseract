using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Zombie
{
    public int spawnTime;
    public ZombieType zombieType;
    public int Spawner;
    public bool RandomSpawn;
    public bool isSpawned;
}
public enum ZombieType
{
    Zombie_Basic,
    Zombie_Cone
}

public class ZombiesSpawner: MonoBehaviour
{
    public List<GameObject> zombiesPrefabs;
    public List<Zombie> zombies;    
    public ZombieCount zombieCounter; // Reference to ZombieCount script

    private void Start()
    {
        // Get reference to ZombieCount script
        zombieCounter = FindObjectOfType<ZombieCount>();
        
        
    }


    private void Update()
    {
        foreach (Zombie zombie in zombies )
        {
            if (zombie.isSpawned == false && zombie.spawnTime <= Time.time-GlobalVariable.Instance.elapsedTime)
            {
                if (zombie.RandomSpawn)
                {
                    zombie.Spawner = Random.Range(0, transform.childCount);
                }
                GameObject zombieInstance = Instantiate(zombiesPrefabs[(int)zombie.zombieType], transform.GetChild(zombie.Spawner).transform);
                transform.GetChild(zombie.Spawner).GetComponent<SpawnPoint>().zombies.Add(zombieInstance);
                zombie.isSpawned = true;

                zombieCounter.UpdateZombieCount(--zombieCounter.zombieCount);

                //zombieInstance.GetComponent<ZombieController>().FinalDestination = transform.GetChild(zombie.Spawner).GetComponent<SpawnPoint>().Destination;
            }
        }
    }
}
