using Unity.Hierarchy;
using UnityEngine;

public class Desaceleracao : MonoBehaviour
{

    private Rigidbody rb;
    public float desaceleracao = 1f; // Taxa de desaceleração
    [SerializeField] private bool dentroDoTrigger = false;
    private float gravidade = 9.98f; // Aceleração da gravidade ajustada pela massa
    private float forcaAtual = 0f; // Força atual aplicada para desaceleração

    private void Start()
    {
        // Obtém o componente Rigidbody do objeto
        rb = GetComponent<Rigidbody>();
        gravidade *= rb.mass; // Ajusta a gravidade com base na massa do objeto
    }

    private void Update()
    {
        // Se o objeto estiver dentro do trigger, desacelera gradualmente
        if (dentroDoTrigger && rb != null)
        {
            // Aumenta gradualmente a força contrária à gravidade
            forcaAtual = gravidade + (desaceleracao * Time.deltaTime);

            // Aplica a força contrária à gravidade
            rb.AddForce(Vector3.up * forcaAtual);

            // Verifica se o objeto está praticamente parado
            if (rb.linearVelocity.magnitude < 0.1f)
            {
                rb.linearVelocity = Vector3.zero; // Garante que o objeto pare completamente
                Debug.Log("Objeto parado");
            }
            else
            {
                desaceleracao++; // Aumenta a taxa de desaceleração
                Debug.Log("Objeto desacelerando");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto entrou no trigger
        if (other.CompareTag("Tecido"))
        {
            dentroDoTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Verifica se o objeto ainda está dentro do trigger
        if (other.CompareTag("Tecido"))
        {
            dentroDoTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica se o objeto saiu do trigger
        if (other.CompareTag("Tecido"))
        {
            dentroDoTrigger = false;
        }
    }

}
