using System.Linq;
using UnityEngine;

public class CollidersTecido : MonoBehaviour
{
    public GameObject cloth; // referência ao objeto Cloth
    private SkinnedMeshRenderer meshRenderer; // referência ao MeshFilter do Cloth
    public GameObject collider; // array com os transform dos colisores (filhos)
    private Vector3[] vertices;
    private Transform[] colliders;

    private void Awake()
    {
        // Obtém o MeshFilter do objeto Cloth
        meshRenderer = cloth.GetComponent<SkinnedMeshRenderer>();
        colliders = collider.GetComponentsInChildren<Transform>().Where(t => t != collider.transform).ToArray(); // Encontra todos os filhos do objeto collider
    }

    void Update()
    {
        Mesh bakedMesh = new Mesh();
        meshRenderer.BakeMesh(bakedMesh); // Obtém o Mesh atualizado
        vertices = bakedMesh.vertices;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (i < vertices.Length)
            {
                Vector3 pos = new Vector3(colliders[i].position.x, vertices[i].y, colliders[i].position.z);

                // Atualiza a posição do colisor para o vértice correspondente
                colliders[i].position = pos;
            }
        }
    }

}
