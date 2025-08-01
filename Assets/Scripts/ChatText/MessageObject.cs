using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;
public class MessageObject : MonoBehaviour
{

    public TMP_Text MessageText;
    private VivoxMessage m_vivoxMessage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextMessage(VivoxMessage message, bool deleted = false)
    {
        var updatedStatusMessage = deleted ? string.Format($"(Deleted) ") : string.Format($"(Edited) ");
        var editedText = m_vivoxMessage != null ? updatedStatusMessage : null;

        m_vivoxMessage = message;


        if (message.FromSelf)
        {
            //MessageText.alignment = TextAnchor.MiddleRight;
            MessageText.alignment = TextAlignmentOptions.Right;
            MessageText.text = string.Format($"{message.MessageText} :<color=blue>{message.SenderDisplayName} </color>\n<color=#5A5A5A><size=30>{editedText}{message.ReceivedTime}</size></color>");
        }
        else
        {
            //MessageText.alignment = TextAnchor.MiddleLeft;
            MessageText.alignment = TextAlignmentOptions.Left;
            MessageText.text = string.IsNullOrEmpty(message.ChannelName)
                ? string.Format($"<color=purple>{message.SenderDisplayName} </color>: {message.MessageText}\n<color=#5A5A5A><size=30>{editedText}{message.ReceivedTime}</size></color>") // DM
                : string.Format($"<color=green>{message.SenderDisplayName} </color>: {message.MessageText}\n<color=#5A5A5A><size=30>{editedText}{message.ReceivedTime}</size></color>"); // Channel Message
        }

    }
}
