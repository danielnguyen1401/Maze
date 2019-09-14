using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public LayerMask HitLayers;
    public Pathfinding Pathfinding;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, 100, HitLayers))
            {
                transform.position = hit.point;
                Pathfinding.Move();
            }
        }
    }
}