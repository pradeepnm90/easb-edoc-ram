require File.join(File.absolute_path('../', File.dirname(__FILE__)),"ActionModules","deal_count_by_status_actions.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Assertions","login_page_assert.rb")
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Assertions","home_page_assert.rb")
class GRSapisitefactory
  def initialize()
    #puts "GRSSiteFactory Initialised"
    #super(reporter)
  end
  def deal_count_by_status_actions(reporter)
    @dealcountbystatusactions = DealCountByStatus_actions.new(reporter)
  end
  def Base_Container(reporter)
    @basec = BaseContainer.new(reporter)
  end
end