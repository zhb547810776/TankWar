using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject explosion;

    private static EnemiesManager instance;

    public static EnemiesManager Instance { get => instance; set => instance = value; }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AllEnemyStop()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().isStopBonus = true;
        }
    }

    public void AllEnemyDestroy()
    {

        for (int i = enemies.Count-1; i >= 0; i--)
        {
            PlayerManager.Instance.PlayerScore++;
            Instantiate(explosion, enemies[i].transform.position, enemies[i].transform.rotation);
            Destroy(enemies[i]);
            enemies.Remove(enemies[i]);
        }
        
    }
}
