using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private IFileDownloader _fileDownloader;

        public InstallerHelper(IFileDownloader fileDownloader)
        {
            _fileDownloader = fileDownloader;
        }
        

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                var url = string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName);

                _fileDownloader.Download(url, _setupDestinationFile);
                return true;
            }
            catch (WebException)
            {
                return false;
            }                        
        }
    }
}