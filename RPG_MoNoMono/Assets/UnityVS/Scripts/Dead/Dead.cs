using UnityEngine;
using System.Collections;

public class Dead : MonoBehaviour
{
    public Vector3 force = Vector3.zero;
    public float DEADLINE = -5f;

    void OnCollisionEnter(Collision co)
    {
        //if (co.transform.name == "Player")
        {
            //GameObject.Find("Player").GetComponent<PlayerAction>().moveFlag = true;
            //co.transform.position = co.transform.GetComponent<PlayerAction>().Get_StartPosition();
            //co.transform.GetComponent<PlayerAction>().Set_Life(co.transform.GetComponent<PlayerAction>().Life - 1);
            //co.transform.rigidbody.velocity = Vector3.zero;
            //co.collider.GetComponent<PlayerAction>().force = new Vector3(0f, 0f, 0f);
            //return;
        }
    }
}
