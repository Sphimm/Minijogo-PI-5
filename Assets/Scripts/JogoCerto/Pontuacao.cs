using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Pontuacao : MonoBehaviour
{
    public bool isColliding = false;

    private GameManager gameManager;

    private Renderer objectRenderer;
    NavMeshAgent agent;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        objectRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
            gameManager.pontuacao++;
            objectRenderer.enabled = false; // Desativa o objeto quando o jogador colide
            agent.SetDestination(gameManager.Move());
            Debug.Log("Pontuação: " + gameManager.pontuacao);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    private void Update()
    {
        if (isColliding == false && gameObject.transform.position == Vector3.zero)
        {
            objectRenderer.enabled = true; // Reativa o objeto quando o jogador sai da colisão
        }
    }
}
