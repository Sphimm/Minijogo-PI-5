using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pontuacao : MonoBehaviour
{
    public bool isColliding = false;

    private GameManager gameManager;

    private Renderer objectRenderer;
    private GameObject luz;
    NavMeshAgent agent;
    private Collider objectCollider;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        objectRenderer = GetComponent<Renderer>();
        luz = GetComponentInChildren<Light>().gameObject; // Obtém o objeto da luz
        objectCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
            objectRenderer.enabled = false; // Desativa o objeto quando o jogador colide
            objectCollider.enabled = false; // Desativa o collider para evitar múltiplas colisões
            luz.SetActive(false); // Desativa a luz
            agent.SetDestination(gameManager.Move());
            StartCoroutine(WaitAndEnable());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false; // Permite que o jogador continue se movendo
            objectRenderer.enabled = false; // Desativa o objeto enquanto o jogador está na colisão
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
        if (isColliding == false && agent.remainingDistance <= agent.stoppingDistance)
        {
            objectRenderer.enabled = true; // Reativa o objeto quando o jogador sai da colisão
            objectCollider.enabled = true; // Reativa o collider
            luz.SetActive(true); // Reativa a luz
            Debug.Log("Reativando objeto");
        }
    }

    private IEnumerator WaitAndEnable()
    {
        yield return new WaitForSeconds(1);
        gameManager.pontuacao += 1; // Adiciona 1 à pontuação
        gameManager.AtualizaPontos(); // Atualiza a pontuação na UI
        Debug.Log("Pontuação: " + gameManager.pontuacao);
    }
}
