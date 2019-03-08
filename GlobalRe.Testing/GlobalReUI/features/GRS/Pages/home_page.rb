require 'selenium-webdriver'
require 'watir'
require 'report_builder'
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","browser_container.rb")

class Home_page < BrowserContainer
  def initialize(browser, reporter)
    #puts "In Home Page"
    super(browser, reporter)

  end
  #@reportbuilderobject = ReportBuilder.new
  $Authorizebuttonlocator = {:id => "btn_toggle_Statuscount_80"}
  $DataEntrybuttonlocator = {:id => "btn_toggle_Statuscount_666"}
  $Outstandingbuttonlocator = {:id => "btn_toggle_Statuscount_2"}
  $ToBeDeclinedbuttonlocator = {:id => "btn_toggle_Statuscount_14"}
  $UnderReviewbuttonlocator = {:id => "btn_toggle_Statuscount_3"}
  @@AUlabelval = []
  @@DElabelval = []
  @@OSlabelval = []
  @@TBDlabelval = []
  @@URlabelval = []

  def fetchAuthorizebuttonelement()
    #BrowserContainer.wait_for {BrowserContainer.displayed?($Authorizebuttonlocator) }
    #sleep(10)
    #@Homepage_driver.wait_until {@Homepage_driver.element(:id => "btn_toggle_Statuscount_80").visible?}
    @@homepage_AuthorizeBTN = @browser.element($Authorizebuttonlocator)
    return @@homepage_AuthorizeBTN
  end
  def fetchDataEntrybuttonelement()
    @@homepage_DataEntryBTN = @browser.element($DataEntrybuttonlocator)
    return @@homepage_DataEntryBTN
  end
  def fetchOutstandingbuttonelement()
    @@homepage_OutstandingBTN = @browser.element($Outstandingbuttonlocator)
    return @@homepage_OutstandingBTN
  end
  def fetchToBeDeclinedbuttonelement()
    @@homepage_ToBeDeclinedBTN = @browser.element($ToBeDeclinedbuttonlocator)
    return @@homepage_ToBeDeclinedBTN
  end
  def fetchUnderReviewbuttonelement()
    @@homepage_UnderReviewBTN = @browser.element($UnderReviewbuttonlocator)
    return @@homepage_UnderReviewBTN
  end
  def checkAuthorizebuttonavailability()
    fetchAuthorizebuttonelement
    @@AUBTNAvailable = @@homepage_AuthorizeBTN.exists?
    return @@AUBTNAvailable
  end
  def checkDataEntrybuttonavailability()
    fetchDataEntrybuttonelement
    @@DEBTNAvailable = @@homepage_DataEntryBTN.exists?
    return @@DEBTNAvailable
  end
  def checkOutstandingbuttonavailability()
    fetchOutstandingbuttonelement
    @@OUBTNAvailable = @@homepage_OutstandingBTN.exists?
    return @@OUBTNAvailable
  end
  def checkToBeDeclinedbuttonavailability()
    fetchToBeDeclinedbuttonelement
    @@TBDBTNAvailable = @@homepage_ToBeDeclinedBTN.exists?
    return @@TBDBTNAvailable
  end
  def checkUnderReviewbuttonavailability()
    fetchUnderReviewbuttonelement
    @@URBTNAvailable = @@homepage_UnderReviewBTN.exists?
    return @@URBTNAvailable
  end
  def checkAuthorizebuttonenabled()
    #fetchAuthorizebuttonelement
    @@AUBTNenabled = @browser.element(:id => 'btn_toggle_Statuscount_80-input').attribute_value('disabled')
    return @@AUBTNenabled
  end
  def checkDataEntrybuttonenabled()
    #fetchDataEntrybuttonelement
    @@DEBTNenabled = @browser.element(:id => 'btn_toggle_Statuscount_666-input').attribute_value('disabled')
    #print "==========="
    #print @@DEBTNenabled
    #print "==========="
    #print @@DEBTNenabled
    return @@DEBTNenabled
  end
  def checkOutstandingbuttonenabled()
    #fetchOutstandingbuttonelement
    @@OUBTNenabled = @browser.element(:id => 'btn_toggle_Statuscount_2-input').attribute_value('disabled')
    return @@OUBTNenabled
  end
  def checkToBeDeclinedbuttonenabled()
    #fetchToBeDeclinedbuttonelement
    @@TBDBTNenabled = @browser.element(:id => 'btn_toggle_Statuscount_14-input').attribute_value('disabled')
    return @@TBDBTNenabled
  end
  def checkUnderReviewbuttonenabled()
    #fetchUnderReviewbuttonelement
    @@URBTNenabled = @browser.element(:id => 'btn_toggle_Statuscount_3-input').attribute_value('disabled')
    return @@URBTNenabled
  end
  def getAUBTNText()
    fetchAuthorizebuttonelement
    @@AuthorizeTextValue=@@homepage_AuthorizeBTN.text.to_s
    #puts @@AuthorizeTextValue
    @@AUlabelval = @@AuthorizeTextValue.split("\n")
    #print "========"
    #print @@AUlabelval[0]
    #print "========"
    #print @@AUlabelval[1]
    return @@AUlabelval
  end
  def getDEBTNText()
    fetchDataEntrybuttonelement
    @@DETextValue = @@homepage_DataEntryBTN.text.to_s
    @@DElabelval= @@DETextValue.split("\n")
    return @@DElabelval

  end
  def getOSBTNText()
    fetchOutstandingbuttonelement
    @@OSTextValue = @@homepage_OutstandingBTN.text.to_s
    @@OSlabelval = @@OSTextValue.split("\n")
    return @@OSlabelval
  end
  def getTBDBTNText()
    fetchToBeDeclinedbuttonelement
    @@TBDTextValue = @@homepage_ToBeDeclinedBTN.text.to_s
    @@TBDlabelval = @@TBDTextValue.split("\n")
    return @@TBDlabelval
  end
  def getURBTNText()
    fetchUnderReviewbuttonelement
    @@URTextValue = @@homepage_UnderReviewBTN.text.to_s
    @@URlabelval = @@URTextValue.split("\n")
    return @@URlabelval
  end
  def HomePage_VerifySuccessfulLogin()
    checkAuthorizebuttonavailability
    checkDataEntrybuttonavailability
    checkOutstandingbuttonavailability
    checkToBeDeclinedbuttonavailability
    checkUnderReviewbuttonavailability

    if ( @@AUBTNAvailable==true && @@DEBTNAvailable==true && @@OUBTNAvailable==true && @@TBDBTNAvailable==true && @@URBTNAvailable==true)
      print("PASSED - GRS Home Page is displayed.\n")
      @reporter.ReportAction("PASSED - GRS Home Page is displayed.\n")
      #@reportbuilderobject.add("PASSED - GRS Home Page is displayed.\n")
    else
      print("FAILED - GRS Home Page is not displayed.\n")
      fail("FAILED - GRS Home Page is not displayed.\n")
      @reporter.ReportAction("FAILED - GRS Home Page is not displayed.\n")
      #@reportbuilderobject.add("FAILED - GRS Home Page is not displayed.\n")
    end
  end
  def HomePage_VerifyStatusButtonAvailability()
    checkAuthorizebuttonavailability
    checkDataEntrybuttonavailability
    checkOutstandingbuttonavailability
    checkToBeDeclinedbuttonavailability
    checkUnderReviewbuttonavailability
    #hpadriver = $browser
    #@loginpageassert_driver = hpadriver
    if ( @@AUBTNAvailable==true && @@DEBTNAvailable==true && @@OUBTNAvailable==true && @@TBDBTNAvailable==true && @@URBTNAvailable==true)
      print("PASSED - GRS Home Page has the required 5 Status panels.\n")
      @reporter.ReportAction("PASSED - GRS Home Page has the required 5 Status panels.\n")
      #@reportbuilderobject.add("PASSED - GRS Home Page has the required 5 Status panels.\n")
    else
      print("FAILED - GRS Home Page does not have the required 5 Status panels..\n")
      fail("FAILED - GRS Home Page does not have the required 5 Status panels..\n")
      @reporter.ReportAction("FAILED - GRS Home Page does not have the required 5 Status panels..\n")
      #@reportbuilderobject.add("FAILED - GRS Home Page does not have the required 5 Status panels..\n")
    end
  end

  def HomePage_VerifyAuthorizeLabelandCountAvailability()
    getAUBTNText
    if(@@AUlabelval.length!=2)
      print("FAILED - STATUS and COUNT are not displayed in AUTHORIZE status button.\n")
      fail("FAILED - STATUS and COUNT are not displayed in AUTHORIZE status button.\n")
      @reporter.ReportAction("FAILED - STATUS and COUNT are not displayed in AUTHORIZE status button.\n")
      #@reportbuilderobject.add("FAILED - STATUS and COUNT are not displayed in AUTHORIZE status button.\n")
    else
      print("PASSED - STATUS and COUNT are displayed in AUTHORIZE status button.\n")
      @reporter.ReportAction("PASSED - STATUS and COUNT are displayed in AUTHORIZE status button.\n")
      #@reportbuilderobject.add("PASSED - STATUS and COUNT are displayed in AUTHORIZE status button.\n")
    end

  end
  def HomePage_VerifyDealEntryLabelandCountAvailability()
    getDEBTNText
    if(@@DElabelval.length!=2)
      print("FAILED - STATUS and COUNT are not displayed in DEAL ENTRY status button.\n")
      fail("FAILED - STATUS and COUNT are not displayed in DEAL ENTRY status button.\n")
      @reporter.ReportAction("FAILED - STATUS and COUNT are not displayed in DEAL ENTRY status button.\n")
      #@reportbuilderobject.add("FAILED - STATUS and COUNT are not displayed in DEAL ENTRY status button.\n")
    else
      print("PASSED - STATUS and COUNT are displayed in DEAL ENTRY status button.\n")
      @reporter.ReportAction("PASSED - STATUS and COUNT are displayed in DEAL ENTRY status button.\n")
      #@reportbuilderobject.add("PASSED - STATUS and COUNT are displayed in DEAL ENTRY status button.\n")
    end

  end
  def HomePage_VerifyOutstandingLabelandCountAvailability()
    getOSBTNText
    if(@@OSlabelval.length!=2)
      print("FAILED - STATUS and COUNT are not displayed in OUTSTANDING status button.\n")
      fail("FAILED - STATUS and COUNT are not displayed in OUTSTANDING status button.\n")
      @reporter.ReportAction("FAILED - STATUS and COUNT are not displayed in OUTSTANDING status button.\n")
      #@reportbuilderobject.add("FAILED - STATUS and COUNT are not displayed in OUTSTANDING status button.\n")
    else
      print("PASSED - STATUS and COUNT are displayed in OUTSTANDING status button.\n")
      @reporter.ReportAction("PASSED - STATUS and COUNT are displayed in OUTSTANDING status button.\n")
      #@reportbuilderobject.add("PASSED - STATUS and COUNT are displayed in OUTSTANDING status button.\n")
    end

  end
  def HomePage_VerifyToBeDeclinedLabelandCountAvailability()
    getTBDBTNText
    if(@@TBDlabelval.length!=2)
      print("FAILED - STATUS and COUNT are not displayed in TO BE DECLINED status button.\n")
      fail("FAILED - STATUS and COUNT are not displayed in TO BE DECLINED status button.\n")
      @reporter.ReportAction("FAILED - STATUS and COUNT are not displayed in TO BE DECLINED status button.\n")
      #@reportbuilderobject.add("FAILED - STATUS and COUNT are not displayed in TO BE DECLINED status button.\n")
    else
      print("PASSED - STATUS and COUNT are displayed in TO BE DECLINED status button.\n")
      @reporter.ReportAction("PASSED - STATUS and COUNT are displayed in TO BE DECLINED status button.\n")
      #@reportbuilderobject.add("PASSED - STATUS and COUNT are displayed in TO BE DECLINED status button.\n")
    end

  end
  def HomePage_VerifyUnderReviewLabelandCountAvailability()
    getURBTNText
    if(@@URlabelval.length!=2)
      print("FAILED - STATUS and COUNT are not displayed in UNDER REVIEW status button.\n")
      fail("FAILED - STATUS and COUNT are not displayed in UNDER REVIEW status button.\n")
      @reporter.ReportAction("FAILED - STATUS and COUNT are not displayed in UNDER REVIEW status button.\n")
      #@reportbuilderobject.add("FAILED - STATUS and COUNT are not displayed in UNDER REVIEW status button.\n")
    else
      print("PASSED - STATUS and COUNT are displayed in UNDER REVIEW status button.\n")
      @reporter.ReportAction("PASSED - STATUS and COUNT are displayed in UNDER REVIEW status button.\n")
      #@reportbuilderobject.add("PASSED - STATUS and COUNT are displayed in UNDER REVIEW status button.\n")
    end

  end
  def HomePage_VerifyZeroCountButtonIsDisabled
    checkAuthorizebuttonenabled
    checkDataEntrybuttonenabled
    checkOutstandingbuttonenabled
    checkToBeDeclinedbuttonenabled
    checkUnderReviewbuttonenabled
    getAUBTNText
    getDEBTNText
    getOSBTNText
    getTBDBTNText
    getURBTNText
    if @@AUlabelval[0].to_s == "0"
      if @@AUBTNenabled.to_s == "true"
        print("PASSED - The Button " + @@AUlabelval[1] +" is having value " + @@AUlabelval[0] + " and is disabled.")
        @reporter.ReportAction("PASSED - The Button " + @@AUlabelval[1] +" is having value " + @@AUlabelval[0] + " and is disabled.")
        #@reportbuilderobject.add("PASSED - The Button " + @@AUlabelval[1] +" is having value " + @@AUlabelval[0] + " and is disabled.")
      else
        print ("FAILED - The Button " + @@AUlabelval[1] +" is having value " + @@AUlabelval[0] + " and is not disabled.")
        fail ("FAILED - The Button " + @@AUlabelval[1] +" is having value " + @@AUlabelval[0] + " and is not disabled.")
        @reporter.ReportAction ("FAILED - The Button " + @@AUlabelval[1] +" is having value " + @@AUlabelval[0] + " and is not disabled.")
        #@reportbuilderobject.add ("FAILED - The Button " + @@AUlabelval[1] +" is having value " + @@AUlabelval[0] + " and is not disabled.")
      end
    end
    if @@DElabelval[0].to_s =="0"
      if @@DEBTNenabled.to_s == "true"
        print("PASSED - The Button " + @@DElabelval[1] +" is having value " + @@DElabelval[0] + " and is disabled.")
        @reporter.ReportAction("PASSED - The Button " + @@DElabelval[1] +" is having value " + @@DElabelval[0] + " and is disabled.")
        #@reportbuilderobject.add("PASSED - The Button " + @@DElabelval[1] +" is having value " + @@DElabelval[0] + " and is disabled.")
      else
        print ("FAILED - The Button " + @@DElabelval[1] +" is having value " + @@DElabelval[0] + " and is not disabled.")
        fail ("FAILED - The Button " + @@DElabelval[1] +" is having value " + @@DElabelval[0] + " and is not disabled.")
        @reporter.ReportAction ("FAILED - The Button " + @@DElabelval[1] +" is having value " + @@DElabelval[0] + " and is not disabled.")
        #@reportbuilderobject.add ("FAILED - The Button " + @@DElabelval[1] +" is having value " + @@DElabelval[0] + " and is not disabled.")
      end
    end
    if @@OSlabelval[0].to_s=="0"
      if @@OUBTNenabled.to_s == "true"
        print("PASSED - The Button " + @@OSlabelval[1] +" is having value " + @@OSlabelval[0] + " and is disabled.")
        @reporter.ReportAction("PASSED - The Button " + @@OSlabelval[1] +" is having value " + @@OSlabelval[0] + " and is disabled.")
        #@reportbuilderobject.add("PASSED - The Button " + @@OSlabelval[1] +" is having value " + @@OSlabelval[0] + " and is disabled.")
      else
        print ("FAILED - The Button " + @@OSlabelval[1] +" is having value " + @@OSlabelval[0] + " and is not disabled.")
        fail ("FAILED - The Button " + @@OSlabelval[1] +" is having value " + @@OSlabelval[0] + " and is not disabled.")
        @reporter.ReportAction ("FAILED - The Button " + @@OSlabelval[1] +" is having value " + @@OSlabelval[0] + " and is not disabled.")
        #@reportbuilderobject.add ("FAILED - The Button " + @@OSlabelval[1] +" is having value " + @@OSlabelval[0] + " and is not disabled.")
      end
    end
    if @@TBDlabelval[0].to_s == "0"
      if @@TBDBTNenabled.to_s == "true"
        print("PASSED - The Button " + @@TBDlabelval[1] +" is having value " + @@TBDlabelval[0] + " and is disabled.")
        @reporter.ReportAction("PASSED - The Button " + @@TBDlabelval[1] +" is having value " + @@TBDlabelval[0] + " and is disabled.")
        #@reportbuilderobject.add("PASSED - The Button " + @@TBDlabelval[1] +" is having value " + @@TBDlabelval[0] + " and is disabled.")
      else
        print ("FAILED - The Button " + @@TBDlabelval[1] +" is having value " + @@TBDlabelval[0] + " and is not disabled.")
        fail ("FAILED - The Button " + @@TBDlabelval[1] +" is having value " + @@TBDlabelval[0] + " and is not disabled.")
        @reporter.ReportAction ("FAILED - The Button " + @@TBDlabelval[1] +" is having value " + @@TBDlabelval[0] + " and is not disabled.")
        #@reportbuilderobject.add("FAILED - The Button " + @@TBDlabelval[1] +" is having value " + @@TBDlabelval[0] + " and is not disabled.")
      end
    end
    if @@URlabelval[0].to_s == "0"
      if @@URBTNenabled.to_s == "true"
        print("PASSED - The Button " + @@URlabelval[1] +" is having value " + @@URlabelval[0] + " and is disabled.")
        @reporter.ReportAction("PASSED - The Button " + @@URlabelval[1] +" is having value " + @@URlabelval[0] + " and is disabled.")
        #@reportbuilderobject.add("PASSED - The Button " + @@URlabelval[1] +" is having value " + @@URlabelval[0] + " and is disabled.")
      else
        print ("FAILED - The Button " + @@URlabelval[1] +" is having value " + @@URlabelval[0] + " and is not disabled.")
        fail ("FAILED - The Button " + @@URlabelval[1] +" is having value " + @@URlabelval[0] + " and is not disabled.")
        @reporter.ReportAction("FAILED - The Button " + @@URlabelval[1] +" is having value " + @@URlabelval[0] + " and is not disabled.")
        #@reportbuilderobject.add("FAILED - The Button " + @@URlabelval[1] +" is having value " + @@URlabelval[0] + " and is not disabled.")
      end
    end
  end
  def self.teardown()
    @browser.quit
  end
end