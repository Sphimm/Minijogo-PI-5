using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public int pontuacao = 0;
    public TextMeshProUGUI textoPontuacao;
    public NavMeshSurface navMeshSurface;

    private void Start()
    {
        // Inicializa a pontuação
        pontuacao = 0;
        if (textoPontuacao == null)
        {
            Debug.LogError("Texto de pontuação não encontrado!");
        }

    }

    public Vector3 Move() 
    {
        // Exemplo de uso do método GetRandomNavMeshPosition
        Vector3 randomPosition = GetRandomNavMeshPosition(Vector3.zero, 30f);
        Debug.Log("Posição aleatória válida: " + randomPosition);
        return randomPosition; // Retorna a posição aleatória válida
    }

    public Vector3 GetRandomNavMeshPosition(Vector3 center, float range)
    {
        for (int i = 0; i <= 10; i++) // Tenta encontrar uma posição válida até 10 vezes
        {
            Vector3 randomPoint = center + new Vector3(
                Random.Range(-range, range),
                0,
                Random.Range(-range, range)
            );

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas))
            {
                return hit.position; // Retorna a posição válida encontrada
            }
        }

        Debug.LogWarning("Nenhuma posição válida encontrada no NavMesh.");
        return center; // Retorna o centro como fallback
    }

    public void AtualizaPontos()
    {
        // Atualiza o texto da pontuação na UI
        if (textoPontuacao != null)
        {
            textoPontuacao.text = "Pontuação: " + pontuacao;
        }
        else
        {
            Debug.LogError("Texto de pontuação não encontrado!");
        }
    }

}
