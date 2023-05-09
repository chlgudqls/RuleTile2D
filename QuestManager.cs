using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //진행중인퀘스트id
    public int questId;
    //만들어진 배열을 가져와서 퀘스트 순서변수
    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GerateData();
    }

    void GerateData()
    {
        questList.Add(10, new QuestData(("마을 사람들과 대화하기."), new int[] {1000, 2000 }));
        questList.Add(20, new QuestData(("루도의 동전 찾아주기."), new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData(("퀘스트 올 클리어!"), new int[] { 0 }));
    }
    //내보낼 함수
    //퀘스트 번호 + 퀘스트 순서 인덱스 = 퀘스트 대화ID
    //토크아이디 받아서 쓰지도 않는데 왜 받는거지
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //그래서 이 문장 밑에 있어야 다음으로 넘어가는건가 없는 퀘스트 2가 되려면
        // if문으로 퀘스트 순서에 따라서 퀘스트오브젝트를 나타내는건데  대화가 끝나면 퀘스트 액션인덱스가 올라가니까
        // 퀘스트오브젝트와 대화가끝나면 퀘스트액션인덱스가 올라가면서 퀘스트오브젝트가 사라지게했음
        if(id == questList[questId].npcId[questActionIndex])
        questActionIndex++;
        ControlObject();
        //대화가 끝났다는 말이라고함
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();
        //딕셔너리에서 키값은 배열안에 있고 점은 벨류값 그것의 배열이 퀘스트인덱스인데 그것은 퀘스트아이디는 퀘스트 시작번호고
        return questList[questId].questName;
    }
    //시작하자마자 퀘스트 나타나게 함
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        //다음퀘스트로 넘어갈때 10이 올라가면서 10뒤에 있는 퀘스트진행 번호를 초기화 시켜주고
        questId += 10;
        questActionIndex = 0;
    }

    public void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if (questActionIndex == 2)
                        questObject[0].SetActive(true);
                    break;

            case 20:
                if(questActionIndex == 0 )
                    questObject[0].SetActive(true);
                else if (questActionIndex == 1)
                        questObject[0].SetActive(false);
                    break;
        }
    }
}
