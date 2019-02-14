using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ConversationManager.SeamButBetter
{
    public class FileDriver : IDriver
    {

        public int DefaultTimeout;
        public ContextList ContextList { get; set; }
        public string Path { get; set; }

        public FileDriver(string path, int _TimeOut = 0)
        {
            Path = path;
            DefaultTimeout = _TimeOut;
        }

        

        public void Add<T>(int id, T obj)
        {

            var json = JsonConvert.SerializeObject(obj);

            string text = File.ReadAllText(Path);
            text = Regex.Unescape(text);

            var exists = false;
            DateTime now = DateTime.Now;
            Context tmp = new Context(id, json, DefaultTimeout, now);
            Context toDelete = new Context();
            ContextList = new ContextList();

            if (text.Length > 0)
            {
                text = RemoveFromText(text);
                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));

                foreach (var c in ContextList.Contexts)
                {
                    if (c.Equals(tmp))
                    {
                        exists = true;

                        if (DateTime.Now.Subtract(c.CreationTime).TotalMinutes >= DefaultTimeout)
                        {
                            toDelete = c;
                            exists = false;
                        }
                    }
                }
            }

            Delete(toDelete.GetId());

            if (!exists)
            {
                ContextList.Append(tmp);
                SerializeAndSave(ContextList);
            }
        }

        public string Get(int id)
        {
            CheckTimeout();
            string text = File.ReadAllText(Path);
            text = Regex.Unescape(text);

            if (text.Length > 0)
            {
                text = RemoveFromText(text);
                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));

                foreach (var c in ContextList.Contexts)
                {
                    if (c.GetId() == id)
                    {
                        var something = c.GetValues();
                        return something;
                    }
                }
            }

            return (string)Convert.ChangeType(null, typeof(string));
        }

        public void Delete(int id)
        {
            string text = File.ReadAllText(Path);
            text = Regex.Unescape(text);
            Context toDelete = new Context();

            if (text.Length > 0)
            {
                text = RemoveFromText(text);
                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));

                foreach (var c in ContextList.Contexts)
                {
                    if (c.GetId() == id)
                    {
                        toDelete = c;
                    }
                }

                ContextList.Contexts.Remove(toDelete);
                SerializeAndSave(ContextList);
            }
        }

        public void DeleteAll()
        {

        }

        public void Update<T>(int id, T obj)
        {
            Delete(id);
            Add(id, obj);
        }

        public void CheckTimeout()
        {
            List<Context> toRemove = new List<Context>();
            string text = File.ReadAllText(Path);
            text = Regex.Unescape(text);

            if (text.Length > 0)
            {
                text = RemoveFromText(text);
                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));

                foreach (Context c in ContextList.Contexts)
                {
                    if (c.TimeOut > 0)
                    {
                        if (DateTime.Now.Subtract(c.CreationTime).TotalMinutes >= DefaultTimeout)
                        {
                            toRemove.Add(c);
                        }
                    }
                }

                foreach (Context c in toRemove)
                {
                    Delete(c.GetId());
                }
            }
        }

        private string RemoveFromText(string txt)
        {
            txt = txt.Remove(0, 1);
            txt = txt.Remove(txt.Length - 1, 1);
            return txt;
        }

        private void SerializeAndSave(ContextList ContextList)
        {
            var settings = new JsonSerializerSettings()
            {
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
            };


            var context = JsonConvert.SerializeObject(ContextList, settings);


            using (StreamWriter file = File.CreateText(Path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, context);
            }
        }
    }
}

