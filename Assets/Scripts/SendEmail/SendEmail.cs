using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class SendEmail : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Input Field")]
    [SerializeField] private TMP_InputField _inputSubject;
    [SerializeField] private TMP_InputField _inputContent;

    private string _sender = "capilotgc@gmail.com";
    private string _password = "kcuw onck elcq ioyv";
    [Header("Email Data")]
    [SerializeField] private string _receiver;

    private MailMessage _mailMessage;
    private SmtpClient _smtpClient;

    void Start()
    {
        InitiateMail();

        InitiateMailMethode();
    }
       
    private void InitiateMail()
    {
        _mailMessage = new MailMessage();
        _mailMessage.From = new MailAddress(_sender);
        _mailMessage.To.Add(_receiver);
        _mailMessage.IsBodyHtml = true;
    }

    private void InitiateMailMethode()
    {
        _smtpClient = new SmtpClient("smtp.gmail.com");
        _smtpClient.Port = 587;
        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        _smtpClient.EnableSsl = true;
        _smtpClient.UseDefaultCredentials = false;

        _smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(_sender, _password) as ICredentialsByHost;

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
    }

    public void SendMentualMail()
    {
        //string jsonstring = File.ReadAllText("save.json");
        //Saving save = JsonUtility.FromJson<Saving>(jsonstring);
        Saving save = JSON_Manager.LoadData<Saving>(Application.dataPath + "/Json/Save.json");

        string mailContent = File.ReadAllText(Application.dataPath + "/index.html");

        mailContent = mailContent.Replace("#NomPatient#", save.profile.Username);


        List<string> dayName = new List<string>();
        List<ContentJournal> contentJournal = new List<ContentJournal>();

        int count = 0;


        foreach(var k in save.journal.journal.Keys.Reverse())
        {
            dayName.Add(StringToDate(k).ToLongDateString());
            contentJournal.Add(ContentJournal.ConvertValueToContentJournal(save.journal.journal[k]));
            count++;
            if (count == 7)
            {
                dayName.Reverse();
                contentJournal.Reverse();
                break;
            }
        }

        for (int i = 0; i < 7; i++)
        {
            mailContent = mailContent.Replace("#Jour" + (i + 1).ToString() + "#", dayName[i]);
            mailContent = mailContent.Replace("#ListeMot" + (i + 1).ToString() + "#", contentJournal[i].content);
        }

        // Récupérer la Date par la Key du journal
        //_mailMessage.Subject = "Cap'îlot → Compte rendu de " + save.profile.Username + " Semaine du : " + DateTime.Now.ToLongDateString();
        _mailMessage.Subject = "Cap'îlot → Compte rendu de " + save.profile.Username;
        _mailMessage.Body = mailContent;

        _mailMessage.Attachments.Add(new Attachment(Application.dataPath + "/Art/img/db.png"));
        //Debug.Log(mailContent);
        _smtpClient.Send(_mailMessage);
        Debug.Log("Message Send");
    }

    public void SendMailWithInputField()
    {
        if (_inputSubject.text == "")
        {
            Debug.LogError("Empty Subject");
            return;
        }
        if (_inputContent.text == "")
        {
            Debug.LogError("Empty Content");
            return;
        }
        _mailMessage.Body = _inputContent.text;

        _smtpClient.Send(_mailMessage);
    }
    
    // Return a DateTime with a string
    private DateTime StringToDate(string str)
    {
        if (String.IsNullOrEmpty(str))
            return DateTime.MinValue;

        return DateTime.Parse(str);
    }

    

    public class ContentJournal
    {
        public string theme;
        public string emotion;
        public string content;
        public static ContentJournal ConvertValueToContentJournal(string saveValue)
        {
            if (String.IsNullOrEmpty(saveValue))
                return null;

            string[] separator = saveValue.Split("\n");
            ContentJournal CJ = new ContentJournal();
            CJ.theme = separator[0];
            CJ.emotion = separator[1];
            CJ.content = separator[2];
            return CJ;
        }
    }
}
