using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    private GameController gc;

    float direction;

    public float speed;
    public float powerJump;
    public float health;
    public bool vulnerable;

    bool isJumping;
    float timeBlink = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // retorna a direcao no eixo x com o valor entre -1 (esquerda) e 1 (direita)
        direction = Input.GetAxis("Horizontal");

        if (direction > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }


        if(direction < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }


        // se estiver se movendo e NAO estiver pulando, animacao run
        if(direction != 0 && isJumping == false)
        {
            anim.SetInteger("transition", 1);
        }

        // se NAO estiver se movendo e NAO estiver pulando, animcao idle
        if(direction == 0 && isJumping == false)
        {
            anim.SetInteger("transition", 0);
        }

        Jump();
    }

    public void FixedUpdate()
    {
        rig.velocity = new Vector2(direction * speed, rig.velocity.y);
    }

    void Jump()
    {
        if (isJumping == true)
        {
            isJumping = false;
        }
        // se tiver apertado o espaco e NAO estiver pulando, animacao jump
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            rig.AddForce(Vector2.up * powerJump, ForceMode2D.Impulse);
            anim.SetInteger("transition", 2);
            isJumping = true;
        }
    }

    public void generateDamage()
    {
        if (vulnerable == false)
        {
            gc.LoseHealth(health);
            health--;
            vulnerable = true;
            StartCoroutine(Respawn());
        }
    }


    IEnumerator Respawn()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(timeBlink);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(timeBlink);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(timeBlink);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(timeBlink);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(timeBlink);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(timeBlink);
 
        vulnerable = false;
    }

    // essa funcao é chamado pela unity qdo o objeto toca em outro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }
}
