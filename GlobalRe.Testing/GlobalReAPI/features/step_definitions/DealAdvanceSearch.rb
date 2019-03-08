Given(/^that I am submitting a GET Deal Advance search api call By passing (.*) (.*) (.*) (.*) (.*) filters$/) do |subdivision, productLine, exposureGroup, exposureType, statusCode|
  puts "Step Name: Fetching deals based on Deal Advance search .\n"
  @excelPath =  File.join(File.absolute_path('../', File.dirname(__FILE__)),"testdata","ExposureTree.xlsx")
  # @excelPath = 'C:\Users\vboya\RubymineProjects\GlobalReAPI\features\testdata\ExposureTree.xlsx'
  @filter = DealByStatusNameActions.new(@reporter)
  @colNameHash = @filter.colNameMapping(@excelPath)
  @endpointc = Endpoints.new
  # @expoType = @filter.getExposureType(@excelPath,@colNameHash,subdivision,productLine,exposureGroup,exposureType)
  @endpoint = @endpointc.getFilters(@excelPath,@colNameHash,subdivision,productLine,exposureGroup,exposureType,statusCode)
  # puts @expoType
  # puts "Endpoint= " + @endpoint
  @advanceFilter = DealByStatusNameActions.new(@reporter)
  @responseval = @advanceFilter.SubmitGETRequest(@endpoint)
  # puts "Response value= " + @responseval.to_s
  # @responsecode = @attachmentbasis.VerifyResponseStatusCode(@responseval).to_s
  # @attachmentbasis.CheckSuccessfulResponse(@responsecode)
end

Then(/^I receive a response with status as successful for the GET Deal Advance search API request$/) do
  @advanceFilter = DealByStatusNameActions.new(@reporter)
  @responsecode = @advanceFilter.VerifyResponseStatusCode(@responseval).to_s
  @advanceFilter.CheckSuccessfulResponse(@responsecode)
end

And(/^I receive a response with deal numbers associated with the filter (.*) (.*) (.*) (.*) (.*)$/) do |subdivision, productLine, exposureGroup, exposureType, statusCode|

  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s
end

Then(/^The GET deal Advance search (.*) (.*) (.*) (.*) (.*) API response  with the Data base is matched successfully$/) do |subdivision, productLine, exposureGroup, exposureType, statusCode|
  print "Step Name: Verify whether the GET deal Advance search API response and the db query result is matched successfully.\n"
  @advanceFilter = DealByStatusNameActions.new(@reporter)
  @expoType = @advanceFilter.getExposureTypes(@excelPath,@colNameHash,subdivision,productLine,exposureGroup,exposureType)
  @dbQuery = DBQueries.new
  @sqlquery =  @dbQuery.getDealsFilterQuery(@expoType,statusCode)
  puts 'sql query : ' + @sqlquery
  @dbresult = @advanceFilter.sendQuery(@sqlquery)
  if @responsebody == ""
    @responsebody = []
  end
  # puts ""
  # puts 'respone  ' + @responsebody.to_s
  # puts 'db result ' + @dbresult.to_s
  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)

end



Given(/^validation data (.*) (.*) (.*) (.*) (.*)$/) do |subdivision, productLine, exposureGroup, exposureType, statusCode|
  @excelPath = 'C:\Users\vboya\RubymineProjects\GlobalReAPI\features\testdata\ExposureTree.xlsx'
  @filter = DealByStatusNameActions.new(@reporter)
  @colNameHash = @filter.colNameMapping(@excelPath)
  @endpoint = Endpoints.new
  @expoType = @filter.getExposureTypes(@excelPath,@colNameHash,subdivision,productLine,exposureGroup,exposureType)
  # print expoType.to_s.gsub(/"/, "'")
  @dbQuery = DBQueries.new
  puts @dbQuery.getDealsFilterQuery(@expoType,statusCode)
end


