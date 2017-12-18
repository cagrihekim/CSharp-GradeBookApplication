using GradeBook;
using GradeBook.Enums;
using GradeBook.GradeBooks;
using GradeBook.UserInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit;

namespace GradeBookTests
{
    /// <summary>
    ///     This class contains all tests related to the implimentation of adding support for Ranked Grading.
    ///     Note: Do not use these tests as example of good testing practices, due to the nature of how Pluralsight projects work
    ///     we have to create tests against code that doesn't exist and changes implimentation, due to this tests are fragile,
    ///     hard to maintain, and don't don't adhere to the "test just one thing" practice commonly used in production tests.
    /// </summary>
    public class RankedGradingTests
    {
        /// <summary>
        ///     Tests all requirements for creating the `GradeBookType` enum.
        /// </summary>
        [Fact(DisplayName = "Create GradeBookType enum @create-a-new-enum-gradebooktype")]
        [Trait("Category", "CreateGradeBookTypeEnum")]
        public void GradeBookTypeEnumTest()
        {
            // Test to make sure `GradeBookType.cs` is in the `Enums` directory.
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "GradeBook" + Path.DirectorySeparatorChar + "Enums" + Path.DirectorySeparatorChar + "GradeBookType.cs";
            Assert.True(File.Exists(filePath), "`GradeBookType.cs` was not found in the `Enums` folder.");

            // Test to make sure the enum `GradeBookType` exists in the `GradeBooks.Enums` namespace.
            var gradebookEnum = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                 from type in assembly.GetTypes()
                                 where type.FullName == "GradeBook.Enums.GradeBookType"
                                 select type).FirstOrDefault();
            Assert.True(gradebookEnum != null, "`GradeBook.Enums.GradeBookType` wasn't found in the `GradeBooks.Enums` namespace.");

            // Test to make sure the enum `GradeBookType` is public.
            Assert.True(gradebookEnum.IsPublic, "`GradeBook.Enums.GradeBookType` exists, but isn't `public`.");

            // Test to make sure that `GradeBookType` is an enum not a class
            Assert.True(gradebookEnum.IsEnum, "`GradeBook.Enums.GradeBookType` exists, but isn't an enum.");

            // Test that `GradeBookType` contains the value `Standard`
            Assert.True(Enum.Parse(gradebookEnum, "Standard", true) != null, "`GradeBook.Enums.GradeBookType` doesn't contain the value `Standard`.");

            // Test that `GradeBookType` contains the value `Ranked`
            Assert.True(Enum.Parse(gradebookEnum, "Ranked", true) != null, "`GradeBook.Enums.GradeBookType` doesn't contain the value `Ranked`.");

            // Test that `GradeBookType` contains the value `ESNU`
            Assert.True(Enum.Parse(gradebookEnum, "ESNU", true) != null, "`GradeBook.Enums.GradeBookType` doesn't contain the value `ESNU`.");

            // Test that `GradeBookType` contains the value `OneToFour`
            Assert.True(Enum.Parse(gradebookEnum, "OneToFour", true) != null, "`GradeBook.Enums.GradeBookType` doesn't contain the value `OneToFour`.");

            // Test that `GradeBookType` contains the value `SixPoint`
            Assert.True(Enum.Parse(gradebookEnum, "SixPoint", true) != null, "`GradeBook.Enums.GradeBookType` doesn't contain the value `SixPoint`.");
        }

        /// <summary>
        ///     Tests all requirements for adding the `Type` property.
        /// </summary>
        [Fact(DisplayName = "Add Type property to BaseGradeBook  @add-a-gradebooktype-property-to-basegradebook")]
        [Trait("Category", "AddTypeProperty")]
        public void AddTypePropertyTest()
        {
            // Assume all requirements of previous tasks were completed successfully
            var gradebookEnum = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                 from type in assembly.GetTypes()
                                 where type.FullName == "GradeBook.Enums.GradeBookType"
                                 select type).FirstOrDefault();

