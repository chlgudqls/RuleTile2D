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
    //�ʱ�ȭ �����ٰ� false���ƴ϶� �ؿ� true�����س����ϱ� ���������������ƾߵǴ°���
    public bool isAction;
    //��ȭ�� �������̸� �׼ǿ��� ��������� ������
    void Start()
    {
        GameLoad();
        //�ٷ� ��
        Debug.Log(questManager.CheckQuest());
        questTalk.text = questManager.CheckQuest();
    }
    //���� ��Ÿ�� ��ư�Է��� ������Ʈ���� �Ѵٰ���
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
    //���� �Լ��� ���� �ҷ����� ����
    public void SubMenuActive()
    {
        if (menuSet.activeSelf)
            menuSet.SetActive(false);
        else
            menuSet.SetActive(true);
    }

    public void Action(GameObject scanObj)
    {
        //�Ǻ��Ҷ� �ƹ����� ���� �߽���
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
            //����� talk�Լ� ���
            //talkText.text = "�̰��� �̸��� " + scanObject.name + "�̶�� �Ѵ�.";
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id , objData.isNpc , scanObject);
        //}
        //talkPanel.SetActive(isAction);
        talkPanel.SetBool("isShow", isAction);
    }
    //void�Լ��� �޺κ� �ڵ带 �������� �ϱ����ؼ� return����
    void Talk(int id , bool isNpc , GameObject scanObject)
    {
        int questTalkIndex = 0;
        string talkData = "";
        //��¥ �̰� ���ϸ� ������ �ȵǴµ� ���� �帧���� �𸣰ڳ�
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
            //�̰� ������ߴµ� �� �Ȼ����
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
            //���� �׳� �����;��°� �´µ� ���ø��ᵵ �����ȳ�
            talk.SetMsg(talkData.Split(":")[0]);
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        talkIndex++;
        isAction = true;
    }
    public void GameSave()
    {
        //������ ������ ������ �����ϴ� Ŭ����
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }
    public void GameLoad()
    {
        //���� ���� ���������� �����Ͱ� �����Ƿ� ����ó��
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