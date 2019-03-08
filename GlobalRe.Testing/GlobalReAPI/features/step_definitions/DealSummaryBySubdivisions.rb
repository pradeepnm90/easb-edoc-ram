

Given(/^that I am submitting a get deal summary by (.*) subdivision request$/) do |subdivisions|
  print "Step Name: Fetching deal summaries by subdivisions.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusSummariesBySubdivisions(subdivisions).to_s
  puts @endpoint
  @DealStatusSummaryBySubDivision = DealByStatusNameActions.new(@reporter)
  @responseval = @DealStatusSummaryBySubDivision.SubmitGETRequest(@endpoint)
end

Then(/^I receive a response with status as successful for the deal summary by (.*) subdivision request$/) do |subdivisions|
  print "Step Name: Verify whether the deal summary by sub division request response is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusSummariesBySubdivisions(subdivisions).to_s
  @DealStatusSummaryBySubDivision = DealByStatusNameActions.new(@reporter)
  @responseval = @DealStatusSummaryBySubDivision.SubmitGETRequest(@endpoint)
  @responsecode = @DealStatusSummaryBySubDivision.VerifyResponseStatusCode(@responseval).to_s
  @DealStatusSummaryBySubDivision.CheckSuccessfulResponse(@responsecode)
end

Then(/^the get deal summary by (.*) subdivision request response and the schema provided is matched successfully$/) do |subdivisions|
  print "Step Name: Matching the deal summary by subdivision request response and the schema provided.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealStatusSummariesBySubdivisions(subdivisions).to_s
  @DealStatusSummaryBySubDivision = DealByStatusNameActions.new(@reporter)
  @responsebody = @DealStatusSummaryBySubDivision.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)
  @responseresvalue = @parsedrespvalue["results"]
  @responsedatavalue = @responseresvalue.first['data']
  #print "res"+@responsedatavalue

  #print ""
  #print ""
  #print ""
  #print @responseresvalue
  #print ""
  #print ""
  #print @responsedatavalue
  #print ""

=begin
  @Schema = {
      "type": "object",
      "required": ["statusCode", "statusName", "sortOrder", "count", "statusSummary"],
      "properties": {
          "statusCode": {
              "type": "String"
          },
          "statusName": {
              "type": "String"
          },
          "sortOrder": {
              "type": "String"
          },
          "count": {
              "type": "String"
          },
          "statusSummary": {
              "type": "array",
              "properties": {
                  "statusCode": {
                      "type": "String"
                  },
                  "statusName": {
                      "type": "String"
                  },
                  "sortOrder": {
                      "type": "String"
                  },
                  "count": {
                      "type": "String"
                  },
                  "workflowId": {
                      "type": "number"},
                  "workflowName":{"type":"String"},
                  "statusSummary":{"type":["String","null"]}
                  }
          }
      }
  }
=end
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
  @DealStatusSummaryBySubDivision.ValidateAPIwithSchema(@Schema,@responsedatavalue)
end

And(/^I fetch the count of deals by (.*) status and (.*) subdivision from DB$/) do |subdivisions|
  print "Step Name: Fetching count of all the status from DB.\n"
  @dbquery = DBQueries.new
  @DealSummaryBySubdivision = DealByStatusNameActions.new(@reporter)
  # Need to update
  @sqlquery=@dbquery.getDealSummaryBySubdivisions(subdivisions).to_s
  @dbresult = @DealSummaryBySubdivision.sendQuery(@sqlquery)
  #print ""
  #print @dbresult
  #print ""
end

Then(/^the get deal summary by (.*) request response to and the db query result is matched successfully$/) do |subdivisions|
  print "Step Name: Verify whether the get request response and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @DealSummaryActions = DealSummaryActions.new(@reporter)

  #@sqlquery=@dbquery.getDealSummary.to_s
  #@dbresult = @DealByStatusName.sendQuery(@sqlquery)
  #print ""
  #print @dbresult
  #print ""
  @endpointc = Endpoints.new
  #Need to update
  @endpoint = @endpointc.getDealSummaryBySubdivisions.to_s
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsebody = BaseContainer.fetchresponsebody(@responseval)
  @modifiedresponsebody = @DealSummaryActions.modifyResponseBody(@responsebody)
  #@splitteddbresult = @DealSummaryActions.modifyDBresults(@dbresult)
  @DealSummaryActions.compareSummaryAPIResponseandDEBResults(@modifiedresponsebody)
  #print ""
  #print @responsebody
  #print ""

end


Then(/^the get deal summary by (.*) subdivision request response and the db query result is matched successfully$/) do |subdivisions|
  print "Step Name: Verify whether the get deal summary by " + subdivisions + " subdivision request response and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @DealSummaryActions = DealSummaryActions.new(@reporter)

  #@sqlquery=@dbquery.getDealSummary.to_s
  #@dbresult = @DealByStatusName.sendQuery(@sqlquery)
  #print ""
  #print @dbresult
  #print ""
  @endpointc = Endpoints.new
  #Need to update
  @endpoint = @endpointc.getDealStatusSummariesBySubdivisions(subdivisions).to_s
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsebody = BaseContainer.fetchresponsebody(@responseval)
  @modifiedresponsebody = @DealSummaryActions.modifyResponseBody(@responsebody)
  #@splitteddbresult = @DealSummaryActions.modifyDBresults(@dbresult)
  @DealSummaryActions.compareSubDivisionsSummaryAPIResponseandDBResults(@modifiedresponsebody,subdivisions)
  #print ""
  #print @responsebody
  #print ""
end