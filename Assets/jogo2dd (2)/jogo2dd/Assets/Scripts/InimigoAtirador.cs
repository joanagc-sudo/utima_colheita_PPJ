using UnityEngine;

public class InimigoAtirador : MonoBehaviour
{
    public GameObject bala;
    public Transform pontoDeTiro;
    public Transform player;

    public float distanciaDetectar = 6f;
    public float forcaDoTiro = 8f;
    public float tempoEntreTiros = 1f;

    private float proximoTiro;
    private bool jaAtirou;

    void Update()
    {
        if (player == null)
            return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= distanciaDetectar)
        {
            Atirar();
        }
    }

    void Atirar()
    {
        if (Time.time < proximoTiro)
            return;

        proximoTiro = Time.time + tempoEntreTiros;

        GameObject temp = Instantiate(bala);
        temp.transform.position = pontoDeTiro.position;

        Vector2 direcao = (player.position - pontoDeTiro.position).normalized;

        temp.GetComponent<Rigidbody2D>().velocity = direcao * forcaDoTiro;
    }
}