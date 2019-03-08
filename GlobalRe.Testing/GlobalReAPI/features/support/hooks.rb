require 'rubygems'
require 'watir'
require 'rubysl-win32ole'
require 'rubysl/rexml'
require 'date'
require 'jsonpath'
require 'json'   
require 'net/http'
require 'minitest'
require 'test'
require 'rest-client'
require 'cucumber'
require 'ansi'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","endpoints.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"ActionModules","db_verification_actions.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"ActionModules","deal_summary_actions.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"ActionModules","subdivisions_actions.rb")
require 'json-schema'
require 'console_color'
require 'report_builder'
require 'jsonpath'
require 'json'
require 'net/http'
include REXML


Before do |scenario|
  # capture the time of the scenario run
  # get feature name based on call
  print "Scenario starting - " + scenario.name.to_s +  ".\n"
  print  "************************************************************************************************************************************************************************************\n"
  #$MyMessage = self
  @tTime = Time.now
  @FeatureName=scenario.feature.name
  # get scenrion name based on run time
  @ScenarioName = scenario.name
  require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
  BaseContainer.fetchenv
  require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","grsapisitefactory.rb")
  @tTime = Time.now
   @reporter = Watir::HtmlReporter.new(true)
  # @reporter.StartIndentation()
  # @reporter.ReportGeneral("Test Execution Started for #{@FeatureName}/#{@ScenarioName} at #{@tTime.strftime("%Y/%m/%d%H:%M:%S")}")
  # @reporter.EndIndentation()
  @dealcountbystatusactions = DealCountByStatus_actions.new(@reporter)
  @DealSummaryActions = DealSummaryActions.new(@reporter)
  @SubDivisionsActions = SubDivsionsActions.new(@reporter)
  @Basecontainerobj = BaseContainer.new(@reporter)
  $reporterobj = @reporter
  $scenario = scenario
  $putmessage = self
end
After do
  @tTime = Time.now
  # @reporter.StartIndentation
  # @reporter.ReportGeneral("Test Execution Ended at #{@tTime.strftime("%Y/%m/%d%H:%M:%S")}")
  # @reporter.EndIndentation
  # @TestResultsFile="C:\\GlobalReAPI\\features\\TestResults\\GRS_API_TestResults-"+@tTime.strftime("%H%M%S").to_s+".html"
  # @reporter.Save(@TestResultsFile)
  print "Scenario Execution Completed.\n"
  print  "************************************************************************************************************************************************************************************\n"
  print  "************************************************************************************************************************************************************************************\n"
end
at_exit do
  print "Generating Test Results.\n"
  print  "************************************************************************************************************************************************************************************\n"
  print  "************************************************************************************************************************************************************************************\n"
  @JSON_FILE = File.join(File.absolute_path('../', File.dirname(__FILE__)),"Cucumber_Results","results.json")
  @tTime = Time.now
  @TestResultsSummaryFile=".\\features\\Detailed_Test_Results"+"\\GRS_API_Test_Results_Summary-"+@tTime.strftime("%H%M%S").to_s
  #######@TestResultsSummaryFile=".\\features\\Detailed_Test_Results"+"\\GRS_API_Test_Results_Summary".to_s # boya Added to override the same report
  ReportBuilder.configure do |config|
    config.json_path = @JSON_FILE
    config.report_path = @TestResultsSummaryFile#'./features/GRS_POC/Detailed_Test_Results/GRS_Test_Results_Summary'
    config.report_types = [:html]
    config.report_tabs = [:overview, :features, :scenarios, :errors]
    config.report_title = 'GRS API Test Results'
    config.compress_images = false
    #config.additional_info = {Browser: 'Firefox', Environment: 'Dev'}
  end
  #BrowserContainer.get_run_stats_standard
  ReportBuilder.build_report
end
