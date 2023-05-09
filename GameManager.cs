using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    public Animator portraitAnim;
    public TypeEffect talk;
    public Text questTalk;
    public Text npcName;
    public GameObject scanObject;
    public GameObject menuSet;
    public GameObject player;
    public Image portraitImg;
    Sprite prevPortrait;

    public int talkIndex;
    //초기화 안헀다고 false가아니라 밑에 true지정해놨으니까 순서생각하지말아야되는건지
    public bool isAction;
    //대화가 여러개이면 액션에서 쓸수가없어서 지웠음
    void Start()
    {
        GameLoad();
        //바로 뜸
        Debug.Log(questManager.CheckQuest());
        questTalk.text = questManager.CheckQuest();
    }
    //보통 단타성 버튼입력은 업데이트에서 한다고함
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SubMenuActive();
            //if (menuSet.activeSelf)
            //    menuSet.SetActive(false);
            //else
            //    menuSet.SetActive(true);
        }
    }
    //따로 함수로 만들어서 불러오게 수정
    public void SubMenuActive()
    {
        if (menuSet.activeSelf)
            menuSet.SetActive(false);
        else
            menuSet.SetActive(true);
    }

    public void Action(GameObject scanObj)
    {
        //판별할때 아무때나 쓰면 잘써짐
        //if (isAction)
        //{
        //    isAction = false;
            //talkPanel.SetActive(false);
        //}
        //else
        //{
            //isAction = true;
            //talkPanel.SetActive(true);
            scanObject = scanObj;
            //지우고 talk함수 사용
            //talkText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id , objData.isNpc , scanObject);
        //}
        //talkPanel.SetActive(isAction);
        talkPanel.SetBool("isShow", isAction);
    }
    //void함수의 뒷부분 코드를 강제종료 하기위해서 return썻음
    void Talk(int id , bool isNpc , GameObject scanObject)
    {
        int questTalkIndex = 0;
        string talkData = "";
        //진짜 이거 안하면 적용이 안되는데 무슨 흐름인지 모르겠네
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            //이거 디버그했는데 왜 안사라짐
            questTalk.text = questManager.CheckQuest(id);
            Debug.Log(questManager.CheckQuest(id));
            return;

        }
        if (isNpc)
        {
            npcName.text = scanObject.GetComponent<ObjData>().npcName;
            talk.SetMsg(talkData.Split(":")[0]);
            portraitImg.sprite = talkManager.GetPortrait(id , int.Parse(talkData.Split(":")[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            npcName.text = "";
            //여기 그냥 데이터쓰는게 맞는데 스플리써도 에러안남
            talk.SetMsg(talkData.Split(":")[0]);
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        talkIndex++;
        isAction = true;
    }
    public void GameSave()
    {
        //간단한 데이터 저장을 지원하는 클래스
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }
    public void GameLoad()
    {
        //최초 게임 실행했을때 데이터가 없으므로 예외처리
        if(!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }
    public void GameExit()
    {
        Application.Quit();
    }
}