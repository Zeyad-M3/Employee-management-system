
Project Management System

Overview

This project is a Project Management System designed to streamline task and employee management, activity tracking, and project oversight. Built using C# and SQL Server, it provides a desktop application to manage employees, projects, tasks, activities, and notes, with a focus on data persistence and security.
Features

✔ Employee Management: Add, update, and delete employee records with role-based access.

✔ Project Management: Create, track, and manage projects with milestones and tasks.

✔ Activity Tracking: Log and manage activity notes and categories for tasks.

✔ Task Assignment: Assign tasks to employees and track their progress.

✔ Credential Management: Securely store and retrieve user credentials using the Windows Registry with encryption support.

✔ Data Persistence: Utilize SQL Server for storing all data with relational integrity.

Usage

✔ Login: Enter your username and password in the login form. Check "Remember Me" to save credentials securely.

✔ Main Interface: After logging in, the main form loads employee data, tasks, projects, and activities.

✔ Management: Use the interface to add, update, or delete records, with automatic logging of deletions in DeletedEmployees and DeletedProjects.

Benefits

✔ Data Security: Credentials are stored encrypted in the Windows Registry, enhancing user privacy.

✔ Audit Trail: Deleted employees and projects are logged with timestamps, aiding in tracking and recovery.

✔ Efficiency: Automated triggers and stored procedures improve data management and reduce manual effort.

✔ Scalability: The relational database design supports growth in project and employee numbers.

✔ User-Friendly: Intuitive interface for managing complex project data.

✔ Error Handling: Robust exception handling in C# code prevents crashes and provides user feedback.


Video Demonstration

For a visual guide on how to use the application, check out this video:

[Project Management System Demo](https://drive.google.com/file/d/1GCPN9MJEpSkMbJK7rQCW5ayeu-yQzvQ7/view)
