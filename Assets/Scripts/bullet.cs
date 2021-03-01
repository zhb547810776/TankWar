using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int BulletSpeed = 10;
    public bool isPlayerBullet;
    public AudioClip DestroyWall;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * BulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Alive":
                collision.SendMessage("DieThenGameOver");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    collision.SendMessage("GetDamage");
                    Destroy(gameObject);
                }
                break;
            case "FeWall":
                Destroy(gameObject);
                break;
            case "Wall":
                MapCreation.Instance.itemPositonList.Remove(collision.transform.position);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(DestroyWall, transform.position);
                break;
            default:
                break;
        }
    }
}
