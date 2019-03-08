require 'nokogiri'

class Endpoints
  def initialize
    endpointsFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","endpoints.xml")
    @@endpointsDoc = Nokogiri::XML(File.open(endpointsFile))
  end
  def getDealCountByStatusendpoints
    return @@endpointsDoc.search("DealCountByStatus").text
  end
  def getBoundDealByStatusendpoints
    return @@endpointsDoc.search("BoundDealByStatus").text
  end
  def getOnHoldDealByStatusendpoints
    return @@endpointsDoc.search("OnHoldDealByStatus").text
  end
  def getInValidDealByStatusendpoints
    return @@endpointsDoc.search("InValidDealByStatus").text
  end
  def getSpecialCDealByStatusendpoints
    return @@endpointsDoc.search("SpecialCDealByStatus").text
  end
  def getNoDealByStatusendpoints
    return @@endpointsDoc.search("NoDealByStatus").text
  end
  def getInProgressDealByStatus
    return @@endpointsDoc.search("InProgressDealByStatus").text
  end
  def getUnderReviewDealByStatus
    return @@endpointsDoc.search("UnderReviewDealByStatus").text
  end
  def getOutstandingQuoteDealByStatus
    return @@endpointsDoc.search("OutstandingQuoteDealByStatus").text
  end
  def getToBeDeclinedDealByStatus
    return @@endpointsDoc.search("ToBeDeclinedDealByStatus").text
  end
  def getBoundPendingDataEntryDealByStatus
    return @@endpointsDoc.search("BoundPendingDataEntryDealByStatus").text
  end
  def getAuthorizeDealByStatus
    return @@endpointsDoc.search("AuthorizeDealByStatus").text
  end
  def getRenewableDealByStatus
    return @@endpointsDoc.search("RenewableDealByStatus").text
  end
  def getDealStatusSummaries
    return @@endpointsDoc.search("DealStatusSummaries").text
  end
  def getSubDivisions
    return @@endpointsDoc.search("SubDivisions").text
  end
  def getPersons
    @finalendpoint = @@endpointsDoc.search("Persons").text
    #@finalendpoint = @finalenpoint.to_s + user.to_s
    return @finalendpoint
  end
  def getPersonProfile
    @finalendpoint = @@endpointsDoc.search("PersonProfile").text
    #@finalendpoint = @finalenpoint.to_s + user.to_s
    return @finalendpoint
  end
  def putByDealNumber(dealnumber)
    @finalendpoint = @@endpointsDoc.search("PutByDealNumber").text
    @finalendpoint = @finalendpoint.to_s + dealnumber.to_s
    return @finalendpoint
  end
  def getDealByDealNumber(dealnumber)
    @finalendpoint = @@endpointsDoc.search("GetByDealNumber").text
    @finalendpoint = @finalendpoint.to_s + dealnumber.to_s
    return @finalendpoint
  end

  def getDealStatusLookup
    return @@endpointsDoc.search("DealStatusLookup").text
  end
  def getRolePersonsLookup(userroles)
    @finalendpoint = @@endpointsDoc.search("RolePersonsLookup").text
    @finalendpoint = @finalendpoint.to_s + userroles.to_s
    return @finalendpoint
  end

  def getCoverageBasisLookup(assumedCededAllFlag)
    @finalendpoint = @@endpointsDoc.search("CoverageBasisLookup").text
    @finalendpoint = @finalendpoint.to_s + assumedCededAllFlag.to_s
    return @finalendpoint
  end

  def getAttachmentBasisLookup(type)
    @finalendpoint = @@endpointsDoc.search("AttachmentBasisLookup").text
    @finalendpoint = @finalendpoint.to_s + type.to_s
    return @finalendpoint
  end

  def getContractTypeLookup(type)
    @finalendpoint = @@endpointsDoc.search("ContractTypeLookup").text
    @finalendpoint = @finalendpoint.to_s + type.to_s
    return @finalendpoint
  end

  def getRolePersonsLookupbasedonRole(userrole)
    case userrole
      when "Actuary"
        @userroles = "GlobalRe.Actuary,GlobalRe.Actuary Manager"
        @finalRolePersonsLookup = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookup
      when "Modeler"
        @userroles = "GlobalRe.Modeler,GlobalRe.Modeler Manager"
        @finalRolePersonsLookup = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookup
      when "Underwriter"
        @userroles = "GlobalRe.Underwriter,GlobalRe.Underwriter Manager"
        @finalRolePersonsLookup = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookup
      when "UA/TA"
        @userroles = "GlobalRe.UA/TA,GlobalRe.Property UA/TA"
        @finalRolePersonsLookup = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookup
      when "Actuary Manager"
        @userroles = "GlobalRe.Actuary Manager"
        @finalRolePersonsLookup = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookup
      when "Underwriter Manager"
        @userroles = "GlobalRe.Underwriter Manager"
        @finalRolePersonsLookup = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookup
      when "Property UA/TA"
        @userroles = "GlobalRe.Property UA/TA"
        @finalRolePersonsLookup = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookup
      when "Modeler Manager"
        @userroles = "GlobalRe.Modeler Manager"
        @finalRolePersonsLookup = getRolePersonsLookup(@userroles)
        return @finalRolePersonsLookup

    end
  end


  def getDealByStatusCodeendpoints(status,user)
    @statusname = status
    @userType = user
    case @userType
      when "PTA"
        @Additionalendpointval = "&subdivisions=3,4"
      when "UW Manager"
        @Additionalendpointval = "&subdivisions=3"
    end
    case @statusname
      when "Bound - Pending Actions"
        @endpoint = getBoundDealByStatusendpoints.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
      when "On Hold"
        @endpoint = getOnHoldDealByStatusendpoints.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
      when "In Progress"
        @endpoint = getInProgressDealByStatus.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
      when "Authorize"
        @endpoint = getAuthorizeDealByStatus.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
      when "Under Review"
        @endpoint = getUnderReviewDealByStatus.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
      when "Outstanding Quote"
        @endpoint = getOutstandingQuoteDealByStatus.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
      when "To Be Declined"
        @endpoint = getToBeDeclinedDealByStatus.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
      when "Bound Pending Data Entry"
        @endpoint = getBoundPendingDataEntryDealByStatus.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
      when "Renewable - 6 Months"
        @endpoint = getRenewableDealByStatus.to_s
        @endpoint = @endpoint.to_s + @Additionalendpointval.to_s
        return @endpoint
    end
  end
  def getDealByStatusendpoints(status)
    @statusname = status
    case @statusname
      when "Bound - Pending Actions"
        @endpoint = getBoundDealByStatusendpoints.to_s
        return @endpoint
      when "On Hold"
        @endpoint = getOnHoldDealByStatusendpoints.to_s
        return @endpoint
      when "In Progress"
        @endpoint = getInProgressDealByStatus.to_s
        return @endpoint
      when "Authorize"
        @endpoint = getAuthorizeDealByStatus.to_s
        return @endpoint
      when "Under Review"
        @endpoint = getUnderReviewDealByStatus.to_s
        return @endpoint
      when "Outstanding Quote"
        @endpoint = getOutstandingQuoteDealByStatus.to_s
        return @endpoint
      when "To Be Declined"
        @endpoint = getToBeDeclinedDealByStatus.to_s
        return @endpoint
      when "Bound Pending Data Entry"
        @endpoint = getBoundPendingDataEntryDealByStatus.to_s
        return @endpoint
      when "Renewable - 6 Months"
        @endpoint = getRenewableDealByStatus.to_s
        return @endpoint
    end
  end
  def getSubdivisionQueryparam
    return @@endpointsDoc.search("SubdivisionsQueryParam").text
  end

  def getDealBySubdivisionsendpoints(subdivisions)
    @subdivisionsName = subdivisions
    case @subdivisionsName
      when "Casualty"
        @casualtyTeam = @@endpointsDoc.search("CasualtyTeam").text
        return @casualtyTeam.to_s
      when "Casualty Treaty"
        @CasualtyTreatySubdivision = @@endpointsDoc.search("CasualtyTreatySubdivision").text
        return @CasualtyTreatySubdivision.to_s
      when "Cas Fac"
        @CasFacSubdivision = @@endpointsDoc.search("CasFacSubdivision").text
        return @CasFacSubdivision.to_s
      when "Property"
        @PropertyTeam = @@endpointsDoc.search("PropertyTeam").text
        return @PropertyTeam.to_s
      when "Intl Property"
        @IntlPropertySubdivisions = @@endpointsDoc.search("IntlPropertySubdivisions").text
        return @IntlPropertySubdivisions.to_s
      when "NA Property"
        @NAPropertySubdivisions = @@endpointsDoc.search("NAPropertySubdivisions").text
        return @NAPropertySubdivisions.to_s
      when "Specialty"
        @SpecialtyTeam = @@endpointsDoc.search("SpecialtyTeam").text
        return @SpecialtyTeam.to_s
      when "Specialty Non-PE"
        @SpecialtyNon_PESubdivision = @@endpointsDoc.search("SpecialtyNon-PESubdivision").text
        return @SpecialtyNon_PESubdivision.to_s
      when "Public Entity"
        @PublicEntitySubdivision = @@endpointsDoc.search("PublicEntitySubdivision").text
        return @PublicEntitySubdivision.to_s
      when "All Team"
        @AllTeam = @@endpointsDoc.search("AllTeam").text
        return @AllTeam.to_s
    end
  end

  def getDealsendpoints
    return @@endpointsDoc.search("Deals").text
  end

  def getDealStatusSummariesBySubdivisions(subdivisions)
    @dealStatusSummaries = getDealStatusSummaries.to_s
    @subdivisionQueryparam = getSubdivisionQueryparam
    @dealSubdivision = getDealBySubdivisionsendpoints(subdivisions)

    @dealStatusSummariesBySubdivisions = @dealStatusSummaries+"?"+@subdivisionQueryparam+@dealSubdivision

    return @dealStatusSummariesBySubdivisions
  end

  def getDealByStatusAndSubdivisions(status, subdivisions)
    @dealStatus = getDealByStatusendpoints(status)
    @subdivisionQueryparam = getSubdivisionQueryparam
    # @dealSubdivision = getDealBySubdivisionsendpoints(subdivisions)
    @dealSubdivision = getSubdivisionsendpoints(subdivisions)
    if status == "All"
      @dealByStatusAndSubDivision = getDealsendpoints.to_s+@subdivisionQueryparam+@dealSubdivision
    else
      @dealByStatusAndSubDivision = @dealStatus+"&"+@subdivisionQueryparam+@dealSubdivision
      #print @dealByStatusAndSubDivision
    end
    return @dealByStatusAndSubDivision
  end

  def getCountofSubDivisionsStatusTypes

    # print @@endpointsDoc.search("SubDivisionsByStatusType").text + "&statuscodes=3,80,2,14,29"
    return @@endpointsDoc.search("SubDivisionsByStatusType").text + "&statuscodes=3,80,2,14,29"
  end
  def getNotesByDealNumber(dealnumber)
    @finalendpoint = @@endpointsDoc.search("getNotesByDealNumber").text
    @finalendpoint = @finalendpoint.to_s + dealnumber.to_s
    return @finalendpoint
  end
  def postNote
    @finalendpoint = @@endpointsDoc.search("postNote").text
    return @finalendpoint
  end

  def putNotes
    @finalendpoint = @@endpointsDoc.search("putNotes").text
    return @finalendpoint
  end

  def getNoteTypes(assumedCededAllFlag)
    @finalendpoint = @@endpointsDoc.search("getNoteTypes").text
    @finalendpoint = @finalendpoint.to_s + assumedCededAllFlag
    return @finalendpoint
  end

  def getNotesByNoteNumber(noteNumber)
    @finalendpoint = @@endpointsDoc.search("getNotesByNoteNumber").text
    @finalendpoint = @finalendpoint.to_s + noteNumber.to_s
    return @finalendpoint
  end


  def getWritingCompanies(flag)
    @finalendpoint = @@endpointsDoc.search("WritingCompanies").text
    @finalendpoint = @finalendpoint.to_s + flag.to_s
    return @finalendpoint
  end
  def getCedantSearch(cedantname,parentgroupname,cedantid,parentgroupid,locationid)
    @endpoint = @@endpointsDoc.search("CedantSearch").text
    # puts "endpoint= " + @endpoint

    # puts "finalendpoint="+@finalendpoint
    if (cedantname == ' ' and parentgroupname == ' ' and cedantid == ' ' and parentgroupid == ' ' and locationid == ' ')
      @finalendpoint=@endpoint+"?cedantname&parentgroupname&cedantid&parentgroupid&locationid&"

    elsif (cedantname != '' and parentgroupname == '' and cedantid == '' and parentgroupid == '' and locationid == '')
      @finalendpoint=@endpoint+"?cedantname="+cedantname+"&parentgroupname&CedantId&ParentGroupId&LocationId"

    elsif (cedantname == '' and parentgroupname != '' and cedantid == '' and parentgroupid == '' and locationid == '')
      @finalendpoint=@endpoint+"?cedantname&parentgroupname="+parentgroupname+ "&CedantId&ParentGroupId&LocationId"
    else
      @finalendpoint=@endpoint

    end
    return @finalendpoint
  end


  def getExposureTypeLookup

    # print @@endpointsDoc.search("ExposureTypeLookup").text
    return @@endpointsDoc.search("ExposureTypeLookup").text
  end

  def getFilters(excelPath,colNameHash,subdivision,productLine, exposureGroup, exposureType, statusCode)
    # @colName = colName
    @colNameHash = colNameHash

    @returnVal = ''
    @excelPath = excelPath
    @arry = [subdivision, productLine, exposureGroup, exposureType]

    for @field in 0..@arry.length-1
      # puts @arry[@field].to_s
      @filter = @arry[@field].split('*')
      for @para in 0...@filter.length
        @rowCounter = 0
        @workbook = RubyXL::Parser.parse(@excelPath)
        @worksheet = @workbook[0]
        @worksheet.each { |row|
          case @field.to_s
            when "0"
              @cellValue1 = @worksheet[@rowCounter][@colNameHash['SubdivisionName']].value
              if @cellValue1 == @filter[@para].gsub(/_/, " ") && @filter[@para].gsub(/_/, " ") != 'NA'
                @subDivisionCodeVal = @worksheet[@rowCounter][@colNameHash['SubdivisionCode']].value
                @subdivionCode = @subdivionCode.to_s + @subDivisionCodeVal.to_s + ','
                break
              # else
                # if @rowCounter == 178
                #   puts "SubdivisionCode "+@filter[@para].gsub(/_/, " ")+" is not valid"
                #   abort "SubdivisionCode "+@filter[@para].gsub(/_/, " ")+" is not valid"
                # end
              end
            when "1"
              @cellValue1 = @worksheet[@rowCounter][@colNameHash['ProductLineName']].value
              if @cellValue1 == @filter[@para].gsub(/_/, " ") && @filter[@para].gsub(/_/, " ") != 'NA'
                @prodLineCodeVal = @worksheet[@rowCounter][@colNameHash['ProductLineCode']].value
                @prodLineCode = @prodLineCode.to_s + @prodLineCodeVal.to_s + ','
                break
              end
            when "2"
              @cellValue1 = @worksheet[@rowCounter][@colNameHash['ExposureGroupName']].value
              if @cellValue1 == @filter[@para].gsub(/_/, " ") && @filter[@para].gsub(/_/, " ") != 'NA'
                @expoGroupCodeVal = @worksheet[@rowCounter][@colNameHash['ExposureGroupCode']].value
                @expoGroupCode = @expoGroupCode.to_s + @expoGroupCodeVal.to_s + ','
                break
              end
            when "3"
              @cellValue1 = @worksheet[@rowCounter][@colNameHash['ExposureTypeName']].value
              if @cellValue1 == @filter[@para].gsub(/_/, " ") && @filter[@para].gsub(/_/, " ") != 'NA'
                @expoTypeCodeVal = @worksheet[@rowCounter][@colNameHash['ExposureTypeCode']].value
                @expoTypeCode = @expoTypeCode.to_s + @expoTypeCodeVal.to_s + ','
                break
              end
          end
          @rowCounter = @rowCounter + 1
        }
      end
    end

    if @subdivionCode.nil?
    elsif @prodLineCode.nil?
      @output =  "subdivisions="+@subdivionCode.chop + "&"
    elsif @expoGroupCode.nil?
      @output = "subdivisions="+@subdivionCode.chop + "&" + "productlines="+@prodLineCode.chop
    elsif @expoTypeCode.nil?
      @output = "subdivisions="+@subdivionCode.chop + "&" + "productlines="+@prodLineCode.chop + "&" + "exposuregroups="+@expoGroupCode.chop
    else
      @output = "subdivisions="+@subdivionCode.chop + "&" +"productlines="+@prodLineCode.chop + "&"  + "exposuregroups="+@expoGroupCode.chop + "&" + "Exposuretypes="+@expoTypeCode.chop
    end

    if statusCode != "NA"
      return @@endpointsDoc.search("DealAdvanceSearch").text+ "StatusCodes=" + statusCode + "&" + @output
    else
      return @@endpointsDoc.search("DealAdvanceSearch").text + @output
    end


  end

  def getWarningFilter(subdivision,productLine, exposureGroup, exposureType, statusCode)
    @subdivision =  "subdivisions="+subdivision + "&"
    @productLine =  "productlines="+productLine + "&"
    @exposureGroup =  "exposuregroups="+exposureGroup + "&"
    @exposureType =  "Exposuretypes="+exposureType
    @statusCode =  "StatusCodes="+statusCode + "&"
    if statusCode != "NA"
      @output =  @@endpointsDoc.search("DealAdvanceSearch").text + @statusCode
      if subdivision != "NA"
        @output = @output + @subdivision
        if productLine != "NA"
          @output = @output + @productLine
          if exposureGroup != "NA"
            @output = @output + @exposureGroup
            if exposureType != "NA"
              @output = @output + @exposureType
            else
              @output
            end
          else
            @output
          end
        else
          @output
        end
      end
    else
      if subdivision != "NA"
        @output =  @@endpointsDoc.search("DealAdvanceSearch").text + @subdivision
        if productLine != "NA"
          @output = @output + @productLine
          if exposureGroup != "NA"
            @output = @output + @exposureGroup
            if exposureType != "NA"
              @output = @output + @exposureType
            else
              @output
            end
          else
            @output
          end
        else
          @output
        end
      end
    end

    return @output
  end

  def putByViewID(viewID)
    @finalendpoint = @@endpointsDoc.search("PutByViewId").text
    @finalendpoint = @finalendpoint.to_s + viewID.to_s
    return @finalendpoint
  end

  def deleteByViewID(viewID)
    @finalendpoint = @@endpointsDoc.search("DeleteByViewId").text
    @finalendpoint = @finalendpoint.to_s + viewID.to_s
    return @finalendpoint
  end

  def getKeyDocumentType(dealNumber,getDocTypesFlag)
    @dealEndPoint = @@endpointsDoc.search("GetByDealNumber").text
    @keyDocTypesEndPoint = @@endpointsDoc.search("GetKeyDocTypes").text
    @finalEndPoint = @dealEndPoint.to_s + dealNumber.to_s + @keyDocTypesEndPoint.to_s + getDocTypesFlag.to_s
    return @finalEndPoint
  end

end