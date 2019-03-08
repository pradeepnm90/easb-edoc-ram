require 'nokogiri'

class DBQueries
  def initialize
    dbQFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","DBQueries.xml")
    @@dbqDoc = Nokogiri::XML(File.open(dbQFile))
  end

  def getFirstAndLastName(userId)
    return @@dbqDoc.search("FetchUserName").text + "'"+userId+"%'"+";"
  end
  def getDBQuery(status)
    @NeededStatus = status
    case @NeededStatus
      when "On Hold"
        @NeededQuery = getOnHoldDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
      when "Bound - Pending Actions"
        @NeededQuery = getBoundDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
      when "In Progress"
        @NeededQuery = getInProgressDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
      when "Renewable - 6 Months"
        @NeededQuery = getRenewableDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
      when "Under Review"
        @NeededQuery = getUnderReviewDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
      when "Authorize"
        @NeededQuery = getAuthorizeDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
      when "Outstanding Quote"
        @NeededQuery = getOutstandingQuoteDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
      when "To Be Declined"
        @NeededQuery = getToBeDeclinedDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
      when "Bound Pending Data Entry"
        @NeededQuery = getBoundPendingDataEntryDealByStatus
        @NeededQuery = @NeededQuery + " order by dealNumber desc;"
        return @NeededQuery
    end
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
  def getDealCountByStatusquery
    return @@dbqDoc.search("DealCountByStatus").text
  end
  def getGlobalReDataViewquery
    return @@dbqDoc.search("GlobalReDataView").text
  end
  def getBoundDealByStatus
    return @@dbqDoc.search("BoundDealByStatus").text
  end
  def getOnHoldDealByStatus
    return @@dbqDoc.search("OnHoldDealByStatus").text
  end
  def getInProgressDealByStatus
    return @@dbqDoc.search("InProgressDealByStatus").text
  end
  def getUnderReviewDealByStatus
    return @@dbqDoc.search("UnderReviewDealByStatus").text
  end
  def getAuthorizeDealByStatus
    return @@dbqDoc.search("AuthorizeDealByStatus").text
  end
  def getOutstandingQuoteDealByStatus
    return @@dbqDoc.search("OutstandingQuoteDealByStatus").text
  end
  def getToBeDeclinedDealByStatus
    return @@dbqDoc.search("ToBeDeclinedDealByStatus").text
  end
  def getBoundPendingDataEntryDealByStatus
    return @@dbqDoc.search("BoundPendingDataEntryDealByStatus").text
  end
  def getRenewableDealByStatus
    @queryvalue = @@dbqDoc.search("RenewableDealByStatus").text
    @newQueryvalue = @queryvalue.gsub(/#/,'<')
    return @newQueryvalue
  end
  def getBoundDByStatus
    return @@dbqDoc.search("BoundDByStatus").text
  end
  def getOnHoldDByStatus
    return @@dbqDoc.search("OnHoldDByStatus").text
  end
  def getInProgressDByStatus
    return @@dbqDoc.search("InProgressDByStatus").text
  end
  def getUnderReviewDByStatus
    return @@dbqDoc.search("UnderReviewDByStatus").text
  end
  def getAuthorizeDByStatus
    return @@dbqDoc.search("AuthorizeDByStatus").text
  end
  def getOutstandingQuoteDByStatus
    return @@dbqDoc.search("OutstandingQuoteDByStatus").text
  end
  def getToBeDeclinedDByStatus
    return @@dbqDoc.search("ToBeDeclinedDByStatus").text
  end
  def getBoundPendingDataEntryDByStatus
    return @@dbqDoc.search("BoundPendingDataEntryDByStatus").text
  end
  def getRenewableDByStatus
    @queryvalue = @@dbqDoc.search("RenewableDByStatus").text
    @newQueryvalue = @queryvalue.gsub(/#/,'<')
    return @newQueryvalue
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
    return @specialtyTeamNew.text
  end

  def getSpecialtyNon_PESubOption
    @SpecialtyNon_PESubOptionold = @@dbqDoc.search("SpecialtyNon-PESubOption").text
    @SpecialtyNon_PESubOptionNew = @SpecialtyNon_PESubOptionold.gsub(/#/,'&')
    return @SpecialtyNon_PESubOptionNew.text
  end

  def getPublicEntitySubOption
    return @@dbqDoc.search("PublicEntitySubOption").text
  end
  def getPropertyTA
    return @@dbqDoc.search("PropertyTA").text
  end
  def getUserFullName
    return @@dbqDoc.search("UserFullName").text
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
  def getUWManagerPersonProfile
    return @@dbqDoc.search("UWManagerPersonProfile").text
  end
  def getPTAPersonProfile
    return @@dbqDoc.search("PTAPersonProfile").text
  end


  def getPersonProfile(user)
    case user
      when "PTA"
        getPTAPersonProfile
      when "UW Manager"
        getUWManagerPersonProfile
    end
  end



  def getUserDBQuery(status,user)
    @NeededStatus = status
    @NeededUser = user
    @NeededAdditionalQuery = getPropertyTA
    case @NeededUser
      when "UW"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + " order by dealNumber desc;"
            return @NeededQuery
        end
      when "NPTA"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" +  " order by dealNumber desc;"
            return @NeededQuery
        end
      when "Actuary"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" +  " order by dealNumber desc;"
            return @NeededQuery
        end
      when "PTA"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDByStatus
            @NeededAdditionalQuery = getPropertyTA
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDByStatus
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery +  " order by dealNumber desc;"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDByStatus
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery +  " order by dealNumber desc;"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDByStatus
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery +  " order by dealNumber desc;"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDByStatus
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery +  " order by dealNumber desc;"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDByStatus
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery +  " order by dealNumber desc;"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDByStatus
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery +  " order by dealNumber desc;"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDByStatus
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery +  " order by dealNumber desc;"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDByStatus
            @NeededQuery = @NeededQuery + " " + @NeededAdditionalQuery +  " order by dealNumber desc;"
            return @NeededQuery
        end
      when "UW Manager"
        @SetENV = getSETUSERENV
        @UnSetEnv = getUNSETUSERENV
        @AdditionalUWQuery = getUWManager
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDByStatus
            @NeededQuery = @SetENV + @NeededQuery + @AdditionalUWQuery +  " order by dealNumber desc;" + @UnSetEnv
            return @NeededQuery
        end
      when "Actuary Manager"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;" #AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 544292 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " +  " order by dealNumber desc;"
            return @NeededQuery
        end
      when "All Access"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDealByStatus
            @NeededQuery = @NeededQuery + " order by dealNumber desc;"
            return @NeededQuery
        end
    end
  end
  def getUserDBCountQuery(status,user)
    @NeededStatus = status
    @NeededUser = user
    @NeededAdditionalQuery = getPropertyTA
    case @NeededUser
      when "UW"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ((uw_n.FullName = 'Mike McCarthy') OR (uw2_n.FullName = 'Mike McCarthy'))" + ";"
            return @NeededQuery
        end
      when "NPTA"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDealCountByStatus
            @NeededQuery = @NeededQuery + " AND ta_n.FullName = 'Rhonda Corbin'" + ";"
            return @NeededQuery
        end
      when "Actuary"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDealCountByStatus
            @NeededQuery = @NeededQuery + " AND a_n.FullName = 'Laurie Slader'" + ";"
            return @NeededQuery
        end
      when "PTA"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDealCountByStatus
            @NeededAdditionalQuery = getPropertyTA
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDealCountByStatus
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDealCountByStatus
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDealCountByStatus
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDealCountByStatus
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDealCountByStatus
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDealCountByStatus
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDealCountByStatus
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDealCountByStatus
            @NeededQuery = @NeededQuery.to_s + " " + @NeededAdditionalQuery.to_s + ";"
            return @NeededQuery
        end
      when "UW Manager"
        @SetENV = getSETUSERENV
        @UnSetEnv = getUNSETUSERENV
        @AdditionalUWQuery = getUWManager
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDealCountByStatus
            @NeededQuery = @SetENV.to_s + @NeededQuery.to_s + @AdditionalUWQuery.to_s + ";" + @UnSetEnv.to_s
            return @NeededQuery
        end
      when "Actuary Manager"
        case @NeededStatus
          when "On Hold"
            @NeededQuery = getOnHoldDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
          when "Bound - Pending Actions"
            @NeededQuery = getBoundDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
          when "In Progress"
            @NeededQuery = getInProgressDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
          when "Renewable - 6 Months"
            @NeededQuery = getRenewableDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
          when "Under Review"
            @NeededQuery = getUnderReviewDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
          when "Authorize"
            @NeededQuery = getAuthorizeDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
          when "Outstanding Quote"
            @NeededQuery = getOutstandingQuoteDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
          when "To Be Declined"
            @NeededQuery = getToBeDeclinedDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
          when "Bound Pending Data Entry"
            @NeededQuery = getBoundPendingDataEntryDealCountByStatus
            @NeededQuery = @NeededQuery + " AND EXISTS (SELECT * FROM tb_person pp WHERE pp.ManagerId = 63184 AND (pp.PersonId = d.act1)) " + ";"
            return @NeededQuery
        end
      when "All Access"
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
    end

  end

  def getDBSubdivisionCountQuery(panels,teams)
    @statusQuery = getDBCountQuery(panels).to_s
    @teamQuery = getTeams(teams)
    @dealCountByteam = @statusQuery.gsub(/;/,@teamQuery+';')
    #print @dealCountByteam
    return @dealCountByteam
  end


  def getDealNeededDetailsByDealNumber(dealnumber)
    @queryvalue = @@dbqDoc.search("DealNeededDetailsByDealNumber").text
    @newQueryvalue = @queryvalue.to_s + dealnumber.to_s
    return @newQueryvalue
  end
  def getDealDetailsByDealNumber(dealnumber)
    @queryvalue = @@dbqDoc.search("DealDetailsByDealNumber").text
    @newQueryvalue = @queryvalue.to_s + dealnumber.to_s
    return @newQueryvalue
  end

  def getNotesUpdates(dealnumber,notes,user)
    @queryvalue = @@dbqDoc.search("NotesUpdate").text
    @newQueryvalue = @queryvalue.to_s + dealnumber.to_s + " and notes like '" + notes.to_s + "%' and (FirstName + ' ' + LastName) like '" + user.to_s + "' ;"
    return @newQueryvalue
  end

  def getAllKeyDocs(dealnumber)
    @queryvalue = @@dbqDoc.search("keyDocuments").text
    @newQueryvalue = @queryvalue.to_s + dealnumber.to_s + " ;"
    return @newQueryvalue
  end

end