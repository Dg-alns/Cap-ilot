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

enum SEND_EMAIL_DEBUG_TEXT
{
    RECEIVER_UNVALIDATED = 0,
    NB_JOURNAL_UNVALIDATED,
    EMAIL_ALREADY_SEND,
    SEND
}

public class SendEmail_DownBar : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TMP_Text mailExplication;
    [SerializeField] private TMP_Text debugText;
    private Saving save;

    // List of string use for debug and inform the player
    List<string> messagesDebug = new List<string>()
    {
        "Aucun mail de médecin n'a été renseigner !",
        "Veuillez selecitonner un nombre de journaux !",
        "Le mail à déjà été envoyé !",
        "L'email à été envoyer !"
    };

    // Format HTML to add a journal information
    private string formatJournalHTML = "<h2><i>#JOUR#</i></h2>\r\n        <p class=\"texte\">Thème : #THEME#</p>\r\n        <p class=\"texte\">Contenu : #CONTENU#</p>\r\n        <br>\r\n        #FORMAT#";

    // Sender Information
    private string _sender = "capilotgc@gmail.com";
    private string _password = "kcuw onck elcq ioyv";

    // Receiver Address
    private string _receiver;

    // Mail Methode variable
    private MailMessage _mailMessage;
    private SmtpClient _smtpClient;

    private bool _isInitiate = false;
    private bool _isSendMail = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Initiate Mail Message
    private void InitiateMail()
    {
        _mailMessage = new MailMessage();
        _mailMessage.From = new MailAddress(_sender);
        _mailMessage.To.Add(_receiver);
        _mailMessage.IsBodyHtml = true;
    }

    // Initiate Send Mail Methode
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
        _isInitiate = true;
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        save = JSON_Manager.LoadData<Saving>("Save");

        // Put 0 to 7 possibility to send nb journal
        int nbKey = save.journal.journal.Keys.Count >= 7 ? 7 : save.journal.journal.Keys.Count;

        List<string> keysDropdown = new List<string>() { "-----" };
        for (int i = 1; i <= nbKey; i++)
        {
            keysDropdown.Add(i.ToString());
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(keysDropdown);

        // Init receiver
        _receiver = save.profile.EmailDoctor;
        mailExplication.text = mailExplication.text.Replace("#DoctorMail", _receiver);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void SendEmail()
    {
        // Check condition to send the mail
        // Have already send it ?
        if(_isSendMail) 
        {
            debugText.text = messagesDebug[(int)SEND_EMAIL_DEBUG_TEXT.EMAIL_ALREADY_SEND];
            return;
        }
        // Have initiate data ?
        if (!_isInitiate)
        {
            if (string.IsNullOrEmpty(_receiver))
            {
                debugText.text = messagesDebug[(int)SEND_EMAIL_DEBUG_TEXT.RECEIVER_UNVALIDATED];
                return;
            }
            InitiateMail();
            InitiateMailMethode();
        }
        // Have select a number of journal
        if(dropdown.value == 0)
        {
            debugText.text = messagesDebug[(int)SEND_EMAIL_DEBUG_TEXT.NB_JOURNAL_UNVALIDATED];
            return;
        }

        // Get data
        save = JSON_Manager.LoadData<Saving>("Save");
        string mailContent = File.ReadAllText(Application.dataPath + "/index.html");

        //string mailpath = Path.Combine(Application.persistentDataPath, "/index.html");
        //string mailContent = File.ReadAllText(mailpath)

        mailContent = mailContent.Replace("#NomPatient#", save.profile.Username);


        int nbValueSend = dropdown.value;

        List<string> dayName = new List<string>();
        List<ContentJournal> contentJournal = new List<ContentJournal>();

        int count = 0;
        // Get last journal and stock them in list
        foreach (var date in save.journal.journal.Keys.Reverse())
        {
            dayName.Add(DateTime.Parse(date).ToLongDateString());
            contentJournal.Add(ContentJournal.ConvertValueToContentJournal(save.journal.journal[date]));
            count++;
            if (count == nbValueSend)
            {
                dayName.Reverse();
                contentJournal.Reverse();
                break;
            }
        }

        // Convert the format with journal value
        string tmpFormat = formatJournalHTML;
        for (int i = 0; i < nbValueSend; i++)
        {
            formatJournalHTML = formatJournalHTML.Replace("#JOUR#", dayName[i]);
            formatJournalHTML = formatJournalHTML.Replace("#THEME#", contentJournal[i].theme);
            formatJournalHTML = formatJournalHTML.Replace("#CONTENU#", contentJournal[i].content);

            mailContent = mailContent.Replace("#FORMAT#", formatJournalHTML);
        }
        mailContent = mailContent.Replace("#FORMAT#", "");

        debugText.text = messagesDebug[(int)SEND_EMAIL_DEBUG_TEXT.SEND];

        // Apply value on the mail
        _mailMessage.Subject = "Cap'îlot --> Compte rendu de " + save.profile.Username;
        _mailMessage.Body = mailContent;
                
        // Send Mail
        _smtpClient.Send(_mailMessage);

        _isSendMail = true;
        Debug.Log(mailContent);
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
