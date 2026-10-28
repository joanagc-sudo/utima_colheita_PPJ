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
    public float raioChecagemChao = 0.2f; // Controla o tamanho da área de deteção do chão

    private float MoveX;
    private bool noChao;
    private bool direita = true;
    private bool tiro;

    private Animator animator;
    private Rigidbody2D rb;

    // START
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // UPDATE
    void Update()
    {
        moveJogador();
        viraJogador();

        // ATAQUE
        if (Input.GetButtonDown("Fire1"))
        {
            tiro = true;

            ataca();
            atirar();
        }
    }

    // MOVIMENTO DO JOGADOR
    void moveJogador()
    {
        // VERIFICA SE ESTÁ NO CHÃO USANDO UM CÍRCULO NOS PÉS (Mais seguro que Linecast)
        noChao = Physics2D.OverlapCircle(terra.position, raioChecagemChao, chao);

        // MOVIMENTO HORIZONTAL
        MoveX = Input.GetAxis("Horizontal");

        // PULO
        if (Input.GetButtonDown("Jump") && noChao)
        {
            pula();
        }

        // FÍSICA
        rb.velocity = new Vector2(MoveX * velocidade, rb.velocity.y);

        // IGNORAR COLISÃO AO SUBIR (Com proteção para evitar erro de Layer inválida)
        int layerChao = LayerMask.NameToLayer("Chao"); // Altere para "Chão" com acento se essa for a grafia no seu Unity

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

            // POSIÇÃO DA ARMA
            temp.transform.position = arma.position;

            // DIREÇÃO DO TIRO
            float direcao = direita ? 1f : -1f;

            // VELOCIDADE DA BALA
            temp.GetComponent<Rigidbody2D>().velocity =
                new Vector2(forcaDoTiro * direcao, 0);

            // VIRA A BALA
            Vector3 escala = temp.transform.localScale;
            escala.x *= direcao;
            temp.transform.localScale = escala;

            // DESTROI A BALA
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

        if ((escala.x > 0 && !direita) || (escala.x < 0 && direita))
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
            transform.parent = outro.transform;
        }
    }

    void OnCollisionExit2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("PlataformaMovel"))
        {
            transform.parent = null;
        }
    }

    // Desenha uma esfera vermelha nos pés na aba Scene para facilitar a configuração
    void OnDrawGizmosSelected()
    {
        if (terra != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(terra.position, raioChecagemChao);
        }
    }
}