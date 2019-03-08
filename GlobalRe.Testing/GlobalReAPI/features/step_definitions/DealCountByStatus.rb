Given(/^the GRS POC environment has servicename provisioned and the client calls the service$/) do
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealCountByStatusendpoints.to_s
  @DealCByStatus = DealCountByStatus_actions.new(@reporter)
  @responseval = @DealCByStatus.SubmitGETRequest(@endpoint)
  @responsecode = @DealCByStatus.VerifyResponseStatusCode(@responseval).to_s
  @DealCByStatus.CheckServiceProvisioned(@responsecode)
end

Then(/^the response is SUCCESS$/) do
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealCountByStatusendpoints.to_s
  @responseval = @DealCByStatus.SubmitGETRequest(@endpoint)
  @responsecode = @DealCByStatus.VerifyResponseStatusCode(@responseval).to_s
  @DealCByStatus.CheckSuccessfulResponse(@responsecode)
end

Given(/^the response is received for the submitted request$/) do
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealCountByStatusendpoints.to_s
  @DealCByStatus = DealCountByStatus_actions.new(@reporter)
  @responseval = @DealCByStatus.SubmitGETRequest(@endpoint)
  @responsecode = @DealCByStatus.VerifyResponseStatusCode(@responseval).to_s
  @DealCByStatus.CheckResponseReceived(@responsecode)
end

Given(/^the corresponding the DB query is executed$/) do
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealCountByStatusquery.to_s
  @DealCByStatus.sendQuery(@sqlquery)
end

Then(/^the submitted request and the DB query result is matching$/) do
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealCountByStatusendpoints.to_s
  @responseval = @DealCByStatus.SubmitGETRequest(@endpoint)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealCountByStatusquery.to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
  @DealCByStatus.compareAPIandDBresult(@responseval,@dbresultvalue)
end


Then(/^the schema for the JSON response for the submitted request and the provided JSON message schema is matching$/) do
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealCountByStatusendpoints.to_s
  @responsebody = @DealCByStatus.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)
  @responseresvalue = @parsedrespvalue["results"]
  @responsedatavalue = @responseresvalue.first['data']
  print "\n"
  print @responsebody
  print "\n"
  print "\n"
  print @responseresvalue
  print "\n"
  print "\n"
  print @responsedatavalue
  print "\n"

  @Schema = {
      "type" => "object",
      "required" => ["statusid","statusName","count"],
      "properties" => {
          "statusid" => {"type" => "String"},
          "statusName" => {"type" => "integer"},
          "count" => {"type" => "String"}
      }
  }
  #require 'json'
  #require 'json-schema'
  #@SchemaFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"testdata/JSON_SCHEMA_FILES","DealCountByStatus_Schema.json")
  #@Schema_data = JSON.parse(File.read(@SchemaFile))
  #@Schema = JsonSchema.parse!(@Schema_data)
  #print @Schema
  @DealCByStatus.ValidateAPIwithSchema(@Schema,@responsedatavalue)
end