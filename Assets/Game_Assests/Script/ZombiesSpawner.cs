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
    

    private void Update()
    {
        Debug.Log(Time.time);
        foreach (Zombie zombie in zombies )
        {
            if (zombie.isSpawned == false && zombie.spawnTime <= Time.time)
            {
                GameObject zombieInstance = Instantiate(zombiesPrefabs[(int)zombie.zombieType], transform.GetChild(zombie.Spawner).transform);
                zombie.isSpawned = true;
                //zombieInstance.GetComponent<ZombieController>().FinalDestination = transform.GetChild(zombie.Spawner).GetComponent<SpawnPoint>().Destination;
            }
        }
    }
}
