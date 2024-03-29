﻿using CourseManagementSystem.API.ViewModels;
using CourseManagementSystem.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.API.Extensions
{
    /// <summary>
    /// extension methods for <see cref="IEnumerable{TestQuestion}"/>
    /// </summary>
    public static class TestQuestionEnumerableExtensions
    {
        /// <summary>
        /// convert enumerable of <see cref="TestQuestion"/> to enumerable of <see cref="TestQuestionVM"/>
        /// </summary>
        /// <param name="questions">enumerable to convert</param>
        /// <returns></returns>
        public static IEnumerable<TestQuestionVM> ToViewModels(this IEnumerable<TestQuestion> questions)
        {
            return questions.Select(q => new TestQuestionVM(q.Number, q.QuestionText, q.CorrectAnswer, q.Points, q.Type));
        }

        /// <summary>
        /// convert enumerable of <see cref="TestQuestionVM"/> to enumerable of <see cref="TestQuestion"/>
        /// </summary>
        /// <param name="questionsVMs">enumerable to convert</param>
        /// <returns></returns>
        public static IEnumerable<TestQuestion> ToModels(this IEnumerable<TestQuestionVM> questionsVMs)
        {
            return questionsVMs.Select(q => new TestQuestion(q.Number, q.QuestionText, q.CorrectAnswer, q.Points, q.Type));
        }
    }
}