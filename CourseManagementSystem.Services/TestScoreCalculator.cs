using CourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementSystem.Services
{
    public static class TestScoreCalculator
    {
        public static double GetScore(TestSubmission testSubmission)
        {
            int maximalScore = 0;
            int studentScore = 0;
            foreach (var answer in testSubmission.Answers)
            {
                maximalScore += answer.Question.Points;
                studentScore += answer.Points;
            }

            return (double)studentScore / maximalScore;
        }
    }
}
