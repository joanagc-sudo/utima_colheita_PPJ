using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject inimigo;
    public GameObject[] pontos;
    public Transform player;

    public float velocidade = 5f;
    public float espera = 2f;

    public float distanciaVisao = 6f;

    public LayerMask layerObstaculo;

    public bool loop = true;
    public bool atacando = false;

    private Transform inimigoTransform;
    private SpriteRenderer sr;

    int i = 0;
    float proxTempo;
    bool seMovendo = true;

    Animator animator;
    Saude saude;

    void Start()
    {
        inimigoTransform = inimigo.transform;

        sr = inimigo.GetComponent<SpriteRenderer>();
        animator = inimigo.GetComponent<Animator>();
        saude = inimigo.GetComponent<Saude>();
    }

    void Update()
    {
        if (saude == null || saude.morto)
            return;

        if (Time.time >= proxTempo)
            seMovendo = true;

        if (!atacando)
        {
            Movimenta();

            if (PodeVerPlayer())
            {
                Ataca();
            }
        }
    }

    void Movimenta()
    {
        if (pontos.Length == 0 || !seMovendo)
            return;

        Vector3 alvo = pontos[i].transform.position;

        // virar
        sr.flipX = alvo.x < inimigoTransform.position.x;

        inimigoTransform.position = Vector3.MoveTowards(
            inimigoTransform.position,
            alvo,
            velocidade * Time.deltaTime
        );

        if (animator != null)
            animator.SetBool("Andando", true);

        if (Vector3.Distance(inimigoTransform.position, alvo) <= 0.5f)
        {
            i++;
            proxTempo = Time.time + espera;
            seMovendo = false;

            if (animator != null)
                animator.SetBool("Andando", false);

            if (i >= pontos.Length)
            {
                if (loop) i = 0;
                else seMovendo = false;
            }
        }
    }

    // =========================
    // 👁️ SISTEMA DE VISÃO
    // =========================
    bool PodeVerPlayer()
    {
        if (player == null)
            return false;

        float distancia = Vector2.Distance(inimigoTransform.position, player.position);

        if (distancia > distanciaVisao)
            return false;

        Vector2 direcao = (player.position - inimigoTransform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            inimigoTransform.position,
            direcao,
            distanciaVisao,
            layerObstaculo | LayerMask.GetMask("Player")
        );

        if (hit.collider != null)
        {
            return hit.collider.CompareTag("Player");
        }

        return false;
    }

    // =========================
    // ATAQUE
    // =========================
    public void Ataca()
    {
        if (atacando) return;

        atacando = true;

        if (animator != null)
            animator.SetTrigger("Ataque");

        Invoke(nameof(PararAtaque), 1f);
    }

    void PararAtaque()
    {
        atacando = false;
    }
}