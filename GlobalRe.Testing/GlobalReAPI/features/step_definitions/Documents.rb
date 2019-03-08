
Given(/^that I am submitting an Key Document Types GET api request for fetching all the key docs types for deal (.*) with documents flag as (.*)$/) do |dealNumber, getDocTypes|
  puts "Step Name: Fetching all Key Document Types for the deal #{dealNumber} and with documents flag as #{getDocTypes}.\n"
  @endPointObj = Endpoints.new
  @endPoint = @endPointObj.getKeyDocumentType(dealNumber, getDocTypes).to_s
  puts "Endpoint = " + @endPoint
  @keyDocTypesObj = Documents.new(@reporter)
  @responseVal = @keyDocTypesObj.SubmitGETRequest(@endPoint)
  puts "Response value= " + @responseVal.to_s
end


Then(/^I receive a response with status as successful for the Key Document Types API request for (.*)$/) do |getDocTypes|
  print "Step Name: Verify whether the Key Document Types GET API request response is successful.\n"
  @responseCode = @keyDocTypesObj.VerifyResponseStatusCode(@responseVal).to_s
  @keyDocTypesObj.CheckSuccessfulResponse(@responseCode)
end


Then(/^the Key Document Types GET API request response and the schema provided is matched successfully$/) do
  print "Step Name: Matching the Key Docs Types API request response and the schema provided are matched successfully.\n"
  @parsedRespValue = JSON.parse(@responseVal)
  @responseResValue = @parsedRespValue["results"]
  # @responseDataValue = @responseResValue.first['data']
  puts @responseResValue

  @Schema = {
      "type" => "array",
      "required" => ["keyDocid","fileNumber","producer","docid","docName","sortOrder","location","drawer","folderid","folderName","docType","ermsClassType","fileType","dmsPath","itemCategoryid","ermsCategory","lastUpdatedUser","lastTimeStamp","dmsCreated","dmsUpdated"],
      "properties" => {
          "keyDocid" => {"type" => "Integer"},
          "fileNumber" => {"type" => "Integer"},
          "producer" => {"type" => "Integer"},
          "docid" => {"type" => "String"},
          "docName" => {"type" => "String"},
          "sortOrder" => {"type" => "Integer"},
          "location" => {"type" => "String"},
          "drawer" => {"type" => "String"},
          "folderid" => {"type" => "String"},
          "folderName" => {"type" => "String"},
          "docType" => {"type" => "String"},
          "ermsClassType" => {"type" => "String"},
          "fileType" => {"type" => "String"},
          "dmsPath" => {"type" => "String"},
          "itemCategoryid" => {"type" => "Integer"},
          "ermsCategory" => {"type" => "String"},
          "lastUpdatedUser" => {"type" => "String"},
          "lastTimeStamp" => {"type" => "String"},
          "dmsCreated" => {"type" => "String"},
          "dmsUpdated" => {"type" => "String"}
      }
  }
  #print @Schema
  @keyDocTypesObj.ValidateAPIwithSchema(@Schema,@responseResValue)
end