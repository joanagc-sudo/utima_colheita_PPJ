using UnityEngine;

public class SeguirPlataforma : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("PlataformaMovel"))
        {
            transform.SetParent(colisao.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("PlataformaMovel"))
        {
            transform.SetParent(null);
        }
    }
}