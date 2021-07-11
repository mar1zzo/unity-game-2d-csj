using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public Rigidbody2D rig;
    public BoxCollider2D box;
    public Transform headPoint;
    public Transform collPoint;

    [Header("Stats")]
    public float speed;
    public int health;

    [Header("Hit Settings")]
    public float headArea;
    public LayerMask playerLayer;
    public float throwPlayerForce;

    private bool isRight;
    private Vector2 direction;


    void Update()
    {
        if (isRight)
        {
            direction = Vector2.right;
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            direction = -Vector2.right;
            transform.eulerAngles = new Vector2(0, 0);
        }

        Destroy();

    }

    void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * speed * Time.deltaTime);

        Hit();
    }

    void Hit()
    {
        Collider2D hit = Physics2D.OverlapCircle(headPoint.position, headArea, playerLayer);
        Collider2D hitPlayer = Physics2D.OverlapCircle(collPoint.position, headArea, playerLayer);

        if (hit != null)
        {
            if (hit.GetComponent<Player>().vulnerable == false)
            {
                health--;
                anim.SetTrigger("hit");
                hit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * throwPlayerForce, ForceMode2D.Impulse);
            }
        }

        //é chamado quando escosta no player de frente
        if (hitPlayer != null)
        {
            hitPlayer.GetComponent<Player>().generateDamage();
        }
    }

    void Destroy()
    {
        if (health <= 0)
        {
            anim.SetTrigger("die");
            speed = 0f;
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checa se está colidindo com algum obstáculo
        if (collision.gameObject.layer == 9)
        {
            isRight = !isRight;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(headPoint.position, headArea);
        Gizmos.DrawSphere(collPoint.position, headArea);
    }

}

