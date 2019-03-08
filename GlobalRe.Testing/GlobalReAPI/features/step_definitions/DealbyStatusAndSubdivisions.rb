Given(/^that I am submitting a get request for fetching all the (.*) deals with (.*) subdivision$/) do |status,subdivisions|
  print "Step Name: Fetching the " + status.to_s + " deals by "+subdivisions.to_s+".\n"

  @endpointc = Endpoints.new
  #@dealStatusQueryparam = @endpointc.getDealByStatusendpoints(status).to_s
  #@subdivisionsQueryparam = @endpointc.getSubdivisionQueryparam.to_s
  #@endpoint = @dealStatusQueryparam+'&'+@subdivisionsQueryparam
  @endpoint = @endpointc.getDealByStatusAndSubdivisions(status,subdivisions)
  puts @endpoint.to_s
  @DealbyStatusAndSubdivisions = DealByStatusNameActions.new(@reporter)
  @responseval = @DealbyStatusAndSubdivisions.SubmitGETRequest(@endpoint)
end

Then(/^I receive a response with status as successful for the (.*) status deal with (.*) subdivision request$/) do |status,subdivisions|
  print "Step Name: Verify whether the " + status.to_s + " status deal by "+ subdivisions.to_s+" response is successful.\n"
  @endpointc = Endpoints.new

  #@dealStatusQueryparam = @endpointc.getDealByStatusendpoints(status).to_s
  #@subdivisionsQueryparam = @endpointc.getSubdivisionQueryparam.to_s
  @endpoint = @endpointc.getDealByStatusAndSubdivisions(status,subdivisions)
  @DealbyStatusAndSubdivisions = DealByStatusNameActions.new(@reporter)
  @responseval = @DealbyStatusAndSubdivisions.SubmitGETRequest(@endpoint)
  #print "\nResponse :" + @responseval
  @responsecode = @DealbyStatusAndSubdivisions.VerifyResponseStatusCode(@responseval).to_s
  @DealbyStatusAndSubdivisions.CheckSuccessfulResponse(@responsecode)
end


Then(/^the get (.*) deals with (.*) subdivision request response and the schema provided is matched successfully$/) do |status,subdivisions|
  print "Step Name: Matching the get deals by status and subdivision request response for " + status.to_s + " and the schema provided.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusAndSubdivisions(status,subdivisions)
  @DealbyStatusAndSubdivisions = DealByStatusNameActions.new(@reporter)
  @responsebody = @DealbyStatusAndSubdivisions.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)
  @responseresvalue = @parsedrespvalue["results"]
  print "\n"
  print "REsponse value : "+@responseresvalue.to_s
  print "\n"
  if !@responseresvalue.blank?
  @responsedatavalue = @responseresvalue.first['data']
  #print "\n"
  #print @responseresvalue.size
  #print "\n"
  #print "\n"
  #print @responseresvalue
  #print "\n"
  # print "\n"
  # print @responsedatavalue
  # print "\n"

  @Schema = {
      "type" => "object",
      "required" => ["dealNumber","dealName","statusCode","status","contractNumber","inceptionDate","targetDate","priority","submittedDate","primaryUnderwriterCode","primaryUnderwriterName","secondaryUnderwriterCode","secondaryUnderwriterName","technicalAssistantCode","technicalAssistantName","modellerCode","modellerName","actuaryCode","actuaryName","expiryDate","brokerCode","brokerName","brokerContactCode","brokerContactName"],
      "properties" => {
          "dealNumber" => {"type" => "String"},
          "dealName" => {"type" => "String"},
          "statusCode" => {"type" => "String"},
          "status" => {"type" => "String"},
          "contractNumber" => {"type" => "String"},
          "inceptionDate" => {"type" => "String"},
          "targetDate" => {"type" => "String"},
          "priority" => {"type" => "String"},
          "submittedDate" => {"type" => "String"},
          "primaryUnderwriterCode" => {"type" => "String"},
          "primaryUnderwriterName" => {"type" => "String"},
          "secondaryUnderwriterCode" => {"type" => "String"},
          "secondaryUnderwriterName" => {"type" => "String"},
          "technicalAssistantCode" => {"type" => "String"},
          "technicalAssistantName" => {"type" => "String"},
          "modellerCode" => {"type" => "String"},
          "modellerName" => {"type" => "String"},
          "actuaryCode" => {"type" => "String"},
          "actuaryName" => {"type" => "String"},
          "expiryDate" => {"type" => "String"},
          "brokerCode" => {"type" => "String"},
          "brokerName" => {"type" => "String"},
          "brokerContactCode" => {"type" => "String"},
          "brokerContactName" => {"type" => "String"}
      }
  }
  #require 'json'
  #require 'json-schema'
  #@SchemaFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"testdata/JSON_SCHEMA_FILES","DealCountByStatus_Schema.json")
  #@Schema_data = JSON.parse(File.read(@SchemaFile))
  #@Schema = JsonSchema.parse!(@Schema_data)
  #print @Schema
  @DealbyStatusAndSubdivisions.ValidateAPIwithSchema(@Schema,@responsedatavalue)
  else
    print "The "+status.to_s+" has no records.\n"
    # fail "The "+status.to_s+" has no records.\n"
  end
