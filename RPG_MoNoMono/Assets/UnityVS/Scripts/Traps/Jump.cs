using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    int Count = 3;
    Vector3 Start_Position;
    //public GameObject JumpPrefab;

    void Start()
    {
        Start_Position = transform.position;
        Count = 3;
    }

    // Use this for initialization
    void OnCollisionEnter(Collision co)
    {
        Count--;
        co.transform.position = new Vector3(0f, 5f, 0f);
    }

    //public void Creat()
    //{
    //    GameObject Jump = MonoBehaviour.Instantiate(JumpPrefab) as GameObject;
    //    Jump.name = "Jump";
    //    Jump.transform.position = Start_Position;
    //}
}