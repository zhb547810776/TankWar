using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    public List<Sprite> TankList;
    private SpriteRenderer Bsr;
    // Start is called before the first frame update
    void Start()
    {
        Initproperty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initproperty()
    {
        base.changeDirTime = 4f;
        Bsr = GetComponent<SpriteRenderer>();
    }

    public override void GetDamage()
    {
        base.GetDamage();

        if(base.EnemyHP == 2)
        {
            Bsr.sprite = TankList[1];
        }
        else if (base.EnemyHP == 1)
        {
            Bsr.sprite = TankList[2];
        }
    }
}
