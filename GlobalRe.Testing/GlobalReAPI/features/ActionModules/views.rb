require 'rest-client'
require 'json'
require 'hashie'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"ActionModules","deal_by_status_name_actions.rb")
class Views < DealByStatusNameActions

  def initialize(reporter)
    @@reporter = reporter
  end

  def submitViewPutRequest(endpointValue,viewValues,field,value)
    @puturi = GenerateURI(endpointValue)
    @configObj = Config.new()
    # print "\n"
    # print @puturi.to_s
    # print "\n"
    # @usernamevalue="vboya"      #---Boya removed the hard coded values
    @usernamevalue= @configObj.getUsername
    # puts @usernamevalue
    @Auth = BaseContainer.generatePutUserToken(@usernamevalue)
    @puturi = @puturi.to_s #+ "?token=" + @Auth.to_s
    # print "\n"
    # print @puturi.to_s
    # print "\n"
    # print "\n"
    # print @Auth
    # print "\n"
    @resource = RestClient::Resource.new(@puturi.to_s)
    @actualvaluesarray = BaseContainer.generatedealvaluesArray(viewValues)
    @goingtobeModifiedvaluesarray = BaseContainer.generateGoingTomodifyvaluesArray(value)
    @fieldData = BaseContainer.generateFieldsArray(field)
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

    #Fetching all the actual vaules and assigning the to be values
    if @actualvaluesarray[0] == ""
      @viewId = nil
    else
      @viewId = @actualvaluesarray[0].to_i
    end
    if @actualvaluesarray[1] == ""
      @userID = nil
    else
      @userID = @actualvaluesarray[1].to_i
    end
    if @actualvaluesarray[2] == ""
      @screenName = nil
    else
      @screenName = @actualvaluesarray[2].to_s
    end
    if @actualvaluesarray[3] == ""
      @viewName = nil
    else
      @viewName = @actualvaluesarray[3].to_s
    end
    if @actualvaluesarray[4] == ""
      @default = nil
    else
      @default = @actualvaluesarray[4].to_s
    end
    if @actualvaluesarray[5] == ""
      @layOut = nil
    else
      @layOut = @actualvaluesarray[5].to_s
    end

    for @field in 0..@fieldData.length-1
      case @fieldData[@field]
        when "viewId"
          @viewId = @value1.to_s
        when "userId"
          @userID = @value1.to_s
        when "screenName"
          @screenName = @value1.to_s
        when "viewname"
          @viewName = @value1.to_s
        when "default"
          @default = @value1.to_s
        when "layout"
          @layOut = @value1.to_s
      end
    end
    @payload = "{\"viewId\": \"#{@viewId}\",\"userId\": \"#{@userID}\",\"screenName\": \"#{@screenName}\",\"viewname\": \"#{@viewName}\",\"default\": \"#{@default}\",\"layout\": \"#{@layOut}\"}"
    #@payload = "{\"dealNumber\": #{@dealNumber},\"dealName\": \"#{@dealName}\",\"statusCode\": #{@statusCode},\"status\": \"#{@status}\",\"contractNumber\": #{@contractNumber},\"inceptionDate\": \"#{@inceptionDate}\",\"targetDate\": \"#{@targetDate}\",\"priority\": \"#{@priority}\",\"submittedDate\": \"#{@submittedDate}\",\"primaryUnderwriterCode\": #{@primaryUnderwriterCode},\"primaryUnderwriterName\": \"#{@primaryUnderwriterName}\",\"secondaryUnderwriterCode\": #{@secondaryUnderwriterCode},\"secondaryUnderwriterName\": \"#{@secondaryUnderwriterName}\",\"technicalAssistantCode\": #{@technicalAssistantCode},\"technicalAssistantName\": \"#{@technicalAssistantName}\",\"modellerCode\": #{@modellerCode},\"modellerName\": \"#{@modellerName}\",\"actuaryCode\": #{@actuaryCode},\"actuaryName\": \"#{@actuaryName}\",\"expiryDate\": \"#{@expiryDate}\",\"brokerCode\": #{@brokerCode},\"brokerName\": \"#{@brokerName}\",\"brokerContactCode\": #{@brokerContactCode},\"brokerContactName\": \"#{@brokerContactName}\"}"
    @payload = @payload.gsub(': ""',': null')
    @payload = @payload.gsub(': "0"',': null')
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

      puts "Response:" +@dealputresponse.to_s

    end
    # print "\n"
    # print @dealputresponse
    # print "\n"
    $putRequestbody = @payload
    # print "\n"
    # puts "Request body:"+$putRequestbody.to_s
    # print "\n"

    return @dealputresponse
  end


  def resetSubmitViewPUTRequest(endpointValue,viewValues)
    @puturi = GenerateURI(endpointValue)
    @configObj = Config.new()
    @usernamevalue= @configObj.getUsername
    # @usernamevalue="vboya"
    @Auth = BaseContainer.generatePutUserToken(@usernamevalue)
    @resource = RestClient::Resource.new(@puturi)
    @actualvaluesarray = BaseContainer.generatedealvaluesArray(viewValues)
    for @count in 0..@actualvaluesarray.length-1
      if @actualvaluesarray[@count]==""
        @actualvaluesarray[@count]=nil
      end
    end
    #------------Fetching all the actual vaules and assigning the to be values-------------------
    if @actualvaluesarray[0] == ""
      @viewId = nil
    else
      @viewId = @actualvaluesarray[0].to_i
    end
    if @actualvaluesarray[1] == ""
      @userID = nil
    else
      @userID = @actualvaluesarray[1].to_i
    end
    if @actualvaluesarray[2] == ""
      @screenName = nil
    else
      @screenName = @actualvaluesarray[2].to_s
    end
    if @actualvaluesarray[3] == ""
      @viewName = nil
    else
      @viewName = @actualvaluesarray[3].to_s
    end
    if @actualvaluesarray[4] == ""
      @default = nil
    else
      @default = @actualvaluesarray[4].to_s
    end
    if @actualvaluesarray[5] == ""
      @layOut = nil
    else
      @layOut = @actualvaluesarray[5].to_s
    end

    @payload = "{\"viewId\": \"#{@viewId}\",\"userId\": \"#{@userID}\",\"screenName\": \"#{@screenName}\",\"viewname\": \"#{@viewName}\",\"default\": \"#{@default}\",\"layout\": \"#{@layOut}\"}"
    @payload = @payload.gsub(': ""',': null')
    @payload = @payload.gsub(': "0"',': null')


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


  def submitDeleteViewRequest(endpointValue)
    @deleteUri = GenerateURI(endpointValue)
    @configObj = Config.new()
    @usernamevalue= @configObj.getUsername
    @Auth = BaseContainer.generateToken()
    @deleteUri = @deleteUri.to_s #+ "?token=" + @Auth.to_s
    print "\n"
    print @deleteUri.to_s
    print "\n"
    # print "\n"
    # print @Auth
    # print "\n"
    @resource = RestClient::Resource.new(@deleteUri.to_s)
    begin
      @dealputresponse = RestClient.delete(@deleteUri.to_s, {:Authorization => @Auth})
    rescue RestClient::ExceptionWithResponse => e
      @dealputresponse = e.response

      puts "Response:" +@dealputresponse.to_s

    end
    # print "\n"
    # print @dealputresponse
    # print "\n"
    # $putRequestbody = @payload
    # print "\n"
    # puts "Request body:"+$putRequestbody.to_s
    # print "\n"

    return @dealputresponse
  end
end