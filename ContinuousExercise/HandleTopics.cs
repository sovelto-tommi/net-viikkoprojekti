using System;
using System.Collections.Generic;
using System.Text;

namespace ContinuousExercise
{
    class HandleTopics
    {
        List<Topic> Topics { get; set; }

        public HandleTopics(List<Topic> topics)
        {
            this.Topics = topics;
        }

        public HandleTopics()
        {      
            Topics = new List<Topic>();
        }

        public void Add(Topic topic)
        {
            Topics.Add(topic);      
        }

        public void PrintTopics()
        {
            foreach (var item in this.Topics)
            {
                try
                {
                    Console.WriteLine(item.ToString());
                }
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("There are some empty lines in the CSV file.");
                }
            }
        }

        public void SaveTopicToCSV()
        {
            
        }
    }
}
