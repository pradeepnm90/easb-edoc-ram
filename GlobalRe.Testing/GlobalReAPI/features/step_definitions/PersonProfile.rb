Given(/^that I am submitting a Person Profile get request for fetching the subdivision details of the user having the access as (.*)$/) do |user|
  print "Step Name: Fetching the subdivision details of the user having the access as " + user + ".\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getPersonProfile.to_s
  puts @endpoint
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
end

Then(/^I receive a response with status as successful for the Person Profile request for user having the access as (.*)$/) do |user|
  print "Step Name: Verify whether the response with status as successful for the Person Profile request for user having the access as " + user + " is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getPersonProfile.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

And(/^I fetch the Person Profile from DB by (.*) access$/) do |user|
  print "Step Name: Fetching Person Profile from DB by " + username + " access.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @sqlquery=@dbquery.getPersonProfile(user).to_s
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
end

Then(/^the Person Profile get request response to and the db query result for the (.*) user access is matched successfully$/) do |user|
  print "Step Name: Verify whether the Person Profile get request response to and the db query result for the "+ user + " user access is matched successfully.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @DealSummaryActions = DealSummaryActions.new(@reporter)
  @sqlquery=@dbquery.getPersonProfile(user).to_s
  puts "SQL Query : "+@sqlquery
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
  print "\n"
  print "DB Result " + @dbresult.to_s
  print "\n"
  @dbresult = @dbresult.to_s.gsub(/\"/,'')
  @dbresult = @dbresult.to_s.gsub(/\[/,'')
  @dbresult = @dbresult.to_s.gsub(/\]/,'')
  @dbresult = @dbresult.to_s.gsub(/ /,'')
  @dbresult = @dbresult.to_s.gsub(/nil/,"")

  #@sqlquery=@dbquery.getDealSummary.to_s
  #@dbresult = @DealByStatusName.sendQuery(@sqlquery)
  print "\n"
  print "DB Result " + @dbresult
  print "\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getPersonProfile.to_s
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["data"]
  @responseresvalue = @responseresvalue.to_s.gsub(/\[/,'')
  @responseresvalue = @responseresvalue.to_s.gsub(/\]/,'')
  @responseresvalue = @responseresvalue.to_s.gsub(/\"/,'')
  @responseresvalue = @responseresvalue.to_s.gsub(/ /,'')

  print "\n"
  print @responseresvalue
  print "\n"
  #@responsebody = BaseContainer.fetchresponsebody(@responseval)
  BaseContainer.CompareSimilarAPIDBResults(@responseresvalue,@dbresult)
  #@modifiedresponsebody = @DealSummaryActions.modifyResponseBody(@responsebody)
  #@splitteddbresult = @DealSummaryActions.modifyDBresults(@dbresult)
  #@DealSummaryActions.compareSummaryAPIResponseandDEBResults(@modifiedresponsebody,user)

end

Then(/^the Person Profile get request response for user having the access as (.*) and the schema provided is matched successfully$/) do |user|
  print "Step Name: Matching the Person Profile get request response for user having the access as " + user + " and the schema provided.\n"
  @endpointc = Endpoints.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @endpoint = @endpointc.getPersonProfile.to_s
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["data"]
  #@responseresvalue = @responseresvalue.to_s.gsub(/\[/,'')
  #@responseresvalue = @responseresvalue.to_s.gsub(/\]/,'')
  #@responsedatavalue = @responseresvalue.first['data']
  #print "\n"
  #print @responsebody
  #print "\n"
  # print "\n"
  # print @responseresvalue
  # print "\n"
  #print "\n"
  #print @responsedatavalue
  #print "\n"
  #
  @Schema = {
      "type" => "object",
      "required" => ["defaultSubdivisions"],
      "properties" => {
          "defaultSubdivisions" => {"type" => "array"}}
  }
  @DealByStatusName.ValidateAPIwithSchema(@Schema,@responseresvalue)
end