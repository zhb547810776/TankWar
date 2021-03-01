using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    // 0 home 1 wall 2 barrier 3 born 4 river 5 grass 6 airWall 7 Bonus
    public GameObject[] items;

    private List<GameObject> AroundHomeWall = new List<GameObject>();
    public List<Vector3> itemPositonList = new List<Vector3>();
    public float GameTime;

    private static MapCreation instance;

    public static MapCreation Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        InitMap();
        instance = this;
        GameTime = 0;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }


    private void InitMap()
    {
        CreateHome();

        CreateAirWall();

        CreateWall(items[1]);

        CreatePlayerBorn();

        CreatePlayerTwoBorn();

        CreateEnemyBorn();

        //每过五秒生产一个敌人
        InvokeRepeating("CreateEnemy", 4, 5);

        CreateMap();


    }

    private GameObject CreateItems(GameObject CreateGameObject, Vector3 CreatePosition, Quaternion CreateRotation)
    {
        GameObject item = Instantiate(CreateGameObject, CreatePosition, CreateRotation);
        item.transform.SetParent(gameObject.transform);

        itemPositonList.Add(CreatePosition);

        return item;
    }

    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            Vector3 creationPositon = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8));
            if (!HasThePosition(creationPositon))
            {
                return creationPositon;
            }
        }
    }

    private bool HasThePosition(Vector3 creationPosition)
    {
        for (int i = 0; i < itemPositonList.Count; i++)
        {
            if (creationPosition == itemPositonList[i])
            {
                return true;
            }
        }
        return false;
    }

    //生成敌人
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        if (num == 0)
        {
            CreateItems(items[3], new Vector3(-10, 8, 0), Quaternion.identity);
        }
        else if (num == 1)
        {
            CreateItems(items[3], new Vector3(0, 8, 0), Quaternion.identity);
        }
        else if (num == 2)
        {
            CreateItems(items[3], new Vector3(10, 8, 0), Quaternion.identity);
        }
    }

    //生成0号元素
    private void CreateHome()
    {

        CreateItems(items[0], new Vector3(0, -8, 0), Quaternion.identity);


    }

    //生成四面的空气墙
    private void CreateAirWall()
    {
        for (int i = -11; i < 12; i++)
        {
            CreateItems(items[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItems(items[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItems(items[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItems(items[6], new Vector3(11, i, 0), Quaternion.identity);
        }
    }

    //生成wall并且加入List
    private void CreateWall(GameObject go)
    {
        GameObject item = CreateItems(go, new Vector3(-1, -8, 0), Quaternion.identity);
        AroundHomeWall.Add(item);
        item =  CreateItems(go, new Vector3(1, -8, 0), Quaternion.identity);
        AroundHomeWall.Add(item);
        for (int i = -1; i < 2; i++)
        {
            item = CreateItems(go, new Vector3(i, -7, 0), Quaternion.identity);
            AroundHomeWall.Add(item);
        }
    }

    //出生点
    private void CreatePlayerBorn()
    {
        GameObject Born = Instantiate(items[3], new Vector3(-2, -8, 0), Quaternion.identity);
        Born.GetComponent<Born>().isCreatePlayerOne = true;
    }

    //双人游戏   实例化玩家二
    private void CreatePlayerTwoBorn()
    {
        if (PlayerPrefs.GetInt("choice") == 2)
        {
            GameObject BornTwo = Instantiate(items[3], new Vector3(2, -8, 0), Quaternion.identity);
            BornTwo.GetComponent<Born>().isCreatePlayerTwo = true;
        }
    }

    //初始三个的敌人出生点
    private void CreateEnemyBorn()
    {

        CreateItems(items[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItems(items[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItems(items[3], new Vector3(10, 8, 0), Quaternion.identity);

    }

    //实例化地图
    private void CreateMap()
    {
        for (int i = 0; i < 20; i++)
        {
            CreateItems(items[2], CreateRandomPosition(), Quaternion.identity);
            CreateItems(items[4], CreateRandomPosition(), Quaternion.identity);
            CreateItems(items[5], CreateRandomPosition(), Quaternion.identity);
        }

        for (int i = 0; i < 60; i++)
        {
            CreateItems(items[1], CreateRandomPosition(), Quaternion.identity);
        }
    }

    //加固10s奖励
    public void reinforce()
    {
        foreach(GameObject wall in AroundHomeWall)
        {
            if (wall)
            {
                Destroy(wall);
            }

            AroundHomeWall = new List<GameObject>();
        }

        CreateWall(items[2]);
        Invoke("CreateInitWall", 10);
    }

    //创建初始化的墙
    private void CreateInitWall()
    {
        foreach (GameObject wall in AroundHomeWall)
        {
            Destroy(wall);
            AroundHomeWall = new List<GameObject>();
        }

        GameObject item = CreateItems(items[1], new Vector3(-1, -8, 0), Quaternion.identity);
        AroundHomeWall.Add(item);
        item = CreateItems(items[1], new Vector3(1, -8, 0), Quaternion.identity);
        AroundHomeWall.Add(item);
        for (int i = -1; i < 2; i++)
        {
            item = CreateItems(items[1], new Vector3(i, -7, 0), Quaternion.identity);
            AroundHomeWall.Add(item);
        }
    }

    //创建一个奖励
    public void CreateBonus()
    {
        CreateItems(items[7], CreateRandomPosition(), Quaternion.identity);
    }

}
