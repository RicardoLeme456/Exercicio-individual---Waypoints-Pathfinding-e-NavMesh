using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Guarda e distribui as informações para cada tipo de classe de uma pra outra
[System.Serializable]

//Construção dos links que vai marcar as conexóes de um ponto a outro
public struct Link
{
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    //Declarando as variáveis dos arrays do waypoint e do link do struct e o arquivo da pasta Graph sendo instanciada
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    // Start is called before the first frame update
    void Start()
    {
        //Condicional waypoint do tamanho dele referente ao meu próximo ponto para se locomover ( apenas esquema )
        if(waypoints.Length > 0)
        {
            foreach(GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            foreach(Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Traçar a linha dos waypoints
        graph.debugDraw();
    }
}
