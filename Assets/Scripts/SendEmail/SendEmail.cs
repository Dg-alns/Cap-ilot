using System.IO;
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
        string jsonstring = File.ReadAllText("save.json");
        Saving save = JsonUtility.FromJson<Saving>(jsonstring);

        string mailContent = File.ReadAllText(Application.dataPath + "/index.html");
        foreach (var k in save.journal.journal.Keys)
        {

            Debug.Log(k.ToString());
        }
        mailContent = mailContent.Replace("#NomPatient#", save.profile.Username);

        // Remplacer les ## par les mots clés du journal, les émotions et le thème
        mailContent = mailContent.Replace("#ListeMot1#", save.journal.journal.Keys.ToString());
        mailContent = mailContent.Replace("#ListeMot2#", "AAHHH");
        mailContent = mailContent.Replace("#ListeMot3#", "BBBBH");
        mailContent = mailContent.Replace("#ListeMot4#", "CCCCH");
        mailContent = mailContent.Replace("#ListeMot5#", "Je sais ça ne veut rien dire");
        mailContent = mailContent.Replace("#ListeMot6#", "mais je suis heureux");
        mailContent = mailContent.Replace("#ListeMot7#", "enfin je crois");

        // Récupérer la Date par la Key du journal
        _mailMessage.Subject = "Cap'îlot → Compte rendu " + save.profile.Username + " Date : " + "- 900 av.J.C.";
        _mailMessage.Body = mailContent;

        _mailMessage.Attachments.Add(new Attachment(Application.dataPath + "/Art/img/db.png"));
        //Debug.Log(mailContent);
        //_smtpClient.Send(_mailMessage);
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
}
