require 'report_builder'
require 'tiny_tds'
require 'json'
require 'json-schema'
require 'hashie'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
class BrowserContainer
  def initialize(browser, reporter)#, webassertion)
    @browser = browser
    @reporter = reporter
    #@webassertion = webassertion
  end
  def self.fetchenv
    # set all suite folder paths as dynamic path relative to hooks
    #$BaseDir="../../..GlobalRe/features/"
    #puts File.absolute_path($BaseDir)
    #$SupportDir=$BaseDir+"/GRS_POC/support/"
    $configFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.xml")
    @configdoc = Config.new()
    $Environment=@configdoc.getRunEnvironment
    if $Environment.downcase == "qa"
      $ApplicationURL = @configdoc.getUrl
      $GRSApplicationURL = @configdoc.getQAUrl
      $GRSUWApplicationURL = @configdoc.getUWQAUrl
      $GRSNPTAApplicationURL = @configdoc.getNPTAQAUrl
      $GRSPTAApplicationURL = @configdoc.getPTAQAUrl
      $GRSActuaryApplicationURL = @configdoc.getActuaryQAUrl
      $GRSActuaryManagerApplicationURL = @configdoc.getActuaryManagerQAUrl
      $GRSUWManagerApplicationURL = @configdoc.getUnderwriterManagerQAUrl
      $GRSReadonlyUserApplicationURL = @configdoc.getReadonlyAccessUserQAUrl
    else
      $GRSApplicationURL = @configdoc.getDEVUrl
      $GRSUWApplicationURL = @configdoc.getUWDEVUrl
      $GRSNPTAApplicationURL = @configdoc.getNPTADEVUrl
      $GRSPTAApplicationURL = @configdoc.getPTADEVUrl
      $GRSActuaryApplicationURL = @configdoc.getActuaryDEVUrl
      $GRSActuaryManagerApplicationURL = @configdoc.getActuaryManagerDEVUrl
      $GRSUWManagerApplicationURL = @configdoc.getUnderwriterManagerDEVUrl
      $GRSReadonlyUserApplicationURL = @configdoc.getReadonlyAccessUserDevUrl

    end

    $ReleaseNumber = @configdoc.getReleaseNumber
    $Environment=@configdoc.getRunEnvironment
    $CommonDir = @configdoc.getCommonDir
    $UtilDir=@configdoc.getUtilityDir
    $PageClassesDir = @configdoc.getPageClassesDir
    #$ActionClassesDir = @configdoc.getActionClassesDir
    $BaseClassesDir = @configdoc.getBaseClassesDir
    $ResultDirPath = @configdoc.getResultsDir
    $TestDirPath = @configdoc.getTestDataDir
    $ConfigDirPath = @configdoc.getConfigDir
    $DriversDirPath = @configdoc.getDriversDir
    $AssertionsDirPath = @configdoc.getAssertionsDir
    $StepDefinitionDirPath = @configdoc.getStepDefinitionsDir
    @userid = ENV['USERNAME']
    $FirefoxExportFolder = "'C:\\Users\\" + @userid + "\\Downloads"
  end
