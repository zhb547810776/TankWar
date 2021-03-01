using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;

    public GameObject[] EnemyList;

    private int MaxRange;

    public bool isCreatePlayerOne;
    public bool isCreatePlayerTwo;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1.2f);
        Destroy(gameObject, 1.2f);
        MaxRange = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (MapCreation.Instance.GameTime <= 60)
        {
            MaxRange = 4;
        }
        else if (MapCreation.Instance.GameTime <= 120)
        {
            MaxRange = 6;
        }
        else if (MapCreation.Instance.GameTime <= 200)
        {
            MaxRange = 7;
        }
        else if (MapCreation.Instance.GameTime <= 300)
        {
            MaxRange = 8;
        }
        else if (MapCreation.Instance.GameTime <= 500)
        {
            MaxRange = 10;
        }
        else
        {
            MaxRange = 12;
        }
    }

    private void BornTank()
    {
        if (isCreatePlayerOne)
        {
            Instantiate(playerOne, transform.position, Quaternion.identity);
        }else if (isCreatePlayerTwo)
        {
            Instantiate(playerTwo, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, MaxRange);
            GameObject enemy = null ;
            if (num == 0)
            {
                enemy = Instantiate(EnemyList[0], transform.position, Quaternion.identity);
            }
            else if (num < 6)
            {
                enemy = Instantiate(EnemyList[1], transform.position, Quaternion.identity);
            }
            else
            {
                enemy = Instantiate(EnemyList[2], transform.position, Quaternion.identity);
            }
            
            EnemiesManager.Instance.enemies.Add(enemy);
        }
    }
}
