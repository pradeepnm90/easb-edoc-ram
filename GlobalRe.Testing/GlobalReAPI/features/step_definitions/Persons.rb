Given(/^that I am submitting a Persons get request for fetching the details of the user having the personid (.*)$/) do |personid|
  print "Step Name: Fetching details for the user having the personid " + personid + ".\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getPersons.to_s
  @endpoint = @endpoint.to_s + personid.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
end

Then(/^I receive a response with status as successful for the Persons request for user having the personid (.*)$/) do |personid|
  print "Step Name: Verify whether the subdivisions request response for user having the personid " + personid + " is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getPersons.to_s
  @endpoint = @endpoint.to_s + personid.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

And(/^I fetch the Persons from DB by (.*) person id$/) do |personid|
  print "Step Name: Fetching person details by PersonID " + personid + " from DB.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @sqlquery=@dbquery.getPersonsByPersonID.to_s
  puts @sqlquery
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
end

Then(/^the Persons get request response to and the db query result for the (.*) person id is matched successfully$/) do |personid|
  print "Step Name: Verify whether the get request response and the db query result for the person id " + personid + " is matched successfully.\n"
  @dbquery = DBQueries.new
  @endpoint = @endpointc.getPersons.to_s
  @endpoint = @endpoint.to_s + personid.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)

  @responsebody = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsebodyvalue = BaseContainer.fetchresponsebody(@responseval)
  #@modifiedresponsebody = @DealSummaryActions.modifyResponseBody(@responsebody)
  #@splitteddbresult = @DealSummaryActions.modifyDBresults(@dbresult)
  @sqlquery=@dbquery.getPersonsByPersonID.to_s
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
  BaseContainer.CompareResults(@responsebodyvalue,@dbresult)
  # print "\n"
  # print @responsebodyvalue
  # print "\n"
  # print @dbresult
  # print "\n"
end

Then(/^the Persons get request response for user having the person id (.*) and the schema provided is matched successfully$/) do |personid|
  print "Step Name: Matching the deal summary request response and the schema provided.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getPersons.to_s
  @endpoint = @endpoint.to_s + personid.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responsebody = @DealByStatusName.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)
  @responseresvalue = @parsedrespvalue["results"]
  # print "\n"
  # print @responseresvalue[0]
  # print "\n"
  @respresvalue = @responseresvalue[0]
  @responsedatavalue = @respresvalue["data"]
  # print "\n"
  # print @responsedatavalue
  # print "\n"
  @Schema = {
      "type" => "object",
      "required" => ["personId","firstName","lastName","displayName"],
      "properties" => {
          "personId" => {"type" => "Integer"},
          "firstName" => {"type" => "String"},
          "lastName" => {"type" => "String"},
          "displayName" => {"type" => "String"},
      }
  }
  @DealByStatusName.ValidateAPIwithSchema(@Schema,@responsedatavalue)
end