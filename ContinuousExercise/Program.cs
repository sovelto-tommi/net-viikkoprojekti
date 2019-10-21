using System;
using System.Collections.Generic;
using System.Text;

namespace ContinuousExercise
{
    class Program
    {
        private const string Path = "../../../material.csv";

        static void Main(string[] args)
        {
            HandleTopics topicHandler = new HandleTopics();
            ReadCSV(topicHandler);
            Start(true, topicHandler);
        }

        public static void WriteToCSV(Topic topic)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Path, true);
            writer.WriteLine(CreateCSVLine(topic));
            writer.Flush();
            writer.Close();
        }

        private static ReadOnlySpan<char> CreateCSVLine(Topic topic)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(topic.Id).Append(",");
            sb.Append(topic.Title).Append(",");
            sb.Append(topic.Description).Append(",");
            sb.Append(topic.EstimatedTimeToMaster).Append(",");
            sb.Append(topic.TimeSpent).Append(",");
            sb.Append(topic.Source).Append(",");
            sb.Append(topic.StartLearningDate).Append(",");
            sb.Append(topic.InProgress).Append(",");
            sb.Append(topic.CompletionDate);
            return sb.ToString();
        }

        public static void ReadCSV(HandleTopics topicHandler)
        {
            // path to the csv file
            string path = Path;

            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                topicHandler.Add(CreateTopicFromCSV(columns));
            }
        }

        private static Topic CreateTopicFromCSV(string[] columns)
        {
            try
            {
                if (columns.Length > 9)
                {
                    throw new Exception();
                }
                Topic temp = new Topic(
                                columns[0],
                                columns[1],
                                columns[2],
                                Double.Parse(columns[3]),
                                Double.Parse(columns[4]),
                                columns[5],
                                DateTime.Parse(columns[6]),
                                Boolean.Parse(columns[7]),
                                DateTime.Parse(columns[8])
                                );
                return temp;
            }
            catch (Exception)
            {
                Console.WriteLine("There is some faulty data in the file. All complete data has been read from the CSV file.");
            }
            return null;
            
        }

        public static void Start(bool on, HandleTopics topicHandler)
        {
            
            while (on)
            {
                Console.WriteLine("Would you like to add a topic or list the current ones?\nType a number of the following action:\n1 add\n2 list\n0 quit\n");
                String userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "0":
                        {
                            Console.WriteLine("See you next time!");
                            on = false;
                            break;
                        }
                    case "1":
                        {
                            CreateTopic(topicHandler);
                            Console.WriteLine("\n");
                            break;
                        }

                    case "2":
                        {
                            topicHandler.PrintTopics();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Not a valid input, try again.");
                            continue;
                        }
                } 
            }
        }

        public static void CreateTopic(HandleTopics topicHandler)
        {
            Topic t = new Topic();
            t.Id = Guid.NewGuid().ToString();
            Console.WriteLine("I need some details for the new topic");
            AddTitle(t);
            AddDescription(t);
            AddEstimatedTimeToMaster(t);
            AddTimeSpent(t);
            AddSource(t);
            AddStartLearningDate(t);
            AddInProgress(t);
            AddCompletionDate(t);
            topicHandler.Add(t);
            WriteToCSV(t);
        }

        private static void AddCompletionDate(Topic t)
        {
            Console.Write("Did you already complete learning this topic? true or false: ");
            String input = Console.ReadLine();
            if (!Skip(input))
            {
                try
                {
                    if (Boolean.Parse(input))
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Write("Add a date when you finished learning this topic: ");

                                String inp = Console.ReadLine();
                                if (!Skip(input))
                                {
                                    t.CompletionDate = DateTime.Parse(inp + " 8:00:00Z");
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            catch (System.FormatException e)
                            {
                                Console.WriteLine("This was not a valid input, try again (example inputs: yyyy-mm-dd).");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.StackTrace);
                                throw;
                            }
                        }
                    }
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("This was not a valid input, try again (true or false).");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
                
            }
        }

        private static bool Skip(string input)
        {
            return input == "-" || input.Trim().Length < 1;
        }

        private static void AddInProgress(Topic t)
        {
           
            while (true)
            {
                try
                {
                    Console.Write("Are you currently learning this topic? (true or false): ");
                    String input = Console.ReadLine();
                    if (!Skip(input))
                    {
                        t.InProgress = Boolean.Parse(input);
                        break;
                    } else
                    {
                        break;
                    }
                    
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("This was not a valid input, try again (example inputs: true, false).");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
            }
        }

        private static void AddSource(Topic t)
        {
            Console.Write("Add a source for material: ");
            String input = Console.ReadLine();
            if (!Skip(input))
            {
                t.Source = input;
            }

        }

        private static void AddDescription(Topic t)
        {
            Console.Write("Give some Description for the topic: ");
            String input = Console.ReadLine();
            if (!Skip(input))
            {
                t.Description = input;
            }
        }

        private static void AddTitle(Topic t)
        {
            while (true)
            {
                Console.Write("Give a Title: ");
                String input = Console.ReadLine();
                if (!Skip(input))
                {
                    t.Title = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Title is mandatory.");
                }
            }   
        }

        private static void AddStartLearningDate(Topic t)
        {
            while (true)
            {
                try
                {
                    Console.Write("Add date when you started/will start learning this topic? (example input yyyy-mm-dd): ");
                    String input = Console.ReadLine();
                    if (!Skip(input))
                    {
                        t.StartLearningDate = DateTime.Parse(input + " 8:00:00Z");
                        break;
                    }
                    else
                    {
                        break;
                    }

                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("This was not a valid input, try again (example inputs: yyyy-mm-dd).");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
            }  
        }

        private static void AddTimeSpent(Topic t)
        {
            while (true)
            {
                try
                {
                    Console.Write("How much time did you use already with this topic?: ");
                    String input = Console.ReadLine();
                    if (!Skip(input))
                    {
                        t.TimeSpent = Double.Parse(input);
                        break;
                    }
                    else
                    {
                        break;
                    }

                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("This was not a valid input, try again (example inputs: 4, 0.5, 112).");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
            }
        }

        private static void AddEstimatedTimeToMaster(Topic t)
        {
            while (true)
            {
                try
                {
                    Console.Write("Estimate the time to master this Topic (time in days): ");
                    String input = Console.ReadLine();
                    if (!Skip(input))
                    {
                        t.EstimatedTimeToMaster = Double.Parse(input);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                catch(System.FormatException e)
                {
                    Console.WriteLine("This was not a valid input, try again (example inputs: 4, 0.5, 112).");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
            }
            
        }
    }
}