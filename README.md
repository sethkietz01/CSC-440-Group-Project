# Project Overview
This application is a simulated grade management system for Eastern Kentucky University, which is the group project for CSC 440 (Applied Software Engineering) at EKU. The system features five main functionalities with an Object-Oriented Design:
1. Import Grade Records (Either from a .CSV file, or a folder containing one or more .CSV file(s))
2. Add Grade Record (Adds a grade record directly to the database)
3. Modify Grade Record (Changes the grade of an already-existing grade record)
4. Delete Grade Record (Removes a grade record from the database)
5. Print Transcript (Outputs the GPA and all grade records for a specified student)

This project is built using C# Windows forms .NET for front-end and back-end design, and MySQL to interact with the Computer Science Lab database at EKU.

# File Navigation
Each form is contained in its own .cs file. The driver form is MainMenu.cs and the most-utilized classes are DatabaseHandler (performs all interactions with the database) and Helper (provides input validation methods).

# Documentation
Our specification report for the system can be found here: [CSC 440 Group Project Specification Report](CSC%20440%20Group%20Project%20Specification%20Report.pdf)

# Contributors
[Leslie Cunningham](https://github.com/Pixxilated)  
[Seth Kietz](https://github.com/sethkietz01)
