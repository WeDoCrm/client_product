using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public class ChatUtils
    {
        public static string GetIdFromNodeTag(string tag)
        {
            string[] tempArr = tag.Split(':');
            if (tempArr.Length > 1)
            {
                return tempArr[0];
            }
            else
            {
                return tag;
            }
        }

        public static string[] GetLoggedInIdFromNodeTag(TreeNodeCollection Nodes)
        {
            List<string> chatterList = new List<string>();
            
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (!((string)Nodes[i].Tag).Contains(CommonDef.CHAT_USER_LOG_OUT))
                {
                    chatterList.Add(ChatUtils.GetIdFromNodeTag((string)Nodes[i].Tag));
                }
            }
            return chatterList.ToArray();

        }

        public static string TagAsLoggedInId(string id)
        {
            return (id + CommonDef.CHAT_USER_LOG_IN);
        }

        public static string TagAsLoggedOutId(string id)
        {
            return (id + CommonDef.CHAT_USER_LOG_OUT);
        }

        public static bool ContainsFormKeyInChatForm(string formKey, string idInMsg)
        {
            string[] formKeyArr = formKey.Split('/');
            string[] idInMsgArr = idInMsg.Split('/');

            if (formKeyArr.Length == 0 || formKeyArr.Length != idInMsg.Length)
            {
                return false;
            }

            for (int i = 0; i < formKeyArr.Length; i++)
            {
                if (!idInMsg.Contains(formKeyArr[i])) return false;
            }
            return true;
        }

        //자기 id를 앞으로 해서 formkey 생성 : myid/id1/id2...
        public static string getFormKey(string ids, string myId)
        {
            string[] idsArr = ids.Split('/');
            string formKey = myId;
            for (int i = 0; i < idsArr.Length; i++)
            {
                if (idsArr[i] != myId)
                    formKey += "/" + idsArr[i];
            }
            return formKey;
        }
    }
}
