using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowPath : MonoBehaviour
{
    //Declarando as variáveis dos pontos, da velocidade da movimentação, o contorno ao atingir um ponto e a velocidade de rotação do objeto
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;

    //Declarando as variáveis de game object, um array os waypoints, o currentNode, o currentWp referente ao ponto de início e o Graph
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    
   

    // Start is called before the first frame update
    void Start()
    {
        //Colocação dos componentes
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }

    //Local do mapa Heliporto
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    //Local do mapa Ruínas
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[5]);
        currentWP = 0;
    }

    //Calcular a movimentação por onde deseja
    private void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        //O nó que estará mais próximo neste momento
        currentNode = g.getPathPoint(currentWP);

        //se estivermos mais próximo bastante do nó o tanque se moverá para o próximo
        if(Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position)< accuracy)
        {
            currentWP++;
        }

        //Quando o currentWp encontrar o getLenght ele vai ter um novo ponto para ter uma movimentação diferenciada
        if(currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        }

        this.transform.Translate(0, 0, speed * Time.deltaTime);

        
    }
}
