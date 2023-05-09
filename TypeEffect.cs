using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    string targetMsg;
    Text msgText;
    int index;
    public GameObject EndCursor;
    float interval;
    AudioSource audioSource;
    public bool isAnim;

    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }
    //msgText, msgText.text, targetMsg, msg  왜이리많지 헷갈림
    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
        targetMsg = msg;
        EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);
        interval = 1.0f / CharPerSeconds;

        isAnim = true;

        Invoke("Effecting", interval);
    }
    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];

        //''를 쓰네 string 이라 그런지
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
        audioSource.Play();

        index++;

        //왜 두번이나 돌리지   
        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
