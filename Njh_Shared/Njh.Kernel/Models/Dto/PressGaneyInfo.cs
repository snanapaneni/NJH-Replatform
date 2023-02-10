using CMS.DataEngine;
using CMS.DocumentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS;
using CMS.Helpers;

//using CMS.DocumentEngine.Types;
using CMS.Base;


namespace Njh.Kernel.Models.Dto
{

    /// <summary>
    /// Summary description for PressGaneyInfo
    /// </summary>

    public class PressGaneyInfo : IDataContainer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int RatingCount { get; set; }
        public int SurveyCount { get; set; }
        public int CommentCount
        {
            get
            {
                if (Comments != null)
                {
                    return Comments.Count;
                }
                return 0;
            }


        }
        public PressGaneyRatingInfo OverallRating { get; set; }
        public List<PressGaneyRatingInfo> Ratings { get; set; }
        public List<PressGaneyCommentInfo> Comments { get; set; }

        public bool RetrievedRatings { get; set; }
        public bool RetrievedComments { get; set; }

        public List<string> ColumnNames
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("Id");
                list.Add("Name");
                list.Add("RatingCount");
                list.Add("SurveyCount");
                list.Add("CommentCount");
                list.Add("OverallRating");
                list.Add("Ratings");
                list.Add("Comments");
                list.Add("RetrievedRatings");
                list.Add("RetrievedComments");
                return list;
            }

            //set;
        }

        public bool SetValue(string columnName, object val)
        {
            switch (columnName.ToLower())
            {
                case "id":
                    Id = val.ToString();
                    return true;
                case "name":
                    Name = val.ToString();
                    return true;
                case "ratingcount":
                    RatingCount = Convert.ToInt32(val);
                    return true;
                case "surveycount":
                    SurveyCount = Convert.ToInt32(val);
                    return true;
                case "commentcount":
                    return true;
                case "overallrating":
                    OverallRating = val as PressGaneyRatingInfo;
                    return true;
                case "ratings":
                    Ratings = val as List<PressGaneyRatingInfo>;
                    return true;
                case "comments":
                    Comments = val as List<PressGaneyCommentInfo>;
                    return true;
                case "retrievedratings":
                    RetrievedRatings = Convert.ToBoolean(val);
                    return true;
                case "retrievedcomments":
                    RetrievedComments = Convert.ToBoolean(val);
                    return true;

            }
            return false;
        }

        public object GetValue(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "id":
                    return Id;
                case "name":
                    return Name;
                case "ratingcount":
                    return RatingCount;
                case "surveycount":
                    return SurveyCount;
                case "commentcount":
                    return CommentCount;
                case "overallrating":
                    return OverallRating;
                case "ratings":
                    return Ratings;
                case "comments":
                    return Comments;
                case "retrievedratings":
                    return RetrievedRatings;
                case "retrievedcomments":
                    return RetrievedComments;
            }
            return null;
        }

        public bool TryGetValue(string columnName, out object val)
        {
            switch (columnName.ToLower())
            {
                case "id":
                    val = Id;
                    return true;
                case "name":
                    val = Name;
                    return true;
                case "ratingcount":
                    val = RatingCount;
                    return true;
                case "surveycount":
                    val = SurveyCount;
                    return true;
                case "commentcount":
                    val = CommentCount;
                    return true;
                case "overallrating":
                    val = OverallRating;
                    return true;
                case "ratings":
                    val = Ratings;
                    return true;
                case "comments":
                    val = Comments;
                    return true;
                case "retrievedratings":
                    val = RetrievedRatings;
                    return true;
                case "retrievedcomments":
                    val = RetrievedComments;
                    return true;
            }
            val = null;
            return false;
        }

        public bool ContainsColumn(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "id":
                    return true;
                case "name":
                    return true;
                case "ratingcount":
                    return true;
                case "surveycount":
                    return true;
                case "commentcount":
                    return true;
                case "overallrating":
                    return true;
                case "ratings":
                    return true;
                case "comments":
                    return true;
                case "retrievedratings":
                    return true;
                case "retrievedcomments":
                    return true;


            }
            return false;
        }

        public object this[string columnName] { get { return GetValue(columnName); } set { SetValue(columnName, value); } }

    }

    public class PressGaneyRatingInfo : IDataContainer
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Count { get; set; }

        public List<string> ColumnNames
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("Name");
                list.Add("Value");
                list.Add("Count");
                return list;
            }
            //set
            //{
            //    ColumnNames = value;
            //}
        }

        public bool SetValue(string columnName, object val)
        {
            switch (columnName.ToLower())
            {
                case "name":
                    Name = val.ToString();
                    return true;
                case "value":
                    Value = val.ToString();
                    return true;
                case "count":
                    Count = (int)val;
                    return true;

            }
            return false;
        }

        public object GetValue(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "name":
                    return Name;
                case "value":
                    return Value;
                case "count":
                    return Count;

            }
            return null;
        }

        public bool TryGetValue(string columnName, out object val)
        {
            switch (columnName.ToLower())
            {
                case "name":
                    val = Name;
                    return true;
                case "value":
                    val = Value;
                    return true;
                case "count":
                    val = Count;
                    return true;
            }
            val = null;
            return false;
        }

        public bool ContainsColumn(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "name":
                    return true;
                case "value":
                    return true;
                case "count":
                    return true;
            }
            return false;
        }

        public object this[string columnName] { get { return GetValue(columnName); } set { SetValue(columnName, value); } }
    }

    public class PressGaneyCommentInfo : IDataContainer
    {
        public string Source { get; set; }
        public DateTime MentionTime { get; set; }
        public int WordCount { get; set; }
        public string Comment { get; set; }
        public PressGaneyRatingInfo OverallRating { get; set; }


        public List<string> ColumnNames { get; set; }

        public bool SetValue(string columnName, object val)
        {
            switch (columnName.ToLower())
            {
                case "source":
                    Source = val.ToString();
                    return true;
                case "mentiontime":
                    MentionTime = Convert.ToDateTime(val);
                    return true;
                case "wordcount":
                    WordCount = Convert.ToInt32(val);
                    return true;
                case "comment":
                    Comment = val.ToString();
                    return true;
                case "overallrating":
                    OverallRating = val as PressGaneyRatingInfo;
                    return true;
            }
            return false;
        }

        public object GetValue(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "source":
                    return Source;
                case "mentiontime":
                    return MentionTime;
                case "wordcount":
                    return WordCount;
                case "comment":
                    return Comment;
                case "overallrating":
                    return OverallRating;
            }
            return null;
        }

        public bool TryGetValue(string columnName, out object val)
        {
            switch (columnName.ToLower())
            {
                case "source":
                    val = Source;
                    return true;
                case "mentiontime":
                    val = MentionTime;
                    return true;
                case "wordcount":
                    val = WordCount;
                    return true;
                case "comment":
                    val = Comment;
                    return true;
                case "overallrating":
                    val = OverallRating;
                    return true;
            }
            val = null;
            return false;
        }

        public bool ContainsColumn(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "source":
                    return true;
                case "mentiontime":
                    return true;
                case "wordcount":
                    return true;
                case "comment":
                    return true;
                case "overallrating":
                    return true;
            }
            return false;
        }

        public object this[string columnName] { get { return GetValue(columnName); } set { SetValue(columnName, value); } }

    }
}


