

Given(/^that I am submitting a CoverageBasis Lookup GET request for fetching all the coveragebasis with the (.*) flag$/) do |assumedCededAllFlag|
  print "Step Name: Fetching CoverageBasis Lookup for #{assumedCededAllFlag} flag.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCoverageBasisLookup(assumedCededAllFlag).to_s
  @coverageBasis = LookUps.new(@reporter)
  @responseval = @coverageBasis.SubmitGETRequest(@endpoint)
end

Then(/^I receive a response with status as successful for the CoverageBasis Lookup request for (.*) flag$/) do |assumedCededAllFlag|
  print "Step Name: Verify whether get successful response code for CoverageBasis Lookup request. \n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCoverageBasisLookup(assumedCededAllFlag).to_s
  @coverageBasis = LookUps.new(@reporter)
  @responseval = @coverageBasis.SubmitGETRequest(@endpoint)
  @responsecode = @coverageBasis.VerifyResponseStatusCode(@responseval).to_s
  @coverageBasis.CheckSuccessfulResponse(@responsecode)
end

Then(/^the CoverageBasis Lookup GET request response for (.*) flag and the db query result is matched successfully$/) do |assumedCededAllFlag|
  print "Step Name: Verify whether the CoverageBasis Lookup GET request response for  #{assumedCededAllFlag} flag and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @coveragebasis = LookUps.new(@reporter)
  @sqlquery=@dbquery.getCoverageBasisOptionsQuery(assumedCededAllFlag).to_s
  # print "\n"
  # puts @sqlquery
  @dbresult = @coveragebasis.sendQuery(@sqlquery)
  # puts @dbresult
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCoverageBasisLookup(assumedCededAllFlag).to_s
  @responseval = @coveragebasis.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s
  # print "\n"
  # puts @responsebody


  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)

end

Then(/^the CoverageBasis Lookup get request response for (.*) flag and the schema provided is matched successfully$/) do |assumedCededAllFlag|
  print "Step Name: Matching the CoverageBasis Lookup request response and the schema provided are matched successfully.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCoverageBasisLookup(assumedCededAllFlag).to_s
  @coverageBasis = LookUps.new(@reporter)
  @responseval = @coverageBasis.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["results"]
  # @responsedatavalue = @responseresvalue.first['data']

  @Schema = {
      "type" => "array",
      "required" => ["coverageCode","coverageDescription","activeFlag","assumedFlag","assumedName","cededFlag","cededName"],
      "properties" => {
          "coverageCode" => {"type" => "Integer"},
          "coverageDescription" => {"type" => "String"},
          "activeFlag" => {"type" => "Boolean"},
          "assumedFlag" => {"type" => "Boolean"},
          "assumedName" => {"type" => "String"},
          "cededFlag" => {"type" => "Boolean"},
          "cededName" => {"type" => "String"}
      }
  }
  #print @Schema
  @coverageBasis.ValidateAPIwithSchema(@Schema,@responseresvalue)
end