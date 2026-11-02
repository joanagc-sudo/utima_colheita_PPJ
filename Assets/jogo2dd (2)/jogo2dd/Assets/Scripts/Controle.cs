
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controle : MonoBehaviour
{
    [Header("Tiro")]
    public GameObject balaprojetil;
    public Transform arma;
    public float forcaDoTiro = 15f;

    [Header("Movimento")]
    public int velocidade = 10;
    public int forcaDoPulo = 250;

    [Header("Chão")]
    public Transform terra;
    public LayerMask chao;
    public float raioChecagemChao = 0.2f;

    private float MoveX;
    private bool noChao;
    private bool direita = true;
    private bool tiro;
    private bool puloAnterior;
    private bool tiroAnterior;

    public int coins;
    private Pontos gcPlayer;

    private Animator animator;
    private Rigidbody2D rb;

    // START
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("Falta Animator no personagem!");

        if (rb == null)
            Debug.LogError("Falta Rigidbody2D no personagem!");

        if (terra == null)
            Debug.LogError("Arraste o objeto Terra no Inspector!");

        gcPlayer = Pontos.gc;

        if (gcPlayer == null)
        {
            Debug.LogError("Objeto Pontos não encontrado na cena!");
            return;
        }

        if (gcPlayer.coinsText == null)
        {
            Debug.LogError("O campo Coins Text está vazio no Inspector!");
            return;
        }

        gcPlayer.coins = 0;
        gcPlayer.coinsText.text = "0";

        if (rb != null)
            rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
    }

    // UPDATE
    void Update()
    {
        moveJogador();
        viraJogador();

        // ATAQUE
        if (Input.GetKeyDown(KeyCode.LeftShift) ||
    (ControleMobile.tiro && !tiroAnterior))
        {
            tiro = true;
            ataca();
            atirar();
        }

        tiroAnterior = ControleMobile.tiro;
    }

    // MOVIMENTO DO JOGADOR
    void moveJogador()
    {
        noChao = Physics2D.OverlapCircle(
            terra.position,
            raioChecagemChao,
            chao
        );

        MoveX = Input.GetAxisRaw("Horizontal");

        if (ControleMobile.esquerda)
            MoveX = -1;

        if (ControleMobile.direita)
            MoveX = 1;

        // PULO
        if ((Input.GetButtonDown("Jump") ||
     (ControleMobile.pulo && !puloAnterior)) && noChao)
        {
            pula();
        }

        puloAnterior = ControleMobile.pulo;

        // FÍSICA
        rb.velocity = new Vector2(
            MoveX * velocidade,
            rb.velocity.y
        );

        // IGNORAR COLISÃO AO SUBIR
        int layerChao = LayerMask.NameToLayer("Chao");

        if (layerChao != -1)
        {
            Physics2D.IgnoreLayerCollision(
                this.gameObject.layer,
                layerChao,
                (rb.velocity.y > 0.0f)
            );
        }

        // ANIMAÇÕES
        animator.SetBool("NoChao", noChao);

        if (MoveX != 0)
        {
            animator.SetBool("Correndo", true);
        }
        else
        {
            animator.SetBool("Correndo", false);
        }
    }

    // TIRO
    private void atirar()
    {
        if (tiro)
        {
            GameObject temp = Instantiate(balaprojetil);

            temp.transform.position = arma.position;

            float direcao = direita ? 1f : -1f;

            temp.GetComponent<Rigidbody2D>().velocity =
                new Vector2(forcaDoTiro * direcao, 0);

            Vector3 escala = temp.transform.localScale;
            escala.x *= direcao;
            temp.transform.localScale = escala;

            Destroy(temp.gameObject, 1);

            tiro = false;
        }
    }

    // ATAQUE / ANIMAÇÃO
    void ataca()
    {
        animator.SetTrigger("Ataque");
    }

    // PULO
    void pula()
    {
        rb.AddForce(Vector2.up * forcaDoPulo);
    }

    // VIRAR PERSONAGEM
    void viraJogador()
    {
        if (MoveX > 0)
        {
            direita = true;
        }
        else if (MoveX < 0)
        {
            direita = false;
        }

        Vector2 escala = transform.localScale;

        if ((escala.x > 0 && !direita) ||
            (escala.x < 0 && direita))
        {
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    // PLATAFORMA MÓVEL
    void OnCollisionEnter2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("PlataformaMovel"))
        {
            // Não altera mais o parent da Gina.
            // Evita herdar a rotação da plataforma.
        }
    }

    void OnCollisionExit2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("PlataformaMovel"))
        {
            // Não precisa remover parent.
            // A Gina permanece independente da plataforma.
        }
    }

    // DESENHA ESFERA NOS PÉS
    void OnDrawGizmosSelected()
    {
        if (terra != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                terra.position,
                raioChecagemChao
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coins"))
        {
            if (gcPlayer == null || gcPlayer.coinsText == null)
            {
                Debug.LogError("Referência dos pontos ou do texto não configurada!");
                return;
            }

            gcPlayer.coins++;
            gcPlayer.coinsText.text = gcPlayer.coins.ToString();

            Destroy(collision.gameObject);
        }
    }
}
