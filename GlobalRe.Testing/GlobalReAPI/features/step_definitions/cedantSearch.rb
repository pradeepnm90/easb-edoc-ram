# Given(/^I submit a GET request for cedant search and select API for (.*) cedantname and (.*) parentgroupname and (.*) cedantid and (.*) parentgroupid and (.*) locationid$/) do |cedantname, parentgroupname, cedantid, parentgroupid, locationid|
#   @endpointc = Endpoints.new
#   @endpoint = @endpointc.getCedantSearch(cedantname,parentgroupname,cedantid,parentgroupid,locationid).to_s
#   puts "endpoint" + @endpoint.to_s
#   @cedantsearchandselect = CedantSearchAndSelect.new(@reporter)
#   @responseval = @cedantsearchandselect.SubmitGETRequest(@endpoint)
#   puts "@responseval" + @responseval.to_s
# end




Given(/^I submit a GET request for cedant search and select API for (.*) cedantname and (.*) parentgroupname and (.*) cedantid and (.*) parentgroupid and (.*) locationid and (.*) expected result$/) do |cedantname, parentgroupname, cedantid, parentgroupid, locationid, expectedresult|
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCedantSearch(cedantname,parentgroupname,cedantid,parentgroupid,locationid).to_s
  puts "endpoint" + @endpoint.to_s
  @cedantsearchandselect = CedantSearchAndSelect.new(@reporter)
  @responseval = @cedantsearchandselect.SubmitGETRequest(@endpoint)
  puts "@responseval" + @responseval.to_s
end



Then(/^I receive a response with status as successful for (.*) cedantname and (.*) parentgroupname and (.*) cedantid and (.*) parentgroupid and (.*) locationid and (.*) expected result$/) do |cedantname, parentgroupname, cedantid, parentgroupid, locationid, expectedresult|
  @responsecode = @cedantsearchandselect.VerifyResponseStatusCode(@responseval).to_s
  # @cedantsearchandselect.CheckResponse(@responsecode)
  @cedantsearchandselect.compareExpectedAndActualResult(@expectedStatus,@actualStatus)
    puts "@responsecode" + @responsecode.to_s
    puts "Response matched"
end




Given(/^I submit a GET request for cedant search and select API for (.*) cedantname and (.*) parentgroupname and (.*) cedantid and (.*) parentgroupid and (.*) locationid$/) do |cedantname, parentgroupname, cedantid, parentgroupid, locationid|
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCedantSearch(cedantname,parentgroupname,cedantid,parentgroupid,locationid).to_s
  puts "endpoint" + @endpoint.to_s
  @cedantsearchandselect = CedantSearchAndSelect.new(@reporter)
  @responseval = @cedantsearchandselect.SubmitGETRequest(@endpoint)
  puts "@responseval" + @responseval.to_s
end


 Then(/^I match the schema with the response with status as successful for (.*) cedantname and (.*) parentgroupname and (.*) cedantid and (.*) parentgroupid and (.*) locationid$/) do |cedantname, parentgroupname, cedantid, parentgroupid, locationid,|
   @parsedrespvalue = JSON.parse(@responseval)
   @responseresvalue = @parsedrespvalue["results"]

   @Schema = {
       "type" => "array",
       "required" => ["cedantid","cedantname","locationid","address","city","state","postalcode","country","parentgroupid","parentgroupname"],
       "properties" => {
           "cedantid" => {"type" => "Integer"},
           "cedantname"=> {"type" => "String"},
           "locationid" => {"type" => "Integer"},
           "address"=> {"type" => "String"},
           "city"=> {"type" => "String"},
           "state"=> {"type" => "String"},
           "postalcode"=> {"type" => "String"},
           "country"=> {"type" => "String"},
           "parentgroupid"  => {"type" => "Integer"},
           "parentgroupname"=> {"type" => "String"}
       }
   }

   puts "Schema= " + @Schema.to_s
    puts "Response= " + @responseval.to_s
    @cedantsearchandselect.ValidateAPIwithSchema(@Schema,@responseresvalue)
  end


Then(/^I receive a response with status as successful for (.*) cedantname and (.*) parentgroupname and (.*) cedantid and (.*) parentgroupid and (.*) locationid$/) do |cedantname, parentgroupname, cedantid, parentgroupid, locationid|
  puts "Step Name: Verify whether the Cedant search and select GET request response f and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @cedantsearchandselect = CedantSearchAndSelect.new(@reporter)
  @sqlquery=@dbquery.cedantSearchQuery(cedantname,parentgroupname)
  puts @sqlquery
  @dbresult = @cedantsearchandselect.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s

  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCedantSearch(cedantname,parentgroupname,cedantid,parentgroupid,locationid).to_s
  puts "endpoint" + @endpoint.to_s
  # @cedantsearchandselect = CedantSearchAndSelect.new(@reporter)
  @responseval = @cedantsearchandselect.SubmitGETRequest(@endpoint)
  puts "@responseval" + @responseval.to_s

  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s

  puts "Response body : "+@responsebody.to_s
  puts "DB query step"

  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)
end