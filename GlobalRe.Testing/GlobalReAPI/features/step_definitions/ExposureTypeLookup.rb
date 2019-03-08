Given(/^that I am submitting an Exposure Tree Lookup GET request for fetching all the Subdivision\/PL2\/Exposure Group\/Exposure Name hierarchy$/) do
  puts "Step Name: Fetching Exposure Type Lookup.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getExposureTypeLookup.to_s
  # puts "Endpoint= " + @endpoint
  @exposureType = LookUps.new(@reporter)
  @responseval = @exposureType.SubmitGETRequest(@endpoint)
  # puts "Response value= " + @responseval.to_s
end

Then(/^I receive a response with status as successful for the Exposure Tree Lookup request$/) do
  print "Step Name: Verify whether the Exposure Typ Lookup request response is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getExposureTypeLookup.to_s
  @exposureType = LookUps.new(@reporter)
  @responseval = @exposureType.SubmitGETRequest(@endpoint)
  @responsecode = @exposureType.VerifyResponseStatusCode(@responseval).to_s
  @exposureType.CheckSuccessfulResponse(@responsecode)

end

Then(/^Exposure Tree Lookup get request response and the db query result is matched successfully$/) do
  print "Step Name: Verify whether the Exposure Type Lookup GET request response and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @exposureType = LookUps.new(@reporter)
  @sqlquery=@dbquery.getExposureTypeLookupQuery.to_s
  print "\n"
  puts @sqlquery
  @dbresult = @exposureType.sendQuery(@sqlquery)
  # puts @dbresult
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getExposureTypeLookup.to_s
  @responseval = @exposureType.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s
  # print "\n"
  # puts @responsebody

  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)
end

Then(/^the Exposure Tree Lookup get request response and the schema provided is matched successfully$/) do
  print "Step Name: Matching the Exposure Type Lookup request response and the schema provided are matched successfully.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getExposureTypeLookup.to_s
  @exposureType = LookUps.new(@reporter)
  @responseval = @exposureType.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["results"]
  # @responsedatavalue = @responseresvalue.first['data']

  @Schema = {
      "type" => "array",
      "required" => ["subdivisioncode","subdivisionname","productLinecode","productLinename","exposuregroupcode","exposuregroupname","exposuretypecode","exposuretypename"],
      "properties" => {
          "subdivisioncode" => {"type" => "Integer"},
          "subdivisionname" => {"type" => "String"},
          "productLinecode" => {"type" => "Integer"},
          "productLinename" => {"type" => "String"},
          "exposuregroupcode" => {"type" => "Integer"},
          "exposuregroupname" => {"type" => "String"},
          "exposuretypecode" => {"type" => "Integer"},
          "exposuretypename" => {"type" => "String"}
    }
  }
  #print @Schema
  @exposureType.ValidateAPIwithSchema(@Schema,@responseresvalue)
end