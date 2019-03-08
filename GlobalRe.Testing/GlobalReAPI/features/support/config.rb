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
  def getDEVUrl
    #@Environment=getReleaseNumber()
    return @@pDoc.search("DevUrl").text
  end
  def getQAUrl
    #@Environment=getReleaseNumber()
    return @@pDoc.search("QAUrl").text
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
  def getActionModuleDir
    return @@pDoc.search("ActionModuleDirectory").text
  end
  def getRunEnvironment
    return @@pDoc.search("RunEnvironment").text
  end
  def getReleaseNumber
	   # get the release number specified in config file
    return @@pDoc.search("ReleaseNumber").text
  end
  def getBrowserMode
	   # get the browser mode specified in config file
    return @@pDoc.search("BrowserMode").text
  end
 end
      
