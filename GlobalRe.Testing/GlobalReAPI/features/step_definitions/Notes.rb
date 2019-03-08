#GET NOTES
Given(/^that I am submitting a Notes API GET request for fetching all the notes associated to a (.*)$/) do |dealnumber|
  puts "Step Name: Fetching all the notes associated with a deal"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getNotesByDealNumber(dealnumber).to_s
  puts @endpoint
  @notes = Notes.new(@reporter)
  @responseval = @notes.SubmitGETRequest(@endpoint)
  puts "Response: " + @responseval
end

Then(/^I receive a response with status as successful for the GET Notes API for deal  (.*)$/) do |dealnumber|
  puts "Step Name: Verify whether we get successful response code for Notes API GET request. \n"
  @responsecode = @notes.VerifyResponseStatusCode(@responseval).to_s
  puts "ResponseCode= " + @responsecode
  @notes.CheckSuccessfulResponse(@responsecode)
end

Then(/^The Notes GET  request response for (.*) deal and the db query result is matched successfully$/) do |dealnumber|
  puts "Step Name: Verify whether the Notes API GET request response for  #{dealnumber} and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @notes = Notes.new(@reporter)
  @sqlquery=@dbquery.getNotesByDealNumberQuery(dealnumber).to_s
   puts @sqlquery
  @dbresult = @notes.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s

  @endpointc = Endpoints.new
  @endpoint = @endpointc.getNotesByDealNumber(dealnumber).to_s
  @responseval = @notes.SubmitGETRequest(@endpoint)

  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s

  puts "Response body : "+@responsebody.to_s

  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)
end

Then(/^The Notes GET  request response for (.*) deal and the schema provided is matched successfully$/) do |deal|
  print "Step Name: Verifying whether the GET Notes request response and the schema provided are matched successfully.\n"
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["results"]


  @Schema = {
      "type" => "array",
      "required" => ["notenum","dealNumber","notedate","notetype","notes","whoentered"],
      "properties" => {
          "notenum" => {"type" => "Integer"},
          "dealNumber" => {"type" => "Integer"},
          "notedate" => {"type" => "Date"},
          "notetype" => {"type" => "Integer"},
          "notes" => {"type" => "String"},
          "whoentered"=> {"type" => "Integer"},
          "name" => {"type" => "String"},
          "firstName" => {"type" => "String"},
          "middleName" => {"type" => "String"},
          "lastName" => {"type" => "String"}
      }
  }
  print @Schema
  @notes.ValidateAPIwithSchema(@Schema,@responseresvalue)
end


#POST NOTES API
Given(/^that I am submitting a Notes API POST request for creating a note with (.*) (.*) (.*) (.*)$/) do |notedate, notetype, notes, dealnumber|
  @endpointc = Endpoints.new
  @endpoint = @endpointc.postNote.to_s
  @notes = Notes.new(@reporter)
  @postJson="{ \"notedate\": \""+notedate+"\", \"notetype\": "+notetype+",  \"notes\": \""+notes+"\", \"dealNumber\":"+dealnumber+" }"
  @responseval=@notes.SubmitPOSTRequest(@endpoint,@postJson)
  @responseval1=@responseval
  puts "Response= "+@responseval.to_s
end


Then(/^I receive a response with status as successful for the POST Notes API$/) do
  puts "Step Name: Verify whether we get successful response code for Notes API POST request. \n"
  @responsecode = @notes.VerifyResponseStatusCode(@responseval).to_s
  puts "ResponseCode= " + @responsecode
  @notes.CheckResponseforPOST(@responsecode)
end

=begin
And(/^I clean up by deleting the created note for dealnumber (.*)$/) do |dealnumber|
  @notes_response_hash = JSON.parse(@responseval1)
  puts "NotesresponseHash"+ @notes_response_hash.to_s
  @h=@notes_response_hash.fetch("data")
  puts "@h= " + @h.to_s
  @notenum =  @h["notenum"]
  puts "Notenum= " + @notenum.to_s
  @notesDescription=@h["notes"]
  puts "Notes=" + @notesDescription
@dealnumber=dealnumber
  @dbquery = DBQueries.new
  @notes = Notes.new(@reporter)
  @sqlquery=@dbquery.deleteNote(@dealnumber,@notenum).to_s
  puts @sqlquery
  @dbresult = @notes.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s
end
=end

