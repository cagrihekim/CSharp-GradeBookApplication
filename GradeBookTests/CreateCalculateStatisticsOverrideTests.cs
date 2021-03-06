﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GradeBook;
using GradeBook.Enums;
using Xunit;

namespace GradeBookTests
{
    public class CreateCalculateStatisticsOverrideTests
    {
        /// <summary>
        ///     All tests related to the "Create CalculateStatistics Override" Task.
        /// </summary>
        [Fact(DisplayName = "Create CalculateStatistics Override @create-override-calculatestatistics")]
        public void OverrideCalculateStatisticsTest()
        {
            //Setup Test
            var rankedGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                   from type in assembly.GetTypes()
                                   where type.FullName == "GradeBook.GradeBooks.RankedGradeBook"
                                   select type).FirstOrDefault();

            var ctor = rankedGradeBook.GetConstructors().FirstOrDefault();

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook");

            MethodInfo method = rankedGradeBook.GetMethod("CalculateStatistics");
            var output = string.Empty;
            Console.Clear();
            try
            {
                //Test that message was written to console when there are less than 5 students.
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    method.Invoke(gradeBook, null);
                    output = consolestream.ToString().ToLower();

                    Assert.True(output.Contains("5 students") || output.Contains("five students"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStatistics` didn't respond with 'Ranked grading requires at least 5 students.' when there were less than 5 students.");

                    //Test that the base calculate statistics didn't still run when there were less than 5 students.
                    Assert.True(!output.Contains("average grade of all students is"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStastics` still ran the base `CalculateStatistics` when there was less than 5 students.");
                }
            }
            finally
            {
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                Console.SetOut(standardOutput);
            }

            var students = new List<Student>
                {
                    new Student("jamie",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 100 }
                    },
                    new Student("john",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 75 }
                    },
                    new Student("jackie",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 50 }
                    },
                    new Student("tom",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 25 }
                    },
                    new Student("tony",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 0 }
                    }
                };

            gradeBook.GetType().GetProperty("Students").SetValue(gradeBook, students);

            //Test that the base calculate statistics did run when there were 5 or more students.
            output = string.Empty;
            Console.Clear();

            try
            {
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    method.Invoke(gradeBook, null);
                    output = consolestream.ToString().ToLower();

                    Assert.True(output.Contains("average grade of all students is"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStastics` did not run the base `CalculateStatistics` when there was 5 or more students.");
                }
            }
            finally
            {
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                Console.SetOut(standardOutput);
            }
        }
    }
}
