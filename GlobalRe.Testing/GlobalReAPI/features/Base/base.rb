require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require 'json'
require 'xmlrpc/base64'
require 'win32ole'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
require 'tiny_tds'
require 'activerecord-sqlserver-adapter'
require 'hashie'
require 'json-schema'
require 'net/http'
class BaseContainer
  def initialize(reporter)
    @@reporter = reporter
  end
  def self.fetchenv
    $configFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.xml")
    $BaseDir="../GlobalReAPI/features/"
    $SupportDir=$BaseDir+"support/"
    @configdoc = Config.new()
    $ReleaseNumber = @configdoc.getReleaseNumber
    $Environment=@configdoc.getRunEnvironment
    $CommonDir = @configdoc.getCommonDir
    $UtilDir=@configdoc.getUtilityDir
    $ResultDirPath = @configdoc.getResultsDir
    $ActionModulesPath = @configdoc.getActionModuleDir
    if $Environment.upcase == "DEV"
      $APP_URL = @configdoc.getDEVUrl
    else
      $APP_URL = @configdoc.getQAUrl
    end


  end
  def self.generatedealvaluesArray(dealvalues)
    @actualdealvalues = dealvalues
    @actualvaluesarray = @actualdealvalues.split("*").map { |s| s.to_s}
     for @count in 0..@actualvaluesarray.length-1
       if @actualvaluesarray[@count]==""
         @actualvaluesarray[@count]=nil
       end
     end
    return @actualvaluesarray
  end
  def self.generateGoingTomodifyvaluesArray(value)
    @actualgoingtoModifyvalues = value
    @actualgoingtoModifyvaluesarray = @actualgoingtoModifyvalues.split("*").map { |s| s.to_s}
    return @actualgoingtoModifyvaluesarray
  end
  def self.generateFieldsArray(field)
    @fieldData = field
    @updatedFieldData = @fieldData.split("*").map { |s| s.to_s}
    return @updatedFieldData
  end
  def self.generatePutUserToken(user)
    @userName=user.to_s
    @tokenendpointvalue = "/token"
    @tokenURI = GenerateURI(@tokenendpointvalue)
    @token_response = RestClient.post(@tokenURI.to_s,{"username" =>@userName, "password" => "", "grant_type" => "password"}, :accept => :json, :content_type => 'application/x-www-form-urlencoded')
    #@token_response = RestClient.post("http://d-rdc-core01:50090/token",{"username" =>@userName, "password" => "", "grant_type" => "password"})
    #@token_response = RestClient.post("http://d-rdc-core01:50090/token",{"username" =>"sjanardanannair", "password" => "", "grant_type" => "password"})
    #@token_response = RestClient.post("http://q-rdc-core01:50090/token",{"username" =>"sjanardanannair", "password" => "", "grant_type" => "password"})
    @token_response_hash = JSON.parse(@token_response)
    @access_token =  @token_response_hash["access_token"]
    @token_type =  @token_response_hash["token_type"]
    @token = @token_type.capitalize + " " + @access_token
    return @token.to_s
  end
  def self.submitDealPutRequest(endpointValue,dealvalues,field,value)
    @puturi = GenerateURI(endpointValue)
    @configObj = Config.new()
    # print "\n"
    # print @puturi.to_s
    # print "\n"
    # @usernamevalue="vboya"      #---Boya removed the hard coded values
    @usernamevalue= @configObj.getUsername
    puts @usernamevalue
    @Auth = BaseContainer.generatePutUserToken(@usernamevalue)
    @puturi = @puturi.to_s #+ "?token=" + @Auth.to_s
    # print "\n"
    # print @puturi.to_s
    # print "\n"
    # print "\n"
    # print @Auth
    # print "\n"
    @resource = RestClient::Resource.new(@puturi.to_s)
    @actualvaluesarray = generatedealvaluesArray(dealvalues)
    @goingtobeModifiedvaluesarray = generateGoingTomodifyvaluesArray(value)
    @fieldData = generateFieldsArray(field)
    # ----- Splitting the values if multiple values are passed -----
    if @goingtobeModifiedvaluesarray.length == 2
      @value1 = @goingtobeModifiedvaluesarray[0]
      @value2 = @goingtobeModifiedvaluesarray[1]
    elsif @goingtobeModifiedvaluesarray.length == 1
      @value1 = @goingtobeModifiedvaluesarray[0]
    end
    # ----- Splitting the fields if multiple fields are passed -----
    # if @fieldData.length == 2
    #   @fieldValue1 = @fieldData[0]
    #   @fieldValue2 = @fieldData[1]
    # elsif @fieldData.length == 1
    #   @fieldValue1 = @fieldData[0]
    # end
    for @count in 0..@actualvaluesarray.length-1
      if @actualvaluesarray[@count]==""
        @actualvaluesarray[@count]=nil
      end
    end
    @dealNumber = @actualvaluesarray[0].to_i
    @dealName = @actualvaluesarray[1].to_s
    @statusCode = @actualvaluesarray[2].to_i
    @status = @actualvaluesarray[3].to_s

    if @actualvaluesarray[4] == ""
      @contractNumber = nil
    else
      @contractNumber = @actualvaluesarray[4].to_i
    end
    if @actualvaluesarray[5] == ""
      @inceptionDate = nil
    else
      @inceptionDate = @actualvaluesarray[5].to_s
    end
    if @actualvaluesarray[6] == ""
      @targetDate = nil
    else
      @targetDate = @actualvaluesarray[6].to_s
    end
    if @actualvaluesarray[7] == ""
      @priority = nil
    else
      @priority = @actualvaluesarray[7].to_i
    end
    if @actualvaluesarray[8] == ""
      @submittedDate = nil
    else
      @submittedDate = @actualvaluesarray[8].to_s
    end
    if @actualvaluesarray[9] == ""
      @primaryUnderwriterCode = nil
    else
      @primaryUnderwriterCode = @actualvaluesarray[9].to_i
    end
    if @actualvaluesarray[10] == ""
      @primaryUnderwriterName = nil
    else
      @primaryUnderwriterName = @actualvaluesarray[10].to_s
    end
    if @actualvaluesarray[11] == ""
      @secondaryUnderwriterCode = nil
    else
      @secondaryUnderwriterCode = @actualvaluesarray[11].to_i
    end
    if @actualvaluesarray[12] == ""
      @secondaryUnderwriterName = nil
    else
      @secondaryUnderwriterName = @actualvaluesarray[12].to_s
    end
    if @actualvaluesarray[13] == ""
      @technicalAssistantCode = nil
    else
      @technicalAssistantCode = @actualvaluesarray[13].to_i
    end
    if @actualvaluesarray[14] == ""
      @technicalAssistantName = nil
    else
      @technicalAssistantName = @actualvaluesarray[14].to_s
    end
    if @actualvaluesarray[15] == ""
      @modellerCode = nil
    else
      @modellerCode = @actualvaluesarray[15].to_i
    end
    if @actualvaluesarray[16] == ""
      @modellerName = nil
    else
      @modellerName = @actualvaluesarray[16].to_s
    end
    if @actualvaluesarray[17] == ""
      @actuaryCode = nil
    else
      @actuaryCode = @actualvaluesarray[17].to_i
    end
    if @actualvaluesarray[18] == ""
      @actuaryName = nil
    else
      @actuaryName = @actualvaluesarray[18].to_s
    end
    if @actualvaluesarray[19] == ""
      @expiryDate = nil
    else
      @expiryDate = @actualvaluesarray[19].to_s
    end
    if @actualvaluesarray[20] == ""
      @brokerCode = nil
    else
      @brokerCode = @actualvaluesarray[20].to_i
    end
    if @actualvaluesarray[21] == ""
      @brokerName = nil
    else
      @brokerName = @actualvaluesarray[21].to_s
    end
    if @actualvaluesarray[22] == ""
      @brokerContactCode = nil
    else
      @brokerContactCode = @actualvaluesarray[22].to_i
    end
    if @actualvaluesarray[23] == ""
      @brokerContactName = nil
    else
      @brokerContactName = @actualvaluesarray[23].to_s
    end
    if @actualvaluesarray[24] == ""
      @cedantCode = nil
    else
      @cedantCode = @actualvaluesarray[24].to_i
    end
    if @actualvaluesarray[25] == ""
      @cedantName = nil
    else
      @cedantName = @actualvaluesarray[25].to_s
    end
    if @actualvaluesarray[26] == ""
      @continuous = nil
    else
      @continuous = @actualvaluesarray[26].to_s
    end
    if @actualvaluesarray[27] == ""
      @cedantLocationCode = nil
    else
      @cedantLocationCode = @actualvaluesarray[27].to_i
    end
    if @actualvaluesarray[28] == ""
      @cedantLocationName = nil
    else
      @cedantLocationName = @actualvaluesarray[28].to_s
    end
    if @actualvaluesarray[29] == ""
      @brokerLocationCode = nil
    else
      @brokerLocationCode = @actualvaluesarray[29].to_i
    end
    if @actualvaluesarray[30] == ""
      @brokerLocationName = nil
    else
      @brokerLocationName = @actualvaluesarray[30].to_s
    end
    if @actualvaluesarray[31] == ""
      @paper = nil
    else
      @paper = @actualvaluesarray[31].to_s
    end
    if @actualvaluesarray[32] == ""
      @renewal = nil
    else
      @renewal = @actualvaluesarray[32].to_i
    end
    if @actualvaluesarray[33] == ""
      @expiresAtEndOfDate = nil
    else
      @expiresAtEndOfDate = @actualvaluesarray[33].to_s
    end
    if @actualvaluesarray[34] == ""
      @exposureTypeCode = nil
    else
      @exposureTypeCode = @actualvaluesarray[34].to_i
    end
    if @actualvaluesarray[35] == ""
      @dealTypeCode = nil
    else
      @dealTypeCode = @actualvaluesarray[35].to_i
    end
    if @actualvaluesarray[36] == ""
      @dealTypeName = nil
    else
      @dealTypeName = @actualvaluesarray[36].to_s
    end
    if @actualvaluesarray[37] == ""
      @coverageType = nil
    else
      @coverageType = @actualvaluesarray[37].to_i
    end
    if @actualvaluesarray[38] == ""
      @coverageName = nil
    else
      @coverageName = @actualvaluesarray[38].to_s
    end
    if @actualvaluesarray[39] == ""
      @policyBasis = nil
    else
      @policyBasis = @actualvaluesarray[39].to_i
    end
    if @actualvaluesarray[40] == ""
      @policyBasisName = nil
    else
      @policyBasisName = @actualvaluesarray[40].to_s
    end
    if @actualvaluesarray[41] == ""
      @currencyCode = nil
    else
      @currencyCode = @actualvaluesarray[41].to_i
    end
    if @actualvaluesarray[42] == ""
      @currencyName = nil
    else
      @currencyName = @actualvaluesarray[42].to_s
    end
    if @actualvaluesarray[43] == ""
      @domicileCode = nil
    else
      @domicileCode = @actualvaluesarray[43].to_i
    end
    if @actualvaluesarray[44] == ""
      @domicileName = nil
    else
      @domicileName = @actualvaluesarray[44].to_s
    end
    if @actualvaluesarray[45] == ""
      @regionCode = nil
    else
      @regionCode = @actualvaluesarray[45].to_i
    end
    if @actualvaluesarray[46] == ""
      @regionName = nil
    else
      @regionName = @actualvaluesarray[46].to_s
    end
    for @field in 0..@fieldData.length-1
      case @fieldData[@field]
        when "Deal Name"
          @dealName = @value1.to_s
        when "Target Date"
          @targetDate = @value1.to_s
        when "Status"
          @status = @value1.to_s
          @statusCode = @value2.to_s
          if @status == "Bound"
            @statusCode = "0"
          end
        when "Underwriter"
          @primaryUnderwriterName = @value1.to_s
          @primaryUnderwriterCode = @value2.to_s
        when "Underwriter 2"
          @secondaryUnderwriterName = @value1.to_s
          @secondaryUnderwriterCode = @value2.to_s
        when "TA"
          @technicalAssistantName = @value1.to_s
          @technicalAssistantCode = @value2.to_s
        when "Modeler"
          @modellerName = @value1.to_s
          @modellerCode = @value2.to_s
        when "Actuary"
          @actuaryName = @value1.to_s
          @actuaryCode = @value2.to_s
        when "Priority"
          @priority = @value1.to_s
        when "ExposureTypeCode"
          @exposureTypeCode = @value1.to_s
        when "CedantCode"
          @cedantCode = @value1.to_s
        when "CedantLocationCode"
          @cedantLocationCode = @value2.to_s
      end
     end
    @payload = "{\"dealNumber\": \"#{@dealNumber}\",\"dealName\": \"#{@dealName}\",\"statusCode\": \"#{@statusCode}\",\"status\": \"#{@status}\",\"contractNumber\": \"#{@contractNumber}\",\"inceptionDate\": \"#{@inceptionDate}\",\"targetDate\": \"#{@targetDate}\",\"priority\": \"#{@priority}\",\"submittedDate\": \"#{@submittedDate}\",\"primaryUnderwriterCode\": \"#{@primaryUnderwriterCode}\",\"primaryUnderwriterName\": \"#{@primaryUnderwriterName}\",\"secondaryUnderwriterCode\": \"#{@secondaryUnderwriterCode}\",\"secondaryUnderwriterName\": \"#{@secondaryUnderwriterName}\",\"technicalAssistantCode\": \"#{@technicalAssistantCode}\",\"technicalAssistantName\": \"#{@technicalAssistantName}\",\"modellerCode\": \"#{@modellerCode}\",\"modellerName\": \"#{@modellerName}\",\"actuaryCode\": \"#{@actuaryCode}\",\"actuaryName\": \"#{@actuaryName}\",\"expiryDate\": \"#{@expiryDate}\",\"brokerCode\": \"#{@brokerCode}\",\"brokerName\": \"#{@brokerName}\",\"brokerContactCode\": \"#{@brokerContactCode}\",\"brokerContactName\": \"#{@brokerContactName}\",\"cedantCode\": \"#{@cedantCode}\",\"cedantName\": \"#{@cedantName}\",\"continuous\": \"#{@continuous}\",\"cedantLocationCode\": \"#{@cedantLocationCode}\",\"cedantLocationName\": \"#{@cedantLocationName}\",\"brokerLocationCode\": \"#{@brokerLocationCode}\",\"brokerLocationName\": \"#{@brokerLocationName}\",\"paper\": \"#{@paper}\",\"renewal\": \"#{@renewal}\",\"expiresAtEndOfDate\": \"#{@expiresAtEndOfDate}\",\"exposureTypeCode\": \"#{@exposureTypeCode}\",\"dealTypeCode\": \"#{@dealTypeCode}\",\"dealTypeName\": \"#{@dealTypeName}\",\"coverageType\": \"#{@coverageType}\",\"coverageName\": \"#{@coverageName}\",\"policyBasis\": \"#{@policyBasis}\",\"policyBasisName\": \"#{@policyBasisName}\",\"currencyCode\": \"#{@currencyCode}\",\"currencyName\": \"#{@currencyName}\",\"domicileCode\": \"#{@domicileCode}\",\"domicileName\": \"#{@domicileName}\",\"regionCode\": \"#{@regionCode}\",\"regionName\": \"#{@regionName}\"}"
    #@payload = "{\"dealNumber\": #{@dealNumber},\"dealName\": \"#{@dealName}\",\"statusCode\": #{@statusCode},\"status\": \"#{@status}\",\"contractNumber\": #{@contractNumber},\"inceptionDate\": \"#{@inceptionDate}\",\"targetDate\": \"#{@targetDate}\",\"priority\": \"#{@priority}\",\"submittedDate\": \"#{@submittedDate}\",\"primaryUnderwriterCode\": #{@primaryUnderwriterCode},\"primaryUnderwriterName\": \"#{@primaryUnderwriterName}\",\"secondaryUnderwriterCode\": #{@secondaryUnderwriterCode},\"secondaryUnderwriterName\": \"#{@secondaryUnderwriterName}\",\"technicalAssistantCode\": #{@technicalAssistantCode},\"technicalAssistantName\": \"#{@technicalAssistantName}\",\"modellerCode\": #{@modellerCode},\"modellerName\": \"#{@modellerName}\",\"actuaryCode\": #{@actuaryCode},\"actuaryName\": \"#{@actuaryName}\",\"expiryDate\": \"#{@expiryDate}\",\"brokerCode\": #{@brokerCode},\"brokerName\": \"#{@brokerName}\",\"brokerContactCode\": #{@brokerContactCode},\"brokerContactName\": \"#{@brokerContactName}\"}"
    @payload = @payload.gsub(': ""',': null')
    @payload = @payload.gsub(': "0"',': null')
    if @status == "Bound"
      @payload = @payload.gsub('statusCode": null','statusCode": 0')
    end
    # print "\n"
    # puts "Payload :" + @payload
    # print "\n"
    # print "parsed" + JSON.parse(@payload.to_s).to_s
    # print "\n"
    # #@jsonbody = JSON.parse(@payload.to_s)
    begin
      @dealputresponse = RestClient.put(@puturi.to_s,@payload,{:content_type => 'application/json',:Authorization => @Auth})
    rescue RestClient::ExceptionWithResponse => e
      @dealputresponse = e.response

      # puts "Response:" +@dealputresponse.to_s

    end
    # print "\n"
    # print @dealputresponse
    # print "\n"
    $putRequestbody = @payload
    # print "\n"
    #  puts "Request body:"+$putRequestbody.to_s
    # print "\n"
    #
    return @dealputresponse
  end
  def self.CheckDealPutSuccessfulResponse(responsecodeval,status)
    @responsecode = responsecodeval
    if @responsecode == "200" && status.to_s != "Bound"
      print "PASSED - Success response " + @responsecode + " is received. \n"
    elsif @responsecode != "200" && status.to_s != "Bound"
      print "FAILED - Response " + @responsecode + " is received. \n"
      fail "FAILED - Response " + @responsecode + " is received. \n"
    elsif @responsecode != "200" && status.to_s == "Bound"
      print "PASSED - Response " + @responsecode + " is received as the transaction is of #{status} status.\n"
    end
    #BaseContainer.ExecuteQuery
  end

  # -------- Warning response code validation ----------
  def self.CheckDealPutWarningResponse(responsecodeval,status)
    @responsecode = responsecodeval
    if @responsecode == "404" && status.to_s != "Bound"
      print "PASSED - Warning response code " + @responsecode + " is received. \n"
    elsif @responsecode == "200" && status.to_s != "Bound"
      print "PASSED - Response " + @responsecode + " is received as cedant information is valid.\n"
      BaseContainer.CheckDealPutSuccessfulResponse(responsecodeval,status)
    elsif @responsecode != "404" && status.to_s != "Bound"
      print "FAILED - Response " + @responsecode + " is received. \n"
      fail "FAILED - Response " + @responsecode + " is received. \n"
    elsif @responsecode != "404" && status.to_s == "Bound"
      print "PASSED - Response " + @responsecode + " is received as the transaction is of #{status} status.\n"
    end
    #BaseContainer.ExecuteQuery
  end

  def self.CheckDealGetSuccessfulResponse(responsecodeval,status)
    @responsecode = responsecodeval
    if @responsecode == "200" #&& status.to_s != "Bound"
      print "PASSED - Success response " + @responsecode + " is received. \n"
    elsif @responsecode != "200" #&& status.to_s != "Bound"
      print "FAILED - Response " + @responsecode + " is received. \n"
      fail "FAILED - Response " + @responsecode + " is received. \n"
    # elsif @responsecode != "200" && status.to_s == "Bound"
    #   print "PASSED - Response " + @responsecode + " is received as the transaction is of #{status} status.\n"
    end
    #BaseContainer.ExecuteQuery
  end
  def self.resetsubmitDealPutRequest(endpointValue,dealvalues)
    @puturi = GenerateURI(endpointValue)
    @configObj = Config.new()
    @usernamevalue= @configObj.getUsername
    # @usernamevalue="vboya"
    @Auth = BaseContainer.generatePutUserToken(@usernamevalue)
    @resource = RestClient::Resource.new(@puturi)
    @actualvaluesarray = generatedealvaluesArray(dealvalues)
    for @count in 0..@actualvaluesarray.length-1
      if @actualvaluesarray[@count]==""
        @actualvaluesarray[@count]=nil
      end
    end
    @dealNumber = @actualvaluesarray[0].to_i
    @dealName = @actualvaluesarray[1].to_s
    @statusCode = @actualvaluesarray[2].to_i
    @status = @actualvaluesarray[3].to_s
    # if @status == "Bound"
    #   @statusCode = "0"
    # end
    if @actualvaluesarray[4] == ""
      @contractNumber = nil
    else
      @contractNumber = @actualvaluesarray[4].to_i
    end
    if @actualvaluesarray[5] == ""
      @inceptionDate = nil
    else
      @inceptionDate = @actualvaluesarray[5].to_s
    end
    if @actualvaluesarray[6] == ""
      @targetDate = nil
    else
      @targetDate = @actualvaluesarray[6].to_s
    end
    if @actualvaluesarray[7] == ""
      @priority = nil
    else
      @priority = @actualvaluesarray[7].to_i
    end
    if @actualvaluesarray[8] == ""
      @submittedDate = nil
    else
      @submittedDate = @actualvaluesarray[8].to_s
    end
    if @actualvaluesarray[9] == ""
      @primaryUnderwriterCode = nil
    else
      @primaryUnderwriterCode = @actualvaluesarray[9].to_i
    end
    if @actualvaluesarray[10] == ""
      @primaryUnderwriterName = nil
    else
      @primaryUnderwriterName = @actualvaluesarray[10].to_s
    end
    if @actualvaluesarray[11] == ""
      @secondaryUnderwriterCode = nil
    else
      @secondaryUnderwriterCode = @actualvaluesarray[11].to_i
    end
    if @actualvaluesarray[12] == ""
      @secondaryUnderwriterName = nil
    else
      @secondaryUnderwriterName = @actualvaluesarray[12].to_s
    end
    if @actualvaluesarray[13] == ""
      @technicalAssistantCode = nil
    else
      @technicalAssistantCode = @actualvaluesarray[13].to_i
    end
    if @actualvaluesarray[14] == ""
      @technicalAssistantName = nil
    else
      @technicalAssistantName = @actualvaluesarray[14].to_s
    end
    if @actualvaluesarray[15] == ""
      @modellerCode = nil
    else
      @modellerCode = @actualvaluesarray[15].to_i
    end
    if @actualvaluesarray[16] == ""
      @modellerName = nil
    else
      @modellerName = @actualvaluesarray[16].to_s
    end
    if @actualvaluesarray[17] == ""
      @actuaryCode = nil
    else
      @actuaryCode = @actualvaluesarray[17].to_s
    end
    if @actualvaluesarray[18] == ""
      @actuaryName = nil
    else
      @actuaryName = @actualvaluesarray[18].to_s
    end
    if @actualvaluesarray[19] == ""
      @expiryDate = nil
    else
      @expiryDate = @actualvaluesarray[19].to_s
    end
    if @actualvaluesarray[20] == ""
      @brokerCode = nil
    else
      @brokerCode = @actualvaluesarray[20].to_i
    end
    if @actualvaluesarray[21] == ""
      @brokerName = nil
    else
      @brokerName = @actualvaluesarray[21].to_s
    end
    if @actualvaluesarray[22] == ""
      @brokerContactCode = nil
    else
      @brokerContactCode = @actualvaluesarray[22].to_i
    end
    if @actualvaluesarray[23] == ""
      @brokerContactName = nil
    else
      @brokerContactName = @actualvaluesarray[23].to_s
    end
    if @actualvaluesarray[24] == ""
      @cedantCode = nil
    else
      @cedantCode = @actualvaluesarray[24].to_i
    end
    if @actualvaluesarray[25] == ""
      @cedantName = nil
    else
      @cedantName = @actualvaluesarray[25].to_s
    end
    if @actualvaluesarray[26] == ""
      @continuous = nil
    else
      @continuous = @actualvaluesarray[26].to_s
    end
    if @actualvaluesarray[27] == ""
      @cedantLocationCode = nil
    else
      @cedantLocationCode = @actualvaluesarray[27].to_i
    end
    if @actualvaluesarray[28] == ""
      @cedantLocationName = nil
    else
      @cedantLocationName = @actualvaluesarray[28].to_s
    end
    if @actualvaluesarray[29] == ""
      @brokerLocationCode = nil
    else
      @brokerLocationCode = @actualvaluesarray[29].to_i
    end
    if @actualvaluesarray[30] == ""
      @brokerLocationName = nil
    else
      @brokerLocationName = @actualvaluesarray[30].to_s
    end
    if @actualvaluesarray[31] == ""
      @paper = nil
    else
      @paper = @actualvaluesarray[31].to_s
    end
    if @actualvaluesarray[32] == ""
      @renewal = nil
    else
      @renewal = @actualvaluesarray[32].to_i
    end
    if @actualvaluesarray[33] == ""
      @expiresAtEndOfDate = nil
    else
      @expiresAtEndOfDate = @actualvaluesarray[33].to_s
    end
    if @actualvaluesarray[34] == ""
      @exposureTypeCode = nil
    else
      @exposureTypeCode = @actualvaluesarray[34].to_i
    end
    if @actualvaluesarray[35] == ""
      @dealTypeCode = nil
    else
      @dealTypeCode = @actualvaluesarray[35].to_i
    end
    if @actualvaluesarray[36] == ""
      @dealTypeName = nil
    else
      @dealTypeName = @actualvaluesarray[36].to_s
    end
    if @actualvaluesarray[37] == ""
      @coverageType = nil
    else
      @coverageType = @actualvaluesarray[37].to_i
    end
    if @actualvaluesarray[38] == ""
      @coverageName = nil
    else
      @coverageName = @actualvaluesarray[38].to_s
    end
    if @actualvaluesarray[39] == ""
      @policyBasis = nil
    else
      @policyBasis = @actualvaluesarray[39].to_i
    end
    if @actualvaluesarray[40] == ""
      @policyBasisName = nil
    else
      @policyBasisName = @actualvaluesarray[40].to_s
    end
    if @actualvaluesarray[41] == ""
      @currencyCode = nil
    else
      @currencyCode = @actualvaluesarray[41].to_i
    end
    if @actualvaluesarray[42] == ""
      @currencyName = nil
    else
      @currencyName = @actualvaluesarray[42].to_s
    end
    if @actualvaluesarray[43] == ""
      @domicileCode = nil
    else
      @domicileCode = @actualvaluesarray[43].to_i
    end
    if @actualvaluesarray[44] == ""
      @domicileName = nil
    else
      @domicileName = @actualvaluesarray[44].to_s
    end
    if @actualvaluesarray[45] == ""
      @regionCode = nil
    else
      @regionCode = @actualvaluesarray[45].to_i
    end
    if @actualvaluesarray[46] == ""
      @regionName = nil
    else
      @regionName = @actualvaluesarray[46].to_s
    end
    @payload = "{\"dealNumber\": \"#{@dealNumber}\",\"dealName\": \"#{@dealName}\",\"statusCode\": \"#{@statusCode}\",\"status\": \"#{@status}\",\"contractNumber\": \"#{@contractNumber}\",\"inceptionDate\": \"#{@inceptionDate}\",\"targetDate\": \"#{@targetDate}\",\"priority\": \"#{@priority}\",\"submittedDate\": \"#{@submittedDate}\",\"primaryUnderwriterCode\": \"#{@primaryUnderwriterCode}\",\"primaryUnderwriterName\": \"#{@primaryUnderwriterName}\",\"secondaryUnderwriterCode\": \"#{@secondaryUnderwriterCode}\",\"secondaryUnderwriterName\": \"#{@secondaryUnderwriterName}\",\"technicalAssistantCode\": \"#{@technicalAssistantCode}\",\"technicalAssistantName\": \"#{@technicalAssistantName}\",\"modellerCode\": \"#{@modellerCode}\",\"modellerName\": \"#{@modellerName}\",\"actuaryCode\": \"#{@actuaryCode}\",\"actuaryName\": \"#{@actuaryName}\",\"expiryDate\": \"#{@expiryDate}\",\"brokerCode\": \"#{@brokerCode}\",\"brokerName\": \"#{@brokerName}\",\"brokerContactCode\": \"#{@brokerContactCode}\",\"brokerContactName\": \"#{@brokerContactName}\",\"cedantCode\": \"#{@cedantCode}\",\"cedantName\": \"#{@cedantName}\",\"continuous\": \"#{@continuous}\",\"cedantLocationCode\": \"#{@cedantLocationCode}\",\"cedantLocationName\": \"#{@cedantLocationName}\",\"brokerLocationCode\": \"#{@brokerLocationCode}\",\"brokerLocationName\": \"#{@brokerLocationName}\",\"paper\": \"#{@paper}\",\"renewal\": \"#{@renewal}\",\"expiresAtEndOfDate\": \"#{@expiresAtEndOfDate}\",\"exposureTypeCode\": \"#{@exposureTypeCode}\",\"dealTypeCode\": \"#{@dealTypeCode}\",\"dealTypeName\": \"#{@dealTypeName}\",\"coverageType\": \"#{@coverageType}\",\"coverageName\": \"#{@coverageName}\",\"policyBasis\": \"#{@policyBasis}\",\"policyBasisName\": \"#{@policyBasisName}\",\"currencyCode\": \"#{@currencyCode}\",\"currencyName\": \"#{@currencyName}\",\"domicileCode\": \"#{@domicileCode}\",\"domicileName\": \"#{@domicileName}\",\"regionCode\": \"#{@regionCode}\",\"regionName\": \"#{@regionName}\"}"
    @payload = @payload.gsub(': ""',': null')
    @payload = @payload.gsub(': "0"',': null')
    if @status == "Bound"
      @payload = @payload.gsub('statusCode": null','statusCode": 0')
    end


    # print "\n"
    # print @payload
    # print "\n"
    begin
      @resetdealputresponse = RestClient.put(@puturi.to_s,@payload,{:content_type => 'application/json',:Authorization => @Auth})
    rescue RestClient::ExceptionWithResponse => e
      @resetdealputresponse = e.response
    end
    #return @resetdealputresponse
    #
    @DealByStatusName = DealByStatusNameActions.new(@reporter)
    @responsecode = @DealByStatusName.VerifyResponseStatusCode(@resetdealputresponse).to_s
    if @responsecode == "200" && @status.to_s != "Bound"
      print "PASSED - Success response " + @responsecode + " is received for the reset data Put request.\n"
    elsif @responsecode != "200" && @status.to_s != "Bound"
      print "FAILED - Response " + @responsecode + " is received for the reset data Put request.\n"
      fail "FAILED - Response " + @responsecode + " is received for the reset data Put request.\n"
    end
  end

  def self.compareDealputRequestvalueAndgetResponsevalue(putRequestBodyValue,responseresvalue,status)
    @modifiedputRequestBodyValue = putRequestBodyValue.to_s.gsub('"','')
    @modifiedgetresponseresvalue = putRequestBodyValue.to_s.gsub('"','')
    # print "\n"
    # print @modifiedputRequestBodyValue.to_s
    # print "\n"
    # print @modifiedgetresponseresvalue.to_s
    # print "\n"
    if status.to_s == "Bound"
      print "Skipping Put Deal Request Body and Get Deal response comparison as it is a #{status} status deal.\n"
    else
      if @modifiedputRequestBodyValue.to_s == @modifiedgetresponseresvalue.to_s
        print "PASSED - Put Deal By Deal Number Request Body value matched successfully with Get Deal By Deal Number response.\n"
      else
        print "FAILED - Put Deal By Deal Number Request Body failed to match with Get Deal By Deal Number response.\n"
        fail "FAILED - Put Deal By Deal Number Request Body failed to match with Get Deal By Deal Number response.\n"
      end
    end
  end

  def self.generateToken
    #@userName="sjanardanannair"
    #@token_response = RestClient.post("http://d-rdc-core01:50090/token",{"username" =>@userName, "password" => "", "grant_type" => "password"})
    #@token_response = RestClient.post("http://d-rdc-core01:50090/token",{"username" =>"sjanardanannair", "password" => "", "grant_type" => "password"})
    #@token_response = RestClient.post("http://q-rdc-core01:50090/token",{"username" =>"sjanardanannair", "password" => "", "grant_type" => "password"})
    @configObj = Config.new()
    @usernamevalue= @configObj.getUsername
    @tokenendpointvalue = "/token"
    @tokenURI = GenerateURI(@tokenendpointvalue)
    # @token_response = RestClient.post(@tokenURI.to_s,{"username" =>"vboya", "password" => "", "grant_type" => "password"})
    @token_response = RestClient.post(@tokenURI.to_s,{"username" =>@usernamevalue.to_s, "password" => "", "grant_type" => "password"})
    @token_response_hash = JSON.parse(@token_response)
    @access_token =  @token_response_hash["access_token"]
    @token_type =  @token_response_hash["token_type"]
    @token = @token_type.capitalize + " " + @access_token
    return @token.to_s
  end
  def self.GenerateURI(endpointValue)
    fetchenv
    @@URL = $APP_URL
    @uri = File.join(@@URL, endpointValue)
    return @uri
  end

  def self.generateUserToken(user)
    @userName=user.to_s
    @tokenendpointvalue = "/token"
    @tokenURI = GenerateURI(@tokenendpointvalue)
    @token_response = RestClient.post(@tokenURI.to_s,{"username" =>@userName, "password" => "", "grant_type" => "password"})
    #@token_response = RestClient.post("http://d-rdc-core01:50090/token",{"username" =>@userName, "password" => "", "grant_type" => "password"})
    #@token_response = RestClient.post("http://d-rdc-core01:50090/token",{"username" =>"sjanardanannair", "password" => "", "grant_type" => "password"})
    #@token_response = RestClient.post("http://q-rdc-core01:50090/token",{"username" =>"sjanardanannair", "password" => "", "grant_type" => "password"})
    @token_response_hash = JSON.parse(@token_response)
    @access_token =  @token_response_hash["access_token"]
    @token_type =  @token_response_hash["token_type"]
    @token = @token_type.capitalize + " " + @access_token
    return @token.to_s
  end

  def self.ExecuteQuery(querystring)
    #####dont delete###########
    #@password="ZEB0QGgwZwo="
    #@decoded_password = XMLRPC::Base64.decode(@password)
    #@@dbconnector = SqlServerConnector.new('ERMSDB_DEV', 'sjanardanannair', @decoded_password, pDBname)
    #@client = TinyTds::Client.new username: 'maxbi', password: 'd@t@h0g', host: 'VA1-TGMRSQL072.markelcorp.markelna.com', database: 'ErmsUATHotFix'
    #@client = TinyTds::Client.new username: 'sjanardanannair', password: @decoded_password, host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    #@client = TinyTds::Client.new username: 'sjanardanannair', host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    ######################3####
    #Get the Environment Details
    @configdoc = Config.new()
    $Environment=@configdoc.getRunEnvironment

    if $Environment.upcase == "DEV"
      $APP_URL = @configdoc.getDEVUrl
    else
      $APP_URL = @configdoc.getQAUrl
    end
    @@datahash = Array.new
    @sqlquerystring = querystring

    if $Environment.upcase == "DEV"
      # DEV Server details
      @client = TinyTds::Client.new(:username => 'maxbi', :password => 'd@t@h0g', :dataserver => 'va1-dgmrsql053.markelcorp.markelna.com', :database => 'DEV_ERMS', :timeout => 0)
    else
      # QA Server details
      @client = TinyTds::Client.new(:username => 'maxbi', :password => 'd@t@h0g', :dataserver => 'VA1-TGMRSQL060.MARKELCORP.MARKELNA.COM', :database => 'GRSQA_ERMS', :timeout => 0)
    end
    #@client = TinyTds::Client.new username: 'maxbi', password: 'd@t@h0g', host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    # @client = TinyTds::Client.new(:username => 'maxbi', :password => 'd@t@h0g', :dataserver => 'va1-dgmrsql053.markelcorp.markelna.com', :database => 'DEV_ERMS', :timeout => 0)

    @result = @client.execute(@sqlquerystring)
    @rowcount = 0
    @result.each_with_index do |row|
      @@datahash[@rowcount]=row
      @rowcount = @rowcount+1
    end
    @client.close
    return @@datahash
    ###dont delete######
    ###@dbobj = SQLQuery.new('DEV_ERMS')
    ###puts "successfully connected"
    ###query = "select * from GRS.v_Deals;"
    ###recordset = WIN32OLE.new('ADODB.Recordset')
    ###a = @dbobj.query(query, 'select', nil, recordset)
    ###puts a.GetString(2)
    ####################
  end

  def self.ExecuteCountQuery(querystring)
    #####dont delete###########
    #@password="ZEB0QGgwZwo="
    #@decoded_password = XMLRPC::Base64.decode(@password)
    #@@dbconnector = SqlServerConnector.new('ERMSDB_DEV', 'sjanardanannair', @decoded_password, pDBname)
    #@client = TinyTds::Client.new username: 'maxbi', password: 'd@t@h0g', host: 'VA1-TGMRSQL072.markelcorp.markelna.com', database: 'ErmsUATHotFix'
    #@client = TinyTds::Client.new username: 'sjanardanannair', password: @decoded_password, host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    #@client = TinyTds::Client.new username: 'sjanardanannair', host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    ######################3####
    #@@datahash = Array.new
    @sqlquerystring = querystring
    #@client = TinyTds::Client.new username: 'maxbi', password: 'd@t@h0g', host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    @client = TinyTds::Client.new(:username => 'maxbi', :password => 'd@t@h0g', :dataserver => 'va1-dgmrsql053.markelcorp.markelna.com', :database => 'DEV_ERMS', :timeout => 0)
    @result = @client.execute(@sqlquerystring)
    #@result_val = Array.NEW
    @result_val = @result.each.split("=>")
    #@rowcount = 0
    #@result.each_with_index do |row|
    #  @@datahash[@rowcount]=row
    #  @rowcount = @rowcount+1
    #end
    @client.close
    return @result_val.to_s
    ###dont delete######
    ###@dbobj = SQLQuery.new('DEV_ERMS')
    ###puts "successfully connected"
    ###query = "select * from GRS.v_Deals;"
    ###recordset = WIN32OLE.new('ADODB.Recordset')
    ###a = @dbobj.query(query, 'select', nil, recordset)
    ###puts a.GetString(2)
    ####################
  end
  def self.CompareAPIAndDBResults(apiresults,queryresults)
    @apiresultval = apiresults
    @queryresultval = Array.new
    @queryresultval = queryresults
    @responsebodyvalue = Array.new
    @responsebodyvalue = fetchresponsebody(@apiresultval)
    # @responsebodyvalue = @responsebodyvalue.join
    # @dbresultval = @dbresultval.join
    #print "\n"
    #print @apiresultval
    # print "\n"
    # print @queryresultval[0]
    # print "\n"
    # print "##########################\n"
    # print @responsebodyvalue[0]
    # print "\n"
    #@@apihash = Hashie::Mash.new
    #@@apihash = @apiresultval
    # print "\n"
    # print @queryresultval
    # print "\n"
    # print @responsebodyvalue
    # print "\n"
    #print @@apihash
    #@queryresultval.extend Hashie::Extensions::DeepLocate
    #@queryresultval.extend Hashie::Extensions::DeepFind
    #@responsebodyvalue.extend Hashie::Extensions::DeepLocate
    #@responsebodyvalue.extend Hashie::Extensions::DeepFind
    #print @queryresultval.deep_locate -> (key,value,object) {key == "statusid" && value == 80}
    #print "\n"
    #print @responsebodyvalue.deep_locate -> (key,value,object) {key == "statusid" && value == 80}
    #print "\n"
    # if @responsebodyvalue.to_s == "" || @responsebodyvalue.to_s == nil
    #   @responsebodyvalue=""
    # end
    # if @queryresultval.to_s == "" || @queryresultval.to_s == nil
    #   @queryresultval=""
    # end
    # if @responsebodyvalue.nil?
    #   @responsebodyvalue == "nil"
    #   print "API Request fetched no deals"
    # end
    # if @queryresultval.nil?
    #   @queryresultval == "nil"
    #   print "DB Results fetched no deals"
    # end
      if @responsebodyvalue.nil? && (@queryresultval.nil? || @queryresultval.any? == false)
        print "PASSED - Both API response and Query result fetched no value and are matching.\n"
      elsif @responsebodyvalue.nil? && (@queryresultval.nil? == false || @queryresultval.any? == true)
        print "FAILED - API response fetched no value and query result have values and the results are not matching.\n"
        fail "FAILED - API response fetched no value and query result have values and the results are not matching.\n"
      elsif @responsebodyvalue.nil? == false  && (@queryresultval.nil? || @queryresultval.any? == false)
        print "FAILED - API response fetched values and query result fetched no values and the results are not matching.\n"
        fail "FAILED - API response fetched values and query result fetched no values and the results are not matching.\n"
      elsif @responsebodyvalue.nil? == false  && (@queryresultval.nil? == false || @queryresultval.any? == true)
        if (@responsebodyvalue - @queryresultval).empty? == true
          print "PASSED - The API Results and DB Query results are matched successfully.\n"
          ##@@reporter.ReportAction("PASSED - The API Results and DB Query results are matched successfully.\n")
        else
          print "FAILED - The API results failed to match with DB Query results.\n"
          fail "FAILED - The API results failed to match with DB Query results.\n"
          # boya Enabled the result on 30 Oct 2018
          @@reporter.ReportAction("FAILED - The API results failed to match with DB Query results.\n")
        end
      end



  end
  def self.CompareResults(actualresults,expectedresults)
    @actualres = actualresults
    @expectedres = expectedresults
    if (@actualres - @expectedres).empty? == true
      print "PASSED - The Expected Results and DB Query results are matched successfully.\n"
      #@@reporter.ReportAction("PASSED - The Expected Results and DB Query results are matched successfully.\n")
    else
      print "FAILED - The Expected results failed to match with DB Query results.\n"
      #@@reporter.ReportAction("FAILED - The Expected results failed to match with DB Query results.\n")
    end

  end
  def self.fetchresponsebody(responsevalue)
    @responsecontent = responsevalue
    @parsedresponse = JSON.parse(@responsecontent)
    @@apihash = Hashie::Mash.new
    @@apidatahash = Hashie::Mash.new
    @@apihash = @parsedresponse["results"]
    @@apihash.extend Hashie::Extensions::DeepLocate
    @@apihash.extend Hashie::Extensions::DeepFind
    #@@apihash.deep_locate -> (key,value,object) {key == "statusid" && value == 80}
    @@apidatahash = @@apihash.deep_find_all('data')
    return @@apidatahash
  end
  def self.ValidateSchema(schema,response)
    @Schema=schema
    @responseval=response
    @schemavalidationresults = JSON::Validator.validate(@Schema,@responseval)
    # puts @schemavalidationresults
    if @schemavalidationresults == true
      print "PASSED - API response matched successfully with Schema provided.\n"
      @@reporter.ReportAction("PASSED - API response matched successfully with Schema provided.\n")
    else
      print "FAILED - API response failed to match with Schema provided.\n"
      fail("FAILED - API response failed to match with Schema provided.\n")
      @@reporter.ReportAction("FAILED - API response failed to match with Schema provided.\n")
    end
  end
  def self.ValidateDealSchema(schema,response,status)
    @Schema=schema
    @responseval=response
    @schemavalidationresults = JSON::Validator.validate(@Schema,@responseval)
    if status.to_s == "Bound"
      print "Schema validation skipped as it is a #{status} status deal.\n"
    else
      if @schemavalidationresults == true
        print "PASSED - API response matched successfully with Schema provided.\n"
      else
        print "FAILED - API response failed to match with Schema provided.\n"
        fail("FAILED - API response failed to match with Schema provided.\n")
      end
    end
  end
  def self.getResponseCount(responseval)
    @responsevalue = responseval
    #@responsebody = BaseContainer.fetchresponsebody(@responseval)
    #print "\n"
    #print @responsevalue
    #print "\n"
    @parsedresponse = JSON.parse(@responsevalue)
    @apihash = Hashie::Mash.new
    @apihash = @parsedresponse["totalRecords"]
    #@responseRecordcount =
    #print @apihash
    return @apihash
  end
  def self.fetchDBcount(dbresultvalue)
    @dbresult = dbresultvalue
    #print "\n"
    #print @dbresult
    #print "\n"
    #put @dbresult["totalRecords"].to_str
    #@parsedresponse = JSON.parse(@dbresult)
    #@fetchedcount = Hashie::Mash.new
    #@fetchedcount = @parsedresponse["totalRecords"]
    #print @fetchedcount
    @dbresult.extend Hashie::Extensions::DeepFind
    @dbresultval = @dbresult.deep_find_all('totalRecords').to_s.to_s.gsub(/\"/, '\'').gsub(/[\[\]]/, '')
    return @dbresultval
  end
  def self.CompareCountResults(responsecountval,dbresultcount)
    @responsecount = responsecountval
    @dbcount = dbresultcount
    if @dbcount==0 || @dbcount==""
      @dbcount = 0
    end
    if @responsecount==0 || @responsecount=="" # boya Added to match the empty response data with db
      @responsecount = 0
    end
    if @responsecount.to_s == @dbcount.to_s
      print "PASSED - API count response " + @responsecount.to_s + " matched successfully with DB count " + @dbcount.to_s + ".\n"
      ##@@reporter.ReportAction("PASSED - API count response matched successfully with DB count.\n")
    else
      print "FAILED - API response " + @responsecount.to_s + " failed to match with the DB Count " + @dbcount.to_s + ".\n"
      fail "FAILED - API response " + @responsecount.to_s + " failed to match with the DB Count " + @dbcount.to_s + ".\n"
      ##@@reporter.ReportAction("FAILED - API response failed to match with the DB Count.\n")
    end
  end
  def self.CompareSimilarAPIDBResults(responseval,dbresult)
    @response = responseval
    @dbvalue = dbresult
    # print "\n"
    # print @response
    # print "\n"
    # print @dbvalue
    # print "\n"

    if @response.to_s == @dbvalue.to_s
      print "PASSED - API response matched successfully with DB result.\n"
      ##@@reporter.ReportAction("PASSED - API count response matched successfully with DB count.\n")
    else
      print "FAILED - API response failed to match with the DB result.\n"
      fail "FAILED - API response failed to match with the DB result.\n"
      ##@@reporter.ReportAction("FAILED - API response failed to match with the DB Count.\n")
    end
  end
  def self.removequotesfromBooleanValues(dbresult)
    @modifiedvalue = dbresult.to_s.gsub(/"true"/,"true")
    @modifiedvalue = @modifiedvalue.to_s.gsub(/"false"/,"false")
    return @modifiedvalue
  end
  def self.CompareAPIandDBResponse(modifiedresponsebody,modifiedDBqueryresult)
    @apiResult = modifiedresponsebody
    @dbResultvalue = modifiedDBqueryresult
    # print "\n"
    # print @apiResult
    # print "\n"
    # print @dbResultvalue
    # print "\n"
    if @apiResult.to_s == @dbResultvalue.to_s
      print "PASSED - API Result and DB Query result matched successfully.\n"
    else
      print "FAILED - API Result and DB Query result failed to match.\n"
      fail "FAILED - API Result and DB Query result failed to match.\n"
    end
  end
  def self.CompareDealAPIandDBResponse(modifiedresponsebody,modifiedDBqueryresult,status)
    @apiResult = modifiedresponsebody
    @dbResultvalue = modifiedDBqueryresult
    # print "\n"
    # print @apiResult
    # print "\n"
    # print @dbResultvalue
    # print "\n"
    if status.to_s == "Bound"
      print "API and DB comparison skipped as it is a Bound deal.\n"
    else
      if @apiResult.to_s == @dbResultvalue.to_s
        print "PASSED - API Result and DB Query result matched successfully.\n"
      else
        print "FAILED - API Result and DB Query result failed to match.\n"
        fail "FAILED - API Result and DB Query result failed to match.\n"
      end
    end

  end




end