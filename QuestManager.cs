using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //������������Ʈid
    public int questId;
    //������� �迭�� �����ͼ� ����Ʈ ��������
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
        questList.Add(10, new QuestData(("���� ������ ��ȭ�ϱ�."), new int[] {1000, 2000 }));
        questList.Add(20, new QuestData(("�絵�� ���� ã���ֱ�."), new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData(("����Ʈ �� Ŭ����!"), new int[] { 0 }));
    }
    //������ �Լ�
    //����Ʈ ��ȣ + ����Ʈ ���� �ε��� = ����Ʈ ��ȭID
    //��ũ���̵� �޾Ƽ� ������ �ʴµ� �� �޴°���
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //�׷��� �� ���� �ؿ� �־�� �������� �Ѿ�°ǰ� ���� ����Ʈ 2�� �Ƿ���
        // if������ ����Ʈ ������ ���� ����Ʈ������Ʈ�� ��Ÿ���°ǵ�  ��ȭ�� ������ ����Ʈ �׼��ε����� �ö󰡴ϱ�
        // ����Ʈ������Ʈ�� ��ȭ�������� ����Ʈ�׼��ε����� �ö󰡸鼭 ����Ʈ������Ʈ�� �����������
        if(id == questList[questId].npcId[questActionIndex])
        questActionIndex++;
        ControlObject();
        //��ȭ�� �����ٴ� ���̶����
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();
        //��ųʸ����� Ű���� �迭�ȿ� �ְ� ���� ������ �װ��� �迭�� ����Ʈ�ε����ε� �װ��� ����Ʈ���̵�� ����Ʈ ���۹�ȣ��
        return questList[questId].questName;
    }
    //�������ڸ��� ����Ʈ ��Ÿ���� ��
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        //��������Ʈ�� �Ѿ�� 10�� �ö󰡸鼭 10�ڿ� �ִ� ����Ʈ���� ��ȣ�� �ʱ�ȭ �����ְ�
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
