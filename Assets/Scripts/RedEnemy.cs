using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    public List<Sprite> TankList;
    private SpriteRenderer Rsr;
    // Start is called before the first frame update
    void Start()
    {
        Initproperty();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        base.Die();

        MapCreation.Instance.CreateBonus();
    }


    private void Initproperty()
    {
        base.changeDirTime = 4f;
        Rsr = GetComponent<SpriteRenderer>();
        int RandomNum = 0;
        if (MapCreation.Instance.GameTime < 200)
        {
            RandomNum = Random.Range(0, 2);
        }
        else
        {
            RandomNum = Random.Range(0, 3);
        }
        
        Rsr.sprite = TankList[RandomNum];

        if(RandomNum == 2)
        {
            base.EnemyHP = 3;
            base.Score = 3;
        }

    }
}
