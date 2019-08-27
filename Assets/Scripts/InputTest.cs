using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;

public class InputTest : MonoBehaviour
{
    private bool _isBaked;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_isBaked)
            {
                _isBaked = true;
//                navMeshSurface.BuildNavMesh();
//                NavMeshAssetManager.instance.StartBakingSurfaces(targets);
            }
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            _isBaked = false;
        }
    }
}