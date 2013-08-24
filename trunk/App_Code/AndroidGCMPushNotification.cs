﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for AndroidGCMPushNotification
/// </summary>
public class AndroidGCMPushNotification
{    
    /*Implement like this
    AndroidGCMPushNotification apnGCM = new AndroidGCMPushNotification();
    string strResponse =
    apnGCM.SendNotification("17BA0791499DB908433B80F37C5FBC89B870084B",
    "Test Push Notification message "); 
     */

	public AndroidGCMPushNotification()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string SendNotification(string deviceId, string message)
    {
        string GoogleAppID = "AIzaSyAVx_czZItZXcmIQHcw4TauzV9g1mertaQ";
        var SENDER_ID = "799634076916";// My Google ApI 12 digits on URL
        var value = message;
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        tRequest.Method = "post";
        tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
        tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
        
        string postData = @"collapse_key=score_update&time_to_live=108&
        delay_while_idle=1&data.message=" + value + "&data.time=" + 
        System.DateTime.Now.ToString() + "registration_id=" + deviceId + "";
        Console.WriteLine(postData);
        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;

        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        WebResponse tResponse = tRequest.GetResponse();

        dataStream = tResponse.GetResponseStream();

        StreamReader tReader = new StreamReader(dataStream);

        String sResponseFromServer = tReader.ReadToEnd();
        
        tReader.Close();
        dataStream.Close();
        tResponse.Close();
        return sResponseFromServer;
    }
}