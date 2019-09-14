using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Grid GridReference; //For referencing the grid class
    public Transform StartPosition; //Starting position to pathfind from
    public Transform TargetPosition; //Starting position to pathfind to
    private List<Node> _finalPath; //List to hold the path sequentially 

    public void Move()
    {
        FindPath();
        StartCoroutine(TranslateTargetCo());
    }

    private IEnumerator TranslateTargetCo()
    {
        if (GridReference.NodeArray != null)
        {
            foreach (Node n in GridReference.NodeArray)
            {
                if (_finalPath != null)
                {
                    if (_finalPath.Contains(n))
                    {
                        yield return new WaitForSeconds(1f);
                        StartPosition.position = n.VPosition;
                    }
                }
            }
        }
    }

    private void FindPath()
    {
        Node startNode = GridReference.NodeFromWorldPoint(StartPosition.position);
        Node targetNode = GridReference.NodeFromWorldPoint(TargetPosition.position);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0]; //Create a node and set it to the first item in the open list
            for (int i = 1; i < openList.Count; i++) //Loop through the open list starting from the second object
            {
                if (openList[i].FCost < currentNode.FCost ||
                    openList[i].FCost == currentNode.FCost && openList[i].CostToGoal < currentNode.CostToGoal
                ) //If the f cost of that object is less than or equal to the f cost of the current node
                {
                    currentNode = openList[i]; //Set the current node to that object
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
            }

            var list = GridReference.GetNeighboringNodes(currentNode);
            for (var i = 0; i < list.Count; i++)
            {
                Node neighborNode = list[i];
                if (!neighborNode.IsWall || closedList.Contains(neighborNode)
                ) //If the neighbor is a wall or has already been checked
                {
                    continue; //Skip it
                }

                int moveCost = currentNode.CostToNext + GetManhattenDistance(currentNode, neighborNode);

                if (moveCost < neighborNode.CostToNext || !openList.Contains(neighborNode)
                ) //If the f cost is greater than the g cost or it is not in the open list
                {
                    neighborNode.CostToNext = moveCost; //Set the g cost to the f cost
                    neighborNode.CostToGoal = GetManhattenDistance(neighborNode, targetNode); //Set the h cost
                    neighborNode.ParentNode = currentNode; //Set the parent of the node for retracing steps

                    if (!openList.Contains(neighborNode)) //If the neighbor is not in the openlist
                    {
                        openList.Add(neighborNode); //Add it to the list
                    }
                }
            }
        }
    }


    private void GetFinalPath(Node startingNode, Node endNode)
    {
        _finalPath = new List<Node>();
        Node currentNode = endNode; //Node to store the current node being checked

        while (currentNode != startingNode)
        {
            _finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        _finalPath.Reverse(); //Reverse the path to get the correct order
        GridReference.FinalPath = _finalPath; //Set the final path
    }

    static Vector3[] SortListV3OnX(List<Vector3> data)
    {
        if (data.Count < 2) return data.ToArray(); //Or throw error, or return null

        var min = data.OrderBy(x => x.x).First();
        var max = data.OrderByDescending(x => x.x).First();

        data.Remove(min);
        data.Remove(max);

        data.Insert(0, min);
        data.Add(max);
        return data.ToArray();
    }

    private int GetManhattenDistance(Node aNodeA, Node aNodeB)
    {
        int x = Mathf.Abs(aNodeA.GridX - aNodeB.GridX); //x1-x2
        int y = Mathf.Abs(aNodeA.GridY - aNodeB.GridY); //y1-y2
        return x + y; //Return the sum
    }
}