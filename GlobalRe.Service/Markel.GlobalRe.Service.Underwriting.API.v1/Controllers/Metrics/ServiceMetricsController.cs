using Markel.GlobalRe.Service.Underwriting.API.v1.Models.Metrics;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Notifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers.Metrics
{
    [Authorize]
    [CacheOutput(NoCache = true)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix(RouteHelper.ServiceMetricsRoutePrefix)]
    public class ServiceMetricsController : BaseApiController
    {
        #region Constants

        // Temp file rule for manually deleting file: Must be older than a certain date (one day)
        private static TimeSpan MANUAL_DELETE_THRESHOLD = new TimeSpan(1, 0, 0, 0);

        private static string[] TEMP_PATHS =
        {
            string.Format(@"{0}\staging", MarkelConfiguration.TempFilePath)
        };

        #endregion Constants

        #region Properties

        private ICacheStoreManager CacheManager { get; set; }

        #endregion Properties

        #region Constructor

        public ServiceMetricsController(IUserManager userManager, ICacheStoreManager cacheManager) : base(userManager)
        {
            CacheManager = ValidateObject(cacheManager);
        }

        #endregion Constructor

        #region Methods

        [Route("")]
        [ResponseType(typeof(APIServiceMetrics))]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new APIServiceMetrics()
            {
                ServiceMetrics = ServiceMetrics.GetMetrics(),
                DataSource = GetDataSource(),
                ServerVersion = IOHelper.GetServerVersion(),
                CacheMetrics = CacheManager.GetMetrics(),
                ServerStorageInfo = GetServerStorageInfo()
            });
        }

        #endregion Methods

        #region Helper Methods

        private string GetDataSource()
        {
            string connectionString = UserIdentity.GetConnectionString();

            if (!string.IsNullOrEmpty(connectionString))
            {
                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
                return sqlConnectionStringBuilder.DataSource;
            }

            return string.Empty;
        }

        private dynamic GetServerStorageInfo()
        {
            try
            {
                List<object> tempFileList = new List<object>();

                // TEMP_PATHS
                Queue<DirectoryInfo> tempPathList = new Queue<DirectoryInfo>();
                foreach (string tempPath in TEMP_PATHS)
                {
                    tempPathList.Enqueue(new DirectoryInfo(tempPath));
                }

                string tempBasePath = MarkelConfiguration.TempFilePath;

                long totalTempFileBytes = 0;
                while (tempPathList.Count > 0)
                {
                    var tempPath = tempPathList.Dequeue();

                    if (tempPath.Exists)
                    {
                        foreach (var directoryInfo in tempPath.GetDirectories())
                        {
                            tempPathList.Enqueue(directoryInfo);
                        }

                        foreach (FileInfo fileInfo in tempPath.GetFiles())
                        {
                            totalTempFileBytes += fileInfo.Length;
                            tempFileList.Add(new
                            {
                                DirectoryName = fileInfo.DirectoryName.Replace(tempBasePath, ""),
                                Name = fileInfo.Name,
                                FullName = fileInfo.FullName,
                                DateModified = fileInfo.LastWriteTime,
                                Size = fileInfo.Length,
                                CanDelete = (fileInfo.LastWriteTime < DateTime.Now.Subtract(MANUAL_DELETE_THRESHOLD))
                            });
                        }
                    }
                }

                DriveInfo tempDriveInfo = DriveInfo.GetDrives().FirstOrDefault(d => d.RootDirectory.Name == Directory.GetParent(MarkelConfiguration.TempFilePath).Root.Name);

                return new
                {
                    TempBasePath = tempBasePath,
                    TempFiles = tempFileList,
                    TotalTempFileBytes = totalTempFileBytes,
                    DriveInfo = new
                    {
                        Name = tempDriveInfo.Name,
                        TotalFreeSpace = tempDriveInfo.TotalFreeSpace
                    }
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Exception = ex.ToString()
                };
            }
        }

        #endregion Helper Methods
    }
}