            // Test that the property `Type` exists in `BaseGradeBook`
            var typeProperty = typeof(BaseGradeBook).GetProperty("Type");
            Assert.True(typeProperty != null, "`GradeBook.GradeBooks.BaseGradeBook` doesn't contain a property `Type` or `Type` is not `public`.");

            // Test that the property `Type` is of type `GradeBookType`
            Assert.True(typeProperty.PropertyType == gradebookEnum, "`GradeBook.GradeBooks.BaseGradeBook` contains a property `Type` but it is not of type `GradeBookType`.");

            // Test that the property `Type`'s `get` method is `public`
            Assert.True(typeProperty.GetGetMethod() != null, "`GradeBook.GradeBooks.BaseGradeBook` contains a property `Type` but it's getter is not `public`.");

            // Test that the property `Type`'s 'set' method is `public`
            Assert.True(typeProperty.GetSetMethod() != null, "`GradeBook.GradeBooks.BaseGradeBook` contains a property `Type` but it's setter is not `public`.");
        }

        /// <summary>
        ///     Tests all requirements for creating the `StandardGradeBook` class.
        /// </summary>
        [Fact(DisplayName = "Create the StandardGradeBook class @create-the-standardgradebook-class")]
        [Trait("Category", "CreateStandardGradeBook")]
        public void CreateStandardGradeBookTest()
        {
            // Test if to make sure that `StandardGradeBook` is in the `GradeBooks` directory.
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "GradeBook" + Path.DirectorySeparatorChar + "GradeBooks" + Path.DirectorySeparatorChar + "StandardGradeBook.cs";
            Assert.True(File.Exists(filePath), "`StandardGradeBook.cs` was not found in the `GradeBooks` folder.");

            // Test if `StandardGradeBook` is in the `GradeBook.GradeBooks` namespace.
            var standardGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                     from type in assembly.GetTypes()
                                     where type.Name == "StandardGradeBook"
                                     select type).FirstOrDefault();
            Assert.True(standardGradeBook != null, "`GradeBook.GradeBooks.StandardGradeBook` doesn't exist.");

            // Test if `StandardGradeBook` is `public`.
            Assert.True(standardGradeBook.IsPublic, "`GradeBook.GradeBooks.StandardGradeBook` isn't public");

            // Test if `StandardGradeBook` is inheritting `BaseGradeBook`.
            Assert.True(standardGradeBook.BaseType == typeof(BaseGradeBook), "`GradeBook.GradeBooks.StandardGradeBook` doesn't inherit `BaseGradeBook`");

            // Test if `StandardGradeBook`'s constructor has the proper signature (consider both this task and later signature)
            var ctor = standardGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "No constructor found for GradeBook.GradeBooks.StardardGradeBook.");

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(standardGradeBook, "LoadTest", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(standardGradeBook, "LoadTest");
            Assert.True(gradeBook != null, "The constructor for GradeBook.GradeBooks.StandardGradeBook have the expected parameters.");

            // Test if `Type` is set to `GradeBookType.Standard`.
            var gradebookEnum = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                 from type in assembly.GetTypes()
                                 where type.FullName == "GradeBook.Enums.GradeBookType"
                                 select type).FirstOrDefault();
            Assert.True(gradebookEnum != null, "`GradeBook.Enums.GradeBookType` wasn't found in the `GradeBooks.Enums` namespace.");

            Assert.True(gradeBook.GetType().GetProperty("Type").GetValue(gradeBook).ToString() == Enum.Parse(gradebookEnum, "Standard", true).ToString(), "`Type` wasn't set to `GradeBookType.Standard` by the `GradeBook.GradeBooks.StandardGradeBook` Constructor.");
        }

        /// <summary>
        ///     Tests all requirements for creating the `RankedGradeBook` class.
        /// </summary>
        [Fact(DisplayName = "Create the RankedGradeBook class @create-the-rankedgradebook-class")]
        [Trait("Category", "CreateRankedGradeBook")]
        public void CreateRankedGradeBookTest()
        {
            // Test if to make sure that `RankedGradeBook` is in the `GradeBooks` directory.
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "GradeBook" + Path.DirectorySeparatorChar + "GradeBooks" + Path.DirectorySeparatorChar + "RankedGradeBook.cs";
            Assert.True(File.Exists(filePath), "`RankedGradeBook.cs` was not found in the `GradeBooks` folder.");

            // Test if `RankedGradeBook` is in the `GradeBook.GradeBooks` namespace.
            var rankedGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                     from type in assembly.GetTypes()
                                     where type.Name == "RankedGradeBook"
                                     select type).FirstOrDefault();
            Assert.True(rankedGradeBook != null, "`GradeBook.GradeBooks.RankedGradeBook` doesn't exist.");

            // Test if `RankedGradeBook` is `public`.
            Assert.True(rankedGradeBook.IsPublic, "`GradeBook.GradeBooks.RankedGradeBook` isn't public");

            // Test if `RankedGradeBook` is inheritting `BaseGradeBook`.
            Assert.True(rankedGradeBook.BaseType == typeof(BaseGradeBook), "`GradeBook.GradeBooks.RankedGradeBook` doesn't inherit `BaseGradeBook`");

            // Test if `RankedGradeBook`'s constructor has the proper signature (consider both this task and later signature)
            var ctor = rankedGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "No constructor found for GradeBook.GradeBooks.RankedGradeBook.");

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "LoadTest", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "LoadTest");
            Assert.True(gradeBook != null, "The constructor for GradeBook.GradeBooks.RankedGradeBook have the expected parameters.");

            // Test if `Type` is set to `GradeBookType.Ranked`.
            var gradebookEnum = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                 from type in assembly.GetTypes()
                                 where type.FullName == "GradeBook.Enums.GradeBookType"
                                 select type).FirstOrDefault();
            Assert.True(gradebookEnum != null, "`GradeBook.Enums.GradeBookType` wasn't found in the `GradeBooks.Enums` namespace.");

            Assert.True(gradeBook.GetType().GetProperty("Type").GetValue(gradeBook).ToString() == Enum.Parse(gradebookEnum, "Ranked", true).ToString(), "`Type` wasn't set to `GradeBookType.Ranked` by the `GradeBook.GradeBooks.RankedGradeBook` Constructor.");
        }

        /// <summary>
        ///     Tests all requipments for adding multiple `GradeBookType` support to `BaseGradeBook`
        /// </summary>
        [Fact(DisplayName = "Add multiple GradeBookType support to BaseGradeBook @add-multiple-gradebooktype-support-to-basegradebook")]
        [Trait("Category","AddMultipleGradeBookTypeSupportToBaseGradeBook")]
        public void AddMultipleGradeBookTypeSupportToBaseGradeBookTest()
        {
            var gradebookEnum = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                 from type in assembly.GetTypes()
                                 where type.FullName == "GradeBook.Enums.GradeBookType"
                                 select type).FirstOrDefault();
            Assert.True(gradebookEnum != null, "GradeBook.Enums.GradeBookType doesn't exist.");

            // Test if when the `GradeBookType` is `Standard` the matching `StandardGradeBook` object is returned.
            var standardGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                     from type in assembly.GetTypes()
                                     where type.Name == "StandardGradeBook"
                                     select type).FirstOrDefault();
            Assert.True(standardGradeBook != null, "`GradeBook.GradeBooks.StandardGradeBook` doesn't exist.");

            var ctor = standardGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "`GradeBook.GradeBooks.StandardGradeBook` doesn't appear to have a constructor.");

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(standardGradeBook, "LoadTest", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(standardGradeBook, "LoadTest");
            Assert.True(gradeBook != null, "The constructor for `GradeBook.GradeBooks.StandardGradeBook` doesn't have the expected parameters.");

            Assert.True(gradeBook.GetType().GetProperty("Type") != null, "`GradeBook.GradeBooks.BaseGradeBook` doesn't appear to have a `Type` property.");
            gradeBook.GetType().GetProperty("Type").SetValue(gradeBook, Enum.Parse(gradebookEnum, "Standard", true));

            try
            {
                using (var file = new FileStream("LoadTest.gdbk", FileMode.Create, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        var json = JsonConvert.SerializeObject(gradeBook);
                        writer.Write(json);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.True(ex != null, "Test for GradeBook.GradeBooks.BaseGradeBook.Load was unable to run. This is likely due to issues being able to read/write gradebook files to the local file system.");
            }

            var actual = BaseGradeBook.Load("LoadTest");
            File.Delete("LoadTest.gdbk");
            Assert.True((string)actual.GetType().GetProperty("Name").GetValue(gradeBook) == "LoadTest", "`GradeBook.GradeBooks.BaseGradeBook.Load` did not properly load when the gradebook is a `StandardGradeBook`.");
            Assert.True(actual.GetType() == standardGradeBook, "`GradeBook.GradeBooks.BaseGradeBook.Load` did not properly create a `StandardGradeBook` when the loaded gradebook is a `StandardGradeBook`.");
            Assert.True(actual.GetType().GetProperty("Type").GetValue(actual).ToString() == Enum.Parse(gradebookEnum, "Standard", true).ToString(), "`GradeBook.GradeBooks.BaseGradeBook.Load` did not properly set the `Type` property of gradebook to `Standard` when the gradebook is a `StandardGradeBook`.");

            // Test if when the `GradeBookType` isn't set the matching `StandardGradeBook` object is returned.
            var esnuGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                     from type in assembly.GetTypes()
                                     where type.Name == "ESNUGradeBook"
                                     select type).FirstOrDefault();

            if (esnuGradeBook == null)
            {
                parameters = ctor.GetParameters();
                gradeBook = null;
                if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                    gradeBook = Activator.CreateInstance(standardGradeBook, "LoadTest", true);
                else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                    gradeBook = Activator.CreateInstance(standardGradeBook, "LoadTest");
                Assert.True(gradeBook != null, "The constructor for `GradeBook.GradeBooks.StandardGradeBook` doesn't have the expected parameters.");

                Assert.True(gradeBook.GetType().GetProperty("Type") != null, "`GradeBook.GradeBooks.BaseGradeBook` doesn't appear to have a `Type` property.");
                gradeBook.GetType().GetProperty("Type").SetValue(gradeBook, Enum.Parse(gradebookEnum, "ESNU", true));


                try
                {
                    using (var file = new FileStream("LoadTest.gdbk", FileMode.Create, FileAccess.Write))
                    {
                        using (var writer = new StreamWriter(file))
                        {
                            var json = JsonConvert.SerializeObject(gradeBook);
                            writer.Write(json);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.True(ex != null, "Test for GradeBook.GradeBooks.BaseGradeBook.Load was unable to run. This is likely due to issues being able to read/write gradebook files to the local file system.");
                }
         
                try
                {
                    BaseGradeBook.Load("LoadTest");
                    Assert.True(1 == 1, "`GradeBook.GradeBooks.BaseGradeBook.Load` didn't throw an `InvalidOperationException` when the `GradeBookType` was not yet implimented.");
                }
                catch (Exception ex)
                {
                    Assert.True(ex.GetType() == typeof(InvalidOperationException), "`GradeBook.GradeBooks.BaseGradeBook.Load` threw an exception, however; it was not an `InvalidOperationException`.");
                    Assert.True(ex.Message == "The gradebook you've attempted to load is not in a supported type of gradebook.", "`GradeBook.GradeBooks.BaseGradeBook`'s `Load` method threw the proper exception type, but didn't have the message \"The gradebook you've attempted to load is not in a supported type of gradebook.\"");
                }
                File.Delete("LoadTest.gdbk");
            }

            // Test if when the `GradeBookType` is `Ranked` the matching `RankedGradeBook` object is returned.
            var rankedGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                     from type in assembly.GetTypes()
                                     where type.Name == "RankedGradeBook"
                                     select type).FirstOrDefault();
            Assert.True(rankedGradeBook != null, "`GradeBook.GradeBooks.RankedGradeBook` doesn't exist.");

            ctor = rankedGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "`GradeBook.GradeBooks.RankedGradeBook` doesn't appear to have a constructor.");

            parameters = ctor.GetParameters();
            gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "LoadTest", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "LoadTest");
            Assert.True(gradeBook != null, "The constructor for `GradeBook.GradeBooks.RankedGradeBook` doesn't have the expected parameters.");

            gradeBook.GetType().GetProperty("Type").SetValue(gradeBook, Enum.Parse(gradebookEnum, "Ranked", true));

            try
            {
                using (var file = new FileStream("LoadTest.gdbk", FileMode.Create, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        var json = JsonConvert.SerializeObject(gradeBook);
                        writer.Write(json);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.True(ex != null, "Test for GradeBook.GradeBooks.BaseGradeBook.Load was unable to run. This is likely due to issues being able to read/write gradebook files to the local file system.");
            }

            actual = BaseGradeBook.Load("LoadTest");
            File.Delete("LoadTest.gdbk");
            Assert.True((string)actual.GetType().GetProperty("Name").GetValue(gradeBook) == "LoadTest", "`GradeBook.GradeBooks.BaseGradeBook.Load` did not properly load when the gradebook is a `RankedGradeBook`.");
            Assert.True(actual.GetType() == rankedGradeBook, "`GradeBook.GradeBooks.BaseGradeBook.Load` did not properly create a `RankedGradeBook` when the loaded gradebook is a `RankedGradeBook`.");
            Assert.True(actual.GetType().GetProperty("Type").GetValue(actual).ToString() == Enum.Parse(gradebookEnum, "Ranked", true).ToString(), "`GradeBook.GradeBooks.BaseGradeBook.Load` did not properly set the `Type` property of gradebook to `Ranked` when the gradebook is a `RankedGradeBook`.");
        }

        /// <summary>
        ///     Tests all functionality related to overriding `LetterGrade`.
        /// </summary>
        [Fact(DisplayName = "Add multiple GradeBookType support to BaseGradeBook @override-rankedgradebook-s-getlettergrade")]
        [Trait("Category","OverrideGetLetterGrade")]
        public void OverrideGetLetterGradeTest()
        {
            //Setup Test
            var rankedGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                   from type in assembly.GetTypes()
                                   where type.Name == "RankedGradeBook"
                                   select type).FirstOrDefault();
            Assert.True(rankedGradeBook != null, "GradeBook.GradeBooks.RankedGradeBook doesn't exist.");

            var ctor = rankedGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "No constructor found for GradeBook.GradeBooks.RankedGradeBook.");

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook");
            Assert.True(gradeBook != null, "The constructor for GradeBook.GradeBooks.RankedGradeBook have the expected parameters.");

            MethodInfo method = rankedGradeBook.GetMethod("GetLetterGrade");

            //Test if exception is thrown when there are less than 5 students.
            var exception = Record.Exception(() => method.Invoke(gradeBook, new object[] { 100 }));
            Assert.True(exception != null, "`GradeBook.GradeBooks.RankedGradeBook.GetLetterGrade` didn't throw an exception when less than 5 students have grades.");

            //Setup successful conditions
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

            //Test if A is given when input grade is in the top 20%.
            Assert.True((char)method.Invoke(gradeBook, new object[] { 100 }) == 'A',"`GradeBook.GradeBooks.RankedGradeBook.GetLetterGrade` didn't give an A to students in the top 20% of the class.");

            //Test if B is given when input grade is between the top 20 and 40%.
            Assert.True((char)method.Invoke(gradeBook, new object[] { 75 }) == 'B', "`GradeBook.GradeBooks.RankedGradeBook.GetLetterGrade` didn't give an B to students between the top 20 and 40% of the class.");

            //Test if C is given when input grade is between the top 40 and 60%.
            Assert.True((char)method.Invoke(gradeBook, new object[] { 50 }) == 'C', "`GradeBook.GradeBooks.RankedGradeBook.GetLetterGrade` didn't give an C to students between the top 40 and 60% of the class.");

            //Test if D is given when input grade is between the top 60 and 80%.
            Assert.True((char)method.Invoke(gradeBook, new object[] { 25 }) == 'D', "`GradeBook.GradeBooks.RankedGradeBook.GetLetterGrade` didn't give an D to students between the top 60 and 80% of the class.");

            //Test if F is given when input grade is below the top 80%.
            Assert.True((char)method.Invoke(gradeBook, new object[] { 0 }) == 'F', "`GradeBook.GradeBooks.RankedGradeBook.GetLetterGrade` didn't give an F to students below the top 80% of the class.");
        }

        /// <summary>
        ///     Tests all functionality arround overriding `CalculateStatistics`.
        /// </summary>
        [Fact(DisplayName = "Override CalculateStatistics @override-calculatestatistics")]
        [Trait("Category","OverrideCalculateStatistics")]
        public void OverrideCalculateStatisticsTest()
        {
            //Setup Test
            var rankedGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                   from type in assembly.GetTypes()
                                   where type.FullName == "GradeBook.GradeBooks.RankedGradeBook"
                                   select type).FirstOrDefault();
            Assert.True(rankedGradeBook != null, "GradeBook.GradeBooks.RankedGradeBook doesn't exist.");

            var ctor = rankedGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "No constructor found for GradeBook.GradeBooks.RankedGradeBook.");

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook");
            Assert.True(gradeBook != null, "The constructor for GradeBook.GradeBooks.RankedGradeBook have the expected parameters.");

            MethodInfo method = rankedGradeBook.GetMethod("CalculateStatistics");
            var output = string.Empty;
            Console.Clear();

            //Test that message was written to console when there are less than 5 students.
            using (var consolestream = new StringWriter())
            {
                Console.SetOut(consolestream);
                method.Invoke(gradeBook, null);
                output = consolestream.ToString().ToLower();
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);

            Assert.True(output.Contains("5 students") || output.Contains("five students"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStatistics` didn't respond with 'Ranked grading requires at least 5 students.' when there were less than 5 students.");

            //Test that the base calculate statistics didn't still run when there were less than 5 students.
            Assert.True(!output.Contains("average grade of all students is"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStastics` still ran the base `CalculateStatistics` when there was less than 5 students.");

            //Test that the base calculate statistics did run when there were 5 or more students.
            output = string.Empty;
            Console.Clear();

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

            using (var consolestream = new StringWriter())
            {
                Console.SetOut(consolestream);
                method.Invoke(gradeBook, null);
                output = consolestream.ToString().ToLower();
            }
            standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);

            Assert.True(output.Contains("average grade of all students is"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStastics` did not run the base `CalculateStatistics` when there was 5 or more students.");
        }

        /// <summary>
        ///     Tests all functionality related to overriding `CalculateStudentStatistics`.
        /// </summary>
        [Fact(DisplayName = "Override CalculateStudentsStatistics @override-calculatestudentstatistics")]
        [Trait("Category", "OverrideCalculateStudentStatistics")]
        public void OverrideCalculateStudentStatisticsTest()
        {
            //Setup Test
            var rankedGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                   from type in assembly.GetTypes()
                                   where type.FullName == "GradeBook.GradeBooks.RankedGradeBook"
                                   select type).FirstOrDefault();
            Assert.True(rankedGradeBook != null, "GradeBook.GradeBooks.RankedGradeBook doesn't exist.");

            var ctor = rankedGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "No constructor found for GradeBook.GradeBooks.RankedGradeBook.");

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook");
            Assert.True(gradeBook != null, "The constructor for GradeBook.GradeBooks.RankedGradeBook have the expected parameters.");

            MethodInfo method = rankedGradeBook.GetMethod("CalculateStudentStatistics");
            var output = string.Empty;
            Console.Clear();

            var students = new List<Student>
                {
                    new Student("jamie",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 100 }
                    }
                };

            gradeBook.GetType().GetProperty("Students").SetValue(gradeBook, students);

            //Test that message was written to console when there are less than 5 students.
            using (var consolestream = new StringWriter())
            {
                Console.SetOut(consolestream);
                method.Invoke(gradeBook, new object[] { "jamie" });
                output = consolestream.ToString().ToLower();
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);

            Assert.True(output.Contains("5 students") || output.Contains("five students"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStudentStatistics` didn't respond with 'Ranked grading requires at least 5 students.' when there were less than 5 students.");

            //Test that the base calculate statistics didn't still run when there were less than 5 students.
            Assert.True(!output.Contains("grades:"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStudentStastics` still ran the base `CalculateStudentStatistics` when there was less than 5 students.");

            //Test that the base calculate statistics did run when there were 5 or more students.
            output = string.Empty;
            Console.Clear();

            students = new List<Student>
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

            using (var consolestream = new StringWriter())
            {
                Console.SetOut(consolestream);
                method.Invoke(gradeBook, new object[] { "jamie" });
                output = consolestream.ToString().ToLower();
            }
            standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);

            Assert.True(output.Contains("grades:"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStudentStastics` did not run the base `CalculateStudentStatistics` when there was 5 or more students.");
        }


        /// <summary>
        ///     Tests all functionality related to updateding the `CreateCommand` to work with multiple types. WARNING! this test accesses a loop so could potentially get stuck in a loop if there is a problem.
        /// </summary>
        [Fact(DisplayName = "Update StartingUserInterface's CreateCommand Method @update-startinguserinterface-s-createcommand-method")]
        [Trait("Category","UpdateCreateCommand")]
        public void UpdateCreateCommandTest()
        {
            //Bypass Test if Create Command for Weighted GPA has been started
            var rankedGradeBook = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                   from type in assembly.GetTypes()
                                   where type.FullName == "GradeBook.GradeBooks.RankedGradeBook"
                                   select type).FirstOrDefault();
            Assert.True(rankedGradeBook != null, "GradeBook.GradeBooks.RankedGradeBook doesn't exist.");

            var ctor = rankedGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "No constructor found for GradeBook.GradeBooks.RankedGradeBook.");

            var parameters = ctor.GetParameters();
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                return;

            //Setup Test
            var output = string.Empty;
            Console.Clear();
            using (var consoleInputStream = new StringReader("close"))
            {
                Console.SetIn(consoleInputStream);
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    StartingUserInterface.CreateCommand("create test");
                    output = consolestream.ToString().ToLower();
                }
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);
            StreamReader standardInput = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(standardInput);

            //Test that message written to console when parts.length != 3.
            Assert.True(output.Contains("command not valid"), "`GradeBook.UserInterfaces.StartingUserInterface` didn't write a message to the console when the create command didn't contain both a name and type.");

            //Test that message written to console is correct.
            Assert.True(output.Contains("command not valid, create requires a name and type of gradebook."), "`GradeBook.UserInterfaces.StartingUserInterface` didn't write 'Command not valid, Create requires a name and type of gradebook.' to the console when the create command didn't contain both a name and type.");

            //Test that `CreateCommand` escapes returns without setting the gradebook when parts.Length != 3.
            Assert.True(!output.Contains("created gradebook"), "`GradeBook.UserInterfaces.StartingUserInterface` still created a gradebook when the create command didn't contain both a name and type.");

            //Test that a `StandardGradeBook` is created with the correct name when value is "standard".
            output = string.Empty;
            Console.Clear();
            using (var consoleInputStream = new StringReader("close"))
            {
                Console.SetIn(consoleInputStream);
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    StartingUserInterface.CreateCommand("create test standard");
                    output = consolestream.ToString().ToLower();
                }
            }
            standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);
            standardInput = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(standardInput);

            Assert.True(output.Contains("standard"), "`GradeBook.UserInterfaces.StartingUserInterface` didn't create a `StandardGradeBook` when 'standard' was used with the `CreateCommand`.");

            //Test that a `RankedGradeBook` is created with the correct name when value is "ranked".
            output = string.Empty;
            Console.Clear();
            using (var consoleInputStream = new StringReader("close"))
            {
                Console.SetIn(consoleInputStream);
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    StartingUserInterface.CreateCommand("create test ranked");
                    output = consolestream.ToString().ToLower();
                }
            }
            standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);
            standardInput = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(standardInput);

            Assert.True(output.Contains("ranked"), "`GradeBook.UserInterfaces.StartingUserInterface` didn't create a `RankedGradeBook` when 'ranked' was used with the `CreateCommand`.");

            //Test that the correct message is written to console when value isn't handled.
            output = string.Empty;
            Console.Clear();
            using (var consolestream = new StringWriter())
            {
                Console.SetOut(consolestream);
                StartingUserInterface.CreateCommand("create test incorrect");
                output = consolestream.ToString().ToLower();
            }
            standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);

            Assert.True(output.Contains("incorrect is not a supported type of gradebook, please try again"), "`GradeBook.UserInterfaces.StartingUserInterface` write the entered type followed by ' is not a supported type of gradebook, please try again' when an unknown value was used with the `CreateCommand`.");
        }

        /// <summary>
        ///     Tests Help Command update to ensure all changes were made correctly.
        /// </summary>
        [Fact(DisplayName = "Update StartingUserInterface's HelpCommand Method @update-startinguserinterface-s-helpcommand-method")]
        [Trait("Category","UpdateHelpCommand")]
        public void UpdateHelpCommandTest()
        {
            //Setup Test
            var output = string.Empty;
            Console.Clear();
            using (var consoleInputStream = new StringReader("close"))
            {
                Console.SetIn(consoleInputStream);
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    StartingUserInterface.HelpCommand();
                    output = consolestream.ToString().ToLower();
                }
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            Console.SetOut(standardOutput);
            StreamReader standardInput = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(standardInput);

            // If help command is updated for weighted GPA bypass test
            if (output.Contains("create 'name' 'type' 'weighted' - creates a new gradebook where 'name' is the name of the gradebook, 'type' is what type of grading it should use, and 'weighted' is whether or not grades should be weighted (true or false)."))
                return;

            // Test if help command message is correct
            Assert.True(output.Contains("create 'name' 'type' - creates a new gradebook where 'name' is the name of the gradebook and 'type' is what type of grading it should use."), "`GradeBook.UserInterfaces.StartingUserInterface.HelpCommand` didn't write \"Create 'Name' 'Type' - Creates a new gradebook where 'Name' is the name of the gradebook and 'Type' is what type of grading it should use.\"");
        }

        /// <summary>
        ///     Tests if `BaseGradeBook` is abstract.
        /// </summary>
        [Fact(DisplayName = "Make BaseGradeBook Abstract @make-basegradebook-abstract")]
        [Trait("Category","MakeBaseGradeBookAbstract")]
        public void MakeBaseGradeBookAbstract()
        {
            // Test if `BaseGradeBook` is abstract.
            Assert.True(typeof(BaseGradeBook).IsAbstract == true, "`GradeBook.GradeBooks.BaseGradeBook` is not abstract.");
        }
    }
}