And(/^I verify if the note is created successfully in the database for dealnumber (.*)$/) do |dealnumber|
  @notes_response_hash = JSON.parse(@responseval1)
  puts "NotesresponseHash"+ @notes_response_hash.to_s
  @h=@notes_response_hash.fetch("data")
  puts "@h= " + @h.to_s
  @notenum =  @h["notenum"]
  puts "Notenum= " + @notenum.to_s
  @notesDescription=@h["notes"]
  puts "Notes=" + @notesDescription
  @dealnumber=dealnumber
  @dbquery = DBQueries.new
  @notes = Notes.new(@reporter)
  @sqlquery=@dbquery.searchNoteByNoteNumber(@dealnumber,@notenum).to_s
  puts @sqlquery
   @dbresult = @notes.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s

  @parsedDBvalue = JSON.parse(@dbresult.to_json)
  @dbnotes = @dbresult[0]["notes"]
  @notes.compareExpectedAndActualResult(@notesDescription,@dbnotes)

end


#PUT NOTES API
And(/^I submit a PUT request to update notes with (.*)$/) do |updatedNotes|
  @notes_response_hash = JSON.parse(@responseval1)
  puts "NotesresponseHashforput"+ @notes_response_hash.to_s
  @h=@notes_response_hash.fetch("data")
  puts "@h= " + @h.to_s
  @notenum =  @h["notenum"]
  @notetype= @h["notetype"]
  @updatedNotes=updatedNotes

  @endpointc = Endpoints.new
  @endpoint = @endpointc.putNotes.to_s
  @notes = Notes.new(@reporter)

 @putJson= "{ \"notenum\": \""+@notenum.to_s+"\", \"notetype\": "+@notetype.to_s+",  \"notes\": \""+@updatedNotes+"\" }"
  puts "putjson= " + @putJson.to_s
  @responsefromput=@notes.SubmitPUTRequest(@endpoint,@putJson)
  puts "Response= " + @responsefromput.to_s
end

Then(/^I verify if I get successful response for PUT notes API request$/) do
  puts "Step Name: Verify whether we get successful response code for Notes API PUT request. \n"
  @responseCodeFromPut = @notes.VerifyResponseStatusCode(@responsefromput).to_s
  puts "@responseCodeFromPut= " + @responseCodeFromPut.to_s
  @notes.CheckSuccessfulResponse(@responseCodeFromPut)
end

And(/^I verify if the note is updated successfull in the database for dealnumber (.*)$/) do |dealnumber|
  @notes_put_response_hash = JSON.parse(@responsefromput)
  @h=@notes_put_response_hash.fetch("data")
  @notenum =  @h["notenum"]
  @notesDescription=@h["notes"]

  @dealnumber=dealnumber
  @dbquery = DBQueries.new
  @notes = Notes.new(@reporter)
  @sqlquery=@dbquery.searchNoteByNoteNumber(@dealnumber,@notenum).to_s

  @dbresult = @notes.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s

  @parsedDBvalue = JSON.parse(@dbresult.to_json)
   @dbnotes = @dbresult[0]["notes"]
  @notes.compareExpectedAndActualResult(@notesDescription,@dbnotes)
  end

And(/^I verify if note description received in response is (.*) for dealnumber (.*)$/) do |updatedNotes, dealnumber|
  @dealnumber=dealnumber
  @updatedNotes=updatedNotes

  @notes_put_response_hash = JSON.parse(@responsefromput)
  @h=@notes_put_response_hash.fetch("data")
  @notenum =  @h["notenum"]
  @notesDescription=@h["notes"]
  @notes.compareExpectedAndActualResult(@notesDescription,@updatedNotes)

  @notes.compareExpectedAndActualResult(@expectedStatus,@actualStatus)
end



Then(/^I verify the response code as (.*) for  (.*)$/) do |expectedResponseCode, dealnumber|
  puts "Step Name: Verify whether we get successful response code for Notes API GET request. \n"
  @responsecode = @notes.VerifyResponseStatusCode(@responseval).to_s
  puts "ResponseCode= " + @responsecode
  @notes.compareExpectedAndActualResult(expectedResponseCode,@responsecode)
end


Given(/^that I am submitting a NoteType API GET request for fetching all the notetypes for (.*)$/) do |assumedCededAllFlag|
  puts "Step Name: Fetching notetypes"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getNoteTypes(assumedCededAllFlag).to_s
  puts @endpoint
  @notes = Notes.new(@reporter)
  @responseval = @notes.SubmitGETRequest(@endpoint)
  puts "Response: " + @responseval
end


Then(/^I verify if I get  response code as (.*)$/) do |expectedResponseCode|
  puts "Step Name: Verify whether we get successful response code for NoteTypes API GET request. \n"
  @responsecode = @notes.VerifyResponseStatusCode(@responseval).to_s
  puts "ResponseCode= " + @responsecode
  @notes.compareExpectedAndActualResult(@responsecode,expectedResponseCode)
end

Then(/^Verify if the NoteTypes GET request response  and the db query result is matched successfully$/) do

end

