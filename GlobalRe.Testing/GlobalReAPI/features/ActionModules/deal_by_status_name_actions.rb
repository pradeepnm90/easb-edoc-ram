require 'rest-client'
require 'json'
require 'hashie'
require 'rubyXL'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
class DealByStatusNameActions < BaseContainer
  def initialize(reporter)
    @@reporter = reporter
  end
  def GenerateURI(endpointValue)
    BaseContainer.fetchenv
    @@URL = $APP_URL
    @uri = File.join(@@URL, endpointValue)
    return @uri
  end
  def SubmitGETRequest(endpointValue)
    @uri = GenerateURI(endpointValue)
    @usernamevalue="vboya"
    @Auth = BaseContainer.generateUserToken(@usernamevalue)
    @resource = RestClient::Resource.new(@uri)
    begin
      @response = @resource.get(:content_type => :json, :Authorization => @Auth)
    rescue RestClient::ExceptionWithResponse => e
        @response = e.response
    end
    return @response
  end
  def SubmitUserGETRequest(endpointValue,user)
    @uri = GenerateURI(endpointValue)
    puts "URI : " + @uri
    @userType=user
    @usernamevalue="vboya"
    if @userType == "All Access"
      @usernamevalue="vboya"
    elsif @userType == "UW"
      @usernamevalue="Mike.McCarthy"
    elsif @userType == "NPTA"
      @usernamevalue="rcorbin"
    elsif @userType == "PTA"
      @usernamevalue="Elena.Marshall"
    elsif @userType == "Actuary"
      @usernamevalue="lslader"
    elsif @userType == "Actuary Manager"
      @usernamevalue="Todd.Glassman"
    elsif @userType == "UW Manager"
      @usernamevalue="Andrew.Barnard"
    end
    # print @usernamevalue
    @Auth = BaseContainer.generateUserToken(@usernamevalue)
    puts "Auth "+ @Auth
    @resource = RestClient::Resource.new(@uri)
    begin
      @response = @resource.get(:content_type => :json, :Authorization => @Auth)
    rescue RestClient::ExceptionWithResponse => e
      @response = e.response
    end
    return @response
  end
  def VerifyResponseStatusCode(response)
    @response=response
    # puts @response.to_s
    @ResponseStatusCode = @response.code
    return @ResponseStatusCode
  end
  def GetResponseMessage(response)
    @response=response
    @ResponseStatusMessage = @response.body
    return @ResponseStatusMessage
  end

  def CheckServiceProvisioned(responsecodeval)
    @responsecode = responsecodeval
    if @responsecode == "200"
      print "PASSED - The GRS environment has the service name provisioned.\n"
      #@@reporter.ReportAction("PASSED - The GRS environment has the service name provisioned.\n")
    else
      print "The GRS environment does not have the service name provisioned.\n"
      fail "The GRS environment does not have the service name provisioned.\n"
      #@@reporter.ReportAction("The GRS environment does not have the service name provisioned.\n")
    end
  end
  def CheckSuccessfulResponse(responsecodeval)
    @responsecode = responsecodeval
    if @responsecode == "200"
      print "PASSED - Success response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("PASSED - Success response " + @responsecode + " is received. \n")
    elsif @responsecode == "404"
      print "PASSED - No Data found " + @responsecode + " is received. \n"
    else
      print "FAILED - Response " + @responsecode + " is received. \n"
      fail "FAILED - Response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("FAILED - Response " + @responsecode + " is received. \n")
    end
    #BaseContainer.ExecuteQuery
  end
  def CheckResponseReceived(responsecodeval)
    @responsecode = responsecodeval
    if @responsecode == "200"
      print "PASSED - A success response is received. \n"
      #@@reporter.ReportAction("PASSED - A success response is received. \n")
    else
      print "FAILED - A Failure Response " + @responsecode + " is received. \n"
      fail "FAILED - A Failure Response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("FAILED - A Failure Response " + @responsecode + " is received. \n")
    end
    #BaseContainer.ExecuteQuery
  end
  def CheckErrorResponse(responsecodeval,responsemsg)
    @responsecode = responsecodeval
    @responsemessage = responsemsg
    #print "\n"
    #print @responsecode
    #print "\n"

    if @responsecode == "422"
      print "PASSED - Error response " + @responsecode + " - " + @responsemessage + " is received. \n"
      #@@reporter.ReportAction("PASSED - Error response " + @responsecode + " - " + @responsemessage + " is received.\n")
    else
      print "FAILED - Response " + @responsecode + " is received. \n"
      fail "FAILED - Response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("FAILED - Response " + @responsecode + " is received. \n")
    end
    #BaseContainer.ExecuteQuery
  end
  def compareAPIandDBresult(apiresponse,dbresult)
    @queryresults=dbresult
    @apiresult = apiresponse
    #print "\n"
    #print @queryresults
    #print "\n"
    #print @apiresult
    #print "\n"
    BaseContainer.CompareAPIAndDBResults(@apiresult,@queryresults)
  end
  def sendQuery(sqlquerystring)
    @sqlquery=sqlquerystring
    @resultdata = BaseContainer.ExecuteQuery(@sqlquery)
    if @resultdata.length.to_s != "0"
      print "Query Executed Successfully and fetched " + @@datahash.length.to_s + " records.\n"
      #@@reporter.ReportAction("PASSED - Query Executed Successfully and fetched " + @@datahash.length.to_s + " records.\n")
    else
      print "Query fetched no results.\n"
      # fail "Query fetched no results.\n"
      #@@reporter.ReportAction("FAILED - Query fetched no results.\n")
    end
    return @resultdata
  end
  def sendCountQuery(sqlquerystring)
    @sqlquery=sqlquerystring
    @resultdata = BaseContainer.ExecuteCountQuery(@sqlquery)
    @finalresultdata = @resultdata.gsub(/\[\[{"totalRecords"=>/,'')
    @finalresultdata = @finalresultdata.gsub(/}\]\]/,'')
    # print "\n"
    # print @resultdata
    # print "\n"
    # print @finalresultdata.to_s
    # print "\n"
    #@finalresult = @result[0]
    # print @result
    # print @finalresult
    #@parsedresdata = JSON.parse(@finalresult)
    #@totalrecordsresdata = @parsedresdata["totalRecords"]
    if @resultdata.nil?
      print "FAILED - Query fetched no results.\n"
      fail "FAILED - Query fetched no results.\n"
      #@@reporter.ReportAction("FAILED - Query fetched no results.\n")
    else
      print "PASSED - Query Executed Successfully and fetched the count as " + @finalresultdata.to_s + ".\n"
      #@@reporter.ReportAction("PASSED - Query Executed Successfully and fetched the count as " + @resultdata.to_s + ".\n")
    end
    return @finalresultdata
  end
  def ValidateAPIwithSchema(schema,response)
    return BaseContainer.ValidateSchema(schema,response)
  end

  def CheckResponseforPOST(responsecodeval)
    @responsecode = responsecodeval
    if @responsecode == "201"
      print "PASSED - A success response is received. \n"
    else
      print "FAILED - A Failure Response " + @responsecode + " is received. \n"
      fail "FAILED - A Failure Response " + @responsecode + " is received. \n"
    end
  end

  def SubmitPOSTRequest(endpointValue,payload)
    @uri = GenerateURI(endpointValue)
    puts "URI: "+@uri
    @configObj = Config.new()
    @usernamevalue= @configObj.getUsername
    @Auth = BaseContainer.generateUserToken(@usernamevalue)
    @resource = RestClient::Resource.new(@uri)
    begin
      puts "Payload=" +payload
      @response = RestClient::Request.new({method: :post, url: @uri, payload: payload, headers: {content_type: 'application/json',  Authorization:@Auth}}).execute
    rescue RestClient::ExceptionWithResponse => e
      @response = e.response
    end
    return @response
  end


  def SubmitPUTRequest(endpointValue,payload)
    @uri = GenerateURI(endpointValue)
    puts "URI: "+@uri
    @usernamevalue="aroy"
    @Auth = BaseContainer.generateUserToken(@usernamevalue)
    @resource = RestClient::Resource.new(@uri)
    begin
      puts "Payload=" +payload
      @response = RestClient::Request.new({method: :put, url: @uri, payload: payload, headers: {content_type: 'application/json',  Authorization:@Auth}}).execute
    rescue RestClient::ExceptionWithResponse => e
      @response = e.response
    end
    return @response
  end

  def compareExpectedAndActualResult(expectedResult,actualResult)
    @expectedResult=expectedResult
    @actualResult= actualResult
    if @expectedResult == @actualResult
      print "PASSED - Actual result is matching with expected result \n"
    else
      print "FAILED - Actual result is not matching with expected result \n"
      fail "FAILED - Actual result is not matching with expected result \n"
    end
  end

  def colNameMapping(excelPath)
    @excelPath = excelPath
    @workbook = RubyXL::Parser.parse(@excelPath)
    @worksheet = @workbook[0]
    @count = 0
    @colors = Hash.new
    @row = @worksheet.sheet_data[0]
    @row.cells.each { |cell|
      @val = cell && cell.value
      # puts @val
      @colors[@val] = @count
      @count = @count + 1
    }

     return @colors



  end

  # def getExposureType(excelPath,colNameHash,subdivision,productLine, exposureGroup, exposureType)
  #   # @colName = colName
  #   @colNameHash = colNameHash
  #   @rowCounter = 0
  #   @returnVal = ''
  #   @excelPath = excelPath
  #   @workbook = RubyXL::Parser.parse(@excelPath)
  #   @worksheet = @workbook[0]
  #   @worksheet.each { |row|
  #     @cellValue1 = @worksheet[@rowCounter][@colNameHash['SubdivisionName']].value
  #     if @cellValue1 == subdivision.gsub(/_/, " ") || subdivision.gsub(/_/, " ") == 'NA'
  #         @cell_value2 = @worksheet[@rowCounter][@colNameHash['ProductLineName']].value
  #         if @cell_value2 ==  productLine.gsub(/_/, " ") || productLine.gsub(/_/, " ") == 'NA'
  #           @cell_value3 = @worksheet[@rowCounter][@colNameHash['ExposureGroupName']].value
  #           if @cell_value3 ==  exposureGroup.gsub(/_/, " ") || exposureGroup.gsub(/_/, " ") == 'NA'
  #             @exposureTypeCode = @worksheet[@rowCounter][@colNameHash['ExposureTypeCode']].value
  #             @returnVal = @returnVal.to_s + @exposureTypeCode.to_s + ','
  #             # puts @exposureTypeCode
  #             # @subdivCode = @worksheet[@rowCounter][@colNameHash['SubdivisionCode']].value
  #             # @subdivionCode = @subdivionCode.to_s + @subdivCode.to_s + ','
  #             # @prodCode = @worksheet[@rowCounter][@colNameHash['ProductLineCode']].value
  #             # @productCode = @productCode.to_s + @prodCode.to_s + ','
  #             # @expCode = @worksheet[@rowCounter][@colNameHash['ExposureGroupCode']].value
  #             # @exposureGroupCode = @exposureGroupCode.to_s + @expCode.to_s + ','
  #           end
  #         end
  #     end
  #     @rowCounter = @rowCounter + 1
  #   }
  #   puts @returnVal
  #   puts 'exposure Type code'
  #   @expTypeCodeArr = @returnVal.split(',')
  #   puts @expTypeCodeArr
  #
  # end



  def getExposureTypes(excelPath,colNameHash,subdivision,productLine, exposureGroup, exposureType)
    @colNameHash = colNameHash

    @returnVal = ''
    @excelPath = excelPath
    @arry = [subdivision, productLine, exposureGroup, exposureType]

    @subfilter = @arry[0].split('*')
    for @para in 0...@subfilter.length
      @rowCounter = 0
      @workbook = RubyXL::Parser.parse(@excelPath)
      @worksheet = @workbook[0]
      @worksheet.each { |row|
        @cellValue1 = @worksheet[@rowCounter][@colNameHash['SubdivisionName']].value
        if @cellValue1 == @subfilter[@para].gsub(/_/, " ") || @subfilter[@para].gsub(/_/, " ") == 'NA'
          @prodfilter = @arry[1].split('*')
          for @para1 in 0...@prodfilter.length
            @cell_value2 = @worksheet[@rowCounter][@colNameHash['ProductLineName']].value
            if @cell_value2 ==  @prodfilter[@para1].gsub(/_/, " ") || @prodfilter[@para1].gsub(/_/, " ") == 'NA'
              @expGrpfilter = @arry[2].split('*')
              for @para2 in 0...@expGrpfilter.length
                @cell_value3 = @worksheet[@rowCounter][@colNameHash['ExposureGroupName']].value
                if @cell_value3 ==  @expGrpfilter[@para2].gsub(/_/, " ") || @expGrpfilter[@para2].gsub(/_/, " ") == 'NA'
                  @expTypfilter = @arry[3].split('*')
                  for @para3 in 0...@expGrpfilter.length
                    @cell_value4 = @worksheet[@rowCounter][@colNameHash['ExposureTypeName']].value
                    if @cell_value4 ==  @expTypfilter[@para3].gsub(/_/, " ") || @expTypfilter[@para3].gsub(/_/, " ") == 'NA'
                      @exposureTypeCode = @worksheet[@rowCounter][@colNameHash['ExposureTypeCode']].value
                      @returnVal = @returnVal.to_s + @exposureTypeCode.to_s + ','
                    end

                  end


                end
              end
            end
          end
        end
        @rowCounter = @rowCounter + 1
      }
    end

    # puts @returnVal
    # puts 'exposure Type code'
    @expTypeCodeArr = @returnVal.split(',')
    puts @expTypeCodeArr
    return @expTypeCodeArr
  end


  def self.ValidateDealAdvanceSearchWarnings(responseMessage,expectedWarningMessage)
    @responseMessage = responseMessage
    @expectedWarningMessage = expectedWarningMessage.to_s
    @responseMsg =  JSON.parse(@responseMessage)
    # puts "response message : "+@responseMsg.to_s
    @responseMessage =  @responseMsg["messages"]
    # puts "response message : "+@responseMessage.to_s
    if @responseMessage.to_s.include? @expectedWarningMessage
      print "PASSED - Advance Search warning message : "+"\""+@expectedWarningMessage +"\""+ "is valid for given cedant information.\n"
    else
      print "FAILED - Advance Search warning message : "+"\""+@expectedWarningMessage +"\""+ "is not expected for given cedant information.\n"
      fail "FAILED - Advance Search warning message : "+"\""+@expectedWarningMessage +"\""+ "is not expected for given cedant information"
    end

  end


end