#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","browser_container.rb")
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","login_page.rb")
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","home_page.rb")
#require 'cucumber'
#require 'watir'
#require 'selenium-webdriver'

Given(/^I open the mentioned link i can see the login page$/) do
  #BrowserContainer.setup
  #@loginpgsf=@grssitefactoryobj.login_webpage(@browser)
  @loginpg = Login_page.new(@browser, @reporter)
  @loginpg.loadwebpage
  @loginpg.LoginPage_VerifyLoginPage

end

Then(/^I login using the valid (.*) and (.*) I can see the GRS Home page$/) do |username, password|
  @loginpg.loginToApp(username,password)
  @loginpg.LoginPage_VerifySuccessfulLogin
end