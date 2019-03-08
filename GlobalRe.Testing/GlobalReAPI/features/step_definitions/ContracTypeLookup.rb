Given(/^that I am submitting a ContractType Lookup GET request for fetching all the ContractTypes for the (.*) flag$/) do |assumedCededFlag|
  print "Step Name: Fetching ContractType Lookup for #{assumedCededFlag} flag.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getContractTypeLookup(assumedCededFlag).to_s
  @contracttype = LookUps.new(@reporter)
  @responseval = @contracttype.SubmitGETRequest(@endpoint)
end

Then(/^I receive a response with status as successful for the ContractType Lookup request for (.*) flag$/) do |assumedCededFlag|
  print "Step Name: Verify whether get successful response code for ContractType Lookup request. \n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getContractTypeLookup(assumedCededFlag).to_s
  @contracttype = LookUps.new(@reporter)
  @responseval = @contracttype.SubmitGETRequest(@endpoint)
  @responsecode = @contracttype.VerifyResponseStatusCode(@responseval).to_s
  @contracttype.CheckSuccessfulResponse(@responsecode)
end

Then(/^the ContractType Lookup GET request response for (.*) flag and the db query result is matched successfully$/) do |assumedCededFlag|
  print "Step Name: Verify whether the ContractType Lookup GET request response for  #{assumedCededFlag} flag and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @contracttype = LookUps.new(@reporter)
  @sqlquery=@dbquery.getContractTypesQuery(assumedCededFlag).to_s
  # print "\n"
  # puts @sqlquery
  @dbresult = @contracttype.sendQuery(@sqlquery)
  @dbresult = @dbresult.to_s.gsub(/true/,"\"assumed\"")
   puts "DB Query : " +@dbresult.to_s
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getContractTypeLookup(assumedCededFlag).to_s
  @responseval = @contracttype.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s
  # print "\n"
   puts "Response body : "+@responsebody.to_s


  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)

end

Then(/^the ContractType Lookup get request response for (.*) flag and the schema provided is matched successfully$/) do |assumedCededFlag|
  print "Step Name: Matching the ContractType Lookup request response and the schema provided are matched successfully.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getContractTypeLookup(assumedCededFlag).to_s
  @contracttype = LookUps.new(@reporter)
  @responseval = @contracttype.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["results"]
  # @responsedatavalue = @responseresvalue.first['data']

  @Schema = {
      "type" => "array",
      "required" => ["level1ExposureTypeCode","level1ExposureTypeValue","level1ContractTypeCode","level1ContractTypeValue","assumedCededFlag"],
      "properties" => {
          "level1ExposureTypeCode" => {"type" => "Integer"},
          "level1ExposureTypeValue" => {"type" => "String"},
          "level1ContractTypeCode" => {"type" => "Integer"},
          "level1ContractTypeValue" => {"type" => "String"},
          "assumedCededFlag" => {"type" => "String"}
      }
  }
  #print @Schema
  @contracttype.ValidateAPIwithSchema(@Schema,@responseresvalue)
end