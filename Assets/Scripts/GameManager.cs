using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector3 m_Player1StartPos;
    [SerializeField] private Vector3 m_Player2StartPos;
    [SerializeField] private Vector3 m_PuckStartPos;

    [SerializeField] Rigidbody m_Player1;
    [SerializeField] Rigidbody m_Player2;
    [SerializeField] Rigidbody m_Puck;

    private int m_ScoreTeam1;
    private int m_ScoreTeam2;

    private void ResetGame()
    {
        m_Player1.velocity = Vector3.zero;
        m_Player2.velocity = Vector3.zero;
        m_Puck.velocity = Vector3.zero;

        m_Player1.MovePosition(m_Player1StartPos);
        m_Player2.MovePosition(m_Player2StartPos);
        m_Puck.MovePosition(m_PuckStartPos);
    }

    private void Start()
    {
        m_Player1StartPos = m_Player1.position;
        m_Player2StartPos = m_Player2.position;
        m_PuckStartPos = m_Puck.position;

        ResetGame();
    }

    /// <summary>
    /// Called when the bll enters a goal
    /// </summary>
    /// <param name="goal"></param>
    public void Score(int goal)
    {
        if (goal == 1)
        {
            m_ScoreTeam2++;
        }
        else if (goal == 2)
        {
            m_ScoreTeam1++;
        }
        else
        {
            Debug.LogError("Incorrect goal number: " + goal);
        }

        ResetGame();
    }
}