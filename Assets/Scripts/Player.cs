using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("坦克自身设置")]
    [Header("移动速度")]
    public float speed = 2;
    [Header("坦克皮肤")]
    public Sprite[] tankSprites = new Sprite[4];
    [Space()]
    [Header("子弹设置")]
    public GameObject bulletPrefabs;
    [Header("子弹冷却时间")]
    public float attackFreeTime = 1;

    private SpriteRenderer sr;
    private Rigidbody2D rigid;
    private float lastAttackTime;
    private int tankDirection = 0;
    private Vector3 bulletEuler;
    // Start is called before the first frame update

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        this.lastAttackTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Attack();
    }

    public void Attack()
    {
        lastAttackTime -= Time.fixedDeltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lastAttackTime <= 0)
            {
                lastAttackTime = attackFreeTime;

                GameObject bullet = Instantiate(this.bulletPrefabs, transform.position, Quaternion.Euler(this.bulletEuler));
            }
        }
    }

    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0)
        {
            rigid.MovePosition(new Vector2(rigid.position.x + h * speed * Time.fixedDeltaTime, rigid.position.y));
            // transform.Translate(Vector2.right * h * speed * Time.deltaTime, Space.World);
            if (h > 0)
            {
                bulletEuler = new Vector3(0, 0, -90);
                tankDirection = 1;
                sr.sprite = tankSprites[1];
            }
            else
            {
                bulletEuler = new Vector3(0, 0, 90);
                tankDirection = 3;
                sr.sprite = tankSprites[3];
            }

        }
        else if (v != 0)
        {
            rigid.MovePosition(new Vector2(rigid.position.x, rigid.position.y + v * speed * Time.fixedDeltaTime));
            // transform.Translate(Vector2.up * v * speed * Time.deltaTime, Space.World);
            if (v > 0)
            {
                bulletEuler = Vector3.zero;
                tankDirection = 0;
                sr.sprite = tankSprites[0];
            }
            else
            {
                bulletEuler = new Vector3(0, 0, -180);
                tankDirection = 2;
                sr.sprite = tankSprites[2];
            }
        }
    }
}
