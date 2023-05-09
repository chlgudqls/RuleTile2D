using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    //��������Ʈ�� ���� �����;ߵ�
    public Sprite[] portraitArr;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        //��������Ʈ�� �������� ���ؼ� �� add�ؼ� npc�϶��� �ҷ��Ȥ�
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        //ǥ���� ����� ���ΰ���� talkIndex�� �����ִ°���
        //�׷��� ���忡 ���� ���ڸ� ���ø����� �����ͼ� �ε����� ����� 
        talkData.Add(1000, new string[] { "�ȳ�?:0", "�̰��� ó�� �Ա���?:2" });
        talkData.Add(2000, new string[] { "����.?:0", "�� ���� �Ƹ�����?:1", "�� �ɿ��� ������ ����� �������ִٰ� ��:2" });
        talkData.Add(3000, new string[] { "����� �������ڴ�." });
        talkData.Add(4000, new string[] { "������ ����ߴ� ������ �ִ� å���̴�." });

        //Quest Talk 
        //�̷��� 1000���� ��� �����ðǵ�?
        //����Ʈ ����
        talkData.Add(10 + 1000, new string[] { "���:0", "�� ������ ���� ������ �ִٴµ�:1", "���������� �ʰ��ø� �絵�� �˷��ٲ���.:0" });
        talkData.Add(11 + 2000, new string[] { "����.?:0", "�� ���� ����� ������ �°ǰ�?:0", "�׷� �� �� �ϳ� ���ָ� �����ٵ�..:1", 
                                                "�� �� ��ó�� ������ ������ �ֿ������� �ϴµ�:0" });

        talkData.Add(20 + 1000, new string[] { "�絵�� ����?:2", "���� �긮�� �ٴϸ� ������:3", "���߿� �絵���� �Ѹ��� �ؾ߰ھ�.:2" });
        talkData.Add(20 + 2000, new string[] { "ã���� �� �� ������ ��.:1" });
        talkData.Add(20 + 5000, new string[] { "��ó���� ������ ã�Ҵ�." });

        talkData.Add(21 + 2000, new string[] { "��, ã���༭ ����.:2" });

        //�ڿ� + 0�� ���� 0123���� ǥ���� 4����� �ϴµ�
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }
    //���� �ε����� �Ű������� ������ talkIndex
    //���ڸ������͵��ǳ� + �� ���� ������ Ű���� �����ǰ� + 0,1,2,3 �����ϴ� ���
    public string GetTalk(int id, int talkIndex)
    {
        //��ȭ ����Ű�� ��� ������ ��
        if(!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                {
                //���⵵ ������ ��� ������ ����Ʈ�ε������� �繰�� ����Ʈ�� �ƴ϶�
                //����Ʈ�� �ƴ� �繰 00���� �������� Ű������ ��ũ������ �������� �ڵ�
                //if (talkIndex == talkData[id - (id % 100)].Length)
                //    return null;
                //else
                //    return talkData[id - (id % 100)][talkIndex];
                 return GetTalk(id - (id % 100), talkIndex);
                }

            else
                {
                        //if�� ���� ��Ȳ�� ����Ȳ�� -- �� �ƴ϶� else�� �� ��Ȳ�̰� if�� ��ũ�ε��� ������ ��ȭ����
                        //�� ��Ȳ�϶� ���ϰ��� ����ϴ� ����
                        //if (talkIndex == talkData[id - (id % 10)].Length)
                        //    return null;
                        //else
                        //    return talkData[id - (id % 10)][talkIndex];
                        return GetTalk(id - (id % 10), talkIndex);
                }
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
        return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id , int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
