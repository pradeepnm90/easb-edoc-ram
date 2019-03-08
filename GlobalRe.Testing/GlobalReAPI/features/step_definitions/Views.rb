Given(/^that I am submitting a PUT view API request by passing viewID (.*) for the field (.*) with the value (.*) to the (.*) actual values$/) do |viewId, field, value, actualValue|
  print "Step Name: Submitting a Put View api call By passing view ID" + viewId + " for the field " + field + " with the value " + value + " \n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.putByViewID(viewId).to_s
  @view = Views.new(@reporter)
  # print "\n"
  # print @endpoint
  # print "\n"
  @responsevalu = @view.submitViewPutRequest(@endpoint,actualValue,field,value)
  # print "\n"
  # print "Responsvalu : "+@responsevalu
  # print "\n"
end

And(/^I receive a response with status as successful for the PUT view API request$/) do
  print "Step Name: Verify whether a response with status as successful is received for the PUT View api request.\n"
  @viewObj = Views.new(@reporter)
  @responsecode = @viewObj.VerifyResponseStatusCode(@responsevalu).to_s
  @viewObj.CheckSuccessfulResponse(@responsecode)
end

And(/^I am able to find the data of the field (.*) in the DB successfully updated the view with viewID (.*) with the new value (.*) for the view with (.*) actual values$/) do |field, viewId, value, actualValue|
  print "Step Name: Verify whether the PUT View API response and the db query result is matched successfully.\n"
  @viewObj = Views.new(@reporter)
  @dbQuery = DBQueries.new
  @sqlquery =  @dbQuery.getExistingViewQuery(viewId)
  # puts 'sql query : ' + @sqlquery
  @dbresult = @viewObj.sendQuery(@sqlquery)
  #-------------API Response body--------------------------
  @parsedrespvalue = JSON.parse(@responsevalu)
  # puts "responevalue"
  # puts @parsedrespvalue
  @responsebody = @parsedrespvalue["data"]
  # @responsebody = BaseContainer.fetchresponsebody(@responsevalu).to_s
  if @responsebody == ""
    @responsebody = []
  end
  # puts ""
  # puts 'respone  ' + @responsebody.to_s
  # puts 'db result ' + @dbresult.to_s
  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult[0])
end

Then(/^reset the data for the future test with the (.*) actual value$/) do |actualValue|
  @viewObj = Views.new(@reporter)
  @viewObj.resetSubmitViewPUTRequest(@endpoint,actualValue)
end

And(/^I receive a PUT view api call By (.*) viewId request response for the (.*) with value (.*) which matches with the schema provided$/) do |viewId, field, value|
  print "Step Name: Matching the PUT api view call By passing #{viewId} viewId request response and the schema provided.\n"
  @viewObj = Views.new(@reporter)
  @parsedrespvalue = JSON.parse(@responsevalu)
  @responsebody = @parsedrespvalue["data"]
  # puts "respone val : " + @responsebody.to_s
  @Schema = {
      "type" => "object",
      "required" => ["viewId","userId","screenName","viewname","default","layout"],
      "properties" => {
          "viewId" => {"type" => "Integer"},
          "userId" => {"type" => "Integer"},
          "screenName" => {"type" => "String"},
          "viewname" => {"type" => "String"},
          "default" => {"type" => "String"},
          "layout" => {"type" => "String"},

      }
  }
  @viewObj.ValidateAPIwithSchema(@Schema,@responsebody)
end

Given(/^that I am submitting a DELETE view API request by passing viewID (.*)$/) do |viewId|
  print "Step Name: Submitting a DELETE View api call By passing view ID" + viewId + " \n"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.deleteByViewID(viewId).to_s
  @view = Views.new(@reporter)
  print "\n"
  print @endpoint
  print "\n"
  @responsevalu = @view.submitDeleteViewRequest(@endpoint)
  # print "\n"
  # print "Responsvalu : "+@responsevalu
  # print "\n"
end

And(/^I receive a response with status as successful for the DELETE view API request$/) do
  print "Step Name: Verify whether a response with status as successful is received for the DELETE View api request.\n"
  @viewObj = Views.new(@reporter)
  @responsecode = @viewObj.VerifyResponseStatusCode(@responsevalu).to_s
  @viewObj.CheckSuccessfulResponse(@responsecode)
end

Then(/^I am not able to find the data of the viewId (.*) in the DB$/) do |viewId|
  print "Step Name: Verify whether the DELETE View API deleted the record in the data base successfully.\n"
  @viewObj = Views.new(@reporter)
  #-------------DB Response body--------------------------
  @dbQuery = DBQueries.new
  @sqlquery =  @dbQuery.getExistingViewQuery(viewId)
  # puts 'sql query : ' + @sqlquery
  @dbresult = @viewObj.sendQuery(@sqlquery)
  #-------------API Response body--------------------------
  @parsedrespvalue = JSON.parse(@responsevalu)
  @responsebody = @parsedrespvalue["data"]
  if @responsebody == ""
    @responsebody = []
  end
  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult[0])
end