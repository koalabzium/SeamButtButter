using System;
using System.Collections.Generic;


namespace ConversationManager.SeamButBetter
{
    public class ContextList
    {
        public List<Context> Contexts { get; set; }

        public ContextList(Context con)
        {
            Contexts.Add(con);
        }

        public ContextList()
        {
            Contexts = new List<Context>();
        }

        public void Append(Context con)
        {
            Contexts.Add(con);
        }

        public Boolean Contains(Context con)
        {
            return Contexts.Contains(con);
        }

    }
}
