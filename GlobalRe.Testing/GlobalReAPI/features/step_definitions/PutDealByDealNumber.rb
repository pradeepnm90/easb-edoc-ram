Given(/^that I am submitting a Put Deal api call By (.*) Deal Number for the field (.*) with the value (.*) for the deal with (.*) status with actual values (.*)$/) do |dealnumber, field, value, status, actualvalue|
  print "Step Name: Submitting a Put Deal api call By " + dealnumber + " Deal Number for the field " + field + " with the value " + value + " for the deal with " + status + " status.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.putByDealNumber(dealnumber).to_s
  # print "\n"
  # print @endpoint
  # print "\n"
  @responsevalu = BaseContainer.submitDealPutRequest(@endpoint,actualvalue,field,value)
  # print "\n"
  # print "Responsvalu : "+@responsevalu
  # print "\n"
end

And(/^I receive a response for the (.*) status with status as successful for the Put Deal By Deal Number api request$/) do |status|
  print "Step Name: Verify whether a response with status as successful is received for the Put Deal By Deal Number api request.\n"
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responsevalu).to_s
  BaseContainer.CheckDealPutSuccessfulResponse(@responsecode,status)
end

And(/^I receive a Put deal request response for the (.*) status which matches with the schema provided$/) do |status|
  print "Step Name: Matching the Put deal request response and the schema provided.\n"
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  # print "\n"
  # print @responseval
  # print "\n"
  @parsedrespvalue = JSON.parse(@responsevalu)
  @responseresvalue = @parsedrespvalue["data"]
  print "\n"
   print @responseresvalue
  print "\n"
  @Schema = {
      "type" => "object",
      "required" => ["dealNumber","dealName","statusCode","status","contractNumber","inceptionDate","targetDate","priority","submittedDate","primaryUnderwriterCode","primaryUnderwriterName","secondaryUnderwriterCode","secondaryUnderwriterName","technicalAssistantCode","technicalAssistantName","modellerCode","modellerName","actuaryCode","actuaryName","expiryDate","brokerCode","brokerName","brokerContactCode","brokerContactName"],
      "properties" => {
          "dealNumber" => {"type" => "Integer"},
          "dealName" => {"type" => "String"},
          "statusCode" => {"type" => "Integer"},
          "status" => {"type" => "String"},
          "contractNumber" => {"type" => "Integer"},
          "inceptionDate" => {"type" => "String"},
          "targetDate" => {"type" => "String"},
          "priority" => {"type" => "Integer"},
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
          "continuous"=> {"type"=> "Integer"},
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
    BaseContainer.ValidateDealSchema(@Schema,@responseresvalue,status)
end

And(/^I am able to find the data of the field (.*) in the DB successfully updated the deal with deal number (.*) with the new value (.*) for the deal with (.*) with actual values (.*)$/) do |field,dealnumber,value, status, actualvalue|
  print "Step Name: Verify whether the data of the field " + field + " in the DB successfully updated with the new value " + value + " for the deal with " + status + " status.\n"
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealDetailsByDealNumber(dealnumber).to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
  #puts @sqlquery
  # print "\n"
  # print @dbresultvalue[0]
  # print "\n"
  # print "\n"
  puts "response value "+ @responseval.to_s#["data"]
  puts "Response value added : " +@responseresvalue.to_s
  # print "\n"
  # @modifiedresponsevaluehash = JSON.parse(@responseval)
  @modifiedresponsevaluehash = JSON.parse(@responsevalu)
  @responseresvalue = @modifiedresponsevaluehash["data"]
  #print "\n"
  # print @modifiedresponsevaluehash["data"]
  # print "\n"
  BaseContainer.CompareDealAPIandDBResponse(@responseresvalue,@dbresultvalue[0],status)
  @putdealsbydealNumber = PutDealsByDealNumberActions.new()
  @putdealsbydealNumber.verifyfieldvalueIsUpdated(field,dealnumber,value,status,actualvalue)
end

And(/^I submit a Get Deal api call By (.*) Deal Number for (.*) status which a successful response is received$/) do |dealnumber,status|
  print "Step Name: Fetching the details of a deal having the deal number #{dealnumber}.\n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getDealByDealNumber(dealnumber).to_s
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @responseval = @DealByStatusName.SubmitGETRequest(@endpoint)
  @responsecode = @DealByStatusName.VerifyResponseStatusCode(@responseval).to_s
  BaseContainer.CheckDealGetSuccessfulResponse(@responsecode,status)
  # print "\n"
  # print @responseval
  # print "\n"
end

And(/^I receive a Get deal api call By (.*) Deal Number request response for the (.*) status which matches with the schema provided$/) do |dealnumber,status|
  print "Step Name: Matching the Get deal api call By #{dealnumber} Deal Number request response and the schema provided.\n"
  @DealByStatusName = DealByStatusNameActions.new(@reporter)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["data"]
  puts "respone val : " + @responseresvalue.to_s
  @Schema = {
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
          "priority" => {"type" => "Integer"},
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
          "continuous"=> {"type"=> "Integer"},
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
  BaseContainer.ValidateDealSchema(@Schema,@responseresvalue,status)
end

And(/^I am able to find the data of the (.*) field having a value (.*) in the response for the get deal by deal number (.*) and the response matching with the data send in the put request for the (.*) status$/) do |field, value, dealnumber,status|
  print "Step Name: Verify whether the data of the field " + field + " having a value " + value + " in the response for the get deal by deal number " + dealnumber + " and the response matching with the data send in the put request.\n"
  @dbquery = DBQueries.new
  @SubDivisionActions = SubDivsionsActions.new(@reporter)
  @sqlquery=@dbquery.getDealDetailsByDealNumber(dealnumber).to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["data"]
  # print "\n"
  # print @responseresvalue
  # print "\n"
  # print @dbresultvalue[0]
  # print "\n"
  @putRequestBodyValue = $putRequestbody
  BaseContainer.CompareDealAPIandDBResponse(@responseresvalue,@dbresultvalue[0],status)
  BaseContainer.compareDealputRequestvalueAndgetResponsevalue(@putRequestBodyValue.to_s,@responseresvalue.to_s,status)
end

Then(/^reset the data for the future test with the value (.*)$/) do |actualvalue|
  BaseContainer.resetsubmitDealPutRequest(@endpoint,actualvalue)
end

And(/^the Deal By Deal Number api GET request By (.*) Deal Number request response for the (.*) status and the db query result is matched successfully$/) do |dealnumber, status|
  print "Step Name: Verify whether the data of the DealByDealNumber API GET request for deal " + dealnumber + " having a status " + status + " JASON response and the Database matching successfully.\n"
  @dbquery = DBQueries.new
  @SubDivisionActions = SubDivsionsActions.new(@reporter)
  @sqlquery=@dbquery.getDealDetailsByDealNumber(dealnumber).to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["data"]
  # print "\n"
  # print @responseresvalue
  # print "\n"
  # print @dbresultvalue[0]
  # print "\n"
  @putRequestBodyValue = $putRequestbody
  BaseContainer.CompareDealAPIandDBResponse(@responseresvalue,@dbresultvalue[0],status)
  #BaseContainer.compareDealputRequestvalueAndgetResponsevalue(@putRequestBodyValue.to_s,@responseresvalue.to_s,status)
end


Then(/^I am able to find the data of the fields RiskCategory, Liability Type and class (.*) in the DB successfully updated for the deal with deal number (.*) with the status (.*)$/) do |value, dealnumber, status|
  print "Step Name: Verify whether the data of the fields RiskCategory, Liability Type and class for deal " + dealnumber + " having a status " + status + " are updated to the Database successfully.\n"
  @putdealsbydealNumber = PutDealsByDealNumberActions.new()
  @putdealsbydealNumber.verifyBusinessClassfieldvalueIsUpdated(dealnumber,value,status)
end

And(/^I receive a response with proper cedant information warning message for the (.*) deal for the Put Deal By Deal Number api request with expected warning message (.*)$/) do |status, expectedWarningMessage|
  print "Step Name: Verify whether proper cedant warning message is populated.\n"
  @dealByStatusName = DealByStatusNameActions.new(@reporter)
  @responsecode = @dealByStatusName.VerifyResponseStatusCode(@responsevalu).to_s
  BaseContainer.CheckDealPutWarningResponse(@responsecode,status)
  @responseMessage = @dealByStatusName.GetResponseMessage(@responsevalu).to_s
  PutDealsByDealNumberActions.ValidateCedantWarnings(@responseMessage,expectedWarningMessage)
end

Then(/^DB should not be updated with Cedant information (.*) for the deal with deal number (.*) with the status (.*)$/) do |value, dealnumber, status|
  print "Step Name: Verify whether the data of the fields CedantCode and CedantLocationCode for deal " + dealnumber + " having a status " + status + " are not updated to the Database.\n"
  @putdealsbydealNumber = PutDealsByDealNumberActions.new()
  @putdealsbydealNumber.verifyCedantfieldvalueIsUpdated(dealnumber,value,status,@responseMessage)
end