require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","home_page.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","login_page.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","grs_home_page.rb")
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Assertions","login_page_assert.rb")
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Assertions","home_page_assert.rb")
class GRSSiteFactory
  def initialize()
    #puts "GRSSiteFactory Initialised"
  end
  def login_webpage(browser,reporter)
    @loginpg = Login_page.new(browser,reporter)
  end
  def home_webpage(browser,reporter)
    @homepg = Home_page.new(browser,reporter)
  end
  def grs_home_page(browser,reporter)
    @grshomepg = GRS_Home_Page.new(browser,reporter)
  end
end