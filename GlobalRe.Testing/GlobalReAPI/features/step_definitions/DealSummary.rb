# Given(/^that I am submitting a get request for fetching count of all the status types$/) do
#   print "Step Name: Fetching count of all the status type deals.\n"
#   @endpointc = Endpoints.new
#   @endpoint = @endpointc.getDealStatusSummaries.to_s
#   @DealByStatusName = DealByStatusNameActions.new(@reporter)
#   @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
# end
#
# Then(/^I receive a response with status as successful for the deal summary request$/) do
#   print "Step Name: Verify whether the deal summary request response is successful.\n"
#   @endpointc = Endpoints.new
#   @endpoint = @endpointc.getDealStatusSummaries.to_s
#   @DealByStatusName = DealByStatusNameActions.new(@reporter)
#   @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
#   @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
#   @DealByStatusName.CheckSuccessfulResponse(@responsecode)
# end
#
# And(/^I fetch the count of deals from DB$/) do
#   print "Step Name: Fetching count of all the status from DB.\n"
#   @dbquery = DBQueries.new
#   @DealByStatusName = DealByStatusNameActions.new(@reporter)
#   @sqlquery=@dbquery.getDealSummary.to_s
#   @dbresult = @DealByStatusName.sendQuery(@sqlquery)
#   #print "\n"
#   #print @dbresult
#   #print "\n"
#
# end
#
# Then(/^the get request response to and the db query result is matched successfully$/) do
#   print "Step Name: Verify whether the get request response and the db query result is matched successfully.\n"
#   @dbquery = DBQueries.new
#   @DealByStatusName = DealByStatusNameActions.new(@reporter)
#   @DealSummaryActions = DealSummaryActions.new(@reporter)
#
#   #@sqlquery=@dbquery.getDealSummary.to_s
#   #@dbresult = @DealByStatusName.sendQuery(@sqlquery)
#   #print "\n"
#   #print @dbresult
#   #print "\n"
#   @endpointc = Endpoints.new
#   @endpoint = @endpointc.getDealStatusSummaries.to_s
#   @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
#   @responsebody = BaseContainer.fetchresponsebody(@responseval)
#   @modifiedresponsebody = @DealSummaryActions.modifyResponseBody(@responsebody)
#   #@splitteddbresult = @DealSummaryActions.modifyDBresults(@dbresult)
#   @DealSummaryActions.compareSummaryAPIResponseandDEBResults(@modifiedresponsebody)
#   #print "\n"
#   #print @responsebody
#   #print "\n"
#
#
# end
#
# Then(/^the get request response and the schema provided is matched successfully$/) do
#   print "Step Name: Matching the deal summary request response and the schema provided.\n"
#   @endpointc = Endpoints.new
#   @DealByStatusName = DealByStatusNameActions.new(@reporter)
#   @endpoint = @endpointc.getDealStatusSummaries.to_s
#   @responsebody = @DealByStatusName.SubmitGETRequest(@endpoint)
#   @parsedrespvalue = JSON.parse(@responsebody)
#   @responseresvalue = @parsedrespvalue["results"]
#   @responsedatavalue = @responseresvalue.first['data']
#   #print "\n"
#   #print @responsebody
#   #print "\n"
#   #print "\n"
#   #print @responseresvalue
#   #print "\n"
#   #print "\n"
#   #print @responsedatavalue
#   #print "\n"
#
#   @Schema = {
#       "type" => "object",
#       "required" => ["statusCode","statusName","sortOrder","count","statusSummary"],
#       "properties" => {
#           "statusCode" => {"type" => "String"},
#           "statusName" => {"type" => "String"},
#           "sortOrder" => {"type" => "String"},
#           "count" => {"type" => "String"},
#           "statusSummary" => {"type" => "String"}
#       }
#   }
#   #require 'json'
#   #require 'json-schema'
#   #@SchemaFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"testdata/JSON_SCHEMA_FILES","DealCountByStatus_Schema.json")
#   #@Schema_data = JSON.parse(File.read(@SchemaFile))
#   #@Schema = JsonSchema.parse!(@Schema_data)
#   #print @Schema
#   @DealByStatusName.ValidateAPIwithSchema(@Schema,@responsedatavalue)
#
# end

Given(/^that I am submitting a get request for fetching count of all the status types for the user with (.*) access$/) do |user|
  print "Step Name: Fetching count of all the status type deals for the user with " + user + " access.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusSummaries.to_s
  puts @endpoint
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)

end

Then(/^I receive a response with status as successful for the deal summary request for the user with (.*) access$/) do |user|
  print "Step Name: Verify whether the deal summary request response for the user with " + user + " access is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusSummaries.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

And(/^I fetch the count of deals from DB for the user with (.*) access$/) do |user|
  print "Step Name: Fetching count of all the status from DB for the user with " + user + " access.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @sqlquery=@dbquery.getDealSummaryByUser(user).to_s
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
  #print "\n"
  #print @dbresult
  #print "\n"

end

Then(/^the get request response for the user with (.*) access and the db query result is matched successfully$/) do |user|
  print "Step Name: Verify whether the get request response for the user with "+ user + " access and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @DealSummaryActions = DealSummaryActions.new(@reporter)

  #@sqlquery=@dbquery.getDealSummary.to_s
  #@dbresult = @DealByStatusName.sendQuery(@sqlquery)
  #print "\n"
  #print @dbresult
  #print "\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusSummaries.to_s
  @responseval = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
  @responsebody = BaseContainer.fetchresponsebody(@responseval)
  @modifiedresponsebody = @DealSummaryActions.modifyResponseBody(@responsebody)
  #boya Added
  #puts "Modifiled resonsebody : "+@modifiedresponsebody.to_s
  #@splitteddbresult = @DealSummaryActions.modifyDBresults(@dbresult)
  @DealSummaryActions.compareSummaryAPIResponseandDEBResults(@modifiedresponsebody,user)
  #print "\n"
  #print @responsebody
  #print "\n"

end

Then(/^the get request response for the user with (.*) access and the schema provided is matched successfully$/) do |user|
  print "Step Name: Matching the deal summary request response for the user with " + user + " access and the schema provided.\n"
  @endpointc = Endpoints.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @endpoint = @endpointc.getDealStatusSummaries.to_s
  @responsebody = @DealByStatusName.SubmitUserGETRequest(@endpoint,user)
  @parsedrespvalue = JSON.parse(@responsebody)
  @responseresvalue = @parsedrespvalue["results"]
  @responsedatavalue = @responseresvalue.first['data']
  #print "\n"
  #print @responsebody
  #print "\n"
  #print "\n"
  #print @responseresvalue
  #print "\n"
  # print "\n"
  # print @responsedatavalue
  # print "\n"

  @Schema = {
      "type" => "object",
      "required" => ["statusCode","statusName","sortOrder","count","statusSummary"],
      "properties" => {
          "statusCode" => {"type" => "String"},
          "statusName" => {"type" => "String"},
          "sortOrder" => {"type" => "String"},
          "count" => {"type" => "String"},
          "statusSummary" => {"type" => "String"}
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