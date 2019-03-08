using System.ComponentModel;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Enums
{
	public enum EntityType
	{
		Deals,
        DealStatusSummaries,
        SubDivisions,
        DealCoverages,
        // Lookups
        DealStatuses,
		CoverageBasis,
		Persons,
        PersonProfile,
        ContractTypes,
        ExposureTree,
        // Notes GRS-473
        Notes,
        UserViews,
        WritingCompany,
        //GRS-510
        AttachmentBasis,
        Cedants,
        NoteTypes,
        //DMS types
        FileNumber,
        DocID,
        Checklists
    }

	public enum EntityLockType
	{
		Unknown = 0,
		Deals = 1,
		MLEPL_Deals = 2,
		Locations = 3
	}

}
