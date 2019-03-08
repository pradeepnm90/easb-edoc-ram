using Markel.Pricing.Service.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    public class Questionnaire
    {
       
        #region Properties       

        public int QuestionId { get; set; }

        public int? GroupId { get; set; }
        public string QuestionText { get; set; }

        public string VariableName { get; set; }

        public int? SequenceNumber { get; set; }

        public int? RowId { get; set; }

        public int? RowSpan { get; set; }

        public int? ColumnId { get; set; }

        public int? ColumnSpan { get; set; }

        public string Format { get; set; }

        public string DataType { get; set; }

        public string Answer { get; set; }
        public bool IsVisible { get; set; }
        public string QuestionTypeName { get; set; }
        internal List<Questionnaire> Questions { get; set; }

        #endregion Properties
        public static List<Questionnaire> BuildQuestionGroupTree(List<Questionnaire> questions, int? parentQuestionId = null)
        {          
            return questions.Where(q => q.GroupId == parentQuestionId && !ReportsSetting.ExcludeQuetions.Contains(q.VariableName)).Select(qa =>
            {
                Questionnaire questionAnswer = qa;
                questionAnswer.Questions = BuildQuestionGroupTree(questions, qa.QuestionId);
                return questionAnswer;
            }
            ).OrderBy(question => question.SequenceNumber).ToList();
        }
    }
}
