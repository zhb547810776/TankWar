using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteEnemy : Enemy
{
    public List<Sprite> TankList;
    private SpriteRenderer Wsr;
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
        Wsr = GetComponent<SpriteRenderer>();
        int RandomNum = Random.Range(0, 2);
        Wsr.sprite = TankList[RandomNum];

    }
}
