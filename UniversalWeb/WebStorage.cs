using Microsoft.Extensions.FileProviders;
namespace UniversalWeb
{
    public static class WebStorage
    {
        public const string DireName = @"Storage";
        public const string RequestName = @"Ftp";

        private static string _checkSlashPrefix(string path)
        {
            if(path.StartsWith(@"/"))
            {
                return path;
            }
            else { return @"/" + path; }
        }

        private static PhysicalFileProvider _getFileLocation(IWebHostEnvironment env,string DirePath) =>
            new PhysicalFileProvider(env.ContentRootPath + _checkSlashPrefix(DirePath));

        public static StaticFileOptions UseOtherStaticFilesOptions(IWebHostEnvironment environment) =>
            UseOtherStaticFilesOptions(environment, DireName);
        public static StaticFileOptions UseOtherStaticFilesOptions(IWebHostEnvironment environment,string DirePath) =>
            new StaticFileOptions() { RequestPath=_checkSlashPrefix(RequestName),FileProvider=_getFileLocation(environment,DirePath)};

        public static DirectoryBrowserOptions UseBrowserDirectoryOptions(IWebHostEnvironment environment) =>
            UseBrowserDirectoryOptions(environment, DireName);
        public static DirectoryBrowserOptions UseBrowserDirectoryOptions(IWebHostEnvironment environment, string DirePath) =>
            new DirectoryBrowserOptions() { RequestPath = _checkSlashPrefix(RequestName), FileProvider = _getFileLocation(environment, DirePath) };
    }
}