Then(/^The GET deal Advance search (.*) (.*) (.*) (.*) (.*) API response  with the schema provided is matched successfully$/) do |subdivision, productLine, exposureGroup, exposureType, statusCode|
  @filter = DealByStatusNameActions.new(@reporter)
  @schema = {
      "type" => "object",
      "required" => ["dealNumber","dealName","statusCode","status","contractNumber","inceptionDate","targetDate","priority","submittedDate","primaryUnderwriterCode","primaryUnderwriterName","secondaryUnderwriterCode","secondaryUnderwriterName","technicalAssistantCode","technicalAssistantName","modellerCode","modellerName","actuaryCode","actuaryName","expiryDate","brokerCode","brokerName","brokerContactCode","brokerContactName","cedantCode","cedantName","continuous","cedantLocationCode","cedantLocationName","brokerLocationCode","brokerLocationName","paper","renewal","expiresAtEndOfDate","exposureTypeCode","dealTypeCode","dealTypeName","coverageType","coverageName","policyBasis","policyBasisName","currencyCode","currencyName","domicileCode","domicileName","regionCode","regionName"],
      "properties" => {
          "dealNumber" => {"type" => "Integer"},
          "dealName" => {"type" => "String"},
          "statusCode" => {"type" => "Integer"},
          "status" => {"type" => "String"},
          "contractNumber" => {"type" => "Integer"},
          "inceptionDate" => {"type" => "String"},
          "targetDate" => {"type" => "String"},
          "priority" => {"type" => "Integer"},    #changed from Integer to String
          "submittedDate" => {"type" => "String"},
          "primaryUnderwriterCode" => {"type" => "Integer"},
          "primaryUnderwriterName" => {"type" => "String"},
          "secondaryUnderwriterCode" => {"type" => "Integer"},
          "secondaryUnderwriterName" => {"type" => "String"},
          "technicalAssistantCode" => {"type" => "Integer"},
          "technicalAssistantName" => {"type" => "String"},
          "modellerCode" => {"type" => "Integer"},
          "modellerName" => {"type" => "String"},
          "actuaryCode" => {"type" => "Integer"},
          "actuaryName" => {"type" => "String"},
          "expiryDate" => {"type" => "String"},
          "brokerCode" => {"type" => "Integer"},
          "brokerName" => {"type" => "String"},
          "brokerContactCode" => {"type" => "Integer"},
          "brokerContactName" => {"type" => "String"},
          "cedantCode"=> {"type"=> "Integer"},
          "cedantName"=> {"type"=> "string"},
          "continuous"=> {"type"=> "Integer"}, # Changed from integer to boolean
          "cedantLocationCode"=> {"type"=> "integer"},
          "cedantLocationName"=> {"type"=> "string"},
          "brokerLocationCode"=> {"type"=> "integer"},
          "brokerLocationName"=> {"type"=> "string"},
          "paper"=> {"type"=> "string"},
          "renewal"=> {"type"=> "integer"},
          "expiresAtEndOfDate"=> {"type"=> "boolean"},
          "exposureTypeCode"=> {"type"=> "integer"},
          "dealTypeCode"=> {"type"=> "integer"},
          "dealTypeName"=> {"type"=> "string"},
          "coverageType"=> {"type"=> "integer"},
          "coverageName"=> {"type"=> "string"},
          "policyBasis"=> {"type"=> "integer"},
          "policyBasisName"=> {"type"=> "string"},
          "currencyCode"=> {"type"=> "integer"},
          "currencyName"=> {"type"=> "string"},
          "domicileCode"=> {"type"=> "integer"},
          "domicileName"=> {"type"=> "string"},
          "regionCode"=> {"type"=> "integer"},
          "regionName"=> {"type"=> "string"}
      }
  }
  @responseresvalue = @parsedrespvalue["results"]
  @responseresvalue = @responseresvalue.first['data']
  puts @responseresvalue.to_s
  @filter.ValidateAPIwithSchema(@schema,@responseresvalue)
end

Given(/^that I am submitting a GET Deal Advance search api call By  invalid parameters (.*) (.*) (.*) (.*) (.*) filters$/) do |subdivision, productLine, exposureGroup, exposureType, statusCode|
  puts "Step Name: Fetching deals based on Deal Advance search .\n"
  # @excelPath = 'C:\Users\vboya\RubymineProjects\GlobalReAPI\features\testdata\ExposureTree.xlsx'
  # @filter1 = DealByStatusNameActions.new(@reporter)
  # @colNameHash = @filter.colNameMapping(@excelPath)
  @endpointc = Endpoints.new
  # @expoType = @filter.getExposureType(@excelPath,@colNameHash,subdivision,productLine,exposureGroup,exposureType)
  @endpoint = @endpointc.getWarningFilter(subdivision,productLine,exposureGroup,exposureType,statusCode)
  # puts @expoType
  # puts "Endpoint= " + @endpoint
  @advanceFilter = DealByStatusNameActions.new(@reporter)
  @responseval = @advanceFilter.SubmitGETRequest(@endpoint)
  # puts "Response value= " + @responseval.to_s
  @responsecode = @advanceFilter.VerifyResponseStatusCode(@responseval).to_s
  # DealByStatusNameActions.ValidateDealAdvanceSearchWarnings(@responseval,)
  # puts "Response value= " + @responsecode.to_s

  # @attachmentbasis.CheckSuccessfulResponse(@responsecode)
  #
  # @responsecode = @dealByStatusName.VerifyResponseStatusCode(@responsevalu).to_s
  # BaseContainer.CheckDealPutWarningResponse(@responsecode,status)
  # @responseMessage = @dealByStatusName.GetResponseMessage(@responsevalu).to_s
  # PutDealsByDealNumberActions.ValidateCedantWarnings(@responseMessage,expectedWarningMessage)
  #
end

Then(/^I receive a response with warning message associated with the filter (.*) (.*) (.*) (.*) (.*) and matches with the (.*)$/) do |subdivision, productLine, exposureGroup, exposureType, statusCode, expectedWarningMessage|
  DealByStatusNameActions.ValidateDealAdvanceSearchWarnings(@responseval,expectedWarningMessage)
end