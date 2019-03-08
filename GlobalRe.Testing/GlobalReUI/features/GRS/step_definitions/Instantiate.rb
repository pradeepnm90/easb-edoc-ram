require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","browser_container.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","login_page.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","home_page.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","grs_site_factory.rb")

Given(/^I Instantiate all the needed classes$/) do
  @grssitefactoryobj = GRSSiteFactory.new()
  @loginpg = @grssitefactoryobj.login_webpage(@browser,@reporter)
  @homepg = @grssitefactoryobj.home_webpage(@browser,@reporter)
end

