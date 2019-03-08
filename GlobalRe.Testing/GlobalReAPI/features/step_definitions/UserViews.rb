Given(/^that I am submitting a Userviews API GET request for fetching all the views for  screen name (.*)$/) do |screenName|
  puts "Step Name: Fetching UserViews"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getUserViews(screenName).to_s
  puts @endpoint
  @userViews = UserViews.new(@reporter)
  @responseval = @userViews.SubmitGETRequest(@endpoint)
  puts "Response: " + @responseval
end

Then(/^I verify if I get  response code for userViews GET API as (.*)$/) do |expectedResponseCode|
  puts "Step Name: Verify whether we get successful response code for userViews API GET request. \n"
  @responsecode = @userViews.VerifyResponseStatusCode(@responseval).to_s
  puts "ResponseCode= " + @responsecode
  @userViews.compareExpectedAndActualResult(@responsecode,expectedResponseCode)
end

Given(/^that I am submitting a Userviews API POST request for fetching all the views for screen name (.*) view name (.*) and layout (.*)$/) do |screenName, viewName, layout|
  @endpointc = Endpoints.new
  @endpoint = @endpointc.postUserViews.to_s
  @userViews = UserViews.new(@reporter)
  @time = Time.now.to_s
  @viewName=viewName.gsub(/#timestamp/,@time)
  puts "Time=" + @time
  @postJson="{ \"screenName\": \""+screenName+"\", \"viewName\": \""+@viewName+"\",  \"layout\": \""+layout+"\" }"
  puts "postJason: " + @postJson
  @responseval=@userViews.SubmitPOSTRequest(@endpoint,@postJson)
   @responseval1=@responseval
  puts "Response= "+@responseval.to_s
end

And(/^I verify if the view is created successfully in the database for screenName (.*)$/) do |screenName|

end

Then(/^Verify if the Userviews GET request response for for screen name (.*) and the db query result is matched successfully$/) do |screenName|
  puts "Step Name: Verify whether the UserViews API GET request response  and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @userViews = UserViews.new(@reporter)
  @sqlquery=@dbquery.getUserViews(screenName).to_s
  puts @sqlquery
  @dbresult = @userViews.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s

  @endpointc = Endpoints.new
  @endpoint = @endpointc.getUserViews(screenName).to_s
  @responseval = @userViews.SubmitGETRequest(@endpoint)

  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s

  puts "Response body : "+@responsebody.to_s

  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)
end

Then(/^The UserViews API GET  request response for  screen name (.*) and the schema provided is matched successfully$/) do |screenName|
  print "Step Name: Verifying whether the GET userViews API request response and the schema provided are matched successfully.\n"
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["results"]


  @Schema = {
      "type" => "array",
      "required" => ["viewId","userId","screenName","viewName","default","layout"],
      "properties" => {
          "viewId" => {"type" => "Integer"},
          "userId" => {"type" => "Integer"},
          "screenName" => {"type" => "String"},
          "viewName" => {"type" => "String"},
          "default" => {"type" => "Boolean"},
          "layout"=> {"type" => "String"},
      }
  }
  print @Schema
  @userViews.ValidateAPIwithSchema(@Schema,@responseresvalue)
end

And(/^I verify if the view is created successfully in the database$/) do
  @userViews_response_hash = JSON.parse(@responseval1)
  @responseData=@userViews_response_hash.fetch("data")
  puts "@responseData= " + @responseData.to_s
  @viewId =  @responseData["viewId"]
  @userId =  @responseData["userId"]
  @screenName =  @responseData["screenName"]
  @viewName =  @responseData["viewname"]
  @default=@responseData["default"]
  @layout=@responseData["layout"]

  @dbquery = DBQueries.new
  @userViews = UserViews.new(@reporter)
  @sqlquery=@dbquery.checkCreatedUserView(@viewId).to_s
  puts @sqlquery
  @dbresult = @userViews.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s
  @parsedDBvalue = JSON.parse(@dbresult.to_json)
  @dbviewId = @dbresult[0]["viewId"]
  @dbuserId = @dbresult[0]["userId"]
  @dbscreenName = @dbresult[0]["screenName"]
  @dbviewName = @dbresult[0]["viewname"]
  @dbdefault=@dbresult[0]["default"]
  @dblayout=@dbresult[0]["layout"]


  @userViews.compareExpectedAndActualResult(@screenName,@dbscreenName)
  @userViews.compareExpectedAndActualResult(@viewName,@dbviewName)
  @userViews.compareExpectedAndActualResult(@userId,@dbuserId)
  @userViews.compareExpectedAndActualResult(@default,@dbdefault)
  @userViews.compareExpectedAndActualResult(@layout,@dblayout)
end

Then(/^I verify if I get  response code for userViews POST API as (.*)$/) do |expectedResponseCode|
  puts "Step Name: Verify whether we get successful response code for userViews API POST request. \n"
  @responsecode = @userViews.VerifyResponseStatusCode(@responseval).to_s
  puts "ResponseCode= " + @responsecode
  @userViews.compareExpectedAndActualResult(@responsecode,expectedResponseCode)
end

And(/^I submit GET request with a specific view ID$/) do
  @userViews_response_hash = JSON.parse(@responseval1)
  @responseData=@userViews_response_hash.fetch("data")
  puts "@responseData= " + @responseData.to_s
  @viewId =  @responseData["viewId"]

  @endpointc = Endpoints.new
  @endpoint = @endpointc.getUserViewFromViewId(@viewId).to_s
  puts @endpoint
  @userViews = UserViews.new(@reporter)
  @responseFromGet = @userViews.SubmitGETRequest(@endpoint)
  puts "Response: " + @responseFromGet

end

Then(/^I verify if I the response code  for GET request for fetching userView with specific view ID is the expected response code (.*)$/) do |expectedResponseCode|
  @responsecode = @userViews.VerifyResponseStatusCode(@responseFromGet).to_s
  puts "ResponseCode= " + @responsecode
  @userViews.compareExpectedAndActualResult(@responsecode,expectedResponseCode)
end



And(/^I verify if the response for GET request for fetching userView with specific view ID is matched with the database$/) do

end

And(/^I verify if the response for fetching userView with specific view ID and Database is matched successfully$/) do
  @userViews_response_hash = JSON.parse(@responseFromGet)
  @responseData=@userViews_response_hash.fetch("data")
  puts "@responseData= " + @responseData.to_s
  @viewId =  @responseData["viewId"]
  @userId =  @responseData["userId"]
  @screenName =  @responseData["screenName"]
  @viewName =  @responseData["viewname"]
  @default=@responseData["default"]
  @layout=@responseData["layout"]

  @dbquery = DBQueries.new
  @userViews = UserViews.new(@reporter)
  @sqlquery=@dbquery.checkCreatedUserView(@viewId).to_s
  puts @sqlquery
  @dbresult = @userViews.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s
  @parsedDBvalue = JSON.parse(@dbresult.to_json)
  @dbviewId = @dbresult[0]["viewId"]
  @dbuserId = @dbresult[0]["userId"]
  @dbscreenName = @dbresult[0]["screenName"]
  @dbviewName = @dbresult[0]["viewname"]
  @dbdefault=@dbresult[0]["default"]
  @dblayout=@dbresult[0]["layout"]


  @userViews.compareExpectedAndActualResult(@screenName,@dbscreenName)
  @userViews.compareExpectedAndActualResult(@viewName,@dbviewName)
  @userViews.compareExpectedAndActualResult(@userId,@dbuserId)
  @userViews.compareExpectedAndActualResult(@default,@dbdefault)
  @userViews.compareExpectedAndActualResult(@layout,@dblayout)
end