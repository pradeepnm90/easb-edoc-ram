require 'nokogiri'
class Config
	def initialize
		xmlFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.xml")
    @@pDoc = Nokogiri::XML(File.open(xmlFile))
  end
  def getPassword
    return @@pDoc.search("password").text
  end
  def getUsername
    return @@pDoc.search("username").text
  end
  def getUrl
    return @@pDoc.search("Url").text
  end
  def getDEVUrl
    return @@pDoc.search("DEVUrl").text
  end
  def getQAUrl
    return @@pDoc.search("QAUrl").text
  end
  def getUWDEVUrl
    return @@pDoc.search("UWDEVUrl").text
  end
  def getUWQAUrl
    return @@pDoc.search("UWQAUrl").text
  end
  def getNPTADEVUrl
    return @@pDoc.search("NPTADEVUrl").text
  end
  def getNPTAQAUrl
    return @@pDoc.search("NPTAQAUrl").text
  end
  def getPTADEVUrl
    return @@pDoc.search("PTADEVUrl").text
  end
  def getPTAQAUrl
    return @@pDoc.search("PTAQAUrl").text
  end
  def getActuaryDEVUrl
    return @@pDoc.search("ActuaryDEVUrl").text
  end
  def getActuaryQAUrl
    return @@pDoc.search("ActuaryQAUrl").text
  end
  def getActuaryManagerDEVUrl
    return @@pDoc.search("ActuaryManagerDEVUrl").text
  end
  def getActuaryManagerQAUrl
    return @@pDoc.search("ActuaryManagerQAUrl").text
  end
  def getUnderwriterManagerDEVUrl
    return @@pDoc.search("UnderwriterManagerDEVUrl").text
  end
  def getReadonlyAccessUserDevUrl
    return @@pDoc.search("ReadonlyAccessUserDevUrl").text
  end
  def getReadonlyAccessUserQAUrl
    return @@pDoc.search("ReadonlyAccessUserQAUrl").text
  end
  def getUnderwriterManagerQAUrl
    return @@pDoc.search("UnderwriterManagerQAUrl").text
  end

  def getCommonDir
    return @@pDoc.search("CommonDirectory").text
  end  
  def getUtilityDir
    return @@pDoc.search("UtilityDirectory").text
  end  
  def getPageClassesDir
    return @@pDoc.search("PageClassesDirectory").text
  end
  def getResultsDir
    return @@pDoc.search("ResultsDirectory").text
  end  
  def getBaseClassesDir
    return @@pDoc.search("BaseClassesDirectory").text
  end
  def getRunEnvironment
    return @@pDoc.search("RunEnvironment").text
  end
  def getTestDataDir
    return @@pDoc.search("TestDataDirectory").text
  end
  def getConfigDir
    return @@pDoc.search("ConfigDirectory").text
  end
  def getDriversDir
    return @@pDoc.search("DriversDirectory").text
  end
  def getAssertionsDir
    return @@pDoc.search("AssertionsDirectory").text
  end
  def getStepDefinitionsDir
    return @@pDoc.search("StepDefinitionDirectory").text
  end
  def getReleaseNumber
	  # get the release number specified in config file
    return @@pDoc.search("StepDefinitionDirectory").text
  end
  def getBrowserMode
	  # get the browser mode specified in config file
    return @@pDoc.search("BrowserMode").text
	end
 end
      
