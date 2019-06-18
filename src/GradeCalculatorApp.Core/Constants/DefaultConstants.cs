namespace GradeCalculatorApp.Core.Constants
{
    public static class DefaultConstants
    {
        public static string SuccessfulCreate { get; } = "Successfully created {0}";
        public static string SuccessfulUpdate { get; } = "Successfully updated {0} with Id {1}";
        public static string SuccessfulDelete { get; } = "Successfully deleted {0} with Id {1}";
        public static string SuccessfulLogIn { get; } = "Successfully logged in user with email {0}";
        public static string SuccessfulRegistered { get; } = "Successfully registered user with email {0}";
        
        public static string SuccessfulMap { get; } = "Successfully mapped {0} to {1}";
        
        public static string FailureCreate { get; } = "Failure in creating {0}";
        public static string FailureUpdate { get; } = "Failure in updating {0} with Id {1}";
        public static string FailureDelete { get; } = "Failure in deleting {0} with Id {1}";
        public static string FailureReadAll { get; } = "Failure in reading all {0}";
        public static string FailureRead { get; } = "Failure in reading {0} with Id {1}";
        public static string FailureLogIn { get; } = "Failure in logging in user with email {0}";
        public static string FailureRegister { get; } = "Failure in registering user with email {0}";
        public static string FailureMap { get; } = "Failure in mapping {0} to {1}";
        
        public static string ExceptionCreate { get; } = "Exception in creating {0}";
        public static string ExceptionUpdate { get; } = "Exception in updating {0} with Id {1}";
        public static string ExceptionDelete { get; } = "Exception in deleting {0} with Id {1}";
        public static string ExceptionReadAll { get; } = "Exception in reading all {0}";
        public static string ExceptionRead { get; } = "Exception in reading {0} with Id {1}";
        public static string ExceptionLogIn { get; } = "Exception in logging in user with email {0}";
        public static string ExceptionRegister { get; } = "Exception in registering user with email {0}";
        public static string ExceptionMap { get; } = "Exception in mapping {0} to {1}";
        
        
        public static string InvalidObject { get; } = "Invalid {0} object";
        public static string CurrentExists { get; } = "Sorry there already exist a current session semester. Kindly deactivate that first";
        public static string InvalidId { get; } = "Invalid ID passed";
        
        
    }
}