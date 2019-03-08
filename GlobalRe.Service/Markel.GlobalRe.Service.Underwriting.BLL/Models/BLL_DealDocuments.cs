using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;

namespace Markel.GlobalRe.Underwriting.Service.BLL.Models
{
    public class BLL_DealDocuments : BaseGlobalReBusinessEntity
    {
        public class DealDocuments
        {
            public FileTypes[] Property { get; set; }
        }

        public class FileTypes
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Container { get; set; }
            public FolderContent[] Contents { get; set; }
            public FolderMetadata[] Metadata { get; set; }
        }

        public class FolderContent
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Container { get; set; }
            public DocumentContent[] Contents { get; set; }
            public DocumentMetadata[] Metadata { get; set; }
        }

        public class DocumentContent
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Container { get; set; }
            public object Contents { get; set; }
            public object Metadata { get; set; }
        }

        public class DocumentMetadata
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string ContainerLevel { get; set; }
        }

        public class FolderMetadata
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string ContainerLevel { get; set; }
        }
    }
}