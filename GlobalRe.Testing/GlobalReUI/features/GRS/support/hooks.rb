require 'rubygems'
require 'watir'
require 'pathname'
require 'win32ole'
require 'date'
require 'nokogiri'
require 'cucumber'
require 'console_color'
#require 'angular-table'
require 'watir_angular'
require 'watir-ng'
require 'base64'
#require 'watir/HtmlReporter'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","browser_container.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","login_page.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","home_page.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require 'cucumber'
require 'watir'
require 'selenium-webdriver'
require 'report_builder'
require 'rautomation'
require 'tiny_tds'
require 'watir-scroll'
require 'fileutils'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","browser_container.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
driver_directory = File.join(File.absolute_path('../', File.dirname(__FILE__)),"Drivers")
ENV['PATH'] = "#{ENV['PATH']}#{File::PATH_SEPARATOR}#{driver_directory}"
# ENV['BROWSER']
# Before ('@firefox') do |scenario|
#   print "Scenario starting - " + scenario.name.to_s +  ".\n"
#   print  "************************************************************************************************************************************************************************************\n"
#   WatirNg.patch!
#   @driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","geckodriver.exe")
#   Selenium::WebDriver::Firefox.driver_path = @driver_path
#   driver = Selenium::WebDriver.for(:firefox, driver_path: @driver_path)#, desired_capabilities: caps)
#   @browser = Watir::Browser.new(driver)
#   @browser.driver.manage.window.maximize
#   #@wait = Watir::Wait
#   @bdriver = @browser
#   $FeatureName=scenario.feature.name
#   # get scenrion name based on run time
#   $ScenarioName = scenario.name
#   @tTime = Time.now
#   @reporter = Watir::HtmlReporter.new(true)
#   #@reporter.StartIndentation()
#   #@reporter.ReportGeneral("Test Execution Started for #{@FeatureName}/#{@ScenarioName} at #{@tTime.strftime("%Y/%m/%d%H:%M:%S")}")
#   #@reporter.EndIndentation()
#   require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","grs_site_factory.rb")
#   @grssitefactoryobj = GRSSiteFactory.new()
#   @bcontainer = BrowserContainer.new(@browser,@reporter)
#   @loginpg = @grssitefactoryobj.login_webpage(@browser,@reporter)
#   @homepg = @grssitefactoryobj.home_webpage(@browser,@reporter)
#   @grshomepg = @grssitefactoryobj.grs_home_page(@browser,@reporter)
#   #@mouseelement = Watir::Element.new
#   $scenario = scenario
#
# end
#Before each scenario this block is run
Before do |scenario|
  print "Scenario starting - " + scenario.name.to_s +  ".\n"
  print  "************************************************************************************************************************************************************************************\n"
  WatirNg.patch!
  #@driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","chromedriver.exe")
  #Selenium::WebDriver::Chrome.driver_path = @driver_path
  #caps = Selenium::WebDriver::Remote::Capabilities.new
  #@browser = Watir::Browser.new :chrome
  ##@driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","geckodriver.exe")
  ##Selenium::WebDriver::Firefox.driver_path = @driver_path
  #profile = Selenium::WebDriver::Firefox::Profile.new
  #profile.assume_untrusted_certificate_issuer = false

  ##@browser = Watir::Browser.new :firefox , driver_path: @driver_path#,  profile => :profile
  #@browser = Watir::Browser.start
  #
  #caps = Selenium::WebDriver::Remote::Capabilities.firefox
  #caps['acceptInsecureCerts'] = true

  #*****************************SETTINGS for FireFox***********************************************************************************
  # @driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","geckodriver.exe")
  # Selenium::WebDriver::Firefox.driver_path = @driver_path
  # driver = Selenium::WebDriver.for(:firefox, driver_path: @driver_path)#, desired_capabilities: caps)
  # @browser = Watir::Browser.new(driver)
  # @browser.driver.execute_script("document.body.style.zoom='100%'")
  #*************************************************************************************************************************************
  #*****************************SETTINGS for IE*****************************************************************************************
   if scenario.name.to_s.include? "Verify that if I apply Teams filter and export the data in Excel, the data is exported based on the filter"
     @download_directory = File.join(File.absolute_path('../', File.dirname(__FILE__)),"GRSExcelExportFiles")
     # @download_directory.tr!('/', '\\') if Selenium::WebDriver::Platform.windows?
     #  @profile = Selenium::WebDriver::Firefox::Profile.new
     #  @profile['browser.download.folderList'] = 2 # custom location
     #  @profile['browser.download.dir'] = @download_directory
     #  @profile['browser.helperApps.neverAsk.saveToDisk'] = 'xls'
      #b = Watir::Browser.new :firefox, profile: profile
      # options = Selenium::WebDriver::Firefox::Options.new
      # options.add_argument("browser.download.folderList = 2")
      # options.add_argument("browser.download.dir = #{@download_directory}")
      # options.add_argument("browser.download.useDownloadDir = true")
      # options.add_argument("browser.helperApps.neverAsk.saveToDisk = 'xls'")
      #options.setPreference("pdfjs.disabled", true);  // disable the built-in PDF viewer
      #@options = Selenium::WebDriver::Firefox::Options.new
      # @options = Selenium::WebDriver::Firefox::Options.new
      # @options.add_preference("browser.download.folderList", 2)
      # @options.add_preference("browser.download.dir", "#{download_directory}")
      # @options.add_preference("browser.helperApps.neverAsk.saveToDisk","application/excel") #This has been commented for so and so reason - Ajay
      #@options.add_preference("browser.helperApps.alwaysAsk.force”, false)
     #@profile = Watir::B
     @profile = Selenium::WebDriver::Firefox::Profile.new
     @profile['browser.download.folderList'] = 2 # custom location
     @profile['browser.download.dir'] = @download_directory.to_s
     @profile['browser.helperApps.neverAsk.saveToDisk'] = "application/excel"



      @driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","geckodriver.exe")
      Selenium::WebDriver::Firefox.driver_path = @driver_path
      driver = Selenium::WebDriver.for(:firefox, driver_path: @driver_path)#, :profile => default)#options: @options)#,profile: @profile)#, desired_capabilities: caps)
      @browser = Watir::Browser.new(driver)
      #@browser.driver.execute_script("document.body.style.zoom='100%'")
   else
     @configInfo = Config.new()
     @browserName = @configInfo.getBrowserMode

     # puts ENV['BROWSER']
     case @browserName
       when "ie"
         caps = Selenium::WebDriver::Remote::Capabilities.ie
         #caps['IGNORE_ZOOM_SETTING'] = true
         #DesiredCapabilities caps = DesiredCapabilities.internetExplorer();
         caps['EnableNativeEvents'] = false
         caps['ignoreZoomSetting'] = true
         @driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","IEDriverServer.exe")
         Selenium::WebDriver::IE.driver_path = @driver_path
         driver = Selenium::WebDriver.for(:ie, driver_path: @driver_path, desired_capabilities: caps)
         @browser = Watir::Browser.new(driver)
       when "firefox"
         caps = Selenium::WebDriver::Remote::Capabilities.firefox
         #caps['IGNORE_ZOOM_SETTING'] = true
         #DesiredCapabilities caps = DesiredCapabilities.internetExplorer();
         caps['acceptInsecureCerts'] = true
         @driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","geckodriver.exe")
         Selenium::WebDriver::Firefox.driver_path = @driver_path
         driver = Selenium::WebDriver.for(:firefox, driver_path: @driver_path, desired_capabilities: caps)
         @browser = Watir::Browser.new(driver)
       #@browser.driver.execute_script("document.body.style.zoom='100%'")
       when "chrome"
         caps = Selenium::WebDriver::Remote::Capabilities.chrome
         caps['acceptInsecureCerts'] = false
         # options = new ChromeOptions();
         # options.AddArgument("--no-sandbox");
         # options = Selenium::WebDriver::Chrome::Options.new(args: ['start-maximized', 'user-data-dir=/tmp/temp_profile'])
         # driver = Selenium::WebDriver.for(:chrome, options: options)
         options = Selenium::WebDriver::Chrome::Options.new
         options.add_argument('start-maximized')
         options.add_argument('--no-sandbox')


         @driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","chromedriver.exe")
         Selenium::WebDriver::Chrome.driver_path = @driver_path
         driver = Selenium::WebDriver.for(:chrome, driver_path: @driver_path, options: options, desired_capabilities: caps)
         @browser = Watir::Browser.new(driver)
       else
         caps = Selenium::WebDriver::Remote::Capabilities.firefox
         #caps['IGNORE_ZOOM_SETTING'] = true
         #DesiredCapabilities caps = DesiredCapabilities.internetExplorer();
         caps['EnableNativeEvents'] = false
         caps['ignoreZoomSetting'] = true
         @driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","geckodriver.exe")
         Selenium::WebDriver::Firefox.driver_path = @driver_path
         driver = Selenium::WebDriver.for(:firefox, driver_path: @driver_path, desired_capabilities: caps)
         @browser = Watir::Browser.new(driver)
     end
   end





  #@browser.findElement(By.tagName("html")).sendKeys(Keys.chord(Keys.CONTROL, "0"));
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^{++}")
  #@browser.driver.execute_script("document.body.style.zoom='100%'")
  #*************************************************************************************************************************************
  #
  # caps = Selenium::WebDriver::Remote::Capabilities.chrome
  # caps['acceptInsecureCerts'] = true
  # @driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","chromedriver.exe")
  # Selenium::WebDriver::Chrome.driver_path = @driver_path
  # driver = Selenium::WebDriver.for(:chrome, desired_capabilities: caps, driver_path: @driver_path)
  # @browser = Watir::Browser.new(driver)
  #

  #chrome_extensions = []
  #chrome_extension_path = '\home\user\packed_chrome_extension.crx'
  #begin
  #  File.open(chrome_extension_path, "rb") do |file|
  #    chrome_extensions << Base64.encode64(file.read.chomp)
  #  end
  #rescue Exception => e
  #  raise "ERROR: Couldn't File.read or Base64.encode64 a Chrome extension: #{e.message}"
  #end

  # Append the extensions to your capabilities hash
  #my_capabilities.merge!({'chrome.extensions' => chrome_extensions})

  #desired_capabilities = Selenium::WebDriver::Remote::Capabilities.chrome(my_capabilities)

  #@browser = Watir::Browser.new(:remote, :desired_capabilities => desired_capabilities)


  #WatirAngular.

  #@driver_path = File.join(File.absolute_path('../', File.dirname(__FILE__)),"drivers","IEDriverServer.exe")
  #Selenium::WebDriver::IE.driver_path = @driver_path
  #caps = Selenium::WebDriver::Remote::Capabilities.new
  #@browser = Watir::Browser.new :ie
  #@browser = Watir::Browser.new :firefox , driver_path: @driver_path
  #@browser.driver.manage.timeouts.implicit_wait = 20
  @browser.driver.manage.window.maximize
  #@wait = Watir::Wait
  @bdriver = @browser
  $FeatureName=scenario.feature.name
  # get scenrion name based on run time
  $ScenarioName = scenario.name
  @tTime = Time.now
  @reporter = Watir::HtmlReporter.new(true)
  #@reporter.StartIndentation()
  #@reporter.ReportGeneral("Test Execution Started for #{@FeatureName}/#{@ScenarioName} at #{@tTime.strftime("%Y/%m/%d%H:%M:%S")}")
  #@reporter.EndIndentation()
  require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","grs_site_factory.rb")
  @grssitefactoryobj = GRSSiteFactory.new()
  @bcontainer = BrowserContainer.new(@browser,@reporter)
  @loginpg = @grssitefactoryobj.login_webpage(@browser,@reporter)
  @homepg = @grssitefactoryobj.home_webpage(@browser,@reporter)
  @grshomepg = @grssitefactoryobj.grs_home_page(@browser,@reporter)
  #@mouseelement = Watir::Element.new
  $scenario = scenario
  $putmessage = self
