Given(/^that I am submitting a Deal Status Lookup get request for fetching all the status types$/) do
  print "Step Name: Fetching Deal Status Lookup.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusLookup.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
end

Then(/^I receive a response with status as successful for the Deal Status Lookup request$/) do
  print "Step Name: Verify whether the Deal Status Lookup request response is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusLookup.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

And(/^I fetch the Deal Status Lookup from DB$/) do
  print "Step Name: Fetching Deal Status Lookup from DB.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @sqlquery=@dbquery.getDealStatusLookup.to_s
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
end

Then(/^the Deal Status Lookup get request response to and the db query result is matched successfully$/) do
  print "Step Name: Verify whether the Deal Status Lookup get request response and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @DealSummaryActions = DealSummaryActions.new(@reporter)
  @SubDivisionActions = SubDivsionsActions.new(@reporter)
  @sqlquery=@dbquery.getDealStatusLookup.to_s
  # print "\n"
  # print @sqlquery
  # print "\n"
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
  @modifieddbresult = BaseContainer.removequotesfromBooleanValues(@dbresult)
  # print "\n"
  # print @modifieddbresult
  # print "\n"
  #@modifiedDBqueryresult = @SubDivisionActions.modifyDBresult(@dbresult)

  #@sqlquery=@dbquery.getDealSummary.to_s
  #@dbresult = @DealByStatusName.sendQuery(@sqlquery)
  # print "\n"
  # print @dbresult.join(',')
  # print "\n"
  #print "\n"
  #print @modifiedDBqueryresult
  #print "\n"

  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusLookup.to_s
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  # print "\n"
  # print @responseval
  # print "\n"
  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = @parsedrespvalue["results"]#BaseContainer.fetchresponsebody(@responseval)
  # print "\n"
  # print @responsebody
  # print "\n"
  #@modifiedresponsebody = @SubDivisionActions.modifyResponseBody(@responsebody)
  #@splitteddbresult = @DealSummaryActions.modifyDBresults(@dbresult)
  #@DealSummaryActions.compareSummaryAPIResponseandDEBResults(@modifiedresponsebody)
  # print "\n"
  # print @modifiedresponsebody
  # print "\n"

  @SubDivisionActions.CompareAPIandDBResponse(@responsebody,@modifieddbresult)
  # print "\n"
  # print @responsebody
  # print "\n"
end

Then(/^the Deal Status Lookup get request response and the schema provided is matched successfully$/) do
  print "Step Name: Matching the Deal Status Lookup request response and the schema provided.\n"
  @endpointc = Endpoints.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @endpoint = @endpointc.getDealStatusLookup.to_s
  @responsebody = @DealByStatusName.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)
  @responseresvalue = @parsedrespvalue["results"]
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

  @Schema = {
      "type" => "array",
      "required" => ["name","value","group","isActive"],
      "properties" => {
          "name" => {"type" => "String"},
          "value" => {"type" => "String"},
          "group" => {"type" => "integer"},
          "isActive" => {"type" => "boolean"}
      }
  }
  #require 'json'
  #require 'json-schema'
  #@SchemaFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"testdata/JSON_SCHEMA_FILES","DealCountByStatus_Schema.json")
  #@Schema_data = JSON.parse(File.read(@SchemaFile))
  #@Schema = JsonSchema.parse!(@Schema_data)
  #print @Schema
  @DealByStatusName.ValidateAPIwithSchema(@Schema,@responseresvalue)
end