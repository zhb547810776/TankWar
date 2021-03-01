using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public Sprite[] spriteArray;    //0 加一条命    1 所有敌人停止所有行动    2 加固老家  3 消灭所有敌人    4 升级坦克  5 无敌一段时间（10s）
    private int RandomBonus;
    private float surviceTime;
    private float shineTime;
    private bool isNone;
    SpriteRenderer sr;
    public AudioClip getBonus;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        RandomBonus = Random.Range(0, 6);
        changeImg();
        surviceTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        imgDestroyShine();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank" && RandomBonus == 0)
        {
            AddHP(collision);
        }
        else if (collision.tag == "Tank" && RandomBonus == 1)
        {
            EnemiesManager.Instance.AllEnemyStop();
        }
        else if (collision.tag == "Tank" && RandomBonus == 2)
        {
            MapCreation.Instance.reinforce();
        }
        else if(collision.tag == "Tank" && RandomBonus == 3)
        {
            EnemiesManager.Instance.AllEnemyDestroy();
        }
        else if (collision.tag == "Tank" && RandomBonus == 4)
        {
            collision.SendMessage("LevelUP");
        }
        else if (collision.tag == "Tank" && RandomBonus == 5)
        {
            collision.SendMessage("ShieldBonus");
        }

        if(collision.tag == "Tank")
        {
            AudioSource.PlayClipAtPoint(getBonus, transform.position);
            DestroyPosAndSelf();
        }

    }

    //增加生命的奖励
    private void AddHP(Collider2D collider2D)
    {
        if (collider2D.name == "Player(Clone)")
        {
            PlayerManager.Instance.PlayerOneHP++;
        }
        else
        {
            PlayerManager.Instance.PlayerTwoHP++;
        }
    }
    
    //渲染奖励的图片
    private void changeImg()
    {
        sr.sprite = spriteArray[RandomBonus];
    }
    
    //奖励消失前的闪烁
    private void imgDestroyShine()
    {
        surviceTime += Time.deltaTime;

        if (surviceTime > 17)
        {
            if (shineTime > 0.25)
            {
                isNone = !isNone;
                shineTime = 0;
            }

            shineTime += Time.deltaTime;

            if (isNone)
            {
                sr.sprite = null;
            }
            else
            {
                sr.sprite = sr.sprite = spriteArray[RandomBonus];
            }

        }

        if (surviceTime > 20)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyPosAndSelf()
    {
        MapCreation.Instance.itemPositonList.Remove(transform.position);
        Destroy(gameObject);
    }

}
