﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC_440_Group_Project
{
    /// <summary>
    /// Data validation methods for input and calculations
    /// </summary>
    internal class Helper
    {
        /// <summary>
        /// Ensures that all fields in the given form are valid.
        /// </summary>
        /// <param name="studentId">The contents of the Student ID field.</param>
        /// <param name="coursePrefix">The contents of the Course Prefix field.</param>
        /// <param name="courseNumber">The contents of the Course Number field.</param>
        /// <param name="grade">The contents of the Grade field.</param>
        /// <param name="year">The contents of the Year field.</param>
        /// <param name="semester">The contents of the Semester field.</param>
        /// <returns>A tuple where [0] is whether all input is valid (true/false) and [1] is the error message to display if isValid is false.</returns>
        public static (bool isValid, string errorMessage) validateModifyRecordForms(string studentID, string coursePrefix, string courseNumber, string grade, string year, string semester)
        {
            // Ensure all fields have data
            if (string.IsNullOrEmpty(studentID) || string.IsNullOrEmpty(coursePrefix) || string.IsNullOrEmpty(courseNumber) 
                || string.IsNullOrEmpty(grade) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(semester))
                return (false, "One or more fields were left empty.");

            // Ensure the Student ID is valid
            if (studentID.Length != 9)
                return (false, "Student ID is invalid: \nStudent ID must be exactly 9 digits in length.");
            if (!studentID.StartsWith("901"))
                return (false, "Student ID is invalid: \nStudent ID must begin with 901.");
            if (!studentID.All(char.IsDigit))
                return (false, "Student ID is invalid: \nStudent ID must only contain digits.");

            // Ensure the Course Prefix is valid
            if (coursePrefix.Length != 3)
                return (false, "Course Prefix is invalid: \nCourse Prefix must be exactly 3 characters in length.");
            if (coursePrefix.Any(char.IsDigit))
                return (false, "Course Prefix is invalid: \nCourse Prefix must only contain letters (no digits).");
            if (!coursePrefix.ToUpper().Equals(coursePrefix))
                return (false, "Course Prefix is invalid: \nCourse Prefix must be all upper case.");

            // Ensure the Course Number is valid
            if (courseNumber.Length != 3)
                return (false, "Course Number is invalid: \nCourse Number must be exactly 3 digits in length.");
            if (!courseNumber.All(char.IsDigit))
                return (false, "Course Number is invalid: \nCourse Number must only contain digits.");

            // Ensure the Grade is valid
            if (!grade.Equals("A") && !grade.Equals("B") && !grade.Equals("C") && !grade.Equals("D") && !grade.Equals("F"))
                return (false, "Grade is invalid: \nGrade must be a single, capital letter in [A, B, C, D, F]");

            // Ensure the Year is valid
            if (year.Length != 4)
                return (false, "Year is invalid: \nYear must be exactly 4 digits in length.");
            if (!year.All(char.IsDigit))
                return (false, "Year is invalid: \nYear must only contain digits.");

            // Ensure the Semester is valid
            if (!semester.Equals("Spring") && !semester.Equals("Summer") && !semester.Equals("Fall") && !semester.Equals("Winter"))
                return (false, "Semester is invalid: \nSemster must exactly match one of the following: [Spring, Summer, Fall, Winter]");

            // Default case
            // Should only be reached if the form is valid
            return (true, "");
        }
    }
}
