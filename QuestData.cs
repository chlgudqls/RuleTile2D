using System.Collections;
using System.Collections.Generic;

public class QuestData
{
    public string questName;
    //����Ʈ ����npc
    public int[] npcId;

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
