Given(/^that I am submitting a get request for fetching count of all the (.*) deals$/) do |status|
  print "Step Name: Fetching count of " + status.to_s + " deals.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusendpoints(status).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
end

And(/^I fetch the count of deals having the status as (.*) from DB$/) do |status|
  print "Step Name: Fetching count of " + status.to_s + " from DB.\n"
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealCountByStatusName(status).to_s
  @DealByStatusName.sendCountQuery(@sqlquery)
end

Then(/^the get request response to fetch the count of deals having (.*) and the schema provided is matched successfully$/) do |status|
  print "Step Name: Matching the count request response for " + status.to_s + " and the schema provided.\n"
  @endpointc = Endpoints.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @endpoint = @endpointc.getDealByStatusendpoints(status).to_s
  @responsebody = @DealByStatusName.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)
  #@responseresvalue = @parsedrespvalue["results"]
  #@responsedatavalue = @responseresvalue.first['data']
  @Schema = {
      "type" => "object",
      "required" => ["totalRecords"],
      "properties" => {
          "totalRecords" => {"type" => "String"}
      }
  }
  @DealByStatusName.ValidateAPIwithSchema(@Schema,@parsedrespvalue)
end

Then(/^I receive a response with status as successful for the (.*) deal request$/) do |status|
  print "Step Name: Verify whether the " + status.to_s + " status deal request response is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusendpoints(status).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

Then(/^I receive a response with status as successful for the (.*) deal count request$/) do |status|
  print "Step Name: Verify whether the " + status.to_s + " status deal count request response is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusendpoints(status).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

Given(/^that I am submitting a get request for fetching all the (.*) deals$/) do |status|
  print "Step Name: Fetching the " + status.to_s + " deals.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusendpoints(status).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
end

Then(/^the (.*) get request response and the db query result is matched successfully$/) do |status|
  print "Step Name: Matching the request response for " + status.to_s + " and the DB Query Results provided.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusendpoints(status).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealByStatusName(status).to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
  BaseContainer.CompareAPIAndDBResults(@responseval,@dbresultvalue)
end

Then(/^the get request response to fetch the count of deals having (.*) and the db query result is matched successfully$/) do |status|
  print "Step Name: Matching the count request response for " + status.to_s + " and the DB Query Results provided.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusendpoints(status).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecountval = BaseContainer.getResponseCount(@responseval)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealCountByStatusName(status).to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
  @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
  #print "\n"
  #print @responsecountval
  #print "\n"
  #print "\n"
  #print @dbresultcount
  #print "\n"
  BaseContainer.CompareCountResults(@responsecountval,@dbresultcount)
end

Then(/^I receive a error response mentioning special characters$/) do
  print "Step Name: Verifying the error response for request with special characters.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getSpecialCDealByStatusendpoints.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsemessage = @DealByStatusName.GetResponseMessage(@responseval)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckErrorResponse(@responsecode,@responsemessage)
end
Then(/^I receive a error response mentioning invalid value$/) do
  print "Step Name: Verifying the error response for request with invalid value.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getInValidDealByStatusendpoints.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsemessage = @DealByStatusName.GetResponseMessage(@responseval)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckErrorResponse(@responsecode,@responsemessage)
end

Given(/^that I am submitting a get request for fetching all the deals with a invalid status code$/) do
  print "Step Name: Fetching all the deal request response for invalid status.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getInValidDealByStatusendpoints.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsemessage = @DealByStatusName.GetResponseMessage(@responseval)

end

Given(/^that I am submitting a get request for fetching all the deals with a invalid status code having special characters$/) do
  print "Step Name: Fetching all the deal request response for status with special characters.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getSpecialCDealByStatusendpoints.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsemessage = @DealByStatusName.GetResponseMessage(@responseval)
end

And(/^I fetch the list of deals having the status as (.*) from DB$/) do |status|
  print "Step Name: Fetching all the deals having the status " + status.to_s + " and meeting the needed logic from DB.\n"
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealByStatusName(status).to_s
  @DealByStatusName.sendQuery(@sqlquery)
end


Then(/^the get (.*) status request response and the schema provided is matched successfully$/) do |status,user|
  print "Step Name: Matching the request response for " + status.to_s + " and the schema provided.\n"
  @endpointc = Endpoints.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @endpoint = @endpointc.getDealByStatusCodeendpoints(status,user).to_s
  @responsebody = @DealByStatusName.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)
  @responseresvalue = @parsedrespvalue["results"]
  @responsedatavalue = @responseresvalue.first['data']
  #print "\n"
  #print @responsebody
  #print "\n"
  #print "\n"
  #print @responseresvalue
  #print "\n"
  #print "\n"
  #print @responsedatavalue
  #print "\n"

  @Schema = {
      "type" => "object",
      "required" => ["dealNumber","dealName","statusCode","status","contractNumber","inceptionDate","targetDate","priority","submittedDate","primaryUnderwriterCode","primaryUnderwriterName","secondaryUnderwriterCode","secondaryUnderwriterName","technicalAssistantCode","technicalAssistantName","modellerCode","modellerName","actuaryCode","actuaryName","expiryDate","brokerCode","brokerName","brokerContactCode","brokerContactName"],
      "properties" => {
          "dealNumber" => {"type" => "String"},
          "dealName" => {"type" => "String"},
          "statusCode" => {"type" => "String"},
          "status" => {"type" => "String"},
          "contractNumber" => {"type" => "String"},
          "inceptionDate" => {"type" => "String"},
          "targetDate" => {"type" => "String"},
          "priority" => {"type" => "String"},
          "submittedDate" => {"type" => "String"},
          "primaryUnderwriterCode" => {"type" => "String"},
          "primaryUnderwriterName" => {"type" => "String"},
          "secondaryUnderwriterCode" => {"type" => "String"},
          "secondaryUnderwriterName" => {"type" => "String"},
          "technicalAssistantCode" => {"type" => "String"},
          "technicalAssistantName" => {"type" => "String"},
          "modellerCode" => {"type" => "String"},
          "modellerName" => {"type" => "String"},
          "actuaryCode" => {"type" => "String"},
          "actuaryName" => {"type" => "String"},
          "expiryDate" => {"type" => "String"},
          "brokerCode" => {"type" => "String"},
          "brokerName" => {"type" => "String"},
          "brokerContactCode" => {"type" => "String"},
          "brokerContactName" => {"type" => "String"}
      }
  }
  #require 'json'
  #require 'json-schema'
  #@SchemaFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"testdata/JSON_SCHEMA_FILES","DealCountByStatus_Schema.json")
  #@Schema_data = JSON.parse(File.read(@SchemaFile))
  #@Schema = JsonSchema.parse!(@Schema_data)
  #print @Schema
  @DealByStatusName.ValidateAPIwithSchema(@Schema,@responsedatavalue)

