using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    //스프라이트는 따로 가져와야됨
    public Sprite[] portraitArr;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        //스프라이트를 가져오기 위해서 또 add해서 npc일때만 불러옴ㄴ
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        //표정이 문장과 매핑관계라서 talkIndex로 쓸수있는건지
        //그래서 문장에 붙은 숫자를 스플릿으로 가져와서 인덱스로 써먹음 
        talkData.Add(1000, new string[] { "안녕?:0", "이곳에 처음 왔구나?:2" });
        talkData.Add(2000, new string[] { "여어.?:0", "꽃 정말 아름답지?:1", "이 꽃에는 무언가의 비밀이 숨겨져있다고 해:2" });
        talkData.Add(3000, new string[] { "평범한 나무상자다." });
        talkData.Add(4000, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

        //Quest Talk 
        //이러면 1000번을 어떻게 가져올건데?
        //퀘스트 순서
        talkData.Add(10 + 1000, new string[] { "어서와:0", "이 마을에 놀라운 전설이 있다는데:1", "오른쪽으로 쪽가시면 루도가 알려줄꺼야.:0" });
        talkData.Add(11 + 2000, new string[] { "여어.?:0", "이 꽃의 비밀을 들으러 온건가?:0", "그럼 일 좀 하나 해주면 좋을텐데..:1", 
                                                "내 집 근처에 떨어진 동전좀 주워줬으면 하는데:0" });

        talkData.Add(20 + 1000, new string[] { "루도의 동전?:2", "돈을 흘리고 다니면 못쓰지:3", "나중에 루도한테 한마디 해야겠어.:2" });
        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다 줘.:1" });
        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });

        talkData.Add(21 + 2000, new string[] { "엇, 찾아줘서 고마워.:2" });

        //뒤에 + 0은 뭔지 0123으로 표정이 4개라고 하는데
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }
    //값의 인덱스를 매개변수로 가져옴 talkIndex
    //숫자만가져와도되나 + 로 따로 놓으면 키값은 유지되고 + 0,1,2,3 구분하는 모양
    public string GetTalk(int id, int talkIndex)
    {
        //대화 참조키가 없어서 에러가 남
        if(!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                {
                //여기도 참조가 없어서 에러남 퀘스트인덱스없는 사물은 퀘스트가 아니라서
                //퀘스트가 아닌 사물 00으로 떨어지는 키참조값 토크데이터 가져오는 코드
                //if (talkIndex == talkData[id - (id % 100)].Length)
                //    return null;
                //else
                //    return talkData[id - (id % 100)][talkIndex];
                 return GetTalk(id - (id % 100), talkIndex);
                }

            else
                {
                        //if문 안의 상황이 현상황임 -- 이 아니라 else가 현 상황이고 if는 토크인덱스 없을때 대화종료
                        //그 상황일때 리턴값을 사용하는 구조
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
