﻿using System;
using System.Net;
using JPush.Api.Common;


namespace JPush.Api.Push
{
    public class MessageResult : BaseResult
    {
        public long msg_id{get;set;}
        public long sendno{ get; set; }

        override public bool isResultOK()
        {
            if (Equals(ResponseResult.responseCode, HttpStatusCode.OK))
            {
                return true;            
            }
            return false;
        }
        public override string ToString()
        {
             return string.Format("sendno:{0},message_id:{1}", sendno, msg_id);
        }
    }
    //"{\"sendno\":\"0\",\"msg_id\":\"1704649583\"}"
    public class JpushSuccess
    {
        public string sendno;
        public string msg_id;
    }
    public class JpushError
    {
        public JpushErrorObject error;
        public long msg_id;
    }
    public class JpushErrorObject
    {
       public int     code;
       public String  message;
    }
}