# no method but all common operatios that can be done on all pages part of the master css sheets will go in this class
  def teardown(browser)
    @browser = browser
    @browser.quit
  end
  def self.get_run_stats_standard()
    stats = { passed: 0, failed: 0, skipped: 0, undefined: 0 }
    #@JSON_FILE = File.join(File.absolute_path('../', File.dirname(__FILE__)),"Cucumber_JSON","results.json")
    @Resultsfile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"Cucumber_Results","features_report.html")
    file = File.read(@Resultsfile)
    scenarios_line = file.lines.last.split('innerHTML = "').last.split('<br />').first
    stats.keys.each do |state|
      if scenarios_line.include? state.to_s
        stats[state] = scenarios_line[/(\d+) #{state.to_s}/, 1].to_i
      end
    end
    #ReportBuilder.build_report
  end
  def get_run_stats_parallel(report)
    stats = { passed: 0, failed: 0, skipped: 0, undefined: 0 }
    stats.keys.each do |state|
      unless report[2].select { |status| status[:name] == state.to_s }.empty?
        stats[state] = report[2].select { |status| status[:name] == state.to_s }.first[:count]
      end
    end
  end
  def self.ExecuteQuery(querystring)
    #####dont delete###########
    #@password="ZEB0QGgwZwo="
    #@decoded_password = XMLRPC::Base64.decode(@password)
    #@@dbconnector = SqlServerConnector.new('ERMSDB_DEV', 'sjanardanannair', @decoded_password, pDBname)
    #@client = TinyTds::Client.new username: 'maxbi', password: 'd@t@h0g', host: 'VA1-TGMRSQL072.markelcorp.markelna.com', database: 'ErmsUATHotFix'
    #@client = TinyTds::Client.new username: 'sjanardanannair', password: @decoded_password, host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    #@client = TinyTds::Client.new username: 'sjanardanannair', host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    ######################3####
    @@datahash = Array.new
    @sqlquerystring = querystring
    #@client = TinyTds::Client.new username: 'maxbi', password: 'd@t@h0g', host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    @client = TinyTds::Client.new(:username => 'maxbi', :password => 'd@t@h0g', :dataserver => 'va1-dgmrsql053.markelcorp.markelna.com', :database => 'DEV_ERMS', :timeout => 0)
    @result = @client.execute(@sqlquerystring)
    @rowcount = 0
    @result.each_with_index do |row|
      @@datahash[@rowcount]=row
      @rowcount = @rowcount+1
    end
    @client.close
    #print "\n"
    #print @@datahash
    #print "\n"
    return @@datahash

    ###dont delete######
    ###@dbobj = SQLQuery.new('DEV_ERMS')
    ###puts "successfully connected"
    ###query = "select * from GRS.v_Deals;"
    ###recordset = WIN32OLE.new('ADODB.Recordset')
    ###a = @dbobj.query(query, 'select', nil, recordset)
    ###puts a.GetString(2)
    ####################
  end
  def self.ExecuteCountQuery(querystring)
    #####dont delete###########
    #@password="ZEB0QGgwZwo="
    #@decoded_password = XMLRPC::Base64.decode(@password)
    #@@dbconnector = SqlServerConnector.new('ERMSDB_DEV', 'sjanardanannair', @decoded_password, pDBname)
    #@client = TinyTds::Client.new username: 'maxbi', password: 'd@t@h0g', host: 'VA1-TGMRSQL072.markelcorp.markelna.com', database: 'ErmsUATHotFix'
    #@client = TinyTds::Client.new username: 'sjanardanannair', password: @decoded_password, host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    #@client = TinyTds::Client.new username: 'sjanardanannair', host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    ######################3####
    #@@datahash = Array.new
    @sqlquerystring = querystring
    #@client = TinyTds::Client.new username: 'maxbi', password: 'd@t@h0g', host: 'va1-dgmrsql053.markelcorp.markelna.com', database: 'DEV_ERMS'
    @client = TinyTds::Client.new(:username => 'maxbi', :password => 'd@t@h0g', :dataserver => 'va1-dgmrsql053.markelcorp.markelna.com', :database => 'DEV_ERMS', :timeout => 0)
    #print "\n"
    #print @sqlquerystring
    #print "\n"
    @result = @client.execute(@sqlquerystring)
    # print "\n"
    # print @result
    # print "\n"
    #@result_val =

    @result_val = @result.each
    #@result_val = @result_val.delete("")
    #@result_val = @result.each.split("=>")
    @final_res_val = @result_val.to_s.gsub("[","")
    @final_res_val = @final_res_val.to_s.gsub("]","")
    @final_res_val = @final_res_val.to_s.gsub("{\"totalRecords\"=>","")
    @final_res_val = @final_res_val.to_s.gsub("\"","")
    @final_res_val = @final_res_val.to_s.gsub(",","")
    @final_res_val = @final_res_val.to_s.gsub("}","")
    @final_res_val = @final_res_val.to_s.gsub(" ","")
    # print "\n"
    # print @final_res_val
    # print "\n"

    #@parsedresponse = JSON.parse(@result_val.to_json)
    #@apihash = Hashie::Mash.new
#    print @parsedresponse[0]["totalRecords"]
#     if @parsedresponse.nil? || @parsedresponse.any? == false
#       print "API response returned no count.\n"
#     else
    #@parsedresponse = @final_res_val
      #@resultcount = @parsedresponse[0]["totalRecords"]
#    end
    @resultcount = @final_res_val

    # print "\n"
    # print @resultcount.to_s
    # print "\n"
    return @resultcount.to_s

    #@rowcount = 0
    #@result.each_with_index do |row|
    #  @@datahash[@rowcount]=row
    #  @rowcount = @rowcount+1
    #end
    @client.close
    #return @apihash.to_s
    ###dont delete######
    ###@dbobj = SQLQuery.new('DEV_ERMS')
    ###puts "successfully connected"
    ###query = "select * from GRS.v_Deals;"
    ###recordset = WIN32OLE.new('ADODB.Recordset')
    ###a = @dbobj.query(query, 'select', nil, recordset)
    ###puts a.GetString(2)
    ####################

  end
  def self.CompareResults(actualresults,expectedresults)
    @actualres = actualresults
    @expectedres = expectedresults
    if (@actualres - @expectedres).empty? == true
      print "PASSED - The Expected Results and DB Query results are matched successfully.\n"
      #@@reporter.ReportAction("PASSED - The Expected Results and DB Query results are matched successfully.\n")
    else
      print "FAILED - The Expected results failed to match with DB Query results.\n"
      fail "FAILED - The Expected results failed to match with DB Query results.\n"
      #@@reporter.ReportAction("FAILED - The Expected results failed to match with DB Query results.\n")
    end
  end
  def self.CompareActualtoExpected(actual,expected)
    @actualvalue = actual
    @expectedvalue = expected
    if @actualvalue.to_s == @expectedvalue.to_s
      print "PASSED - Actual Result and Expected result matched successfully.\n"
    else
      print "FAILED - Actual Result and Expected result failed to match.\n"
      fail "FAILED - Actual Result and Expected result failed to match.\n"
    end
  end
  def self.CompareUserSpecificActualtoExpected(user,actual,expected)
    @actualvalue = actual
    @expectedvalue = expected
    if @actualvalue.to_s == @expectedvalue.to_s && user != "Read Only Access"
      print "PASSED - Actual Result and Expected result matched successfully.\n"
    elsif @actualvalue.to_s != @expectedvalue.to_s && user == "Read Only Access"
      print "PASSED - Actual Result and Expected result failed to match as the data was not submitted as it is a #{user} user.\n"
    else
      print "FAILED - Actual Result and Expected result failed to match.\n"
      fail "FAILED - Actual Result and Expected result failed to match.\n"
    end
  end

  def self.CompareArrayResults(actualresults,expectedresults)
    @actualres = actualresults
    @expectedres = expectedresults
    if (@actualres - @expectedres).empty? == true
      print "PASSED - The array results are matched successfully.\n"
      #@@reporter.ReportAction("PASSED - The Expected Results and DB Query results are matched successfully.\n")
    else
      print "FAILED - The array results failed to match.\n"
      fail "FAILED - The array results failed to match.\n"
      #@@reporter.ReportAction("FAILED - The Expected results failed to match with DB Query results.\n")
    end
  end

  def self.WaitUntilElementLoad(element)
    @element = element
    @i = 0
    @num = 10
    until @i > @num do
      begin
        if @element.text == "Loading..."
          sleep 2
          @i = @i + 1
          puts @i
        elsif @i == 10
          print "FAILED - The Loading... element is still appears .\n"
          fail "FAILED - The Loading... element is still appears.\n"
        end
      rescue Watir::Exception::UnknownObjectException, Timeout::Error
        sleep 2
        print "Passed - The Loading... element is disappeared appears .\n"
        # fail "FAILED - The Loading... element is still appears.\n"
        break
      end
    end

  end

end