end

Given(/^that I am submitting a get request for fetching all the (.*) deals for the user having (.*) access$/) do |status, user|
  print "Step Name: Fetching the " + status.to_s + " deals for the user having " + user + " access.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusCodeendpoints(status,user).to_s
  puts @endpoint
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
end


Then(/^I receive a response with status as successful for the (.*) deal request for the user having (.*) access$/) do |status, user|
  print "Step Name: Verify whether the " + status.to_s + " status deal request response for the user having the "+ user + " access is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusCodeendpoints(status,user).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

Then(/^the (.*) get request response for the user having (.*) access and the db query result is matched successfully$/) do |status, user|
  print "Step Name: Matching the request response for " + status.to_s + " for the user having " + user + " access and the DB Query Results provided.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusCodeendpoints(status,user).to_s
  # print "\n"
  # print @endpoint
  # print "\n"
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getUserDealByStatusName(status,user).to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
  # print "\n"
  # print @responseval
  # print "\n"
  # print @dbresultvalue
  # print "\n"
  BaseContainer.CompareAPIAndDBResults(@responseval,@dbresultvalue)
end

Then(/^the get (.*) status request response for the user having (.*) access and the schema provided is matched successfully$/) do |status, user|
  print "Step Name: Matching the request response for " + status.to_s + " and the schema provided.\n"
  @endpointc = Endpoints.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @endpoint = @endpointc.getDealByStatusCodeendpoints(status,user).to_s
  @responsebody = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
  @parsedrespvalue = JSON.parse(@responsebody)
  @responseresvalue = @parsedrespvalue["results"]
  if @responseresvalue.nil? || @responseresvalue.any? == false
    print "The API fetched no data in the response. So skipping the schema comparison.\n"
  elsif @responseresvalue.nil? == false || @responseresvalue.any? == true
    @responsedatavalue = @responseresvalue.first['data']
    #print "\n"
    #print @responsebody
    #print "\n"
    #print "\n"
    #print @responseresvalue
    #print "\n"
    #print "\n"
    #print @responsedatavalue
    #print "\n"

    @Schema = {
        "type" => "object",
        "required" => ["dealNumber","dealName","statusCode","status","contractNumber","inceptionDate","targetDate","priority","submittedDate","primaryUnderwriterCode","primaryUnderwriterName","secondaryUnderwriterCode","secondaryUnderwriterName","technicalAssistantCode","technicalAssistantName","modellerCode","modellerName","actuaryCode","actuaryName","expiryDate","brokerCode","brokerName","brokerContactCode","brokerContactName"],
        "properties" => {
            "dealNumber" => {"type" => "String"},
            "dealName" => {"type" => "String"},
            "statusCode" => {"type" => "String"},
            "status" => {"type" => "String"},
            "contractNumber" => {"type" => "String"},
            "inceptionDate" => {"type" => "String"},
            "targetDate" => {"type" => "String"},
            "priority" => {"type" => "String"},
            "submittedDate" => {"type" => "String"},
            "primaryUnderwriterCode" => {"type" => "String"},
            "primaryUnderwriterName" => {"type" => "String"},
            "secondaryUnderwriterCode" => {"type" => "String"},
            "secondaryUnderwriterName" => {"type" => "String"},
            "technicalAssistantCode" => {"type" => "String"},
            "technicalAssistantName" => {"type" => "String"},
            "modellerCode" => {"type" => "String"},
            "modellerName" => {"type" => "String"},
            "actuaryCode" => {"type" => "String"},
            "actuaryName" => {"type" => "String"},
            "expiryDate" => {"type" => "String"},
            "brokerCode" => {"type" => "String"},
            "brokerName" => {"type" => "String"},
            "brokerContactCode" => {"type" => "String"},
            "brokerContactName" => {"type" => "String"}
        }
    }
    #require 'json'
    #require 'json-schema'
    #@SchemaFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"testdata/JSON_SCHEMA_FILES","DealCountByStatus_Schema.json")
    #@Schema_data = JSON.parse(File.read(@SchemaFile))
    #@Schema = JsonSchema.parse!(@Schema_data)
    #print @Schema
    @DealByStatusName.ValidateAPIwithSchema(@Schema,@responsedatavalue)
  end
end

