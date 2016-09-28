using UnityEngine;
using System.Collections;

public class Traps_Move : MonoBehaviour
{

    bool move_Left = true;
    Vector3 Start_Position;

    void Start()
    {
        Start_Position = transform.position;
    }
    // Update is called once per frame
    void Update()
    {

        if ( move_Left == true )
        {
            transform.Translate( Vector3.left * Time.deltaTime * 5f );

            if ( transform.position.x < Start_Position.x - 5f )
                move_Left = false;
        }

        else
        {
            move_Left = false;
            transform.Translate( Vector3.right * Time.deltaTime * 5f );

            if ( transform.position.x > Start_Position.x + 5f )
                move_Left = true;
        }
    }

    void OnCollisionStay( Collision co )
    {
        co.transform.parent = this.transform;
    }

    void OnCollisionExit( Collision co )
    {
        co.transform.parent = null;
    }

}