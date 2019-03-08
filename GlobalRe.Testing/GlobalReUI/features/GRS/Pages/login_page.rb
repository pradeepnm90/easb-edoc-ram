require 'selenium-webdriver'
require 'watir'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","browser_container.rb")
require 'report_builder'
class Login_page < BrowserContainer

  def initialize(browser,reporter)
    #puts "In Login Page"
    #BrowserContainer.new()
    super(browser,reporter)
  end

  #@reportbuilderobject = ReportBuilder.
  $usernamelocator = {:id =>"tb_Username"}
  $passwordlocator = {:id => "tb_Password"}
  $loginbuttonlocator = {:id => "btn_Submit"}

  #ldriver = $browser
  #@loginpage_driver = ldriver
  def usernameelement()
   # ldriver = $browser
    #@loginpage_driver = ldriver
    $loginpage_usernameTB = @browser.text_field($usernamelocator)
    return $loginpage_usernameTB
  end
  def passwordelement()
    #ldriver = $browser
    #@loginpage_driver = ldriver
    $loginpage_passwordTB = @browser.text_field($passwordlocator)
    return $loginpage_passwordTB
  end
  def loginbuttonelement()
    #ldriver = $browser
    #@loginpage_driver = ldriver
    $loginpage_loginBTN = @browser.element($loginbuttonlocator)
    return $loginpage_loginBTN
  end
  def enter_username(username)
    #ldriver = $browser
    #@loginpage_driver = ldriver
    $loginpage_usernameTB = @browser.text_field($usernamelocator)
    $loginpage_usernameTB.send_keys(username)
  end
  def enter_password(password)
    $loginpage_passwordTB = @browser.text_field($passwordlocator)
    $loginpage_passwordTB.send_keys(password)
  end
  def selectloginbutton
    #ldriver = $browser
    #@loginpage_driver = ldriver
    $loginpage_loginBTN = @browser.element($loginbuttonlocator)
    $loginpage_loginBTN.click!
  end

  def loginToApp(usernamevalue,passwordvalue)
    enter_username(usernamevalue)
    enter_password(passwordvalue)
    selectloginbutton()
  end
  def loadwebpage()
    #ldriver = $browser
    #@loginpage_driver = ldriver

    BrowserContainer.fetchenv
    @browser.goto($ApplicationURL)

  end
  def LoginPage_VerifyLoginPage()#(scenarioname,browsertype)
    #ldriver = $browser
    #@loginpageassert_driver = ldriver
    #@@loginpage_usernameTB=Login_page.usernameelement()#loginpageassert_driver.text_field(:id =>"tb_Username")#Login_page.usernameelement(@@loginpageassert_driver)
    #@@loginpage_passwordTB=Login_page.passwordelement()#loginpageassert_driver.text_field(:id => "tb_Password")#Login_page.passwordelement(@@loginpageassert_driver)
    #@@loginpage_loginBTN=Login_page.loginbuttonelement()#loginpageassert_driver.element(:id => "btn_Submit")#Login_page.loginbuttonelement(@@loginpageassert_driver)
    usernameelement
    passwordelement
    loginbuttonelement
    @@usernameavailable = $loginpage_usernameTB.exists?
    @@passwordavailable = $loginpage_passwordTB.exists?
    @@loginbuttonavailable = $loginpage_loginBTN.exists?
    if (@@usernameavailable==true && @@passwordavailable==true && @@loginbuttonavailable==true)
      print("PASSED - Successfully loaded the GRS Login Page.\n")
      @reporter.ReportAction("PASSED - Successfully loaded the GRS Login Page.\n")
      #@reportbuilderobject = ReportBuilder.new
      #@reportbuilderobject.add("PASSED - Successfully loaded the GRS Login Page.\n")

    else
      print("FAILED - Failed to load the GRS Login Page.\n")
      fail("FAILED - Failed to load the GRS Login Page.\n")
      @reporter.ReportAction("FAILED - Failed to load the GRS Login Page.\n")
      #@reportbuilderobject.add("FAILED - Failed to load the GRS Login Page.\n")
    end
  end

  def LoginPage_VerifySuccessfulLogin()
    #ldriver = $browser
    #@loginpageassert_driver = ldriver
    #hpcon = Home_page.new()
    $HP_Authorizebuttonlocator = {:id => "btn_toggle_Statuscount_80"}
    $HP_DataEntrybuttonlocator = {:id => "btn_toggle_Statuscount_666"}
    $HP_Outstandingbuttonlocator = {:id => "btn_toggle_Statuscount_2"}
    $HP_ToBeDeclinedbuttonlocator = {:id => "btn_toggle_Statuscount_14"}
    $HP_UnderReviewbuttonlocator = {:id => "btn_toggle_Statuscount_3"}
    #@lpa_homepage_AuthorizeBTN=Home_page.Authorizebuttonelement()
    #@lpa_homepage_DataEntryBTN=Home_page.DataEntrybuttonelement()
    #@lpa_homepage_OutstandingBTN=Home_page.Outstandingbuttonelement()
    #@lpa_homepage_ToBeDeclinedBTN=Home_page.ToBeDeclinedbuttonelement()
    #@lpa_homepage_UnderReviewBTN=Home_page.UnderReviewbuttonelement()
    @lpa_homepage_AuthorizeBTN=@browser.element($HP_Authorizebuttonlocator)
    @lpa_homepage_DataEntryBTN=@browser.element($HP_DataEntrybuttonlocator)
    @lpa_homepage_OutstandingBTN=@browser.element($HP_Outstandingbuttonlocator)
    @lpa_homepage_ToBeDeclinedBTN=@browser.element($HP_ToBeDeclinedbuttonlocator)
    @lpa_homepage_UnderReviewBTN=@browser.element($HP_UnderReviewbuttonlocator)
    @@AUBTNAvailable = @lpa_homepage_AuthorizeBTN.exists?
    @@DEBTNAvailable = @lpa_homepage_DataEntryBTN.exists?
    @@OUBTNAvailable = @lpa_homepage_OutstandingBTN.exists?
    @@TBDBTNAvailable = @lpa_homepage_ToBeDeclinedBTN.exists?
    @@URBTNAvailable = @lpa_homepage_UnderReviewBTN.exists?
    if ( @@AUBTNAvailable==true && @@DEBTNAvailable==true && @@OUBTNAvailable==true && @@TBDBTNAvailable==true && @@URBTNAvailable==true)
      print("PASSED - Successfully logged in to GRS Home Page.\n")
      @reporter.ReportAction("PASSED - Successfully logged in to GRS Home Page.\n")
      #@reportbuilderobject.add("PASSED - Successfully logged in to GRS Home Page.\n")
    else
      print("FAILED - Failed to log in to the GRS Home Page.\n")
      fail("FAILED - Failed to log in to the GRS Home Page.\n")
      @reporter.ReportAction("FAILED - Failed to log in to the GRS Home Page.\n")
      #@reportbuilderobject.add("FAILED - Failed to log in to the GRS Home Page.\n")
    end
  end

end