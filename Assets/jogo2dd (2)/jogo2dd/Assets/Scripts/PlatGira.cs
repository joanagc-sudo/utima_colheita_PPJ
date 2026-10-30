
using UnityEngine;

public class PlatGira : MonoBehaviour
{
    public float velocidade = 50f;
    public float espera = 1f;
    public float angulo = 90f;

    bool aberta = false;
    float tempo = 0f;

    void Update()
    {
        tempo += Time.deltaTime;

        if (tempo >= espera)
        {
            aberta = !aberta;
            tempo = 0f;
        }

        float destino = aberta ? angulo : 0f;

        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            Quaternion.Euler(0, 0, destino),
            velocidade * Time.deltaTime
        );
    }
}
