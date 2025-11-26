using Microsoft.AspNetCore.Http;

namespace SalesService.Helpers
{
    public static class SessionHelper
    {
        // Employee Info
        public static void SetEmployeeId(ISession session, string employeeId)
        {
            session.SetString("EmployeeId", employeeId);
        }

        public static string GetEmployeeId(ISession session)
        {
            return session.GetString("EmployeeId");
        }

        public static void SetUsername(ISession session, string username)
        {
            session.SetString("Username", username);
        }

        public static string GetUsername(ISession session)
        {
            return session.GetString("Username");
        }

        // Company Info
        public static void SetCompany(ISession session, string company)
        {
            session.SetString("Company", company);
        }

        public static string GetCompany(ISession session)
        {
            return session.GetString("Company");
        }

        // Database Info
        public static void SetDatabase(ISession session, string database)
        {
            session.SetString("Database", database);
        }

        public static string GetDatabase(ISession session)
        {
            return session.GetString("Database");
        }

        // Connection String
        public static void SetConnectionString(ISession session, string connectionString)
        {
            session.SetString("ConnectionString", connectionString);
        }

        public static string GetConnectionString(ISession session)
        {
            return session.GetString("ConnectionString");
        }

        // Check if logged in
        public static bool IsLoggedIn(ISession session)
        {
            return !string.IsNullOrEmpty(session.GetString("EmployeeId"));
        }

        // Clear all session
        public static void Clear(ISession session)
        {
            session.Clear();
        }
    }
}