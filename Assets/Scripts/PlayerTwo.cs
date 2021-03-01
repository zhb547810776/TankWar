using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MonoBehaviour
{
    public float playerSpeed = 3;

    private float shootCD;
    private float DefendTime;
    private bool isDefended;
    private float DefendBonusTime;
    private bool isDefendedBonus;
    private int level;  //坦克等级

    private SpriteRenderer sr;
    public GameObject bullet;
    public GameObject explosion;
    public GameObject defended;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        Initproperty();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }

        if (shootCD >= 0.5f)
        {
            Shoot();
        }
        else
        {
            shootCD += Time.deltaTime;
        }

        if (DefendTime > 3f && DefendBonusTime > 10f)
        {
            isDefended = false;
            isDefendedBonus = false;
            defended.SetActive(false);
        }
        else
        {
            DefendTime += Time.deltaTime;
            DefendBonusTime += Time.deltaTime;
        }
  
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }

        Move();

    }

    private void Initproperty()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        level = 1;
        shootCD = 0.5f;
        DefendTime = 0;
        DefendBonusTime = 10f;
        isDefended = true;
        defended.SetActive(true);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Instantiate(bullet, transform.position, transform.rotation);
            shootCD = 0;
        }
    }

    private void Move()
    {
        float h = 0;
        float v = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            v = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            v = -1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            h = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            h = 1;
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

    private void Die()
    {
        if (isDefended || isDefendedBonus)
        {
            return;
        }
        PlayerManager.Instance.PlayerTwoisDead = true;

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void ShieldBonus()
    {
        isDefendedBonus = true;
        defended.SetActive(true);
        DefendBonusTime = 0;
    }

    private void LevelUP()
    {
        level++;
        animator.SetInteger("level", level);
    }
}
