using System;
using System.Collections.Generic;
using System.Text;

namespace ContinuousExercise
{
    class Topic
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime CompletionDate { get; set; }

        public Topic(string id, string title, string description, double estimatedTimeToMaster, double timeSpent, string source, DateTime startLearningDate, bool inProgress, DateTime completionDate)
        {
            Id = id;
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startLearningDate;
            InProgress = inProgress;
            CompletionDate = completionDate;
        }

        public Topic()
        {
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id: ");
            sb.Append(Id);
            sb.Append("\nTitle: ");
            sb.Append(Title);
            sb.Append("\nDescription: ");
            sb.Append(Description);
            sb.Append("\nEstimatedTimeToMaster: ");
            sb.Append(EstimatedTimeToMaster);
            sb.Append("\nTimeSpent: ");
            sb.Append(TimeSpent);
            sb.Append("\nSource: ");
            sb.Append(Source);
            sb.Append("\nStartLearningDate: ");
            CheckIfDateSkipped(sb, StartLearningDate);
            sb.Append("\nInProgress: ");
            sb.Append(InProgress);
            sb.Append("\nCompletionDate: ");
            CheckIfDateSkipped(sb, CompletionDate);
            sb.Append("\n");
            return sb.ToString();
        }

        private void CheckIfDateSkipped(StringBuilder sb, DateTime d)
        {
            if (d.Year.Equals(0001))
            {
                sb.Append("-");
            }
            else
            {
                sb.Append(d.ToString().Split(" ")[0]);
            }
        }
    }
}
