Given(/^that I am submitting a SubDivisions get request for fetching all the subdivisions$/) do
  print "Step Name: Fetching SubDivisions.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getSubDivisions.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
end

Then(/^I receive a response with status as successful for the SubDivisions request$/) do
  print "Step Name: Verify whether the subdivisions request response is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getSubDivisions.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

And(/^I fetch the SubDivisions from DB$/) do
  print "Step Name: Fetching Subdivisions from DB.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @sqlquery=@dbquery.getSubDivisions.to_s
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
end

Then(/^the SubDivisions get request response to and the db query result is matched successfully$/) do
  print "Step Name: Verify whether the get request response and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @DealSummaryActions = DealSummaryActions.new(@reporter)
  @SubDivisionActions = SubDivsionsActions.new(@reporter)
  @sqlquery=@dbquery.getSubDivisions.to_s
  @dbresult = @DealByStatusName.sendQuery(@sqlquery)
  @modifiedDBqueryresult = @SubDivisionActions.modifyDBresult(@dbresult)
  puts @modifiedDBqueryresult # boya added this line

  #@sqlquery=@dbquery.getDealSummary.to_s
  #@dbresult = @DealByStatusName.sendQuery(@sqlquery)
  # print "\n"
  # print @dbresult.join(',')
  # print "\n"

  @endpointc = Endpoints.new
  @endpoint = @endpointc.getSubDivisions.to_s
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsebody = BaseContainer.fetchresponsebody(@responseval)
  @modifiedresponsebody = @SubDivisionActions.modifyResponseBody(@responsebody)
  puts @modifiedresponsebody # boya added this line
  #@splitteddbresult = @DealSummaryActions.modifyDBresults(@dbresult)
  #@DealSummaryActions.compareSummaryAPIResponseandDEBResults(@modifiedresponsebody)
  @SubDivisionActions.CompareAPIandDBResponse(@modifiedresponsebody,@modifiedDBqueryresult)
  #print "\n"
  #print @responsebody
  #print "\n"
end

Then(/^the SubDivisions get request response and the schema provided is matched successfully$/) do
  print "Step Name: Matching the subdivisions request response and the schema provided.\n"
  @endpointc = Endpoints.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @endpoint = @endpointc.getSubDivisions.to_s
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
      "required" => ["id","name","sortOrder"],
      "properties" => {
          "id" => {"type" => "integer"},
          "name" => {"type" => "String"},
          "sortOrder" => {"type" => "integer"}
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

Given(/^that I am submitting a SubDivisions get request for fetching count of all the status types$/) do
  print "Step Name: Fetching SubDivisions count with status types.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCountofSubDivisionsStatusTypes.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
end

Then(/^I receive a response with status as successful for the count of SubDivisions status types request$/) do
  print "Step Name: Fetching SubDivisions count with status types.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCountofSubDivisionsStatusTypes.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  @DealByStatusName.CheckSuccessfulResponse(@responsecode)
end

Then(/^the SubDivisions status type count GET request response and the schema provided is matched successfully$/) do
  print "Step Name: Fetching SubDivisions count with status types Json schema .\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getCountofSubDivisionsStatusTypes.to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["results"]
  @responsedatavalue = @responseresvalue.first['data']
  puts @responsedatavalue # boya added this comment
  @Schema = {
      "type" => "object",
      "required" => ["dealNumber","dealName","statusCode","status",],
      "properties"=> {
          "dealNumber"=> {"type"=> "integer"},
          "dealName"=> {"type"=> "string"},
          "statusCode"=> {"type"=> "integer"},
          "status"=> {"type"=> "boolean"},
      }
  }
  @DealByStatusName.ValidateAPIwithSchema(@Schema,@responsedatavalue)
  #print @Schema
  # if @DealByStatusName.ValidateAPIwithSchema(@Schema,@responsedatavalue)
  #     puts "passed"
  # else
  #   fail("Failed")
  # end
end