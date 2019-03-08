require 'rest-client'
require 'json'
require 'hashie'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
class DealCountByStatus_actions < BaseContainer

  def initialize(reporter)
    @@reporter = reporter
  end

  def GenerateURI(endpointValue)
    BaseContainer.fetchenv
    @@URL = $APP_URL
    @uri = File.join(@@URL, endpointValue)
    return @uri
  end
  def SubmitGETRequest(endpointValue)
    GenerateURI(endpointValue)
    @usernamevalue="sjanardanannair"
    if @userType == "All Access"
      @usernamevalue="sjanardanannair"
    elsif @userType == "UW"
      @usernamevalue="Mike.McCarthy"
    elsif @userType == "NPTA"
      @usernamevalue="rcorbin"
    elsif @userType == "PTA"
      @usernamevalue="Elena.Marshall"
    elsif @userType == "Actuary"
      @usernamevalue="lslader"
    elsif @userType == "Actuary Manager"
      @usernamevalue="Todd.Glassman"
    elsif @userType == "UW Manager"
      @usernamevalue="Andrew.Barnard"
    end
    @Auth = BaseContainer.generateUserToken(@usernamevalue)
    @resource = RestClient::Resource.new(@uri)
    @response = @resource.get(:content_type => :json, :Authorization => @Auth)
    return @response
  end
  def SubmitUserGETRequest(endpointValue)
    GenerateURI(endpointValue)
    @usernamevalue="sjanardanannair"
    @Auth = BaseContainer.generateUserToken(@usernamevalue)
    @resource = RestClient::Resource.new(@uri)
    @response = @resource.get(:content_type => :json, :Authorization => @Auth)
    return @response
  end
  def VerifyResponseStatusCode(response)
    @response=response
    @ResponseStatusCode = @response.code
    return @ResponseStatusCode
  end
  def CheckServiceProvisioned(responsecodeval)
    @responsecode = responsecodeval
    if @responsecode == "200"
      print "PASSED - The GRS POC environment has the service name provisioned.\n"
      #@@reporter.ReportAction("PASSED - The GRS POC environment has the service name provisioned.\n")
    else
      print "The GRS POC environment does not have the service name provisioned.\n"
      fail "The GRS POC environment does not have the service name provisioned.\n"
      #@@reporter.ReportAction("The GRS POC environment does not have the service name provisioned.\n")
    end
  end
  def CheckSuccessfulResponse(responsecodeval)
    @responsecode = responsecodeval
    if @responsecode == "200"
      print "PASSED - Success response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("PASSED - Success response " + @responsecode + " is received. \n")
      #$MyMessage.puts "PASSED - Success response " + @responsecode + " is received. \n"
    else
      print "FAILED - Response " + @responsecode + " is received. \n"
      fail "FAILED - Response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("FAILED - Response " + @responsecode + " is received. \n")
    end
    #BaseContainer.ExecuteQuery
  end
  def CheckErrorResponse(responsecodeval)
    @responsecode = responsecodeval
    if @responsecode == 422
      print "PASSED - Error response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("PASSED - Error response " + @responsecode + " is received. \n")
    else
      print "FAILED - Response " + @responsecode + " is received. \n"
      fail "FAILED - Response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("FAILED - Response " + @responsecode + " is received. \n")
    end
    #BaseContainer.ExecuteQuery
  end



  def CheckResponseReceived(responsecodeval)
    @responsecode = responsecodeval
    if @responsecode == "200"
      print "PASSED - A success response is received. \n"
      #@@reporter.ReportAction("PASSED - A success response is received. \n")
    else
      print "FAILED - A Failure Response " + @responsecode + " is received. \n"
      fail "FAILED - A Failure Response " + @responsecode + " is received. \n"
      #@@reporter.ReportAction("FAILED - A Failure Response " + @responsecode + " is received. \n")
    end
    #BaseContainer.ExecuteQuery
  end
  def compareAPIandDBresult(apiresponse,dbresult)
    @queryresults=dbresult
    @apiresult = apiresponse
    BaseContainer.CompareAPIAndDBResults(@apiresult,@queryresults)
  end
  def sendQuery(sqlquerystring)
    @sqlquery=sqlquerystring
    @resultdata = BaseContainer.ExecuteQuery(@sqlquery)
    if @resultdata.length.to_s != "0"
      print "PASSED - Query Executed Successfully and fetched " + @resultdata.length.to_s + " records.\n"
      #@@reporter.ReportAction("PASSED - Query Executed Successfully and fetched " + @resultdata.length.to_s + " records.\n")
    else
      print "FAILED - Query fetched no results.\n"
      fail "FAILED - Query fetched no results.\n"
      #@@reporter.ReportAction("FAILED - Query fetched no results.\n")
    end
  end
  def ValidateAPIwithSchema(schema,response)
    BaseContainer.ValidateSchema(schema,response)
  end
end