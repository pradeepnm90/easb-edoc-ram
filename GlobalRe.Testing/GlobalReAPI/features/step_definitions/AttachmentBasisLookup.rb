Given(/^that I am submitting an Attachment Basis Lookup get request for fetching all the attachment basis types for type (.*)$/) do |assumedCededAllFlag|
  puts "Step Name: Fetching Attachment Basis Lookup.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getAttachmentBasisLookup(assumedCededAllFlag).to_s
  puts "Endpoint= " + @endpoint
  @attachmentbasis = LookUps.new(@reporter)
  @responseval = @attachmentbasis.SubmitGETRequest(@endpoint)
  puts "Response value= " + @responseval.to_s
end

Then(/^I receive a response with status as successful for the Attachment Basis Lookup request for (.*)$/) do |assumedCededAllFlag|
  print "Step Name: Verify whether the Attachment Basis Lookup request response is successful.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getAttachmentBasisLookup(assumedCededAllFlag).to_s
  @attachmentbasis = LookUps.new(@reporter)
  @responseval = @attachmentbasis.SubmitGETRequest(@endpoint)
  @responsecode = @attachmentbasis.VerifyResponseStatusCode(@responseval).to_s
  @attachmentbasis.CheckSuccessfulResponse(@responsecode)
end

Then(/^the Attachment Basis Lookup get request response for (.*) and the db query result is matched successfully$/) do |assumedCededAllFlag|
  print "Step Name: Verify whether the AttachmentBasis Lookup GET request response for  #{assumedCededAllFlag} flag and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @attachmentbasis = LookUps.new(@reporter)
  @sqlquery=@dbquery.getAttachmentBasisOptionsQuery(assumedCededAllFlag).to_s
  # print "\n"
  # puts @sqlquery
  @dbresult = @attachmentbasis.sendQuery(@sqlquery)
  # puts @dbresult
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getAttachmentBasisLookup(assumedCededAllFlag).to_s
  @responseval = @attachmentbasis.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s
  # print "\n"
  # puts @responsebody

  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)

end

Then(/^the Attachment Basis Lookup get request response for (.*) and the schema provided is matched successfully$/) do |assumedCededAllFlag|
  print "Step Name: Matching the Attachment Basis Lookup request response and the schema provided are matched successfully.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getAttachmentBasisLookup(assumedCededAllFlag).to_s
  @attachmentbasis = LookUps.new(@reporter)
  @responseval = @attachmentbasis.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["results"]
  # @responsedatavalue = @responseresvalue.first['data']
puts @responseresvalue

  @Schema = {
      "type" => "array",
      "required" => ["attachmentCode","attachmentDescription","activeFlag","assumedFlag","assumedDescription","cededFlag","cededDescription"],
      "properties" => {
          "coverageCode" => {"type" => "Integer"},
          "coverageDescription" => {"type" => "String"},
          "activeFlag" => {"type" => "Boolean"},
          "assumedFlag" => {"type" => "Boolean"},
          "assumedName" => {"type" => "String"},
          "cededFlag" => {"type" => "Boolean"},
          "cededName" => {"type" => "String"}
      }
  }
  #print @Schema
  @attachmentbasis.ValidateAPIwithSchema(@Schema,@responseresvalue)
end
