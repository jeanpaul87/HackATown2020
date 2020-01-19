using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class takeScreenshot : MonoBehaviour
{
    //  [SerializeField] GameObject blink;

    public string youremail;
    public string subject;
 
    public string mailBodyy;
    public Text errrrror;
    public string pathh;

    public GameObject pannel;


    private void Start()
    {
        pannel.SetActive(true);
    }
    public void removeThePannelByClicking()
    {
        pannel.SetActive(false);
    }
    private void Update()
    {

        errrrror.text = "lattitude :" + Gps.lattitude.ToString()+ " , longtitude: " + Gps.longtitude.ToString() ; 

    }


    public void TakeAShot()
    {
        StartCoroutine("CaptureIt");
        StartCoroutine(sendmail());
        //Sennd Email
        // public Text coor
        //coor.text = "Lat :"GPS.Instance.latitude.ToString();
        //              Longitude...
        

    }
    IEnumerator CaptureIt()
    {
        
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "ScreenShot" + timeStamp + ".png";
        string pathToSave = fileName;
        pathh = pathToSave ;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
    }
    public IEnumerator sendmail()
    {
        yield return new WaitForSeconds(0.0f);
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(youremail);
        mail.To.Add("jp.khoueiry@gmail.com");
        mail.Subject = subject;
        mail.Body = mailBodyy + "https://www.google.com/maps/search/?api=1&query="+Gps.lattitude+","+Gps.longtitude;

        Attachment attachment = null;
        attachment = new Attachment(pathh);
        mail.Attachments.Add(attachment);


        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential(youremail, "!@#456qwe") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
   
        smtpServer.Send(mail);
        errrrror.text = "The Mail was sent successessfully";
    }
}
