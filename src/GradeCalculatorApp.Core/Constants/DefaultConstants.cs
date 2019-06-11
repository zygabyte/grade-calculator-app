namespace GradeCalculatorApp.Core.Constants
{
    public static class DefaultConstants
    {
        public static string SuccessfulCreate { get; } = "Successfully created {0}";
        public static string SuccessfulUpdate { get; } = "Successfully updated {0} with Id {1}";
        public static string SuccessfulDelete { get; } = "Successfully deleted {0} with Id {1}";
        
        public static string FailureCreate { get; } = "Failure in creating {0}";
        public static string FailureUpdate { get; } = "Failure in updating {0} with Id {1}";
        public static string FailureDelete { get; } = "Failure in deleting {0} with Id {1}";
        public static string FailureReadAll { get; } = "Failure in reading all {0}";
        public static string FailureRead { get; } = "Failure in reading {0} with Id {1}";
        
        public static string ExceptionCreate { get; } = "Exception in creating {0}";
        public static string ExceptionUpdate { get; } = "Exception in updating {0} with Id {1}";
        public static string ExceptionDelete { get; } = "Exception in deleting {0} with Id {1}";
        public static string ExceptionReadAll { get; } = "Exception in reading all {0}";
        public static string ExceptionRead { get; } = "Exception in reading {0} with Id {1}";
        
        
        public static string InvalidObject { get; } = "Invalid {0} object";
        public static string CurrentExists { get; } = "Sorry there already exist a current session semester. Kindly deactivate that first";
        public static string InvalidId { get; } = "Invalid ID passed";
        
        
    }
}