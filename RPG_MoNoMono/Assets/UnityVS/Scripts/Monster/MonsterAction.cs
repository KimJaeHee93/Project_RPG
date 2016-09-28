using UnityEngine;
using System.Collections;

public class MonsterAction : MonoBehaviour
{
    public Vector3 targetpos = Vector3.zero;
    public float MoveSpeed = 5f;
    public GameObject HitEffect;
    public GameObject DeadEffect;
    public int HP = 300;


    Animator ani;

    public enum MonsterState
    {
        IDLE = -1,
        MOVE = 0,
        ATTACK,
        Hit,
        DEAD,
    }

    private MonsterState state = MonsterState.IDLE; 

    void Start()
    {
        // 시작시 애니메이션 실행
        //ani.SetBool("Move", true);
        
        HP = 300;            
    }

    // Update is called once per frame
    void Update()
    {
        SearchTarger();

        Vector3 currentPos = transform.position;
        Vector3 diffPos = targetpos - currentPos;

        
        if(diffPos.magnitude < 10f) // 플레이어와 몬스터의 거리 측정
        {
            Debug.Log("캐릭터와 가깝다!");

            // 가까우면 아무일도 x
            return; 
            //아니면
        }
        // normalized
        diffPos = diffPos.normalized;
        // 몬스터가 타겟을 따라옴
        transform.Translate(diffPos * Time.deltaTime * MoveSpeed, Space.World);
        // 몬스터가 타겟을 바라봄
        transform.LookAt(targetpos);        
    }

    void SearchTarger()
    {
        // 플레이어를 찾음
        GameObject target = GameObject.Find("Player");
        targetpos = target.transform.position;
    }

    void OnTriggerEnter(Collider ohter)
    {
        Debug.Log("캐릭터와 부딫혔다!");
        // 태그된 sword에 OnTriggerEnter 되면
        if (ohter.gameObject.tag == "sword")
        {
            Debug.Log("칼에 맞았다!");
            // 상태가 Hit로 바뀌고 데미지를 입는 함수 발생
            ani.SetTrigger("Hit");
            state = MonsterState.Hit;
            Instantiate(HitEffect, ohter.transform.position, transform.rotation);
            CheckDead(50);
        }
    }

    void CheckDead(int damage)
    {
        Debug.Log("데미지를 입는다!");
        HP -= damage;
        Debug.Log("HP :" + HP.ToString());
        if(HP <= 0)
        {
            Instantiate(DeadEffect, transform.position, transform.rotation);
            //Destroy(gameObject);
        }
    }
}
