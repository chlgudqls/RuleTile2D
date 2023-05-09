using System.Collections;
using System.Collections.Generic;

public class QuestData
{
    public string questName;
    //퀘스트 진행npc
    public int[] npcId;

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
