Given(/^that I am submitting a Writingcompanies  GET request for fetching all the WritingCompanies for the (.*) flag$/) do |flag|
  print"Step name: Fetching all the valid writing companies"
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getWritingCompanies(flag).to_s
  puts "endpoint" + @endpoint.to_s
  @writingcompanies = WritingCompanies.new(@reporter)
  @responseval = @writingcompanies.SubmitGETRequest(@endpoint)
  puts "@responseval" + @responseval.to_s

end


Then(/^I receive a response with status as successful for theWriting Companies API request for (.*) flag$/) do |flag|

  @responsecode = @writingcompanies.VerifyResponseStatusCode(@responseval).to_s
  @writingcompanies.CheckSuccessfulResponse(@responsecode)
  puts "@responsecode" + @responsecode.to_s

end

Then(/^the Writingcompanies  GET request response for (.*) flag and the db query result is matched successfully$/) do |flag|
  @dbquery = DBQueries.new
  @writingcompanies = WritingCompanies.new(@reporter)
  @sqlquery=@dbquery.getWritingCompaniesQuery(flag).to_s
  # print "\n"
  # puts @sqlquery
  @dbresult = @writingcompanies.sendQuery(@sqlquery)
  #@dbresult = @dbresult.to_s.gsub(/true/,"\"assumed\"")
  puts "DB Query : " +@dbresult.to_s
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getWritingCompanies(flag).to_s
  @responseval = @writingcompanies.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s
  # print "\n"
  puts "Response body : "+@responsebody.to_s


  BaseContainer.CompareAPIandDBResponse(@responsebody,@dbresult)
end

Then(/^the Writingcompanies  get request response for (.*) flag and the schema provided is matched successfully$/) do |flag|
  @endpointc = Endpoints.new
  @endpoint = @endpointc.getWritingCompanies(flag).to_s
  @writingcompanies = WritingCompanies.new(@reporter)
  @responseval = @writingcompanies.SubmitGETRequest(@endpoint)
  @parsedrespvalue = JSON.parse(@responseval)
  @responsebody = BaseContainer.fetchresponsebody(@responseval).to_s
  @responseresvalue = @parsedrespvalue["results"]
  # @responsedatavalue = @responseresvalue.first['data']

  @Schema = {
      "type" => "array",
      "required" => ["papernum","companyname","cpnum","relatedcompany","address1","address2","address3","city","state","postalcode","country","phone","fax","imagefilename","companyshortname","sltrequired","iptrequired","policynumbertoken","currency","territory","active","hideunusedclaimcategory","jecode","isgrsdisplay","flag"],
      "properties" => {
          "papernum" => {"type" => "Integer"},
          "companyname" => {"type" => "String"},
          "cpnum" => {"type" => "Integer"},
          "relatedcompany" => {"type" => "String"},
          "address1" => {"type" => "String"},
          "address2" => {"type" => "String"},
          "address3" => {"type" => "String"},
          "city" => {"type" => "String"},
          "state" => {"type" => "String"},
          "postalcode" => {"type" => "String"},
          "country" => {"type" => "String"},
          "phone" => {"type" => "String"},
          "fax" => {"type" => "String"},
          "imagefilename" => {"type" => "String"},
          "companyshortname" => {"type" => "String"},
          "sltrequired" => {"type" => "Integer"},
          "iptrequired" => {"type" => "Integer"},
          "policynumbertoken" => {"type" => "String"},
          "currency" => {"type" => "Integer"},
          "territory" => {"type" => "Integer"},
          "active" => {"type" => "Integer"},
          "hideunusedclaimcategory" => {"type" => "Boolean"},
          "jecode" => {"type" => "String"},
          "isgrsdisplay" => {"type" => "Integer"},
          "flag" => {"type" => "boolean"},

      }
  }
  #print @Schema
  @writingcompanies.ValidateAPIwithSchema(@Schema,@responseresvalue)

  # Given(/^that I am submitting a Writingcompanies  GET request for fetching all the WritingCompanies for the (.*) flag$/) do |flag|
  #   print"Step name: Fetching all the valid writing companies"
  #   @endpointc = Endpoints.new
  #   @endpoint = @endpointc.getWritingCompanies(flag).to_s
  #   puts "endpoint" + @endpoint.to_s
  #   @writingcompanies = WritingCompanies.new(@reporter)
  #   @responseval = @writingcompanies.SubmitGETRequest(@endpoint)
  #   puts "@responseval" + @responseval.to_s
  #
  # end
  #
  # Then(/^I receive a response with status as successful for theWriting Companies API request for (.*) flag$/) do |flag|
  #
  #   @responsecode = @writingcompanies.VerifyResponseStatusCode(@responseval).to_s
  #   @writingcompanies.CheckSuccessfulResponse(@responsecode)
  #   puts "@responsecode" + @responsecode.to_s
  #
  # end



end






