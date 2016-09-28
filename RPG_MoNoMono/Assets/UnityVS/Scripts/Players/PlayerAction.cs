using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour
{
    public float speed = 5f;
    public float trunSpeed = 10f;
    public float trunRadius = 50f;
    public int statExp = 0; // 전투를 통해 얻는 경험치
    public int statLevel = 1; // 플레이어의 레벨    
    private float rot = 300f; // 회전값(비율)  

    // public Transform targer; ( 몬스터 )   

    private struct Key
    {
        public bool up;
        public bool down;
        public bool right;
        public bool left;
        public bool x;
    };

    private Key key;
    Rigidbody rigdbody;
    Vector3 movement;   
    Animator ani;

    enum STATE
    {
        IDEL = -1,
        MOVE = 0,
        ATTACK,
        DIE,
        NUM,
    };

    enum SKILL_ATTACK
    {
        NON = -1,
        ATT = 0,
        SKILL_1,
        SKILL_2,
    };

    // 캐릭터 행동 상태값 저장
    private STATE state = STATE.IDEL;
    // 캐릭터 공격 상태값 저장
    private SKILL_ATTACK skillATT = SKILL_ATTACK.NON;

    void Start()
    {
        rigdbody = GetComponent<Rigidbody>();

        ani = transform.GetComponentInChildren<Animator>(); // 자식의 애니메이터를 찾는다.
        if (ani == null)
        {
            Debug.Log(" 플레이어의 애니메이션을 못찾겠다! ");
            return;
        }
        ani.speed = 1f; //애니메이션의 스피드 증가 기본은 1  
    }

    void Update()
    {
        get_input();
        move_control();        

        //if (a == true)
        //{
        //    transform.Translate(Vector3.forward * Time.deltaTime * 3f);
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    a = true;
        //    Quaternion b = Quaternion.Euler(0f, 270f, 0f);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, b, rot);
        //    rot += 0.15f;
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    a = true;
        //    Quaternion b = Quaternion.Euler(0f, 0f, 0f);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, b, rot);
        //    rot += 0.15f;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    a = true;
        //    Quaternion b = Quaternion.Euler(0f, 180f, 0f);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, b, rot);
        //    rot += 0.15f;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    a = true;
        //    Quaternion b = Quaternion.Euler(0f, 90f, 0f);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, b, rot);
        //    rot += 0.15f;
        //}

        //if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    a = false;
        //    rot = 0.3f;
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    state = STATE.MOVE;
        //    MoveRight();
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    state = STATE.MOVE;
        //    MoveLeft();
        //}
        //else if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    state = STATE.MOVE;
        //    MoveFront();
        //}
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    state = STATE.MOVE;
        //    MoveBack();
        //}
        //else
        //{   // 키 입력 없으면 대기상태
        //    state = STATE.IDEL;
        //}

        //switch (state)
        //{   // Bool의 T or F 여부


        //    // 캐릭터의 행동상태가 대기중일때 "MOVE"애니메이션 false 
        //    case STATE.IDEL:
        //        ani.SetBool("Move", false);
        //        break;

        //    // 캐릭터의 행동상태가 이동중일때 "MOVE"애니메이션 true
        //    case STATE.MOVE:
        //        Debug.Log("ggg");
        //        ani.SetBool("Move", true);
        //        break;
        //}
    }
    private void get_input()
    {
        key.up = false;
        key.down = false;
        key.right = false;
        key.left = false;
        key.x = false;

        // A |= B  ==  A = A | B  (or연산)
        key.up |= Input.GetKey(KeyCode.UpArrow);
        key.up |= Input.GetKey(KeyCode.Keypad8);

        //key.down |= Input.GetKey(KeyCode.DownArrow);
        //key.down |= Input.GetKey(KeyCode.Keypad2);

        key.right |= Input.GetKey(KeyCode.RightArrow);
        key.right |= Input.GetKey(KeyCode.Keypad6);

        key.left |= Input.GetKey(KeyCode.LeftArrow);
        key.left |= Input.GetKey(KeyCode.Keypad4);

        key.x |= Input.GetKeyDown(KeyCode.X);


    }
    private void move_control()
    {
        Vector3 move_vector = Vector3.zero;
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            ani.SetBool("Move", true);
        }
        else
        {
            ani.SetBool("Move", false);
        }

        if (key.right)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);          
            transform.Rotate(Vector3.up, Time.deltaTime * rot, Space.Self);
        }       

        if (key.left)
        {
            transform.Rotate(Vector3.down, Time.deltaTime * rot, Space.Self);
        }

        if (key.up)
        {
            move_vector = transform.TransformDirection(Vector3.forward);
            move_vector.Normalize();
            move_vector *= speed * Time.deltaTime;
            position += move_vector;
            transform.position = position;
            //ani.SetBool("Move", true);
            ani.speed = 1.5f;
        }

        if (key.x)
        {
            ani.SetBool("Attack", true);
            ani.speed = 1f;
        }

        //if (key.down)
        //{
        //    move_vector = transform.TransformDirection(Vector3.back);
        //    move_vector.Normalize();
        //    move_vector *= speed * Time.deltaTime;
        //    position += move_vector;
        //    transform.position = position;
        //}

        position.y = 0.0f;
        position.y = transform.position.y;
    }  
}


// targer을 중심으로 회전 ( 몬스터 구현에 쓰자 )
//Vector3 relativePos = targer.position - transform.position;
//transform.rotation = Quaternion.LookRotation(relativePos);

//void MoveRight()
//{
//    transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f));

//    //Quaternion newRotation = Quaternion.LookRotation(movement);        
//    //transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, trunSpeed * Time.deltaTime);
//    //// ( A에서 , B까지 , 회전 시간)

//    //rigdbody.rotation = Quaternion.Slerp(rigdbody.rotation, newRotation, trunSpeed * Time.deltaTime);  
//}

//void MoveLeft()
//{
//    transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));
//}

//void MoveFront()
//{
//    transform.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
//}

//void MoveBack()
//{
//    transform.Translate(new Vector3(0f, 0f, -speed * Time.deltaTime));
//}  