end
#Before do |scenario|

#end

After do
  @tTime = Time.now
  # closing the browser window that was open

  #@reporter.StartIndentation
  #@reporter.ReportGeneral("Test Execution Ended at #{@tTime.strftime("%Y/%m/%d%H:%M:%S")}")
  #@reporter.EndIndentation
  BrowserContainer.fetchenv
  #@TestResultsFile=$ResultDirPath.to_s+"\\GRS_TestResults-"+@tTime.strftime("%H%M%S").to_s+".html"
  #@reporter.Save(@TestResultsFile)
  #ldriver = $browser
  #@tdriver = ldriver

  #TestAutomationBase.teardown
  #if @configdoc.getBrowserMode != "Phantom"
    #@browser.close
    # @headless.destroy
  #end
  #@browser = @browser
  #if @TestResultsName == nil
  #  @TestResultsName = @ResultDirPath+"/"+@FeatureName #+"/Failure/"+"Failure"+"#{@tTime.strftime("%m%d%Y%M%S")}"+".html"
  #elsif @TestResultsName != nil and @@assertioncounter > 0
  #  @TestResultsName = @ResultDirPath+"/"+@FeatureName #+"/Error/"+"#{@TestResultsName}/"+@TestResultsName+"#{@tTime.strftime("%m%d%Y%M%S")}"+".html"
  #elsif @TestResultsName != nil and @@assertioncounter == 0
  #  @TestResultsName =  @ResultDirPath+"/"+@FeatureName #+"/Success/"+"#{@TestResultsName}/"+@TestResultsName+"#{@tTime.strftime("%m%d%Y%M%S")}"+".html"
  #end
  #if @TestResultsName != nil
  #  @reporter.Save("#{@TestResultsName}")
  #end

  @browser.close
  # if $scenario.failed?
  #   subject = "[GRS] #{$scenario.exception.message}"
  #   send_failure_email(subject)
  # end
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
  @TestResultsSummaryFile=".\\features\\GRS\\Detailed_Test_Results"+"\\GRS_UI_Test_Results_Summary-"+@tTime.strftime("%H%M%S").to_s
  ReportBuilder.configure do |config|
    config.json_path = @JSON_FILE
    config.report_path = @TestResultsSummaryFile#'./features/GRS_POC/Detailed_Test_Results/GRS_Test_Results_Summary'
    config.report_types = [:html]
    config.report_tabs = [:overview, :features, :scenarios, :errors]
    config.report_title = 'GRS UI Test Results'
    config.compress_images = false
    #config.additional_info = {Browser: 'Firefox', Environment: 'Dev'}
  end
  #BrowserContainer.get_run_stats_standard
  ReportBuilder.build_report
end

