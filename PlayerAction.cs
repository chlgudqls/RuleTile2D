using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;

    float h;
    float v;
    bool isHorizoneMove;
    public GameManager manager;

    Rigidbody2D rigid;
    Animator anim;
    //바라보고있는방향값
    Vector3 dirVec;
    GameObject scanObject;

    //mobile
    //전역변수라서 한번 true로 돌리면 false로 안돌아온다고함 그래서 프레임마다 false로 돌려놓는다고함
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        #region 십자이동로직
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + left_Value + right_Value;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value;


        //true 한번만 나오면 무조건 true이기때문에 합친다고함
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        //v를 때면 hDown했을때 트루가 되게 하는건지
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;


        //bool hDown = manager.isAction ? false : right_Down || left_Down;
        //bool vDown = manager.isAction ? false : up_Down || down_Down;
        //bool hUp = manager.isAction ? false : right_Up || left_Up;
        //bool vUp = manager.isAction ? false : up_Up || down_Up;

        if (hDown)
            isHorizoneMove = true;
        else if (vDown)
            isHorizoneMove = false;
        //이게 속도측정?
        else if (vUp || hUp)
            isHorizoneMove = h != 0;
        #endregion
        //Animation
        //처음엔 set만 해서 파라미터로 값보냈는데 같은값이면 값안주게 했고 값줄때 플레그도 추가
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

        //Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        //Scan Object
        //퀘스트 넘버 추가했기 때문에 퀘스트아니면 대화가 안됨 어떻게 해결할건지
        if (Input.GetButtonDown("Jump") && scanObject != null)
                 manager.Action(scanObject);
        //Debug.Log("This is " + scanObject.gameObject.name);
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;
    }

    void FixedUpdate()
    {
        #region 십자이동로직
        //move
        Vector2 moveVec = isHorizoneMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;
        #endregion
        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec , 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }

    //mobile 객체에 이벤트 트리거 컴포넌트 추가하고 인자값전달하면 그에 대한 값을 리턴받음
    //왜 이렇게 많이 나누지
    public void ButtonDown(string type)
    {
        switch(type)
        {
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D":
                down_Value = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
                //근데 왜 점프는 빼는거임 버튼 누르는게 아니라서 빼는거지   
            case "A":
                if (scanObject != null)
                    manager.Action(scanObject);
                break;
            case "C":
                manager.SubMenuActive();
                break;
        }
    }


    //mobile
    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;
            case "D":
                down_Value = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;
        }
    }
}
