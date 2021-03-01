using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float playerSpeed = 3;
    public int EnemyHP = 1;
    public int Score = 1;

    private float shootCD;
    public float changeDirTime;
    private float v;
    private float h;

    public bool isStopBonus;
    private float stopTime;

    private SpriteRenderer sr;
    public GameObject bullet;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        shootCD = 2f;
        changeDirTime = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopBonus && stopTime < 5)
        {
            stopTime += Time.deltaTime;
        }
        else
        {
            stopTime = 0;
            isStopBonus = false;
        }
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat || isStopBonus)
        {
            return;
        }
        Move();
        if (shootCD >= 3f)
        {
            Shoot();
        }

        shootCD += Time.fixedDeltaTime;

    }

    private void Shoot()
    {
            Instantiate(bullet, transform.position, transform.rotation);
            shootCD = 0;
    }

    private void Move()
    {
        if(changeDirTime >= 4)
        {
            int num = Random.Range(0, 8);

            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num<= 2)
            {
                v = 0;
                h = 1;
            }else if(num>2 && num <= 4)
            {
                v = 0;
                h = -1;
            }

            changeDirTime = 0;
        }
        else
        {
            changeDirTime += Time.fixedDeltaTime;
        }

        if (h < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.Translate(new Vector2(0, -h) * playerSpeed * Time.fixedDeltaTime);
        }
        else if (h > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            transform.Translate(new Vector2(0, h) * playerSpeed * Time.fixedDeltaTime);
        }

        if (v > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(new Vector2(0, v) * playerSpeed * Time.fixedDeltaTime);
        }
        else if (v < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            transform.Translate(new Vector2(0, -v) * playerSpeed * Time.fixedDeltaTime);
        }
    }

    public virtual void GetDamage()
    {
        EnemyHP--;

        if(EnemyHP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        PlayerManager.Instance.PlayerScore += Score;

        EnemiesManager.Instance.enemies.Remove(gameObject);

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            changeDirTime = 4;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "FeWall" || collision.transform.tag == "Wall" || collision.transform.tag == "River")
        {
            changeDirTime = 3;
        }
    }
}
