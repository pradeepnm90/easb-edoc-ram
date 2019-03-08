
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_KeyDocuments : BaseGlobalReBusinessEntity
    {
        public int KeyDocid { get; set; } // keydocid (Primary key)
        public int? FileNumber { get; set; } // filenumber
        public int? Producer { get; set; } // producer
        public string Docid { get; set; } // docid
        public string DocName { get; set; } // docname
        public int SortOrder { get; set; } // sortorder
        public string Location { get; set; } // location
        public string Drawer { get; set; } // drawer
        public string Folderid { get; set; } // folderid
        public string FolderName { get; set; } // foldername
        public string DocType { get; set; } // doctype
        public string ErmsClassType { get; set; } // ermsclasstype
        public string FileType { get; set; } // filetype
        public string DmsPath { get; set; } // dmspath
        public int? ItemCategoryid { get; set; } // itemcategoryid
        public string ErmsCategory { get; set; } // ermscategory
        public string LastUpdatedUser { get; set; } // lastupdateduser
        public DateTime? LastTimeStamp { get; set; } // lasttimestamp
        public DateTime? DmsCreated { get; set; } // dmscreated
        public DateTime? DmsUpdated { get; set; } // dmsupdated
    }
}