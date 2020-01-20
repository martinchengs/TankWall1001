using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite[] bulletSprites = new Sprite[4];
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetUp(int dir)
    {
        if (dir >= 0 && dir < 4)
        {
            this.sr.sprite = bulletSprites[dir];
        }
        else
        {
            this.sr.sprite = bulletSprites[0];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