end

And(/^I fetch the list of deals having (.*) status and (.*) from DB$/) do |status,subdivisions|
  print "Step Name: Fetching all the deals having the status " + status.to_s + " of subdivision "+ subdivisions.to_s+" and meeting the needed logic from DB.\n"
  @dbquery = DBQueries.new
  @DealByStatuAndSubdivision = DealByStatusNameActions.new(@reporter)
  @sqlquery=@dbquery.getDealsByStatusAndSubdivision(status,subdivisions).to_s
  # puts "SQL Query : " + @sqlquery
  @DealByStatuAndSubdivision.sendQuery(@sqlquery)
end

Then(/^the get deals by (.*) status and (.*) subdivision request response and the db query result is matched successfully$/) do |status,subdivisions|
  print "Step Name: Matching the request response for get deals by" + status.to_s + "status and "+ subdivisions.to_s+" and the DB Query Results provided.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusAndSubdivisions(status,subdivisions).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealsByStatusAndSubdivision(status,subdivisions).to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
  BaseContainer.CompareAPIAndDBResults(@responseval,@dbresultvalue)
end

And(/^I fetch the count of deals by (.*) status and (.*) subdivisions from DB$/) do |status, subdivisions|
  print "Step Name : To fetch the count of "+status.to_s+" status and "+subdivisions.to_s+ " subdivision from DB.\n"
  @dbquery = DBQueries.new
  @DealByStatuAndSubdivision = DealByStatusNameActions.new(@reporter)
  @sqlquery=@dbquery.getDealsCountByStatusAndSubdivision(status,subdivisions).to_s
  @DealByStatuAndSubdivision.sendQuery(@sqlquery)
end


Given(/^that I am submitting a get request for fetching count of (.*) deals with (.*) subdivision$/) do |status,subdivisions|
  print "Step Name : To fetch the count of " + status.to_s + " status deals with " + subdivisions.to_s+ " subdivision.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusAndSubdivisions(status,subdivisions)
  @DealbyStatusAndSubdivisions = DealByStatusNameActions.new(@reporter)
  @responsebody = @DealbyStatusAndSubdivisions.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)
  @totalrecords = @parsedrespvalue["totalRecords"]
  #print "\n"+@totalrecords.to_s
end

And(/^I fetch the count of deals having (.*) status and (.*) from DB$/) do |status,subdivisions|
  print "Step Name : To fetch the count "+status.to_s+" deals with "+subdivisions.to_s+ " subdivision from DB.\n"
  @dbquery = DBQueries.new
  @DealByStatusAndSubdivision = DealByStatusNameActions.new(@reporter)
  @sqlquery=@dbquery.getDealCountByStatusAndSubdivision(status,subdivisions).to_s
  @dbcount = @DealByStatusAndSubdivision.sendCountQuery(@sqlquery)
  #print "\nDB :"+ @dbcount
end

Then(/^I will be able to see the deal count of the request deals by (.*) status and (.*) match with DB count$/) do |status,subdivisions|
  print "Step Name : To verify the deal count of request deals by "+status.to_s+" and "+ subdivisions.to_s+" matching with DB count.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByStatusAndSubdivisions(status,subdivisions)
  puts @endpoint.to_s

  @DealbyStatusAndSubdivisions = DealByStatusNameActions.new(@reporter)
  @responsebody = @DealbyStatusAndSubdivisions.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responsebody)


  # print "\n"
  # print @responsebody
  # print "\n"
  # print @parsedrespvalue
  # print "\n"
  @totalrecords = @parsedrespvalue["totalRecords"]
  @records = @totalrecords.to_s
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealCountByStatusAndSubdivision(status,subdivisions)
  # print "\n"
  # print @sqlquery#.to_s
  # print "\n"
  @dbcount = @DealByStatusAndSubdivision.sendCountQuery(@sqlquery)
  #@dbcountnew = @dbcount.to_s
  # print "\n"
  # print @totalrecords#.to_s
  # print "\n"
  # print @dbcount
  # print "\n"
  #@DealByStatusAndSubdivision.verifyCount(@records,@dbcountnew)
  BaseContainer.CompareCountResults(@records,@dbcount)
end