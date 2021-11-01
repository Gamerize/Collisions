using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    [SerializeField] private GameManager m_Manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.name == "Goal1")
        {
            m_Manager.Score(1);
        }
        else if(other.transform.root.name == "Goal2")
        {
            m_Manager.Score(2);
        }
    }
}
