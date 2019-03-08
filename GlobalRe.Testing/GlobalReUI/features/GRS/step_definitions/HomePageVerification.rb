#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","browser_container.rb")
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","home_page.rb")
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","login_page.rb")


require 'cucumber'
require 'watir'
require 'selenium-webdriver'


Given(/^I logged in to GRS I can see the GRS Home page$/) do
  #@bcontainer = BrowserContainer.new(@browser)
  @loginpg = Login_page.new(@browser,@reporter)
  @loginpg.loadwebpage
  @loginpg.loginToApp("test","test")
  @homepg = Home_page.new(@browser,@reporter)
  @homepg.HomePage_VerifySuccessfulLogin
end

And(/^I see five panels with various status$/) do
  #@homepg = Home_page.new(@browser)
  @homepg.HomePage_VerifyStatusButtonAvailability
end

And(/^I see each of the panels have the label and count of deals having the mentioned status$/) do
  #@homepg = Home_page.new(@browser)
  @homepg.HomePage_VerifyAuthorizeLabelandCountAvailability
  @homepg.HomePage_VerifyDealEntryLabelandCountAvailability
  @homepg.HomePage_VerifyOutstandingLabelandCountAvailability
  @homepg.HomePage_VerifyToBeDeclinedLabelandCountAvailability
  @homepg.HomePage_VerifyUnderReviewLabelandCountAvailability
end

And(/^I see a panel as disabled if the count is zero$/) do
  #@homepg = Home_page.new(@browser)
  @homepg.HomePage_VerifyZeroCountButtonIsDisabled
end

Then(/^if I click any of the enabled panels having a count more than zero, i see deals having that status is displayed below$/) do
  #@homepg = Home_page.new(@browser)
  #@homepg.HomePage_VerifyZeroCountButtonIsDisabled
  #@bcontainer = BrowserContainer.new
  #@bcontainer.teardown(@browser)
end


And(/^if I click any of the enabled panels having a count more than zero, i see deals having that status is displayed below in ascending order$/) do

end

And(/^I click on the grid column the deals are sorted in descending order$/) do

end

Then(/^I click on the grid column the deals are sorted in ascending order$/) do

end