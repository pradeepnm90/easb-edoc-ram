namespace Markel.GlobalRe.Service.Underwriting.Test.Helpers
{
    static class AppSettings
    {
        public static string BASEURL { get; } = "http://localhost:60090/";
        public static string HTTPGET { get; } = "Get";

		public static string HTTPPUT { get; } = "Put";
        public static string HTTPPATCH { get; } = "Patch";
        public static string HTTPPOST { get; } = "Post";
        public static string HTTPDELETE { get; } = "Delete";
    }
}
