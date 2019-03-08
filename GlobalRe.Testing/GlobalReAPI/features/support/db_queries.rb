require 'nokogiri'

class DBQueries
  def initialize
    dbQFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","DBQueries.xml")
    @@dbqDoc = Nokogiri::XML(File.open(dbQFile))
  end
  def getDealCountByStatusquery
    return @@dbqDoc.search("DealCountByStatus").text

  end
  def getGlobalReDataViewquery
    return @@dbqDoc.search("GlobalReDataView").text
  end
  def getBoundDealByStatusName
    @queryvalue = @@dbqDoc.search("BoundDealByStatusName").text
  end
  def getOnHoldDealByStatusName
    return @@dbqDoc.search("OnHoldDealByStatusName").text
  end
  def getBoundDealCountByStatusName
    return @@dbqDoc.search("BoundDealCountByStatusName").text
  end
  def getOnHoldDealCountByStatusName
    return @@dbqDoc.search("OnHoldDealCountByStatusName").text
  end
  def getInProgressDealCountByStatusName
    return @@dbqDoc.search("InProgressDealCountByStatusName").text
  end
  def getInProgressDealByStatusName
    return @@dbqDoc.search("InProgressDealByStatusName").text
  end
  def getUnderReviewDealCountByStatusName
    return @@dbqDoc.search("UnderReviewDealCountByStatusName").text
  end
  def getUnderReviewDealByStatusName
    return @@dbqDoc.search("UnderReviewDealByStatusName").text
  end
  def getAuthorizeDealCountByStatusName
    return @@dbqDoc.search("AuthorizeDealCountByStatusName").text
  end
  def getAuthorizeDealByStatusName
    return @@dbqDoc.search("AuthorizeDealByStatusName").text
  end
  def getOutstandingQuoteDealCountByStatusName
    return @@dbqDoc.search("OutstandingQuoteDealCountByStatusName").text
  end
  def getOutstandingQuoteDealByStatusName
    return @@dbqDoc.search("OutstandingQuoteDealByStatusName").text
  end
  def getToBeDeclinedDealCountByStatusName
    return @@dbqDoc.search("ToBeDeclinedDealCountByStatusName").text
  end
  def getToBeDeclinedDealByStatusName
    return @@dbqDoc.search("ToBeDeclinedDealByStatusName").text
  end
  def getBoundPendingDataEntryDealCountByStatusName
    return @@dbqDoc.search("BoundPendingDataEntryDealCountByStatusName").text
  end
  def getBoundPendingDataEntryDealByStatusName
    return @@dbqDoc.search("BoundPendingDataEntryDealByStatusName").text
  end
  def getRenewableDealCountByStatusName
    @queryvalue = @@dbqDoc.search("RenewableDealCountByStatusName").text
    @newQueryvalue = @queryvalue.gsub(/#/,'<')
    # print "/n"
    # print @newQueryvalue
    # print "/n"
    return @newQueryvalue
  end
  def getRenewableDealByStatusName
    @queryvalue = @@dbqDoc.search("RenewableDealByStatusName").text
    @newQueryvalue = @queryvalue.gsub(/#/,'<')
    # print "/n"
    # print @newQueryvalue
    # print "/n"
    return @newQueryvalue
  end
  def getDealSummary
    return @@dbqDoc.search("DealSummary").text
  end

  def getInProgressDealSummaryCountByStatusCode
    return @@dbqDoc.search("InProgressDealSummaryCountByStatusCode").text
  end
  def getUnderReviewDealSummaryCountByStatusCode
    return @@dbqDoc.search("UnderReviewDealSummaryCountByStatusCode").text
  end
  def getAuthorizeDealSummaryCountByStatusCode
    return @@dbqDoc.search("AuthorizeDealSummaryCountByStatusCode").text
  end
  def getOutstandingQuoteDealSummaryCountByStatusCode
    return @@dbqDoc.search("OutstandingQuoteDealSummaryCountByStatusCode").text
  end
  def getToBeDeclinedDealSummaryCountByStatusCode
    return @@dbqDoc.search("ToBeDeclinedDealSummaryCountByStatusCode").text
  end
  def getBPDEDealSummaryCountByStatusCode
    return @@dbqDoc.search("BPDEDealSummaryCountByStatusCode").text
  end
  def getOnHoldDealSummaryCountByStatusCode
    return @@dbqDoc.search("OnHoldDealSummaryCountByStatusCode").text
  end
  def getBoundPendingActionsDealSummaryCountByStatusCode
    return @@dbqDoc.search("BoundPendingActionsDealSummaryCountByStatusCode").text
  end
  def getRenewable6MonthsDealSummaryCountByStatusCode
    @queryvalue = @@dbqDoc.search("Renewable6MonthsDealSummaryCountByStatusCode").text
    @newQueryvalue = @queryvalue.gsub(/#/,'<')
    # print "/n"
    # print @newQueryvalue
    # print "/n"

    return @newQueryvalue
  end
  def getSubDivisions
    return @@dbqDoc.search("SubDivisions").text
  end
  def getCasualtyTeam
    return @@dbqDoc.search("CasualtyTeam").text
  end

  def getCasualtyTreatySubOption
    return @@dbqDoc.search("CasualtyTreatySubOption").text
  end

  def getCasualtyFacSubOption
    return @@dbqDoc.search("CasualtyFacSubOption").text
  end

  def getPropertyTeam
    return @@dbqDoc.search("PropertyTeam").text
  end

  def getPropertyInternationalSubOption
    return @@dbqDoc.search("PropertyInternationalSubOption").text
  end

  def getPropertyNorthAmericaSubOption
    return @@dbqDoc.search("PropertyNorthAmericaSubOption").text
  end

  def getSpecialtyTeam
    @specialtyTeamold = @@dbqDoc.search("SpecialtyTeam").text
    @specialtyTeamNew = @specialtyTeamold.gsub(/#/,'&')
    return @specialtyTeamNew
  end

  def getSpecialtyNon_PESubOption
    @SpecialtyNon_PESubOptionold = @@dbqDoc.search("SpecialtyNon-PESubOption").text
    @SpecialtyNon_PESubOptionNew = @SpecialtyNon_PESubOptionold.gsub(/#/,'&')
    return @SpecialtyNon_PESubOptionNew
  end

  def getPublicEntitySubOption
    return @@dbqDoc.search("PublicEntitySubOption").text
  end

  def getAllTeamSubOption
    @AllTeamsOldSubOption = @@dbqDoc.search("AllTeams").text
    @AllTeamsNewSubOption = @AllTeamsOldSubOption.gsub(/#/,'&')
    return @AllTeamsNewSubOption
  end

  def getPersonsByPersonID
    return @@dbqDoc.search("PersonsByPersonID").text
  end
  def getPropertyTA
    return @@dbqDoc.search("PropertyTA").text
  end
  def getUWManagerPersonProfile
    return @@dbqDoc.search("UWManagerPersonProfile").text
  end
  def getPTAPersonProfile
    return @@dbqDoc.search("PTAPersonProfile").text
  end
  def getDealStatusLookup
    @queryvalue = @@dbqDoc.search("DealStatusLookup").text
    @newQueryvalue = @queryvalue.gsub(/#/,'>')
    return @newQueryvalue
  end

  def getDealDetailsByDealNumber(dealnumber)
    @queryvalue = @@dbqDoc.search("DealDetailsByDealNumber").text
    @newQueryvalue = @queryvalue.to_s + dealnumber.to_s
    return @newQueryvalue
  end



  def getDealFieldByDealNumber(field,dealnumber)
    @queryvalue = @@dbqDoc.search("DealFieldByDealNumber").text
    case field
      when "Deal Name"
        @neededFieldvalue = " d.dealname AS dealName "
      when "Target Date"
        @neededFieldvalue = " FORMAT(d.targetdt,'MM-dd-yyyy') AS targetDate "

      when "Status"
        @neededFieldvalue = " st.name AS status,d.status AS statusCode "

      when "Underwriter"
        @neededFieldvalue =" uw_n.FullName AS primaryUnderwriterName ,d.uw1 AS primaryUnderwriterCode  "

      when "Underwriter 2"
        @neededFieldvalue = " uw2_n.FullName AS secondaryUnderwriterName,d.uw2 AS secondaryUnderwriterCode "

      when "TA"
        @neededFieldvalue = " ta_n.FullName AS technicalAssistantName,dp.TA AS technicalAssistantCode  "

      when "Modeler"
        @neededFieldvalue = " m_n.FullName AS modellerName,d.Modeller AS modellerCode  "

      when "Actuary"
        @neededFieldvalue = "  a_n.FullName AS actuaryName,d.act1 AS actuaryCode "

      when "Priority"
        @neededFieldvalue = " dp.ModelPriority as priority "

    end
    @newQueryvalue = "SELECT " + @neededFieldvalue + @queryvalue.to_s + dealnumber.to_s
    return @newQueryvalue
  end

  def getRolePersonsLookup(userroles)
    @queryvalue = @@dbqDoc.search("RolePersonsLookup").text
    @newQueryvalue = @queryvalue.to_s + userroles.to_s + ") ORDER BY name ASC;"
    return @newQueryvalue
  end


  def getPersonProfile(user)
    case user
      when "PTA"
        getPTAPersonProfile
      when "UW Manager"
        getUWManagerPersonProfile
    end
  end

  def getRolePersonsLookupbasedonRole(userrole)
    case userrole
      when "Actuary"
        @userroles = "'GlobalRe.Actuary','GlobalRe.Actuary Manager'"
        @finalRolePersonsLookupquery = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookupquery
      when "Modeler"
        @userroles = "'GlobalRe.Modeler','GlobalRe.Modeler Manager'"
        @finalRolePersonsLookupquery = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookupquery
      when "Underwriter"
        @userroles = "'GlobalRe.Underwriter','GlobalRe.Underwriter Manager'"
        @finalRolePersonsLookupquery = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookupquery
      when "UA/TA"
        @userroles = "'GlobalRe.UA/TA','GlobalRe.Property UA/TA'"
        @finalRolePersonsLookupquery = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookupquery
      when "Actuary Manager"
        @userroles = "'GlobalRe.Actuary Manager'"
        @finalRolePersonsLookupquery = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookupquery
      when "Underwriter Manager"
        @userroles = "'GlobalRe.Underwriter Manager'"
        @finalRolePersonsLookupquery = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookupquery
      when "Property UA/TA"
        @userroles = "'GlobalRe.Property UA/TA'"
        @finalRolePersonsLookupquery = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookupquery
      when "Modeler Manager"
        @userroles = "'GlobalRe.Modeler Manager'"
        @finalRolePersonsLookupquery = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookupquery
    end
  end





  def getDealCountByStatusName(status)
    @StatusName = status
    case @StatusName
      when "Bound - Pending Actions"
        @sqlQuery = getBoundDealCountByStatusName
        return @sqlQuery
      when "On Hold"
        @sqlQuery = getOnHoldDealCountByStatusName
        return @sqlQuery
      when "In Progress"
        @sqlQuery = getInProgressDealSummaryCountByStatusCode
        return @sqlQuery
      when "Authorize"
        @sqlQuery = getAuthorizeDealCountByStatusName
        return @sqlQuery
      when "Outstanding Quote"
        @sqlQuery = getOutstandingQuoteDealCountByStatusName
        return @sqlQuery
      when "To Be Declined"
        @sqlQuery = getToBeDeclinedDealCountByStatusName
        return @sqlQuery
      when "Bound Pending Data Entry"
        @sqlQuery = getBoundPendingDataEntryDealCountByStatusName
        return @sqlQuery
      when "Renewable - 6 Months"
        @sqlQuery = getRenewableDealCountByStatusName
        return @sqlQuery
      when "Under Review"
        @sqlQuery = getUnderReviewDealCountByStatusName
        return @sqlQuery
    end
  end
  def getDealByStatusName(status)
    @StatusName = status
    case @StatusName
      when "Bound - Pending Actions"
        @sqlQuery = getBoundDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
      when "On Hold"
        @sqlQuery = getOnHoldDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
      when "In Progress"
        @sqlQuery = getInProgressDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
      when "Authorize"
        @sqlQuery = getAuthorizeDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
      when "Outstanding Quote"
        @sqlQuery = getOutstandingQuoteDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
      when "To Be Declined"
        @sqlQuery = getToBeDeclinedDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
      when "Bound Pending Data Entry"
        @sqlQuery = getBoundPendingDataEntryDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
      when "Renewable - 6 Months"
        @sqlQuery = getRenewableDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
      when "Under Review"
        @sqlQuery = getUnderReviewDealByStatusName
        @sqlQuery = @sqlQuery.to_s + " order by dealNumber desc;"
        return @sqlQuery
    end
  end

  def getDealSummaryCountByStatusCode(status)
    @StatusName = status
    case @StatusName
      when "Bound - Pending Actions"
        @sqlQuery = getBoundPendingActionsDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + " group by st.name;"
        return @sqlQuery
      when "On Hold"
        @sqlQuery = getOnHoldDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + " group by st.name;"
        return @sqlQuery
      when "In Progress"
        @sqlQuery = getInProgressDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + ";"
        return @sqlQuery
      when "Authorize"
        @sqlQuery = getAuthorizeDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + " group by st.name;"
        return @sqlQuery
      when "Outstanding Quote"
        @sqlQuery = getOutstandingQuoteDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + " group by st.name;"
        return @sqlQuery
      when "To Be Declined"
        @sqlQuery = getToBeDeclinedDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + " group by st.name;"
        return @sqlQuery
      when "Bound Pending Data Entry"
        @sqlQuery = getBPDEDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + " group by st.name;"
        return @sqlQuery
      when "Renewable - 6 Months"
        @sqlQuery = getRenewable6MonthsDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + " group by st.name;"
        return @sqlQuery
      when "Under Review"
        @sqlQuery = getUnderReviewDealSummaryCountByStatusCode
        @sqlQuery = @sqlQuery.to_s + " group by st.name;"
        return @sqlQuery
    end
  end
  def getTeams(subdivisions)
    @SubdivisionName = subdivisions

    case @SubdivisionName
      when "Casualty"
        return getCasualtyTeam.to_s
      when "Casualty Treaty"
        return getCasualtyTreatySubOption.to_s
      when "Cas Fac"
        return getCasualtyFacSubOption.to_s
      when "Property"
        return  getPropertyTeam.to_s
      when "Intl Property"
        return getPropertyInternationalSubOption.to_s
      when "NA Property"
        return getPropertyNorthAmericaSubOption.to_s
      when "Specialty"
        return getSpecialtyTeam.to_s
      when "Specialty Non-PE"
        return getSpecialtyNon_PESubOption.to_s
      when "Public Entity"
        return getPublicEntitySubOption.to_s
      when "All Team"
        return getAllTeamSubOption.to_s
    end

  end
  def getBoundDealCountByStatus
    return @@dbqDoc.search("BoundDealCountByStatus").text
  end
  def getOnHoldDealCountByStatus
    return @@dbqDoc.search("OnHoldDealCountByStatus").text
  end
  def getInProgressDealCountByStatus
    return @@dbqDoc.search("InProgressDealCountByStatus").text
  end
  def getUnderReviewDealCountByStatus
    return @@dbqDoc.search("UnderReviewDealCountByStatus").text
  end
  def getAuthorizeDealCountByStatus
    return @@dbqDoc.search("AuthorizeDealCountByStatus").text
  end
  def getOutstandingQuoteDealCountByStatus
    return @@dbqDoc.search("OutstandingQuoteDealCountByStatus").text
  end
  def getToBeDeclinedDealCountByStatus
    return @@dbqDoc.search("ToBeDeclinedDealCountByStatus").text
  end
  def getBoundPendingDataEntryDealCountByStatus
    return @@dbqDoc.search("BoundPendingDataEntryDealCountByStatus").text
  end
  def getRenewableDealCountByStatus
    @queryvalue = @@dbqDoc.search("RenewableDealCountByStatus").text
    @newQueryvalue = @queryvalue.gsub(/#/,'<')
    return @newQueryvalue
  end

  def getDealSummaryBySubdivisions(subdivisions)
    # Need to update
    return @@dbqDoc.search("DealSummaryBySubdivisions").text
  end

  def getUWManager
    # Need to update
    return @@dbqDoc.search("UWManager").text
  end
  def getSETUSERENV
    # Need to update
    return @@dbqDoc.search("SETUSERENV").text
  end
  def getUNSETUSERENV
    # Need to update
    return @@dbqDoc.search("UNSETUSERENV").text
  end

  def getDBCountQuery(status)
    @NeededStatus = status
    case @NeededStatus
      when "On Hold"
        @NeededQuery = getOnHoldDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
      when "Bound - Pending Actions"
        @NeededQuery = getBoundDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
      when "In Progress"
        @NeededQuery = getInProgressDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
      when "Renewable - 6 Months"
        @NeededQuery = getRenewableDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
      when "Under Review"
        @NeededQuery = getUnderReviewDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
      when "Authorize"
        @NeededQuery = getAuthorizeDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
      when "Outstanding Quote"
        @NeededQuery = getOutstandingQuoteDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
      when "To Be Declined"
        @NeededQuery = getToBeDeclinedDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
      when "Bound Pending Data Entry"
        @NeededQuery = getBoundPendingDataEntryDealCountByStatus
        @NeededQuery = @NeededQuery + ";"
        return @NeededQuery
    end
  end

  def getContractTypesQuery(assumedCededFlag)
    @Flag = assumedCededFlag
    case @Flag
      when "Assumed"
        @FinalQuery = @@dbqDoc.search("ContractTypesLookup").text + "where AssumedFlag = 1 order by catorder"+";"
        return @FinalQuery
      when "Ceded"
        @FinalQuery = @@dbqDoc.search("CoverageBasisOptions").text + "where cededflag=1"+";"
        return @FinalQuery
    end
  end


   def getCoverageBasisOptionsQuery(assumedCededAllFlag)
    @Flag = assumedCededAllFlag
    case @Flag
      when "Assumed"
        @FinalQuery = @@dbqDoc.search("CoverageBasisOptions").text + "where AssumedFlag=1 ORDER BY catorder asc"+";"
        return @FinalQuery
      when "Ceded"
        @FinalQuery = @@dbqDoc.search("CoverageBasisOptions").text + "where cededflag=1"+";"
        return @FinalQuery
      when "All"
        @FinalQuery = @@dbqDoc.search("CoverageBasisOptions").text + "ORDER BY catorder asc"+";"
        return @FinalQuery
    end
  end

  def getAttachmentBasisOptionsQuery(assumedCededAllFlag)
    @Flag = assumedCededAllFlag
    case @Flag
      when "Assumed"
        @FinalQuery = @@dbqDoc.search("AttachmentBasisOptions").text + "where AssumedFlag=1 ORDER BY catorder asc"+";"
        return @FinalQuery
      when "Ceded"
        @FinalQuery = @@dbqDoc.search("AttachmentBasisOptions").text + "where cededflag=1"+";"
        return @FinalQuery
      when "All"
        @FinalQuery = @@dbqDoc.search("AttachmentBasisOptions").text + "ORDER BY catorder asc"+";"
        return @FinalQuery
    end
  end

  def getDealCountByStatusAndSubdivision(status, subdivisions)
    @dealsByStatus =getDBCountQuery(status).to_s
    # puts @dealsByStatus.to_s
    # @Team = getTeams(subdivisions)
    @expType = getExposureTypCodeQuery(subdivisions)
    @teamString = "AND d.exposuretype IN ("+@expType+");"

    @getdealbystatusandSubdivision = @dealsByStatus.gsub(/;/,@teamString)
    # print "\n"+ @getdealbystatusandSubdivision
    return @getdealbystatusandSubdivision

    #if status == "In Progress"
    # @getdealcountbystatusandSubdivision = @dealsByStatus.gsub(";",@Team+" group by st.name;")
    #print @getdealbystatusandSubdivision
    # return @getdealcountbystatusandSubdivision
    #else
    #@getdealcountbystatusandSubdivision = @dealsByStatus.gsub("group by st.name;",@Team+" group by st.name;")
    #print @getdealbystatusandSubdivision
    #return @getdealcountbystatusandSubdivision
    # end
  end


  def getDealsByStatusAndSubdivision(status, subdivisions)
    @dealsByStatus =getDealByStatusName(status)
    # @Team = getTeams(subdivisions)
    @expType = getExposureTypCodeQuery(subdivisions)
    # puts @dealsByStatus
    # puts ""
    # puts @Team


    @getdealbystatusandSubdivision = @dealsByStatus.gsub("order by dealNumber desc;","AND d.exposuretype IN ("+@expType+") order by dealNumber desc;")
    #print @getdealbystatusandSubdivision
    return @getdealbystatusandSubdivision

  end
  def getDealSummaryByUser(user)
    @NeededUsertype = user
    @NeededQueryAdditionvalue = getPropertyTA
    @SetENV = getSETUSERENV
    @UnSetEnv = getUNSETUSERENV
    case @NeededUsertype
      when "UW"
        @NeededQuery = getDealSummary
        @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " group by statusname,statuscode,statusgroupname,statusgroup" + ";"
        return @NeededQuery
      when "NPTA"
        @NeededQuery = getDealSummary
        @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + " group by statusname,statuscode,statusgroupname,statusgroup" + ";"
        return @NeededQuery
      when "All Access"
        @NeededQuery = getDealSummary
        @NeededQuery = @NeededQuery + " group by statusname,statuscode,statusgroupname,statusgroup" + ";"
        return @NeededQuery
      when "PTA"
        @NeededQuery = getDealSummary
        @NeededQuery = @NeededQuery + " " + @NeededQueryAdditionvalue +" group by statusname,statuscode,statusgroupname,statusgroup" + ";"
        return @NeededQuery
      when "Actuary"
        @NeededQuery = getDealSummary
        @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + " group by statusname,statuscode,statusgroupname,statusgroup" + ";"
        return @NeededQuery
      when "Actuary Manager"
        @NeededQuery = getDealSummary
        @NeededQuery = @NeededQuery + "  AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1))  " + " group by statusname,statuscode,statusgroupname,statusgroup" + ";" #" AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + " group by statusname,statuscode,statusgroupname,statusgroup" + ";"
        return @NeededQuery
      when "UW Manager"
        @NeededQuery = getDealSummary
        @SetENV = getSETUSERENV
        @UnSetEnv = getUNSETUSERENV
        @AdditionalUWQuery = getUWManager
        @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " group by statusname,statuscode,statusgroupname,statusgroup" + ";" + @UnSetEnv
        #" AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 35402 AND (pp.PersonId = d.uw1 OR pp.PersonId = d.uw2 OR pp.PersonId = dp.TA)) " +
        return @NeededQuery
    end
  end


  def getDealSummaryCountByStatusCodeforUser(status,user)
    @StatusName = status
    @NeededUsertype = user
    @NeededQueryAdditionvalue = getPropertyTA
    @SetENV = getSETUSERENV
    @UnSetEnv = getUNSETUSERENV
    #@AdditionalUWQuery = getUWManager
    case @NeededUsertype
      when "UW"
        @NeededQueryAddition = " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy')) "
      when "NPTA"
        @NeededQueryAddition = " AND ta_n.FullName = 'Rhonda Corbin' "
      when "All Access"
        @NeededQueryAddition = ""
      when "PTA"
        @NeededQueryAddition =  "" + @NeededQueryAdditionvalue
      when "Actuary"
        @NeededQueryAddition = " AND a_n.FullName = 'Laurie Slader' "
      when "Actuary Manager"
        @NeededQueryAddition = " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " #AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 544292 AND (pp.PersonId = d.act1)) "
      when "UW Manager"
        @NeededQueryAddition = getUWManager
    end
    case @StatusName
      when "Bound - Pending Actions"
        @sqlQuery = getBoundPendingActionsDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " group by st.name;" + @UnSetEnv
        return @sqlQuery
      when "On Hold"
        @sqlQuery = getOnHoldDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " group by st.name;" + @UnSetEnv
        return @sqlQuery
      when "In Progress"
        @sqlQuery = getInProgressDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s +  ";" + @UnSetEnv
        return @sqlQuery
      when "Authorize"
        @sqlQuery = getAuthorizeDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " group by st.name;" + @UnSetEnv
        return @sqlQuery
      when "Outstanding Quote"
        @sqlQuery = getOutstandingQuoteDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " group by st.name;" + @UnSetEnv
        return @sqlQuery
      when "To Be Declined"
        @sqlQuery = getToBeDeclinedDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " group by st.name;" + @UnSetEnv
        return @sqlQuery
      when "Bound Pending Data Entry"
        @sqlQuery = getBPDEDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " group by st.name;" + @UnSetEnv
        return @sqlQuery
      when "Renewable - 6 Months"
        @sqlQuery = getRenewable6MonthsDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " group by st.name;" + @UnSetEnv
        return @sqlQuery
      when "Under Review"
        @sqlQuery = getUnderReviewDealSummaryCountByStatusCode
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " group by st.name;" + @UnSetEnv
        return @sqlQuery
    # print "\n"
    # print @sqlQuery
    # print "\n"
    end
  end
  def getUserDealByStatusName(status,user)
    @StatusName = status
    @NeededUsertype = user
    @NeededQueryAdditionvalue = getPropertyTA
    @SetENV = getSETUSERENV
    @UnSetEnv = getUNSETUSERENV
    case @NeededUsertype
      when "UW"
        @NeededQueryAddition = " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy')) "
      when "NPTA"
        @NeededQueryAddition = " AND ta_n.FullName = 'Rhonda Corbin' "
      when "All Access"
        @NeededQueryAddition = ""
      when "PTA"
        @NeededQueryAddition =  "" +  @NeededQueryAdditionvalue
      when "Actuary"
        @NeededQueryAddition = " AND a_n.FullName = 'Laurie Slader' "
      when "Actuary Manager"
        @NeededQueryAddition = " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " #AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 544292 AND (pp.PersonId = d.act1)) "
      when "UW Manager"
        @NeededQueryAddition = getUWManager
    end
    case @StatusName
      when "Bound - Pending Actions"
        @sqlQuery = getBoundDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
      when "On Hold"
        @sqlQuery = getOnHoldDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
      when "In Progress"
        @sqlQuery = getInProgressDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
      when "Authorize"
        @sqlQuery = getAuthorizeDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
      when "Outstanding Quote"
        @sqlQuery = getOutstandingQuoteDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
      when "To Be Declined"
        @sqlQuery = getToBeDeclinedDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
      when "Bound Pending Data Entry"
        @sqlQuery = getBoundPendingDataEntryDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
      when "Renewable - 6 Months"
        @sqlQuery = getRenewableDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
      when "Under Review"
        @sqlQuery = getUnderReviewDealByStatusName
        @sqlQuery = @SetENV + @sqlQuery.to_s + @NeededQueryAddition.to_s + " order by dealNumber desc;" + @UnSetEnv
        return @sqlQuery
    end
  end

  #-------Fetch exposureType DB query ------

  def getExposureType(exposureName)
    @exposureName = exposureName
    @query = @@dbqDoc.search("ExposureTypeCode").text
    @query = @query + @exposureName + ';'
    return @query
  end

  def getBusinessClass(dealnumber)
    @dealNumber = dealnumber
    @query = @@dbqDoc.search("DeriveBusinessClass").text
    @query = @query + @dealNumber + ';'
    return @query
  end

  def getCedantInformation(dealnumber)
    @dealNumber = dealnumber
    @query = @@dbqDoc.search("CedantInformation").text
    @query = @query + @dealNumber + ';'
    return @query
  end
  
  
  def getNotesByDealNumberQuery(dealnumber)
    puts("Inside dealnumber="+dealnumber.to_s)
    @dealnum=dealnumber.to_s
        @FinalQuery = @@dbqDoc.search("getNotes").text.gsub(/#dealnumber/,@dealnum) +";"
    puts("Final query="+@FinalQuery)
        return @FinalQuery
  end
  def getNotesByDealandNoteNumberQuery(dealnumber,notenum)
    puts("Inside dealnumber="+dealnumber.to_s)
    @dealnum=dealnumber.to_s
    @notenum=notenum.to_s
    @SubQuery = @@dbqDoc.search("fetchCreatednote").text.gsub(/#dealnumber/,@dealnum)
    @FinalQuery = @SubQuery.gsub(/#notenum/,@notenum) +";"
    puts("Final query="+@FinalQuery)
    return @FinalQuery
  end

  def deleteNote(dealnumber,notenum)
    @dealnum=dealnumber.to_s
    @notenum=notenum.to_s
    @SubQuery = @@dbqDoc.search("deleteNote").text.gsub(/#dealnumber/,@dealnum)
    @FinalQuery = @SubQuery.gsub(/#notenum/,@notenum) +";"
    puts("Final query="+@FinalQuery)
    return @FinalQuery
  end

  def getExposureTypeLookupQuery
    @query = @@dbqDoc.search("ExposureTypeLookup").text
    return @query
  end

  def searchNoteByNoteNumber(dealnumber,notenum)
    @dealnum=dealnumber.to_s
    @notenum=notenum.to_s
    @SubQuery = @@dbqDoc.search("searchNoteByNotenum").text.gsub(/#dealnumber/,@dealnum)
    @FinalQuery = @SubQuery.gsub(/#notenum/,@notenum) +";"
    puts("Final query="+@FinalQuery)
    return @FinalQuery
  end

  def getWritingCompaniesQuery(flag)

    @FinalQuery = @@dbqDoc.search("WritingCompanies").text + ";"
    return @FinalQuery

  end
  def cedantSearchQuery(cedantname,parentgroupname)

    @FinalQuery = @@dbqDoc.search("CedantSearchAndSelect").text  + "  "+ ";"
    return @FinalQuery

  end

  def getNoteTypesQuery(assumedCededAllFlag)
    if assumedCededAllFlag=="assumed"
    @FinalQuery = @@dbqDoc.search("assumedNoteTypes").text + ";"
    elsif assumedCededAllFlag == 'ceded'
      @FinalQuery = @@dbqDoc.search("cededNoteTypes").text + ";"
    else
      @FinalQuery = @@dbqDoc.search("allNoteTypes").text + ";"
    return @FinalQuery
    end
  end

  def getDealsFilterQuery(expoType,statusCode)
    @arrVal = ''
    @statusArrVal = ''
    expoType.each { |row|
      @arrVal = @arrVal + '\'' + row + '\'' + ','
    }
    if statusCode == 'NA'
      @query = @@dbqDoc.search("DealFilterQuery").text + 'vgbs.exposuretype IN (' + @arrVal.chop + ') order by dealnum desc ;'
    else
      @statusCodeArray = statusCode.split(',')
      @statusCodeArray.each { |status|
        @statusArrVal = @statusArrVal + '\'' + status + '\'' + ','
      }

      @query = @@dbqDoc.search("DealFilterQuery").text + 'vgbs.StatusCode in (' + @statusArrVal.chop + ') and ' + 'vgbs.exposuretype IN (' + @arrVal.chop + ') order by dealnum desc ;'
    end


    return @query
  end

  def getExistingViewQuery(viewId)
    @query = @@dbqDoc.search("ViewQuery").text + viewId + ';'
    return @query
  end

end