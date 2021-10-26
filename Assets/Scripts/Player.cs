using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
#region variables
    /// <summary>
    /// The player number
    /// </summary>
    [SerializeField] private int m_PlayerNumber;
    /// <summary>
    /// The attached rigidbody
    /// </summary>
    private Rigidbody m_RB;
    /// <summary>
    /// Reference to the currently running slapshot coroutine
    /// </summary>
    private Coroutine m_cSlapshot;
    /// <summary>
    /// Speed of the player
    /// </summary>
    private const float m_Speed = 1f;
    /// <summary>
    /// Amount of time to activate slapshot 
    /// </summary>
    private const float m_SlapshotTime = 0.3f;
    /// <summary>
    /// Tracks the slapshot button
    /// </summary>
    private bool m_SlapshotButtonDown;
#endregion

    private void Awake()
    {
        //gets the attached rigidbody component
        m_RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //check if the slapshot button has been pressed
        m_SlapshotButtonDown = Input.GetButtonDown("Slapshot" + m_PlayerNumber.ToString());
    }

    private void FixedUpdate()
    {
        //gets the direction based on input
        Vector3 direction = new Vector3(Input.GetAxis("Vertical" + m_PlayerNumber.ToString()), 0f, Input.GetAxis("Horizontal" + m_PlayerNumber.ToString()));
        //Add a force to the attached rigidbody
        m_RB.AddForce(direction * m_Speed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if the object is the puck
        if(collision.gameObject.name == "Puck")
        {
            //ensure there is only one instance of the coroutine running
            if(m_cSlapshot != null)
            {
                //stop the coroutine
                StopCoroutine(m_cSlapshot);
            }
            //start the slapshot coroutine
            m_cSlapshot = StartCoroutine(C_Slapshot(collision.gameObject.GetComponent<Rigidbody>()));
        }
    }

    IEnumerator C_Slapshot(Rigidbody puck)
    {
        //setting the time based on the const timer variable
        float time = m_SlapshotTime;

        //loop while the time is greater than 0
        while(time > 0)
        {
            //decrease time every frame
            time -= Time.deltaTime;
            
            //check if the slapshot button has been pressed
            if(m_SlapshotButtonDown)
            {
                //add a force to the puck to send it away from the player
                puck.AddForce((puck.transform.position - transform.position) * 50f, ForceMode.Impulse);
                //set time to 0 so that this can't be spammed
                time = 0;
            }
            yield return new WaitForEndOfFrame();

        }
    }
}
