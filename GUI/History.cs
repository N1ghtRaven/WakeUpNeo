using System.Collections.Generic;

namespace GUI
{
    public class History
    {
        private readonly List<string> historyList = new List<string>();
        private ushort current = 0;
        
        public void Put(string command)
        {
            historyList.Add(command);
            current = (ushort)(historyList.Count);
        }
        public string GetOlder(string command)
        {
            if (current > 0)
            {
                return historyList[--current];
            }

            return command;
        }

        public string GetYounger(string command)
        {
            if (current < historyList.Count - 1)
            {
                return historyList[++current];
            }

            return command;
        }
    }
}
