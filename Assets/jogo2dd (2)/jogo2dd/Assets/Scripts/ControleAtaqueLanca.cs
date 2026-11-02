
using UnityEngine;

public class ControleAtaqueLanca : MonoBehaviour
{
    public LancaDano lanca;

    // CHAMADO NO INÍCIO DO GOLPE
    public void ativarLanca()
    {
        lanca.iniciarAtaque();
    }

    // CHAMADO NO FINAL DO GOLPE
    public void desativarLanca()
    {
        lanca.finalizarAtaque();
    }
}