Then(/^Verify if the NoteTypes GET request response  for (.*) assumedCededAllFlag and the db query result is matched successfully$/) do |assumedCededAllFlag|
  puts "Step Name: Verify whether the NoteType API GET request response and the db query result is matched successfully.\n"
  @dbquery = DBQueries.new
  @notes = Notes.new(@reporter)
  @sqlquery=@dbquery.getNoteTypesQuery(assumedCededAllFlag).to_s
  puts @sqlquery
  @dbresult = @notes.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s

  @endpointc = Endpoints.new
  @endpoint = @endpointc.getNoteTypes(assumedCededAllFlag).to_s
  @responseval = @notes.SubmitGETRequest(@endpoint)

  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s

  puts "Response body : "+@responsebody.to_s

  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)
end

Then(/^The NoteTypes GET  request response for (.*) and the schema provided is matched successfully$/) do |assumedCededAllFlag|
  print "Step Name: Verifying whether the GET NoteTypes request response and the schema provided are matched successfully.\n"
  @parsedrespvalue = JSON.parse(@responseval)
  @responseresvalue = @parsedrespvalue["results"]


  @Schema = {
      "type" => "array",
      "required" => ["code","notetype","active","assumedFlag","assumedName","cededFlag","cededName"],
      "properties" => {
          "code"=> {"type" => "Integer"},
          "notetype"=> {"type" => "String"},
          "active"=> {"type" => "Boolean"},
          "assumedFlag"=> {"type" => "Integer"},
          "assumedName"=> {"type" => "String"},
          "cededFlag"=> {"type" => "Integer"},
          "cededName"=> {"type" => "String"}
      }
  }
  print @Schema
  @notes.ValidateAPIwithSchema(@Schema,@responseresvalue)
end

And(/^I submit a GET request for fetching note for notenumber$/) do
  @notes_response_hash = JSON.parse(@responseval1)
  puts "NotesresponseHashforput"+ @notes_response_hash.to_s
  @h=@notes_response_hash.fetch("data")
  puts "@h= " + @h.to_s
  @notenum =  @h["notenum"]
  puts "Note number= " + @notenum.to_s

  @endpointc = Endpoints.new
  @endpoint = @endpointc.getNotesByNoteNumber(@notenum).to_s
  puts @endpoint
  @notes = Notes.new(@reporter)
  @responseNew = @notes.SubmitGETRequest(@endpoint)
  puts "Response: " + @responseNew
end

And(/^I verify if I get response code as (.*)$/) do |expectedResponseCode|
  puts "Step Name: Verify whether we get successful response code\n"
  @responsecode = @notes.VerifyResponseStatusCode(@responseNew).to_s
  puts "ResponseCode= " + @responsecode
  @notes.compareExpectedAndActualResult(@responsecode,expectedResponseCode)
end


And(/^I verify if  the API response  and the db query result for the dealnumber (.*) is matched successfully$/) do |dealnumber|
  @notes_response_hash = JSON.parse(@responseval1)
  @h=@notes_response_hash.fetch("data")
  puts "@h= " + @h.to_s
  @notenum =  @h["notenum"]
  puts "Note number= " + @notenum.to_s

  @dealnumber=dealnumber
  @dbquery = DBQueries.new
  @notes = Notes.new(@reporter)
  @sqlquery=@dbquery.searchNoteByNoteNumber(@dealnumber,@notenum).to_s

  @dbresult = @notes.sendQuery(@sqlquery)
  puts "DB Query Result : " +@dbresult.to_s

  @parsedDBvalue = JSON.parse(@dbresult.to_json)
  # @dbnotes = @dbresult[0]["notes"]
  # @notes.compareExpectedAndActualResult(@notesDescription,@dbnotes)
  #
  #  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s

  puts "Response body : "+@h.to_s
  @modifiedResponse="["+@h.to_s+"]"

  BaseContainer.CompareAPIandDBResponse(@modifiedResponse,@dbresult)
end


And(/^I verify if the response matches with the schema provided$/) do
  print "Step Name: Verifying whether the GET NoteTypes request response and the schema provided are matched successfully.\n"
  @parsedrespvalue = JSON.parse(@responseNew)
  @responseresvalue = @parsedrespvalue["results"]


  @Schema = {
      "type" => "array",
      "required" => ["notenum","dealNumber","notedate","notetype","notes","whoentered"],
      "properties" => {
          "notenum" => {"type" => "Integer"},
          "dealNumber" => {"type" => "Integer"},
          "notedate" => {"type" => "Date"},
          "notetype" => {"type" => "Integer"},
          "notes" => {"type" => "String"},
          "whoentered"=> {"type" => "Integer"},
          "name" => {"type" => "String"},
          "firstName" => {"type" => "String"},
          "middleName" => {"type" => "String"},
          "lastName" => {"type" => "String"}
      }
  }
  print @Schema
  print @responseresvalue1
   @notes.ValidateAPIwithSchema(@Schema,@responseresvalue)
end